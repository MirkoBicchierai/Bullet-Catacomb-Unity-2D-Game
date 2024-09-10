using UnityEngine;

public class Bullet : MonoBehaviour{
    
    public GameObject ExplosionEffectPrefabs;
    private float dmg = 1;
    private bool damaged = false;

    void OnCollisionEnter2D(Collision2D other) {
        if(6 == other.gameObject.layer || 13 == other.gameObject.layer){
            Instantiate(ExplosionEffectPrefabs,transform.position,transform.rotation);
            gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("Enemy") && !damaged){   
            damaged = true;
            GameObject collidedObject = other.gameObject;
            GameObject e = Instantiate(ExplosionEffectPrefabs,transform.position,transform.rotation);
            e.transform.localScale = new Vector3(Inventory.InventoryManager.areaExplosionsEffectBuff, Inventory.InventoryManager.areaExplosionsEffectBuff, 0);
            gameObject.SetActive(false);
            collidedObject.GetComponentInChildren<Enemy>().takeDmg(dmg);
        }
    }

    public void setDmg(float d){
        dmg = d;
        damaged = false;
    }

}
