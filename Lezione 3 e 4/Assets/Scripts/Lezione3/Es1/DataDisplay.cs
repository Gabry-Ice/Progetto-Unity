using TMPro;
using UnityEngine;

namespace MiciomaXD.Es1
{
    public class DataDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text displayText;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            displayText.text = Settings.Instance.mainMenuSettings.dataToPass;
        }
    }
}