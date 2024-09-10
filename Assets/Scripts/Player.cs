using System.Collections;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Player : MonoBehaviour{
    public static Player PlayerManager;
    public float dmgDelay = 0.5f;
    public float rollDelay = 2f;
    public float flashHitDuration = 0.1f;
    public Color flashColor = Color.red;
    public Transform attachWeapon;
    public Transform dropPosition;
    public TMP_Text wastedText;
    public AudioSource stepsSound;
    public GameObject weapon1Prefabs;
    public GameObject weapon2Prefabs;
    public GameObject weapon3Prefabs;
    public Transform PlayerCenter;
    private float moveSpeedRoolMultiplyer = 1f;
    private GameObject actualWeapon;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator playerAnimator;
    private SpriteRenderer sp;
    private float lastDmg;
    private bool ActualRool = false;
    private float lastRoll;
    private Color originalColor;
    
    private void Awake() {
        if(PlayerManager!=null && PlayerManager!=this)
            Destroy(this);
        else
            PlayerManager = this;
    }
    void Start(){   
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        originalColor = sp.color;
        wastedText.gameObject.SetActive(false);
    }
    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1){
            movement.Normalize();
        }

        if (movement.x!=0 || movement.y!=0){
            playerAnimator.SetBool("Walk", true);
            if (!stepsSound.isPlaying)
                stepsSound.Play();
            if (Input.GetKeyDown(KeyCode.Space) && !ActualRool){
                if(lastRoll <= Time.time){ 
                    StartRool();
                    lastRoll = Time.time + rollDelay;
                }
            }  
        }
        else{
            playerAnimator.SetBool("Walk", false);
            if(stepsSound.isPlaying)
                stepsSound.Stop();
        }

        if (movement.x<0)
           sp.flipX = true;
        else if (movement.x>0)
           sp.flipX = false;
    }
    public float getLastRoll(){ 
        return lastRoll;
    }

    public bool getActualRool(){
        return ActualRool;
    }
    void FixedUpdate(){   
        rb.MovePosition(rb.position + movement * Inventory.InventoryManager.moveSpeed * moveSpeedRoolMultiplyer * Time.fixedDeltaTime);
    }
    public void Die(){   
        ShowWasted();
        playerAnimator.SetBool("Die", true);
    }
    public void Lose(){   
        gameObject.SetActive(false);
        File.Delete(Application.persistentDataPath + "/gamestate.json");
        SceneManager.LoadScene("LoseScene");
    }
    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") && !ActualRool){
            Enemy e = other.gameObject.GetComponentInChildren<Enemy>();
            TakeDmg(e.meleeDmg);
        }
    }
    public void TakeDmg(float d){
        if(lastDmg <= Time.time) {
            Inventory.InventoryManager.life -= d;
            FlashOnHit();
            lastDmg = Time.time + dmgDelay;
            if(Inventory.InventoryManager.life <= 0)
                Die();
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") && !ActualRool){
            Rigidbody2D rbE = other.gameObject.GetComponent<Rigidbody2D>();
            if (rbE != null) {
                Vector2 forceDirection = (other.transform.position - transform.position).normalized;
                rbE.velocity = forceDirection * 3;
                AIPath aiPath = other.gameObject.GetComponent<AIPath>();
                CapsuleCollider2D collider = other.gameObject.GetComponent<CapsuleCollider2D>();
                StartCoroutine(DisableAIMovement(collider, aiPath, 0.5f));
            }
        }
        if(other.gameObject.CompareTag("Weapon") && !ActualRool){   
            weaponSwitch(other.gameObject);
        }
    }
    private void weaponSwitch(GameObject newWeapon){
        newWeapon.GetComponent<BoxCollider2D>().enabled = false;
        if (actualWeapon != null){   
            StartCoroutine(AbleWeaponCollider(actualWeapon, 2.5f));
        }
        actualWeapon = newWeapon;
        Vector3 scale = newWeapon.GetComponent<PickUpItem>().getStartScale();
        newWeapon.GetComponent<PickUpItem>().enabled = false;
        newWeapon.GetComponent<PickUpItem>().SwitchEnableArrow(false);
        newWeapon.transform.localScale = scale;
        actualWeapon.GetComponent<Weapon>().enabled = true;
        actualWeapon.transform.SetPositionAndRotation(attachWeapon.position, attachWeapon.rotation);
        actualWeapon.transform.parent = attachWeapon;
    }
    IEnumerator AbleWeaponCollider(GameObject weapon, float delay){   
        weapon.GetComponent<Weapon>().enabled = false;
        weapon.transform.parent = null;
        weapon.transform.SetPositionAndRotation(dropPosition.position, dropPosition.rotation);
        weapon.GetComponent<SpriteRenderer>().flipY = false;
        yield return new WaitForSeconds(delay);
        if (weapon != actualWeapon){   
            weapon.GetComponent<BoxCollider2D>().enabled = true;    
            weapon.GetComponent<PickUpItem>().enabled = true;
            weapon.GetComponent<PickUpItem>().SwitchEnableArrow(true);
        }
    }
    IEnumerator DisableAIMovement(CapsuleCollider2D collider,AIPath aiPath, float delay) {
        collider.enabled = false;
        aiPath.enabled = false;
        yield return new WaitForSeconds(delay);
        aiPath.enabled = true;
        collider.enabled = true;
    }
    public void StartRool(){
        ActualRool = true;
        if (actualWeapon != null)
            actualWeapon.SetActive(false);
        playerAnimator.SetBool("Rool", true);
        moveSpeedRoolMultiplyer = Inventory.InventoryManager.SpeedUpRool;
    }
    public void EndRool(){
        moveSpeedRoolMultiplyer = 1;
        playerAnimator.SetBool("Rool", false);
        if (actualWeapon != null)
            actualWeapon.SetActive(true);
        ActualRool = false;
    }
    private void FlashOnHit(){
        if (sp != null){
            StartCoroutine(FlashRoutine());
        }
    }
    private IEnumerator FlashRoutine(){
        sp.color = flashColor;
        yield return new WaitForSeconds(flashHitDuration);
        sp.color = originalColor;
    }
    public GameObject GetEquippedWeapon(){
        return actualWeapon;
    }
    public void EquipWeapon(GameObject newWeapon){
        weaponSwitch(newWeapon);
    }
    public void ShowWasted(){
        wastedText.gameObject.SetActive(true);
    }
}
