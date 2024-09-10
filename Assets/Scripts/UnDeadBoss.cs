using Pathfinding;
using UnityEngine;

public class UnDeadBoss : Enemy{   
    public GameObject KeyDrop;
    public Transform KeyDropPosition;
    public float TimeSpawn;
    public GameObject GhostPrefabs;
    public Transform[] spawnPoints;
    private float spawnTimer;
    public override void Update(){
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
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= TimeSpawn){   
                spawnTimer = 0f;
                animator.SetBool("Summon", true);
            } 
        }  
        
    }
    
    public void EndSummon(){
        animator.SetBool("Summon", false);
        for (int i = 0;i<spawnPoints.Length;i++){
            GhostSpawn(spawnPoints[i]);
        }
    }

    private void GhostSpawn(Transform spawnPoint){
        GameObject x = Instantiate(GhostPrefabs, spawnPoint.position, spawnPoint.rotation);
        x.GetComponent<AIDestinationSetter>().target = Player.PlayerManager.PlayerCenter;
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

}
