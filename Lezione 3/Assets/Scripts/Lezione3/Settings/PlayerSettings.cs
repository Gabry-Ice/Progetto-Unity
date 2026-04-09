using UnityEngine;
using UnityEngine.Audio;

namespace MiciomaXD
{
    /// <summary>
    /// Class keeping player settings, such as volume. This class is used to keep the settings alive across scenes, so it should be kept in a game object with the Persistent component.
    /// </summary>
    public class PlayerSettings : MonoBehaviour
    {
        public float sfxVolume = 1;
        public float musicVolume = 1;
        public AudioMixer mainMixer;
        public float maxVolume = 10f; //usually is -20
        public float zeroVolume = -80f;
    }
}