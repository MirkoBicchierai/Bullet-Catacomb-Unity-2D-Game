using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour{    
    public Slider loadingBar;
    public TMP_Text progressText;
    public TMP_Text loadingText;

    private void Start(){
        StartCoroutine(LoadSceneAsync(Parser.nextScene));
    }
    IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";
            if (operation.progress >= 0.9f){
                loadingText.text = "Press any key to continue...";
                if (Input.anyKeyDown)
                    operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
