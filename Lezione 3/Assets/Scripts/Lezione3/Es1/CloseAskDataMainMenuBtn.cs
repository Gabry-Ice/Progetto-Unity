using UnityEngine;

namespace MiciomaXD.Es1
{
    public class CloseAskDataMainMenuBtn : MonoBehaviour
    {
        [SerializeField] CanvasGroup mainMenuGr;
        [SerializeField] CanvasGroup askDataGr;
        public void OnCloseAskDataPressed()
        {
            askDataGr.alpha = 0;
            askDataGr.interactable = false;
            askDataGr.blocksRaycasts = false;

            mainMenuGr.interactable = true;
        }
    }
}