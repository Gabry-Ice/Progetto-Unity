using UnityEditor;
using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// Plays a sound effect when the player clicks on a button in the main menu (PlaySFX needs to be linked to OnClick button event in the editor). The volume of the sound effect is set to the value of the sfxVolume variable in the PlayerSettings class, so it will be affected by the sfx slider in the settings panel of the main menu.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BtnAudioplayer : MonoBehaviour
    {
        PlayerSettings playerSettings;
        AudioSource audioSource;
        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if (playerSettings == null)
            {
                playerSettings = FindFirstObjectByType<PlayerSettings>();
            }
        }

        public void PlaySFX()
        {
            audioSource.volume = playerSettings.sfxVolume;
            audioSource.Play();
        }
    }
}