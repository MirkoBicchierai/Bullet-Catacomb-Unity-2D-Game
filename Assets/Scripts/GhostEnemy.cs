using UnityEngine;

public class GhostEnemy : Enemy{
    
    public override void Update(){
        if (Vector3.Distance(Player.PlayerManager.PlayerCenter.position, transform.position) < AggroDistance)
            ai.canMove = true;
        else
            ai.canMove = false;
        if(ai.desiredVelocity.x>0)
            sp.flipX = false;
        else if(ai.desiredVelocity.x<0)
                sp.flipX = true;
    }

    public override void Die(){   
        float r = Random.value;
        if(r <= HealthDropPercentage)
            Instantiate(healPrefab, dropPosition.position, dropPosition.rotation);
        GameObject x = Instantiate(orbPrefab, dropPosition.position, dropPosition.rotation);
        x.GetComponent<ExperienceParticle>().value = experienceAmount;
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }

}