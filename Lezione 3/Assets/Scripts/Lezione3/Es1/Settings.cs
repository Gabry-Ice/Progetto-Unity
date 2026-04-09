using UnityEngine;

namespace MiciomaXD.Es1
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance { get; private set; }

        public MainMenuSettings mainMenuSettings = new();

        private void Awake()
        {

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}