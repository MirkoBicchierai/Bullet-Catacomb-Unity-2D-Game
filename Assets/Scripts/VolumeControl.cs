using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour{
    public AudioSource musicAudioSource;
    public GameObject audioSourcesContainer;
    public Slider musicSlider;
    public Slider effectsSlider;
    private AudioSource[] effectsAudioSource;

    void Start(){

        if (audioSourcesContainer != null){
            effectsAudioSource = audioSourcesContainer.GetComponents<AudioSource>();
        }
        
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); 
        float savedEffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);

        Debug.Log(savedEffectsVolume);
        Debug.Log(savedMusicVolume);

        if (musicSlider != null && musicAudioSource != null){
            musicSlider.value = savedMusicVolume;
            SetMusicVolume(savedMusicVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (effectsSlider != null && effectsAudioSource != null){
            effectsSlider.value = savedEffectsVolume;
            SetEffectsVolume(savedEffectsVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        }

    }

    public void SetMusicVolume(float volume){
        if (musicAudioSource != null){
            musicAudioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume); 
            PlayerPrefs.Save();
        }
    }

    public void SetEffectsVolume(float volume){
        if (effectsAudioSource != null){
            foreach (var audioSource in effectsAudioSource){
                audioSource.volume = volume;
            }
            PlayerPrefs.SetFloat("EffectsVolume", volume);
            PlayerPrefs.Save();
        }
    }
    
}
