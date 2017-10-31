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

    private bool facingRight, onGround;
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
    /// Update is called once per frame.
    /// </summary>
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        onGround = onGroundCheck(groundCheck.position, groundRadius, whatIsGround);

        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
        //myAnimator.SetBool("Ground", onGround); Estourava exception

        KeyActionHandler();
        MouseActionHandler();
        Movement(horizontal, vertical);
    }

    /// <summary> 
    /// Check if player is on ground. 
    /// </summary>
    /// <param name="pos"> Player position. </param>
    /// <param name="radius"> Limit. </param>
    /// <param name="ground"> Object definied as ground. </param>
    /// <returns> True if player is on ground. </returns>
    private bool onGroundCheck(Vector3 pos, float radius, LayerMask ground)
    {
        return Physics2D.OverlapCircle(pos, radius, ground);
    }
    
    /// <summary>
    /// Handle keyboard actions such as: Get object, jump...
    /// </summary>
    public void KeyActionHandler()
    {
        // If E key pressed, get object.
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject flask = GameObject.Find(Flask.FlaskResource);

            if (collisions.Contains(flask))
            {
                collisions.Remove(flask);
                inventory.AddItem(flask);
                //flask.active = false; GameObject.active is obsolete, SetActive(false) used.  
                flask.SetActive(false);
            }
        }
        
        // If space pressed, player jumps
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    /// <summary>
    /// Handle mouse click actions such as: Throws/User objects.
    /// </summary>
    public void MouseActionHandler()
    {
        // Throw object if mouse clicked and inventory list is not empty.
        if (Input.GetMouseButtonDown(0) && inventory.items.Count > 0)
        {
            ThrowObject(inventory.GetFirstItem());
        }
    }

    /// <summary>
    /// Move player.
    /// </summary>
    /// <param name="horizontal"> How much player moves on horizontal. </param>
    /// <param name="vertical"> How much player moves on vertical. </param>
    public void Movement(float horizontal, float vertical)
    {
        ChangeDirection(horizontal);

        if (collisions.Contains(GameObject.Find(Ladder.LadderResource)))
        {
            myRigidBody.gravityScale = 10;
            myRigidBody.velocity = new Vector2(horizontal * 10, vertical * 10);
        }

        myRigidBody.velocity = new Vector2(horizontal * 10, myRigidBody.velocity.y);
    }

    /// <summary>
    /// Change horizontal player direction.
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
    /// Throw object from user inventory.
    /// </summary>
    /// <param name="obj"> Object that will be throwed away. </param>
    private void ThrowObject(GameObject obj)
    {
        if (obj != null)
        {
            // Movimento a posição do objeto até a posição do personagem.
            obj = GameObject.Instantiate(obj, transform.position, transform.rotation);
           
            obj.GetComponent<Rigidbody2D>().AddForce(this.transform.up * 1000);
        }
    }

    /// <summary>
    /// Collide with actives object. When a object it's on player inventory, it will be desactive.
    /// </summary>
    /// <param name="collision"> Colision generated. </param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.active)
            collisions.Add(collision.gameObject);
    }

    /// <summary>
    /// Removes objects collision.
    /// </summary>
    /// <param name="collision"> Collision between objects. </param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision.gameObject);
    }
}
