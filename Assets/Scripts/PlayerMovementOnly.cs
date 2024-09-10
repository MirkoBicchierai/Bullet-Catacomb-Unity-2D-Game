using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator playerAnimator;
    private SpriteRenderer sp;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }
    private void Update(){
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical"); 
        if (movement.magnitude > 1){
            movement.Normalize();
        }
        if (movement.x != 0 || movement.y != 0){
            playerAnimator.SetBool("Walk", true);
        }
        else{
            playerAnimator.SetBool("Walk", false);
        }

        if (movement.x < 0)
            sp.flipX = true;
        else if (movement.x > 0)
            sp.flipX = false;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
