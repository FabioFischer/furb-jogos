using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IPlayer
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private PlayerInventory inventory { get; set; }
    private List<GameObject> collisions { get; set; }

    private bool facingRight;
    
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
    /// Update is called once per frame
    /// </summary>    
    void Update()
    {
        KeyboardHandler();
        MouseClickHandler();
    }

    /// <summary>
    /// 
    /// </summary>
    public void KeyboardHandler()
    {
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject flask = GameObject.Find("Frasco");

            if (collisions.Contains(flask))
            {
                collisions.Remove(flask);
                inventory.AddItem(flask);
                flask.active = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TODO: Van Halen Jump
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

        if (collisions.Contains(GameObject.Find("Escada")))
        {
            myRigidBody.gravityScale = 10;
            myRigidBody.velocity = new Vector2(horizontal * 10, vertical * 10);
        }

        myRigidBody.velocity = new Vector2(horizontal * 10, myRigidBody.velocity.y);
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
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
