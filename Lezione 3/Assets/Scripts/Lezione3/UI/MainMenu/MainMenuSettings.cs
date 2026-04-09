using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// Open and closed the settings panel in the main menu by changing the properties of the CanvasGroup component attached to it. When the settings panel is open, the main menu panel is not interactable and vice versa.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class MainMenuSettings : MonoBehaviour
    {
        public CanvasGroup settingsCanvasGroup;
        CanvasGroup mainMenuCanvasGroup;

        private void OnValidate()
        {
            if (mainMenuCanvasGroup == null)
            {
                mainMenuCanvasGroup = GetComponent<CanvasGroup>();
            }
        }

        private void Awake()
        {
            ResetSettingsCanvas();
        }

        private void ResetSettingsCanvas()
        {
            mainMenuCanvasGroup.interactable = true;

            settingsCanvasGroup.alpha = 0;
            settingsCanvasGroup.blocksRaycasts = false;
            settingsCanvasGroup.interactable = false;
        }

        public void MainMenuOpenSettings()
        {
            mainMenuCanvasGroup.interactable = false;

            settingsCanvasGroup.alpha = 1;
            settingsCanvasGroup.blocksRaycasts = true;
            settingsCanvasGroup.interactable = true;
        }

        public void MainMenuCloseSettings()
        {
            ResetSettingsCanvas();

        }
    }
}