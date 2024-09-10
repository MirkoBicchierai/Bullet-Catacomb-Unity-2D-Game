using UnityEngine;

public class Laser : MonoBehaviour{
    public float damage;
    private CapsuleCollider2D col;
    
    void Start(){
        col = GetComponent<CapsuleCollider2D>();
        col.enabled = false;
    }
    public void EndAnimation(){
        Destroy(gameObject);
    }
    public void activeCollider(){
        col.enabled = true;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && !Player.PlayerManager.getActualRool())
        {
            Player.PlayerManager.TakeDmg(damage);
        }
    }

}
