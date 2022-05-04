using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Players rigidbody//
    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    //Variables for Jump and movement speed//
    [SerializeField] private float speed = 500f;
    [SerializeField] private float sprintSpeed = 200f;
    [SerializeField] private float jumpF = 100f;

    private int count;
    [SerializeField] private float countMax = 13;

    private float moveX;
    private float moveZ;

    //public for testing reasons remember to place as private//
    public bool jumping = false;
    public bool grounded;
    public bool sprinting = false;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        SetCountText();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate(){
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        if (jumping && grounded)
        {
            rb.AddForce(0, jumpF * Time.deltaTime, 0, ForceMode.Impulse);
            jumping = false;
        }
        if(!sprinting)
            rb.AddForce(movement * speed * Time.deltaTime);
        else
        {
            rb.AddForce(movement * (speed + sprintSpeed) * Time.deltaTime);
        }
    }

    private void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveZ = movementVector.y;
    }

    /* If this is not working on your machine going into
     * your inspector and click on input system that you
     * added in the roll-a-ball demo, there should be a
     * plus. Click and add the action jump. this will
     * give you access to the OnJump function. If you 
     * have anymore issues please let me know. */
    private void OnJump()
    {
        if (grounded)
            jumping = true;
    }

    private void OnSprintPress()
    {
        sprinting = true;
    }
    private void OnSprintRelease()
    {
        sprinting = false;
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= countMax)
            winTextObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
    private void OnCollisionExit(Collision collision){
        grounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
