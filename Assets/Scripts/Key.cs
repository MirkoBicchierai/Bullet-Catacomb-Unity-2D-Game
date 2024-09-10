using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour, IIdentifiable
{
    public bool goldenKey = false;
    public string keyId;

    private bool takeKey = false;

    void Awake(){
        keyId = GenerateIdFromPosition(transform.position);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && !Player.PlayerManager.getActualRool() && !takeKey){
            takeKey = true;
            if(goldenKey)
               Inventory.InventoryManager.goldenKey++;
            else
                Inventory.InventoryManager.silverKey++;
            Destroy(gameObject);
        }
    }
    private string GenerateIdFromPosition(Vector2 position){
        return "Key_" + position.GetHashCode();
    }
    public string GetId(){
        return keyId;
    }
}
