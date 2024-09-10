using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour{
    public TextMeshProUGUI enemy;
    public TextMeshProUGUI boss;
    void Start() {
        enemy.text = "Enemy Kill:" + Parser.counterEnemyKill.ToString();
        boss.text =  "Boss Kill:" + Parser.counterBossKill.ToString() + "/4";
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
    public void WinMainMenuButton(){
        File.Delete(Application.persistentDataPath + "/gamestate.json");
        SceneManager.LoadScene("MainMenu");
    }

}
