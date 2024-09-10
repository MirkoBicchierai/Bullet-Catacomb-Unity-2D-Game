using UnityEngine;
using UnityEngine.UI;
public class PowerUp : MonoBehaviour{
    public Image imageRenderBuff;
    public PoweUpEffect powerUp;
    public GameObject LevelMenu;

    public void SetPowerUp() {
        imageRenderBuff.sprite = powerUp.image;
    }
    public void SelectAug(){
        powerUp.Apply();
        LevelMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
