using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Source Audio")]
    public AudioSource audioSource;

    [Header("Clips Audio")]
    public AudioClip sonBouton;
    public AudioClip sonBonneLettre;
    public AudioClip sonMauvaiseLettre;
    public AudioClip sonVictoire;
    public AudioClip sonDefaite;

    public void JouerSonBouton()
    {
        if (sonBouton != null) audioSource.PlayOneShot(sonBouton);
    }

    public void JouerSonBonneLettre()
    {
        if (sonBonneLettre != null) audioSource.PlayOneShot(sonBonneLettre);
    }

    public void JouerSonMauvaiseLettre()
    {
        if (sonMauvaiseLettre != null) audioSource.PlayOneShot(sonMauvaiseLettre);
    }

    public void JouerSonVictoire()
    {
        if (sonVictoire != null) audioSource.PlayOneShot(sonVictoire);
    }

    public void JouerSonDefaite()
    {
        if (sonDefaite != null) audioSource.PlayOneShot(sonDefaite);
    }
}