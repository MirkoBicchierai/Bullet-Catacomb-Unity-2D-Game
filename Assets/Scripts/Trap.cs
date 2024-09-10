using System.Collections.Generic;
using UnityEngine;
public class Trap : MonoBehaviour{   
    public float switchTime = 1f;
    public Sprite activateSprite;
    public float dmg = 0.25f;
    private Sprite disabledSprite;
    private bool actualState = false;
    private float lastActivate;
    private bool playerTakedmg = false;
    private List<GameObject> enemies = new List<GameObject>();

    void Start(){
        disabledSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update(){
        if(lastActivate > Time.time) return;
            switchMode();
        lastActivate = Time.time + switchTime;
    }

    public void switchMode(){
        if (actualState){
            GetComponent<SpriteRenderer>().sprite = activateSprite;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = disabledSprite;
            GetComponent<BoxCollider2D>().isTrigger = true;
            enemies.Clear();
            playerTakedmg = false;
        } 
        actualState = !actualState;
    }

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.CompareTag("Enemy") && !enemies.Contains(other.gameObject)){ 
           other.gameObject.GetComponentInChildren<Enemy>().takeDmg(dmg);
           enemies.Add(other.gameObject);
        }
        if(other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<Player>().getActualRool() && !playerTakedmg){
           other.gameObject.GetComponentInChildren<Player>().TakeDmg(dmg);
           playerTakedmg = true;
        }
    }

}
