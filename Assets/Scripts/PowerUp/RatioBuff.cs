using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/RatioBuff")]
public class RatioBuff : PoweUpEffect{
    public override void Apply(){   
        Inventory.InventoryManager.ratioBuff += 0.01f;
    }
}

