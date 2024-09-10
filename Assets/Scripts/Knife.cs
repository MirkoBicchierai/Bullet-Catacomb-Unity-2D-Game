using UnityEngine;

public class Knife : MonoBehaviour{   
    public float dmg = 0;

    void OnCollisionEnter2D(Collision2D other) {
        if(6 == other.gameObject.layer || 13 == other.gameObject.layer)
            Destroy(gameObject);
        if(other.gameObject.CompareTag("Player") && !Player.PlayerManager.getActualRool())
        {   
            Destroy(gameObject);
            Player.PlayerManager.TakeDmg(dmg);
        }
    }
    
}
