using TMPro;
using UnityEngine;

namespace MiciomaXD.Es1
{
    public class DataInput : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        public void OnDataChanged(string _)
        {
            string content = inputField.text;
            if (string.IsNullOrEmpty(content))
            {
                Settings.Instance.mainMenuSettings.dataToPass = "Default data";
            }
            else
            {
                Settings.Instance.mainMenuSettings.dataToPass = content;
            }
        }
    }
}