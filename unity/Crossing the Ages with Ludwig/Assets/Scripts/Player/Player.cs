using Assets.Scripts;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private PlayerInventory inventory { get; set; }
    private List<GameObject> collisions { get; set; }

    private bool facingRight;
    bool onGround;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    float jumpForce = 10000f;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        inventory = new PlayerInventory();
        collisions = new List<GameObject>();
        
        facingRight = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        myAnimator.SetBool("Ground", onGround);

        KeyboardHandler();
        MouseClickHandler();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>    
    void Update()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            print("Pulando " + jumpForce);
            myAnimator.SetBool("Ground", false);
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void KeyboardHandler()
    {
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject flask = GameObject.Find(Flask.FlaskResource);

            if (collisions.Contains(flask))
            {
                collisions.Remove(flask);
                inventory.AddItem(flask);
                flask.active = false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void MouseClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowObject(inventory.GetFirstItem());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public void Movement(float horizontal, float vertical)
    {
        ChangeDirection(horizontal);

        if (collisions.Contains(GameObject.Find(Ladder.LadderResource)))
        {
            myRigidBody.gravityScale = 10;
            myRigidBody.velocity = new Vector2(horizontal * 10, vertical * 10);
        }

        myRigidBody.velocity = new Vector2(horizontal * 10, myRigidBody.velocity.y);
        myAnimator.SetFloat("hSpeed", Mathf.Abs(horizontal));
        myAnimator.SetFloat("vSpeed", Mathf.Abs(Input.GetAxis("Vertical")));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="horizontal"></param>
    private void ChangeDirection (float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    private void ThrowObject(GameObject obj)
    {
        if (obj != null)
        {
            //TODO: Throw that godamn obj
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.active)  collisions.Add(collision.gameObject);
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision.gameObject);
    }
}
