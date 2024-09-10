using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/RoolVelocityBuff")]
public class RoolVelocityBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.SpeedUpRool += 0.35f;
    }
}
