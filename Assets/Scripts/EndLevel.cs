using System;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndLevel : MonoBehaviour
{
    public String levelToGo = "2";
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){   
            if(levelToGo!="VictoryScene")
                LoadParser();
            Parser.nextScene = levelToGo;
            SceneManager.LoadScene("LoadingScene");
        }
    }

    private void LoadParser(){   
        Parser.maxLife = Inventory.InventoryManager.maxLife;
        Parser.life = Inventory.InventoryManager.life;
        Parser.moveSpeed = Inventory.InventoryManager.moveSpeed;
        Parser.dmgUp = Inventory.InventoryManager.dmgUp;
        Parser.bulletNumber = Inventory.InventoryManager.bulletNumber;
        Parser.ratioBuff = Inventory.InventoryManager.ratioBuff;
        Parser.areaBuff = Inventory.InventoryManager.areaBuff;
        Parser.silverKey = Inventory.InventoryManager.silverKey;
        Parser.goldenKey  = Inventory.InventoryManager.goldenKey;
        Parser.currentExp = Inventory.InventoryManager.currentExp;
        Parser.maxExp = Inventory.InventoryManager.maxExp;
        Parser.currentLevel = Inventory.InventoryManager.currentLevel;
        Parser.BulletForceBuff = Inventory.InventoryManager.BulletForceBuff;
        Parser.SpeedUpRool = Inventory.InventoryManager.SpeedUpRool;
        Parser.areaExplosionsEffectBuff = Inventory.InventoryManager.areaExplosionsEffectBuff;
        Parser.weapon = Player.PlayerManager.GetEquippedWeapon().GetComponent<Weapon>().getType();
        Parser.loadParser = true;
    }

}
