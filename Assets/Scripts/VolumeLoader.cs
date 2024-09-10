using UnityEngine;

public class LoadVolumeSettings : MonoBehaviour{
    public AudioSource musicAudioSource;
    public AudioSource effectsAudioSource;

    void Start(){
        if (musicAudioSource != null){
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            musicAudioSource.volume = savedMusicVolume;
        }

        if (effectsAudioSource != null){
            float savedEffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
            effectsAudioSource.volume = savedEffectsVolume;
        }
    }

}
