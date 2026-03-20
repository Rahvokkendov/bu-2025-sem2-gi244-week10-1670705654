using UnityEngine;
using UnityEngine.InputSystem;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;

    private float leftBound = -15;

    public float speedModifier = 2f;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!playerController.gameOver)
        {
            if (playerController.dashAction.WasPressedThisDynamicUpdate())
            {
                speed *= speedModifier;

            }
            else if (playerController.dashAction.WasReleasedThisDynamicUpdate())
            {
                speed /= speedModifier;
            }
            
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
