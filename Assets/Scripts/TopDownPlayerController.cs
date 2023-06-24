using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float runMultiplier = 1.5f;

    Rigidbody2D rb;
    Collider2D col;
    Animator anim;

    Vector2 moveVector;
    bool isGod = false;
    bool isSprinting = false;


    Vector3 startPosition;
    Vector3 newPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() => rb.velocity = speed * moveVector;

    private void Start()
    {
        startPosition = gameObject.transform.position;
    }

    private void Update()
    {
        GetInput();
        SetAnimations();
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Debug Previous"))
        {
            isGod = !isGod;
            col.enabled = !isGod;            
        }

        isSprinting = Input.GetButton("Fire3");
      
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (isGod) moveVector *= 5f;
        if (isSprinting) moveVector *= runMultiplier;

    }

    private void SetAnimations()
    { 
        // If the player is moving
        if (moveVector != Vector2.zero)
        {
            // Trigger transition to moving state
            anim.SetBool("IsMoving", true);

            // Set X and Y values for Blend Tree
            anim.SetFloat("MoveX", moveVector.x);
            anim.SetFloat("MoveY", moveVector.y);
        }
        else
            anim.SetBool("IsMoving", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //triggers for rooms
        if (col.gameObject.tag == "Cell Hole")
        {
            newPosition = new Vector3(32f, 0.5f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Hallway Stairs Down")
        {
            newPosition = new Vector3(113.82f, -34.46f, 0f);
            gameObject.transform.position = newPosition;
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if (col.gameObject.tag == "Cellar Stairs Up")
        {
            newPosition = new Vector3(74.83f, -19.79f, 0f);
            gameObject.transform.position = newPosition;
            transform.GetChild(2).gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Hallway Door")
        {
            newPosition = new Vector3(165.21f, 3.84f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Checkerboard Door")
        {
            newPosition = new Vector3(116.45f, 0.86f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Checkerboard Stairs Up")
        {
            newPosition = new Vector3(-31.019f, -24.693f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Attic Stairs Down")
        {
            newPosition = new Vector3(170.912f, -24.786f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Cellar Door")
        {
            newPosition = new Vector3(60.48f, -44.09f, 0f);
            gameObject.transform.position = newPosition;
        }

        if (col.gameObject.tag == "Treasure Chamber Door")
        {
            newPosition = new Vector3(101.23f, -41.019f, 0f);
            gameObject.transform.position = newPosition;
        }

        // triggers for slimes
        if(col.gameObject.tag == "Slime Cell" & Input.GetButtonDown("Fire1"))
        {
            // insert code to save the player position and place code at the start to load coordinates when switching back to dungeon scene
            SceneManager.LoadScene("Opponent 1");
        }

        if (col.gameObject.tag == "Slime Hallway" & Input.GetButtonDown("Fire1"))
        {
            // insert code to save the player position and place code at the start to load coordinates when switching back to dungeon scene
            SceneManager.LoadScene("Opponent 2");
        }

    }
}
