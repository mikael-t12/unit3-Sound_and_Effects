using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    private AudioSource playerSounds;

    public AudioClip jumpSound;
    public AudioClip crashSound;

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

        dirtParticle.Stop();

        playerSounds = GetComponent<AudioSource>();
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

            //Sounds for jumping
            playerSounds.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player collides with the ground, make the isOnGround variable true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            //Add dirt when the player collides with the ground
            dirtParticle.Play();
        }
        //Change the gameOver variable to true if the player collides with the obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Add explosion particle when the player collides with the obstacle
            explosionParticle.Play();

            //Stop playing the dirtParticle
            dirtParticle.Stop();

            //End the game.
            gameOver = true;
            Debug.Log("Game over!!!");

            //Death animation
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);

            //Sounds for the crash
            playerSounds.PlayOneShot(crashSound, 1.0f);
        }
    }
}
