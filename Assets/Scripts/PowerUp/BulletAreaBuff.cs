using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BulletAreaBuff")]
public class BulletAreaBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.areaBuff += 0.01f;
        Inventory.InventoryManager.areaExplosionsEffectBuff += 0.075f;
    }
}
