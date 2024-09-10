using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BulletBuff")]
public class BulletBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.bulletNumber ++;
    }
}

