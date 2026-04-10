using UnityEngine;

namespace MiciomaXD.Es1
{
    public class AskDataMainMenuBtn : MonoBehaviour
    {
        [SerializeField] CanvasGroup mainMenuGr;
        [SerializeField] CanvasGroup askDataGr;
        public void OnAskDataPressed()
        {
            mainMenuGr.interactable = false;


            askDataGr.alpha = 1;
            askDataGr.interactable = true;
            askDataGr.blocksRaycasts = true;
        }
    }
}