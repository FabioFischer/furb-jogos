using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private PlayerInventory inventory { get; set; }
    private bool facingRight;
    public bool onLadder { get; set; }
    public bool onFlask { get; set; }

    // Use this for initialization
    void Start()
    {
        onLadder = false;
        onFlask = false;
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        KeyBoardActionHandler();
        MovimentHandler(horizontal, vertical);
        Flip(horizontal);
    }

    private void KeyBoardActionHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onFlask)
            {
            }
        }
    }

    private void MovimentHandler(float horizontal, float vertical)
    {
        if (onLadder)
        {
            myRigidBody.velocity = new Vector2(horizontal * 10, vertical * 10);
        }

        myRigidBody.velocity = new Vector2(horizontal * 10, myRigidBody.velocity.y);
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    private void ThrowObject(GameObject obj)
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Escada":
                myRigidBody.gravityScale = 10;
                onLadder = true;
                break;
            case "Frasco":
                onFlask = true;
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Escada":
                myRigidBody.gravityScale = 10;
                onLadder = false;
                break;
        }
    }
}
