using System.Collections.Generic;
using UnityEngine;

public class HangmanUIController : MonoBehaviour
{
    public List<HangmanInfo> hangmanInfos;

    void Start()
    {
        // On s'assure que tout est propre au premier lancement
        HideAllParts();
    }

    /// <summary>
    /// Cache toutes les parties du pendu. Rendue publique pour être appelée de l'extérieur.
    /// </summary>
    public void HideAllParts()
    {
        // On parcourt chaque état possible du pendu...
        foreach (var info in hangmanInfos)
        {
            // ...et pour chaque état, on cache toutes les parties associées.
            // C'est plus sûr que de ne cacher que celles du dernier état.
            foreach (var part in info.hangmanParts)
            {
                part.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Affiche les parties du pendu correspondant au nombre de vies restantes.
    /// </summary>
    public void DisplayParts(int life)
    {
        // On vérifie que l'index est valide pour éviter les erreurs
        int infoIndex = 7 - life;

        if (infoIndex >= 0 && infoIndex < hangmanInfos.Count)
        {
            HideAllParts();
            // On active la ou les parties correspondant à cette étape
            foreach (var part in hangmanInfos[infoIndex].hangmanParts)
            {
                part.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class HangmanInfo
{
    public int life; // Ce champ est utile pour vous repérer dans l'inspecteur
    public List<GameObject> hangmanParts;
}