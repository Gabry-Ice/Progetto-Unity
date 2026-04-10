using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiciomaXD.Es1
{
    public class LoadBtnMainMenu : MonoBehaviour
    {
        public void OnLoadNexSceneRequested()
        {
            SceneManager.LoadScene("Es1_LoadedFromMenu");
        }
    }
}