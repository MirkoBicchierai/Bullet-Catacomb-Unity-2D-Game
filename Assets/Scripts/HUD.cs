using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite part1Heart;
    public Sprite part3Heart;
    public Sprite emptyHeart;
    public Image[] hearts;
    public Image roll;
    public TextMeshProUGUI counter_silverkey;
    public TextMeshProUGUI counter_goldkey;
    public Slider ExpSlider;
    public TextMeshProUGUI ratioExp;
    public TextMeshProUGUI level;

    public void SetSlider(int currentExp, int maxExp)
    {
        ExpSlider.maxValue = Inventory.InventoryManager.maxExp;
        ExpSlider.minValue = 0;
        ExpSlider.value = Inventory.InventoryManager.currentExp;
        level.text = "Level "+Inventory.InventoryManager.currentLevel.ToString();
        ratioExp.text = Inventory.InventoryManager.currentExp.ToString()+"/"+Inventory.InventoryManager.maxExp.ToString();
    }

    void Start(){
        SetSlider(Inventory.InventoryManager.currentExp, Inventory.InventoryManager.maxExp);
    }

    void Update()
    {   
        UpdateKeyInventory();
        UpdateRoll();
        UpdateHearts();
    }

    void UpdateKeyInventory()
    {
        counter_goldkey.text = "x"+Inventory.InventoryManager.goldenKey.ToString();
        counter_silverkey.text = "x"+Inventory.InventoryManager.silverKey.ToString();
    }
    void UpdateRoll()
    {   
        if(Player.PlayerManager.getLastRoll() <= Time.time)
            roll.enabled = true;
        else
            roll.enabled = false;
    }
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Inventory.InventoryManager.maxLife)
            {
                hearts[i].enabled = true;
                if (i < Mathf.FloorToInt(Inventory.InventoryManager.life))
                {
                    hearts[i].sprite = fullHeart;
                }
                else if (i == Mathf.FloorToInt(Inventory.InventoryManager.life))
                {
                    float fractionalLife = Inventory.InventoryManager.life - Mathf.Floor(Inventory.InventoryManager.life);
                    if (fractionalLife >= 0.75f)
                        hearts[i].sprite = part3Heart;
                    else if (fractionalLife >= 0.5f)
                        hearts[i].sprite = halfHeart;
                    else if (fractionalLife >= 0.25f)
                        hearts[i].sprite = part1Heart;
                    else
                        hearts[i].sprite = emptyHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
