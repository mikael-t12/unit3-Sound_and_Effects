using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables for jump force and gravity modifier
    public float jumpForce = 100;
    public float gravityModifier;

    public bool isOnGround = true;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        //Get player rigidbody component
        playerRb = GetComponent<Rigidbody>();
        //Add gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // If the spacebar is pressed down and player is on the ground, jump;
        if (Input.GetKeyDown(KeyCode.Space)&&isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
