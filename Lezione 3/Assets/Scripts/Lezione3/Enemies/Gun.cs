using UnityEngine;

namespace MiciomaXD
{
    [RequireComponent(typeof(AudioSource))]
    public class Gun : MonoBehaviour
    {
        AudioSource[] audioSourcesAvailable;
        private void Awake()
        {
            audioSourcesAvailable = GetComponents<AudioSource>();
        }

        public void PlayShootSFX()
        {
            int ix = Random.Range(0, audioSourcesAvailable.Length);
            var chosenAudioSource = audioSourcesAvailable[ix];
            chosenAudioSource.pitch = Random.Range(0.8f, 1.2f);
            chosenAudioSource.Play();
        }
    }
}