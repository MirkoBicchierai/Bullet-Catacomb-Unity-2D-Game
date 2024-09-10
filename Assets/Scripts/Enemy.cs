using UnityEngine;
using Pathfinding;
using System.Collections;

public abstract class Enemy : MonoBehaviour, IIdentifiable
{
    public AIPath ai;
    public float life = 3;
    public Animator animator;
    public float meleeDmg = 1;
    public bool HitAnimation = true;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    public EnemyHeathBar heathbar;
    [SerializeField, Range(0f, 1f)] public float HealthDropPercentage = 0f;
    public GameObject healPrefab;
    public Transform dropPosition;
    public float AggroDistance = 30f;
    public int experienceAmount = 20;
    public GameObject orbPrefab;
    public string enemyId;
    private float Speed;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    protected SpriteRenderer sp; 
    protected float maxLife = 3;

    void Awake(){
        enemyId = GenerateIdFromPosition(transform.position);
    }

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        maxLife = life;
        heathbar.SetHeath(life, maxLife);
        Speed = ai.maxSpeed;
    }

    public abstract void Update();

    public void takeDmg(float dmg){
        life -= dmg;
        heathbar.SetHeath(life, maxLife);
        FlashOnHit();
        if(life<=0){
            DisableAI();
            animator.SetBool("Die", true);
            Collider2D[] colliders = GetComponentsInParent<Collider2D>();
            foreach (Collider2D col in colliders){
                col.enabled = false;
            }
        }else{
            if(HitAnimation){
                animator.SetBool("Hit", true);
            }
        }
    }

    public void resetWalk(){
        animator.SetBool("Hit", false);
    }

    public abstract void Die();
    public void kill(){
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }

    public bool IsEnabled(){
        return ai.enabled;
    }

    public void DisableAI(){
        ai.maxSpeed = 0;
        ai.enabled = false;
    }

    public void EnableAI(){
        ai.maxSpeed = Speed;
        ai.enabled = true;
    }
    public void FlashOnHit(){
        if (spriteRenderer != null){
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine(){
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private string GenerateIdFromPosition(Vector2 position){
        return "Enemy_" + position.GetHashCode();
    }

    public string GetId(){
        return enemyId;
    }

}
