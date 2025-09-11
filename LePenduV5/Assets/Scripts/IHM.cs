using UnityEngine;
using TMPro;

public class IHM : MonoBehaviour
{
    [Header("Références UI")]
    public TextMeshProUGUI wordToGuessText;
    public TextMeshProUGUI wrongLettersText;
    public GameObject imageVictoire;
    public GameObject imageDefaite;

    [Header("Référence au Manager")]
    public GameManager gameManager;

    /// <summary>
    /// Réinitialise l'interface pour une nouvelle partie.
    /// </summary>
    public void StartNewGameUI()
    {
        // On cache les écrans de fin de partie
        if (imageVictoire != null) imageVictoire.SetActive(false);
        if (imageDefaite != null) imageDefaite.SetActive(false);

        // On met à jour l'affichage du mot et des lettres
        UpdateWordToGuess();
        UpdateWrongLetters();
    }

    /// <summary>
    /// Met à jour l'affichage du mot avec les lettres trouvées et des tirets.
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
    /// Met à jour l'affichage de la liste des lettres incorrectes.
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
    /// Affiche l'écran de victoire.
    /// </summary>
    public void AfficherImageVictoire()
    {
        if (imageVictoire != null) imageVictoire.SetActive(true);
    }

    /// <summary>
    /// Affiche l'écran de défaite.
    /// </summary>
    public void AfficherImageDefaite()
    {
        if (imageDefaite != null) imageDefaite.SetActive(true);
    }
}