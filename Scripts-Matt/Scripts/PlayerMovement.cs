using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Players rigidbody//
    private Rigidbody rb;

    //Variables for Jump and movement speed//
    [SerializeField] private float speed = 500f;
    [SerializeField] private float jumpF = 100f;

    private float moveX;
    private float moveZ;

    //public for testing reasons remember to place as private//
    public bool jumping = false;
    public bool grounded;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        //unused for now
    }

    private void FixedUpdate(){
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        if (jumping && grounded)
        {
            rb.AddForce(0, jumpF * Time.deltaTime, 0, ForceMode.Impulse);
            jumping = false;
        }
        else
            jumping = false;

        rb.AddForce(movement * speed * Time.deltaTime);
    }

    private void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveZ = movementVector.y;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
    private void OnCollisionExit(Collision collision){
        grounded = false;
    }

    /* If this is not working on your machine going into
     * your inspector and click on input system that you
     * added in the roll-a-ball demo, there should be a
     * plus. Click and add the action jump. this will
     * give you access to the OnJump function. If you 
     * have anymore issues please let me know. */
    private void OnJump(){
        if (grounded)
            jumping = true; 
    }
}
