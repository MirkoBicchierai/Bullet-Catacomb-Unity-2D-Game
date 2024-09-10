using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    public GameObject resumeButton; 
    public GameObject audioSettingsCanva;
    private string saveFilePath;

    void Start(){
        audioSettingsCanva.SetActive(false);
        saveFilePath = Application.persistentDataPath + "/gameState.json";
        try{
            if (!File.Exists(saveFilePath)){
                resumeButton.SetActive(false);
            }
            else{
                resumeButton.SetActive(true);
            }
        }
        catch (Exception e){
            Debug.LogError("error in start main menu, message: " + e.Message);
        }
    }
    public void PlayGame(){   
        Parser.ResumePressed = false;
        Parser.nextScene = "Level1";
        SceneManager.LoadScene("LoadingScene");
    }
    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void ResumeGame(){
        if (File.Exists(saveFilePath)){   
            Parser.ResumePressed = true;
            string json = File.ReadAllText(saveFilePath);
            PlayerState savedState = JsonUtility.FromJson<PlayerState>(json);
            Parser.nextScene = savedState.sceneName;
            SceneManager.LoadScene("LoadingScene");
        }
        else{
            Debug.Log("no saved scene found");
        }
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
    public void SettingsButton(){
        audioSettingsCanva.SetActive(!audioSettingsCanva.activeSelf);
    }

}
