using UnityEngine;

public class RockBoss : Enemy{   
    public GameObject KeyDrop;
    public Transform KeyDropPosition;
    public GameObject Laser;
    public Transform LaserPosition;
    public GameObject Bullet;
    public Transform BulletPositionR;
    public Transform BulletPositionL;
    public float laserCooldown = 6f; 
    public float LaserDmg = 2;
    public float bulletCooldown = 3f; 
    public float bulletDmg = 1;
    public float bulletForce = 10f;
    private bool activeLaser = false;
    private float laserShootTimer = 0f;
    private float ShootTimer = 0;

    public override void Update()
    {
        if (Vector3.Distance(Player.PlayerManager.PlayerCenter.position, transform.position) < AggroDistance)
            EnableAI();
        else
            DisableAI();

        if(ai.desiredVelocity.x>0){
            sp.flipX = false;
        }
        else if(ai.desiredVelocity.x<0){
            sp.flipX = true;
            }
            
        if(IsEnabled()){
            laserShootTimer += Time.deltaTime;
            if (laserShootTimer >= laserCooldown){   
                laserShootTimer = 0f;
                if (!activeLaser){   
                    Vector3 playerPositionAtShoot = Player.PlayerManager.PlayerCenter.position;
                    animator.SetBool("Laser", true);
                    GameObject laserInstance = Instantiate(Laser, LaserPosition.position, Quaternion.identity);
                    laserInstance.GetComponent<Laser>().damage = LaserDmg;
                    laserInstance.transform.SetParent(LaserPosition, false);

                    Vector3 directionToPlayer = playerPositionAtShoot - LaserPosition.position;
                    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                    laserInstance.transform.rotation = Quaternion.Euler(0, 0, angle);

                    Transform laserPointStart = laserInstance.transform.Find("laserPointStart");
                    Vector3 offset = laserPointStart.position - laserInstance.transform.position;
                    laserInstance.transform.position = LaserPosition.position - offset;

                    activeLaser = true;
                    ShootTimer = 0;
                }
            }
        
            ShootTimer += Time.deltaTime;
            if (ShootTimer >= bulletCooldown && !activeLaser){   
                ShootTimer = 0f; 
                animator.SetBool("Shoot", true);
                Transform pos;
                if (sp.flipX)
                    pos = BulletPositionL;
                else
                    pos = BulletPositionR;
                SpawnKnife(45f, pos);
                SpawnKnife(90f, pos);
                SpawnKnife(0f, pos);
                SpawnKnife(-45f, pos);
                SpawnKnife(-90f, pos);
            }
        }     
    }

    void SpawnKnife(float angle, Transform pos){ 
        GameObject b = Instantiate(Bullet, pos.position, Quaternion.Euler(0, 0, angle));
        b.GetComponent<Knife>().dmg = bulletDmg;
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        if (sp.flipX) {
            direction = -direction;
        }else
            b.GetComponent<SpriteRenderer>().flipX = true;
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && !Player.PlayerManager.getActualRool()){
            animator.SetBool("Attack", true);
        }
    }

    public override void Die(){   
        float r = Random.value;
        if(r <= HealthDropPercentage)
            Instantiate(healPrefab, dropPosition.position, dropPosition.rotation);
        GameObject x = Instantiate(orbPrefab, dropPosition.position, dropPosition.rotation);
        x.GetComponent<ExperienceParticle>().value = experienceAmount;
        Instantiate(KeyDrop, KeyDropPosition.position, KeyDropPosition.rotation);
        Parser.counterBossKill++;
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }

    public void EndAttack(){
        animator.SetBool("Attack", false);
    }

    public void EndLaser(){
        animator.SetBool("Laser", false);
        activeLaser = false;
    }

    public void EndShoot(){
        animator.SetBool("Shoot", false);
    }
    
}
