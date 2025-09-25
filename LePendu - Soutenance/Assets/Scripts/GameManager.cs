using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Liste des mots de secours")]
    public List<string> wordList = new List<string>();

    [Header("Paramètres de la partie")]
    public int playerStartLife = 7;

    [Header("Références")]
    public IHM ihm;
    public HangmanUIController hangmanUIController;
    public SoundManager soundManager;
    public WebRequest webRequest;

    public Game currentGame;
    private bool isGameFinished = false;

    private void Awake()
    {
        // Vérif pour éviter les erreurs NullReferenceException
        if (webRequest == null)
        {

        }
    }

    private void OnEnable()
    {
        WebRequest.OnWordReady += InitializeNewGame;
    }

    private void OnDisable()
    {
        WebRequest.OnWordReady -= InitializeNewGame;
    }

    void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        isGameFinished = false;
        ihm.StartNewGameUI();

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Source du mot : Liste locale");
            string localWord = GetRandomWord();
            InitializeNewGame(localWord);
        }
        else
        {
            webRequest.GetNewWord();
        }
    }

    private void InitializeNewGame(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            if (wordList.Count > 0)
            {
                word = GetRandomWord();
            }
            else
            {
                return;
            }
        }

        currentGame = new Game(word, playerStartLife);

        Debug.Log("Le mot à deviner est : " + currentGame.word);

        hangmanUIController.DisplayParts(currentGame.remainingLife);
        ihm.UpdateWordToGuess();
    }

    public void OnLetterPlayed(string letter)
    {
        if (isGameFinished || currentGame == null) return;
        if (string.IsNullOrEmpty(letter)) return;

        letter = letter.ToUpper();
        if (currentGame.HasLetterPlayed(letter)) return;

        bool isAGoodMove = IsAGoodMove(letter);
        currentGame.usedLetters.Add(letter);

        if (isAGoodMove)
        {
            soundManager.JouerSonBonneLettre();
            if (currentGame.IsWordGuess())
            {
                EndGame(true);
            }
        }
        else
        {
            soundManager.JouerSonMauvaiseLettre();
            currentGame.RemoveLife();
            hangmanUIController.DisplayParts(currentGame.remainingLife);
            if (currentGame.remainingLife <= 0)
            {
                EndGame(false);
            }
        }

        ihm.UpdateWordToGuess();
        ihm.UpdateWrongLetters();
    }

    private void EndGame(bool hasWon)
    {
        isGameFinished = true;
        if (hasWon)
        {
            soundManager.JouerSonVictoire();
            ihm.AfficherImageVictoire();
        }
        else
        {
            soundManager.JouerSonDefaite();
            ihm.AfficherImageDefaite();
        }
    }

    private string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Count);
        return wordList[randomIndex];
    }

    private bool IsAGoodMove(string letter)
    {
        if (currentGame.HasLetterPlayed(letter)) return false;
        return currentGame.WordContainsLetter(letter);
    }
}