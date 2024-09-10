using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.moveSpeed = Inventory.InventoryManager.moveSpeed + 0.5f;
    }
}

