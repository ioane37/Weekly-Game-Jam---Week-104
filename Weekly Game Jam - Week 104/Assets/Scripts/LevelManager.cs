using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

namespace Main.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] GameObject sliderHolder = null;

        [SerializeField] Slider slider;
        [SerializeField] TextMeshProUGUI progressText;

        private bool loading = false;

        public void LoadLevelAsynchronously(string levelToLoad)
        {
            sliderHolder.SetActive(true);
            StartCoroutine(LoadAsynchronously(levelToLoad));
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        private IEnumerator LoadAsynchronously(string levelToLoad)
        {
            if (!loading)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);

                slider.gameObject.SetActive(true);

                loading = true;

                while (!operation.isDone)
                {
                    float progress = Mathf.Clamp01(operation.progress / 0.9f);

                    slider.value = progress;
                    progressText.text = "Loading: " + (progress * 100f).ToString("f2") + "%";

                    yield return null;
                }
            }
        }
    }
}