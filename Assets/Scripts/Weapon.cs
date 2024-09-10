using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour{
    public GameObject bullet;   
    public Transform firePoint;
    public float shootDelay = 0.25f;
    public GameObject shootEffect;
    public bool shootgun = false;
    public float bulletForce = 20f;
    public float dmg = 1;
    public AudioSource firingSound;
    public enum SystemOptions
    {
        Pistol = 1,
        AssoultRifle = 2,
        ShootGun = 3
    }
    [SerializeField] public SystemOptions WeaponType;

    private float lastShot;
    private SpriteRenderer sp;
    private Camera cam;

    void Start(){
        cam = Camera.main;
        sp = GetComponent<SpriteRenderer>();
    }
    void Update(){
        RotateWeaponTowardsMouse();
        if(!PauseMenu.isPaused){
            if (Input.GetButton("Fire1")){
                if(lastShot > Time.time) return;
                    shoot();
                lastShot = Time.time + (shootDelay-Inventory.InventoryManager.ratioBuff);
            }
        }
    }
    private void shoot(){   
        firingSound.Play();
        StartCoroutine(ShootBurst());
    }
    private IEnumerator ShootBurst(){
        for (int i = 0; i < Inventory.InventoryManager.bulletNumber; i++){
            if(!shootgun){
                Rigidbody2D rb = spawnBullet();
                GameObject eff = Instantiate(shootEffect, firePoint.position,Quaternion.identity);
                eff.transform.SetParent(gameObject.transform);
                eff.transform.rotation = transform.rotation;
                Destroy(eff,0.1f);
                rb.AddForce(firePoint.up*(bulletForce+Inventory.InventoryManager.BulletForceBuff),ForceMode2D.Impulse);
            }else{
                Rigidbody2D rb1 = spawnBullet();
                Rigidbody2D rb2 = spawnBullet();
                Rigidbody2D rb3 = spawnBullet();
                GameObject eff = Instantiate(shootEffect, firePoint.position,Quaternion.identity);
                eff.transform.SetParent(gameObject.transform);
                eff.transform.rotation = transform.rotation;
                Destroy(eff,0.1f);
                rb1.AddForce(firePoint.up * (bulletForce+Inventory.InventoryManager.BulletForceBuff), ForceMode2D.Impulse);
                rb2.AddForce(Quaternion.Euler(0, 0, 10) * firePoint.up * (bulletForce+Inventory.InventoryManager.BulletForceBuff), ForceMode2D.Impulse);
                rb3.AddForce(Quaternion.Euler(0, 0, -10) * firePoint.up * (bulletForce+Inventory.InventoryManager.BulletForceBuff), ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.025f);
        }
    }

    private Rigidbody2D spawnBullet(){
        GameObject b = ObjectPool.SharedInstance.GetPooledObject(); 
        if (b != null) {
            b.transform.position = firePoint.position;
            b.transform.rotation = firePoint.rotation;
            b.SetActive(true);
        }else{
            b = Instantiate(bullet, firePoint.position, firePoint.rotation);
            ObjectPool.SharedInstance.pooledObjects.Add(b);
        }
        b.transform.localScale = new Vector3(Inventory.InventoryManager.areaBuff, Inventory.InventoryManager.areaBuff, 0);
        b.GetComponent<Bullet>().setDmg(dmg+Inventory.InventoryManager.dmgUp);
        return b.GetComponent<Rigidbody2D>();
    }

    private void RotateWeaponTowardsMouse(){
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.DrawLine(transform.position, mousePosition, Color.red);
        if (mousePosition.x < transform.position.x)
            sp.flipY = true;
        else
            sp.flipY = false;
    }
    public int getType(){
        return (int)WeaponType;
    }
}
