using UnityEngine;

public class Heal : MonoBehaviour{
    
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){   
            Inventory.InventoryManager.life = Inventory.InventoryManager.life + 3;
            if(Inventory.InventoryManager.life>Inventory.InventoryManager.maxLife)
                Inventory.InventoryManager.life = Inventory.InventoryManager.maxLife;
            Destroy(gameObject);
        }
    }

}
