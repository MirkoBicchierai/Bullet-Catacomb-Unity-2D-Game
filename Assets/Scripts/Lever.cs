using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour{
    public Transform LeverCenter;
    public TextMeshProUGUI text;
    public GameObject[] gates;
    public string leverId;
    public Animator animator;
    private float distanceThreshold = 2.5f;
    public bool actualState = false;

    void Awake(){
        leverId = GenerateIdFromPosition(transform.position);
    }
    void Start(){
        animator = GetComponent<Animator>();
    }
    void Update(){
        if (Vector3.Distance(Player.PlayerManager.PlayerCenter.position, LeverCenter.position) < distanceThreshold){   
            if(Input.GetKeyDown(KeyCode.E)){
                animator.SetBool("Active", !actualState);
                actualState = !actualState;
                if (actualState)
                    OpenGate();
                else
                    CloseGate();
            }
            text.enabled = true;
        }else{
            text.enabled = false;
        }   
    }
    public void OpenGate(){
        for (int i = 0; i < gates.Length; i++){
            gates[i].GetComponent<Animator>().SetBool("Open", true);
            gates[i].GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    public void CloseGate(){
        for (int i = 0; i < gates.Length; i++){
            gates[i].GetComponent<Animator>().SetBool("Open", false);
            gates[i].GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private string GenerateIdFromPosition(Vector2 position){
        return "Lever_" + position.GetHashCode();
    }
    public string GetId(){
        return leverId;
    }
    public bool IsOpen(){
        return actualState;
    }
}
