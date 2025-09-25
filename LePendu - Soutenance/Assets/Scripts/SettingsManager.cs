using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public const string MUSIC_VOLUME_KEY = "musicVolume";
    public const string SFX_VOLUME_KEY = "sfxVolume";

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        LoadSettings();
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
    }

    private void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.75f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }
}