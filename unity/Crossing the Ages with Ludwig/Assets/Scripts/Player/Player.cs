using Assets.Scripts;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private PlayerInventory inventory { get; set; }
    private RaycastHit2D hit { get; set; }
    private bool facingRight, onGround, onLadder;

    public Transform ground;
    public Transform holdPoint;
    public LayerMask groundObjects;
    public LayerMask notGrabbedObjects;
    public float groundRadius = 0.2f;
    public float jumpForce = 10000f;
    public float throwDistance = 2f;
    public float throwForce = 10f;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = new PlayerInventory();
        
        facingRight = true;
        onLadder = false;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        onGround = onGroundCheck(ground.position, groundRadius, groundObjects);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        //myAnimator.SetBool("Ground", onGround); Descomentar ao adicionar sprite de pulo do personagem

        Movement(horizontal, vertical);
        CheckCollision();
        KeyActionHandler();
        MouseActionHandler();
    }

    /// <summary>
    /// Move player.
    /// </summary>
    /// <param name="horizontal"> How much player moves on horizontal. </param>
    /// <param name="vertical"> How much player moves on vertical. </param>
    public void Movement(float horizontal, float vertical)
    {
        ChangeDirection(horizontal);

        if (onLadder)
        {
            rigidBody.gravityScale = 10;
            rigidBody.velocity = new Vector2(horizontal * 10, vertical * 10);
        }
        else
        {
            rigidBody.velocity = new Vector2(horizontal * 10, rigidBody.velocity.y);
        }
    }

    /// <summary>
    /// Change horizontal player direction.
    /// </summary>
    /// <param name="horizontal"></param>
    private void ChangeDirection(float horizontal)
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
    public void CheckCollision()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(
            transform.position, 
            Vector2.right * transform.localScale.x, 
            throwDistance);
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
    /// Handle keyboard actions such as: 
    ///     E:      get object
    ///     R:      throw object
    ///     SPACE:  jump
    /// </summary>
    public void KeyActionHandler()
    {
        // If E key pressed, get object.
        if (Input.GetKeyDown(KeyCode.E) && hit != null)
        {
            if (hit.collider != null && hit.collider.tag == "Grabbable")
            {
                GameObject obj = hit.collider.gameObject;
                
                inventory.AddItem(obj);
                obj.SetActive(false);
            }
        }

        // If R key pressed, throw first object of inventory.
        if (Input.GetKeyDown(KeyCode.R) && inventory.items.Count > 0)
        {
            GameObject obj = inventory.GetFirstItem();

            if (obj != null)
            {
                obj.SetActive(true);
                inventory.RemoveItem(obj);
                ThrowObject(obj);
            }
        }

        // If space pressed, player jumps
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    /// <summary>
    /// Handle mouse click actions.
    /// </summary>
    public void MouseActionHandler()
    {
    }

    /// <summary>
    /// Throw object from user inventory.
    /// </summary>
    /// <param name="obj"> Object that will be throwed away. </param>
    private void ThrowObject(GameObject obj)
    {
            // Movimento a posição do objeto até a posição do personagem.
            obj.transform.position = transform.position;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
    }

    /// <summary>
    /// Collide with actives object. When a object it's on player inventory, it will be desactive.
    /// </summary>
    /// <param name="collision"> Colision generated. </param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        onLadder = collision.gameObject.Equals(GameObject.Find(Ladder.LadderResource));
    }

    /// <summary>
    /// Removes objects collision.
    /// </summary>
    /// <param name="collision"> Collision between objects. </param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        onLadder = !collision.gameObject.Equals(GameObject.Find(Ladder.LadderResource));
    }
    
    /// <summary>
    /// Draw a reference line based on object throwing distance
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * throwDistance);
    }
}
