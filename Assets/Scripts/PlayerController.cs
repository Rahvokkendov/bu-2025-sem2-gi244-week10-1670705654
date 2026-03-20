using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public float hp;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    public InputAction dashAction;
    private bool isOnGround = true;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    public int jumpCount = 2;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint");

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && jumpCount > 0 && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
            jumpCount--;
            if (jumpCount < 0)
            {
                jumpCount = 0;
            }
        }
        if (isOnGround)
        {
            jumpCount = 2;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Game Over!");
            //gameOver = true;

            hp--;
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSfx);

            if (hp <= 0)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
                gameOver = true;
            }
    
            Destroy(collision.gameObject);
        }
    }

}