using UnityEngine;

public class ExperienceParticle : MonoBehaviour{

    public int value = 5;
    private float AggroDistance = 10f;
    private Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if (Vector3.Distance(Player.PlayerManager.PlayerCenter.position, transform.position) < AggroDistance)
            rb.velocity = (Player.PlayerManager.PlayerCenter.transform.position - transform.position).normalized * 2.5f;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {   
            Inventory.InventoryManager.AddExperience(value);
            Destroy(gameObject);
        }
    }
    
}
