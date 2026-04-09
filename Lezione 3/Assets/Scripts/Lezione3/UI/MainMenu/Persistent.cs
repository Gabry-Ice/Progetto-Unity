using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// The game object with this component will not be destroyed when loading a new scene. Useful for keeping music or other objects alive across scenes.
    /// </summary>
    public class Persistent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}