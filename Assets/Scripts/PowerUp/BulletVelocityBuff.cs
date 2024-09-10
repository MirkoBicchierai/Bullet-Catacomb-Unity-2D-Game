using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BulletVelocityBuff")]
public class BulletVelocityBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.BulletForceBuff += 2f;
    }
}
