using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;     // Referencia al Audio Mixer
    public Slider musicSlider;        // Slider para controlar volumen música
    public Slider sfxSlider;          // Slider para controlar volumen efectos

    private const string MusicVolumeParam = "MusicVolume";
    private const string SFXVolumeParam = "SFXVolume";

    void Start()
    {
        // Cargar valores guardados o establecer por defecto
        musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeParam, 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeParam, 0.75f);

        // Aplicar volúmenes iniciales
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        // Añadir listeners para cuando cambian los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        // AudioMixer usa decibelios, 0 = max, -80 = silencio
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(MusicVolumeParam, dB);
        PlayerPrefs.SetFloat(MusicVolumeParam, volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(SFXVolumeParam, dB);
        PlayerPrefs.SetFloat(SFXVolumeParam, volume);
    }
}