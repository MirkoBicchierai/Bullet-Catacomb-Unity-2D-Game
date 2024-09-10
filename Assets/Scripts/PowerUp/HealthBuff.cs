using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthBuff")]
public class HealthBuff : PoweUpEffect{
    public override void Apply(){   
        if(Inventory.InventoryManager.maxLife<10)
            Inventory.InventoryManager.maxLife++;
    }
}
