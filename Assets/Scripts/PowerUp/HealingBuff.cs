using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealingBuff")]
public class HealingBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.life = Inventory.InventoryManager.maxLife;
    }
}
