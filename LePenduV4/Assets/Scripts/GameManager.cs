using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Liste des mots")]
    public List<string> wordList = new List<string>();

    [Header("Param�tres de la partie")]
    public int playerStartLife = 7;

    [Header("R�f�rences")]
    public IHM ihm;
    public HangmanUIController hangmanUIController;
    public SoundManager soundManager;

    public Game currentGame;
    private bool isGameFinished = false;

    void Start()
    {
        StartNewGame();
    }

    /// <summary>
    /// Pr�pare et lance une nouvelle partie.
    /// </summary>
    public void StartNewGame()
    {
        isGameFinished = false;

        if (wordList.Count == 0)
        {
            Debug.LogError("La liste de mots est vide ! Impossible de commencer.");
            return;
        }

        currentGame = new Game(GetRandomWord(), playerStartLife);

        Debug.Log("Le mot � deviner est : " + currentGame.word);

        ihm.StartNewGameUI(); // Appelle une fonction pour r�initialiser l'IHM
        hangmanUIController.DisplayParts(currentGame.remainingLife);
    }

    /// <summary>
    /// Fonction appel�e lorsqu'une lettre est jou�e.
    /// </summary>
    public void OnLetterPlayed(string letter)
    {
        if (isGameFinished) return;
        
        if (string.IsNullOrEmpty(letter)) return;

        letter = letter.ToUpper();
        if (currentGame.HasLetterPlayed(letter)) return;

        bool isAGoodMove = IsAGoodMove(letter);
        currentGame.usedLetters.Add(letter);

        if (isAGoodMove)
        {
            soundManager.JouerSonBonneLettre();
            Debug.Log("Bravo");
            if (currentGame.IsWordGuess())
            {
                EndGame(true); // Termine la partie en mode victoire
            }
        }
        else
        {
            soundManager.JouerSonMauvaiseLettre();
            currentGame.RemoveLife();
            hangmanUIController.DisplayParts(currentGame.remainingLife);
            if (playerStartLife <= 0)
            {
                EndGame(false); // Termine la partie en mode d�faite
            }
        }

        ihm.UpdateWordToGuess();
        ihm.UpdateWrongLetters();
    }
    
    /// <summary>
    /// G�re la fin de la partie.
    /// </summary>
    private void EndGame(bool hasWon)
    {
        isGameFinished = true;
        
        if (hasWon)
        {
            Debug.Log("Bravo vous avez gagn� la partie !");
            soundManager.JouerSonVictoire();
            ihm.AfficherImageVictoire();
        }
        else
        {
            Debug.Log("Vous avez perdu. Le mot �tait : " + currentGame.word);
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