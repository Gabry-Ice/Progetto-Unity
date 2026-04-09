using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiciomaXD
{
    /// <summary>
    /// Manages the transition from the main menu to the game scene, including fade effects and loading progress
    /// display.
    /// </summary>
    /// <remarks>This component coordinates scene loading with visual feedback, ensuring a smooth user
    /// experience by enforcing a minimum loading time and displaying progress updates. It requires references to the
    /// main menu and loading canvas groups, as well as a text component for progress indication. Attach this script to
    /// a GameObject in the main menu scene to enable seamless scene transitions.</remarks>
    public class SceneLoaderFromMainMenu : MonoBehaviour
    {
        public float loadingTimeMinTime = 2f;
        public float fadeOutTime;
        public float fadeInTime;

        public CanvasGroup mainMenuCanvasGr;
        public CanvasGroup loadingCanvasGr;
        public TMP_Text progressText;

        private void Awake()
        {
            if (mainMenuCanvasGr == null)
            {
                //CanvasGroup cGr = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None).First(x => x.name.Contains("MainMenu"));

                CanvasGroup[] canvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
                for (int i = 0; i < canvasGroups.Length; i++)
                {
                    CanvasGroup cg = canvasGroups[i];
                    if (cg.name.Contains("MainMenu"))
                    {
                        mainMenuCanvasGr = cg;
                        break;
                    }
                }
            }

            if (loadingCanvasGr == null)
            {
                //CanvasGroup loadingCanvasGroup = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None).First(x => x.name.Contains("Loading"));

                CanvasGroup[] canvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
                for (int i = 0; i < canvasGroups.Length; i++)
                {
                    CanvasGroup cg = canvasGroups[i];
                    if (cg.name.Contains("Loading"))
                    {
                        loadingCanvasGr = cg;
                        break;
                    }
                }
            }

            if(progressText == null)
            {
                //progressText = GetComponentsInChildren<TMP_Text>().First(x => x.name.Contains("Progress"));

                TMP_Text[] textFields = FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);
                for (int i = 0; i < textFields.Length; i++)
                {
                    TMP_Text t = textFields[i];
                    if (t.name.Contains("MainMenu"))
                    {
                        progressText = t;
                        break;
                    }
                }
            }
        }

        public void LoadGameFromMainMenu()
        {
            Debug.Log("Loading game from main menu...");
            StartCoroutine(Do_LoadGameFromMainMenu());
        }

        IEnumerator Do_LoadGameFromMainMenu()
        {
            float elapsed = 0; //make sure to show loading screen at least for 2 seconds, cooler

            yield return StartCoroutine(Do_FadeOut(mainMenuCanvasGr));

            ShowLoading();

            var load = SceneManager.LoadSceneAsync(SceneName.MainGame.ToString());
            load.allowSceneActivation = false;
            while (load.progress < 0.9f)
            {
                elapsed += Time.deltaTime;

                progressText.text =  $"{((int)(load.progress / 0.9f * 100f))}%";
                yield return null;
            }
            progressText.text = "100%";

            if (elapsed <= loadingTimeMinTime)
            {
                yield return new WaitForSeconds(loadingTimeMinTime - elapsed);
            }

            load.allowSceneActivation = true;

            Scene gameScene = SceneManager.GetSceneByName(SceneName.MainGame.ToString());
            yield return new WaitUntil(() => gameScene.isLoaded);

            SceneManager.SetActiveScene(gameScene);

            //var unload = SceneManager.UnloadSceneAsync(SceneName.MainMenu.ToString());

            //yield return StartCoroutine(Do_FadeIn());
        }

        private void ShowLoading()
        {
            loadingCanvasGr.blocksRaycasts = true;

            StartCoroutine(Do_FadeIn(loadingCanvasGr));
        }

        IEnumerator Do_FadeIn(CanvasGroup cGr)
        {
            cGr.alpha = 0f;
            while (cGr.alpha < 1)
            {
                cGr.alpha += Time.deltaTime / fadeInTime;
                cGr.alpha = Mathf.Clamp01(cGr.alpha);

                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator Do_FadeOut(CanvasGroup cGr)
        {
            cGr.alpha = 1f;
            while (cGr.alpha > 0)
            {
                cGr.alpha -= Time.deltaTime / fadeOutTime;
                cGr.alpha = Mathf.Clamp01(cGr.alpha);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}