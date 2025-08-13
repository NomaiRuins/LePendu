using UnityEngine;
using TMPro;

public class IHM : MonoBehaviour
{
    [Header("R�f�rences UI")]
    public TextMeshProUGUI wordToGuessText;
    public TextMeshProUGUI wrongLettersText;
    public GameObject imageVictoire;
    public GameObject imageDefaite;

    [Header("R�f�rence au Manager")]
    public GameManager gameManager;

    /// <summary>
    /// R�initialise l'interface pour une nouvelle partie.
    /// </summary>
    public void StartNewGameUI()
    {
        // On cache les �crans de fin de partie
        if (imageVictoire != null) imageVictoire.SetActive(false);
        if (imageDefaite != null) imageDefaite.SetActive(false);

        // On met � jour l'affichage du mot et des lettres
        UpdateWordToGuess();
        UpdateWrongLetters();
    }

    /// <summary>
    /// Met � jour l'affichage du mot avec les lettres trouv�es et des tirets.
    /// </summary>
    public void UpdateWordToGuess()
    {
        string wordToDisplay = "";
        foreach (char letter in gameManager.currentGame.word)
        {
            if (gameManager.currentGame.usedLetters.Contains(letter.ToString()))
            {
                wordToDisplay += letter + " ";
            }
            else
            {
                wordToDisplay += "_ ";
            }
        }
        wordToGuessText.text = wordToDisplay;
    }

    /// <summary>
    /// Met � jour l'affichage de la liste des lettres incorrectes.
    /// </summary>
    public void UpdateWrongLetters()
    {
        string wrongLetters = "";
        foreach (string letter in gameManager.currentGame.usedLetters)
        {
            if (!gameManager.currentGame.WordContainsLetter(letter))
            {
                wrongLetters += letter + " ";
            }
        }
        wrongLettersText.text = wrongLetters;
    }

    /// <summary>
    /// Affiche l'�cran de victoire.
    /// </summary>
    public void AfficherImageVictoire()
    {
        if (imageVictoire != null) imageVictoire.SetActive(true);
    }

    /// <summary>
    /// Affiche l'�cran de d�faite.
    /// </summary>
    public void AfficherImageDefaite()
    {
        if (imageDefaite != null) imageDefaite.SetActive(true);
    }
}