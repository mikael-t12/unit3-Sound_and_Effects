using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
    public ParticleSystem explosionParticle;

    //Variables for jump force and gravity modifier
    public float jumpForce = 100;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get player rigidbody component
        playerRb = GetComponent<Rigidbody>();
        //Add gravity
        Physics.gravity *= gravityModifier;

        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the spacebar is pressed down and player is on the ground, jump, and if the game is not over
        if ((Input.GetKeyDown(KeyCode.Space) && isOnGround) && gameOver == false)
        {
            //Add force to the player
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //Player is not grounded anymore
            isOnGround = false;

            //Play the jump animation
            playerAnimation.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player collides with the ground, make the isOnGround variable true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        //Change the gameOver variable to true if the player collides with the obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
         {
            explosionParticle.Play();
            gameOver = true;
            Debug.Log("Game over!!!");

            //Death animation
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
        }
    }
}
