using UnityEngine;
using UnityEngine.UI;

namespace MiciomaXD
{
    /// <summary>
    /// Keeps settings saved in the PlayerSettings class updated with the values of the music and sfx sliders in the settings panel of the main menu. It also updates the volume of the music and sfx audio mixers accordingly. When the value of the sfx slider changes, it plays a sample sound to let the player hear how the new volume will sound like.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class VolumeSliders : MonoBehaviour
    {
        AudioSource settingsAudioSource;
        [SerializeField] Slider sfxSlider;
        [SerializeField] Slider musicSlider;

        PlayerSettings playerSettings;

        private void OnValidate()
        {
            if (settingsAudioSource == null)
            {
                settingsAudioSource = GetComponent<AudioSource>();
            }
        }

        private void Awake()
        {
            playerSettings = FindFirstObjectByType<PlayerSettings>();
        }

        public void PlaySFXSample()
        {
            settingsAudioSource.Play();
        }

        public void OnMusicSliderValueChange()
        {
            playerSettings.musicVolume = musicSlider.value;

            playerSettings.mainMixer.SetFloat("MusicVolume", ValueToVolume(musicSlider.value));
        }

        public void OnSFXSliderValueChange()
        {
            playerSettings.sfxVolume = sfxSlider.value;
            playerSettings.mainMixer.SetFloat("SFXVolume", ValueToVolume(sfxSlider.value));

            settingsAudioSource.Play();
        }

        /// <summary>
        /// Conversion from linear to perceptual sound space.
        /// </summary>
        private float ValueToVolume(float value)
        {
            return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * (playerSettings.maxVolume - playerSettings.zeroVolume) / 4f + playerSettings.maxVolume;
        }
    }
}