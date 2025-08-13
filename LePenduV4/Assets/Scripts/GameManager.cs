using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Liste des mots")]
    public List<string> wordList = new List<string>();

    [Header("Paramètres de la partie")]
    public int playerStartLife = 7;

    [Header("Références")]
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
    /// Prépare et lance une nouvelle partie.
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

        Debug.Log("Le mot à deviner est : " + currentGame.word);

        ihm.StartNewGameUI(); // Appelle une fonction pour réinitialiser l'IHM
        hangmanUIController.DisplayParts(currentGame.remainingLife);
    }

    /// <summary>
    /// Fonction appelée lorsqu'une lettre est jouée.
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
                EndGame(false); // Termine la partie en mode défaite
            }
        }

        ihm.UpdateWordToGuess();
        ihm.UpdateWrongLetters();
    }
    
    /// <summary>
    /// Gère la fin de la partie.
    /// </summary>
    private void EndGame(bool hasWon)
    {
        isGameFinished = true;
        
        if (hasWon)
        {
            Debug.Log("Bravo vous avez gagné la partie !");
            soundManager.JouerSonVictoire();
            ihm.AfficherImageVictoire();
        }
        else
        {
            Debug.Log("Vous avez perdu. Le mot était : " + currentGame.word);
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