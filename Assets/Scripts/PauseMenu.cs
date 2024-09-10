using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour{
    public GameObject pauseMenu;
    public GameObject pauseSquareButton;
    public GameObject levelUpMenu;
    public static bool isPaused;
    public GameObject audioSettingsCanva;

    void Start(){
        audioSettingsCanva.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && !levelUpMenu.activeSelf){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        } 
    }
    public void PauseGame(){
        pauseMenu.SetActive(true);
        switchToPause();
        Time.timeScale = 0f;
        isPaused = true;
    }
     public void ResumeGame(){
        pauseMenu.SetActive(false);
        switchToPlay();
        Time.timeScale = 1f;
        isPaused = false;

    }
     public void GoToMainMenuButton(){
        Time.timeScale = 1f;
        //string filePath = Path.Combine(Application.persistentDataPath, "gameState.json");
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }
    public void resumeButton(){
        ResumeGame();
    }
     public void quitButton(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void pauseButton(){
        PauseGame();
    }
    public void switchToPause(){
        pauseSquareButton.SetActive(false);
    }
    public void switchToPlay(){
        pauseSquareButton.SetActive(true);
    }
    public void SettingsButton(){
        audioSettingsCanva.SetActive(!audioSettingsCanva.activeSelf);
    }
}
