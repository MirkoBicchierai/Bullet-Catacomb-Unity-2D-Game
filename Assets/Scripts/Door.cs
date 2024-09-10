using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool goldenDoor = false;
    public GameObject padlock;
    private Animator animator;
    private float distanceThreshold = 3.8f;

    public bool isOpen = false;
    public string doorId;

    void Awake(){
        doorId = GenerateIdFromPosition(transform.position);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {   
        if (Vector3.Distance(Player.PlayerManager.PlayerCenter.position, transform.position) < distanceThreshold)
        {   
            if(Input.GetKeyDown(KeyCode.E))
            {   
                if(goldenDoor && Inventory.InventoryManager.goldenKey>0){
                    OpenDoor();
                    Inventory.InventoryManager.goldenKey--;
                }

                if(!goldenDoor && Inventory.InventoryManager.silverKey>0){
                    OpenDoor();
                    Inventory.InventoryManager.silverKey--;
                }
            }
        }    
        
    }

    private string GenerateIdFromPosition(Vector2 position)
    {
        return "Door_" + position.GetHashCode();
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public string GetId()
    {
        return doorId;
    }

    public void SetDoorState(bool state)
    {
        isOpen = state;
    }

    public void OpenDoor()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        padlock.SetActive(false);
        animator.SetBool("Open",true);
        isOpen=true;
    }
}
