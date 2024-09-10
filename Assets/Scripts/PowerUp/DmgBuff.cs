using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DmgBuff")]
public class DmgBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.dmgUp += 0.05f;
    }
}

