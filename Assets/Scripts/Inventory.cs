using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory:MonoBehaviour
{   
    public static Inventory InventoryManager;
    public float maxLife = 3f;
    public float life = 3f;
    public float moveSpeed = 5f;
    public float dmgUp = 0f;
    public int bulletNumber = 0;
    public float ratioBuff = 0f;
    public float areaBuff = 1f;
    public float areaExplosionsEffectBuff = 1f;
    public int silverKey = 0;
    public int goldenKey = 0;
    public int currentExp = 0;
    public int maxExp = 100;
    public int expEncrease = 100;
    public int currentLevel = 1;
    public float BulletForceBuff = 0;
    public float SpeedUpRool = 2f;
    public HUD hud;
    public GameObject LevelUpMenu;
    public TextMeshProUGUI levelUpText;
    public PoweUpEffect[] list;
    public PowerUp[] PowerUps;

    private void Awake() {
        if(InventoryManager!=null && InventoryManager!=this)
            Destroy(this);
        else
            InventoryManager = this;
    }

    public void AddExperience(int amount){
        currentExp += amount;
        if(currentExp>=maxExp)  
            LevelUp();

        hud.SetSlider(currentLevel, currentExp);
    }

    private void LevelUp(){
        currentLevel++;
        currentExp = 0;
        maxExp += expEncrease;
        LevelUpMenu.SetActive(true);
        StartLevelingSystem();
        levelUpText.text = "Level " + currentLevel.ToString();
        Time.timeScale = 0f;
    }

    private void StartLevelingSystem(){
        List<PoweUpEffect> listTmp = new List<PoweUpEffect>();
        for (int i = 0; i < 3; i++){
            PoweUpEffect selectedEffect = SelectWithProbability(list, listTmp);
            listTmp.Add(selectedEffect);
        }
        for (int i = 0; i < 3; i++){
            PowerUps[i].powerUp = listTmp[i];
            PowerUps[i].SetPowerUp();
        }
    }

    PoweUpEffect SelectWithProbability(PoweUpEffect[] list, List<PoweUpEffect> excluded){
        float[] cumulativeProbabilities = new float[list.Length];
        float sum = 0f;
        for (int i = 0; i < list.Length; i++){
            float weight = 0.9f - (i * 0.8f / (list.Length - 1));
            sum += weight;
            cumulativeProbabilities[i] = sum;
        }

        for (int i = 0; i < cumulativeProbabilities.Length; i++){
            cumulativeProbabilities[i] /= sum;
        }
        float randomValue = UnityEngine.Random.value;

        for (int i = 0; i < list.Length; i++){
            if (randomValue <= cumulativeProbabilities[i]){
                if (!excluded.Contains(list[i])){
                    return list[i];
                }
                else{
                    return SelectWithProbability(list, excluded);
                }
            }
        }
        return list[list.Length - 1];
    }
    
}
