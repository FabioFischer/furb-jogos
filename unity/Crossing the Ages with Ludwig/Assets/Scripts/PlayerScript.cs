using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private bool facingRight;
    public bool OnLadder { get; set; }


    // Use this for initialization
    void Start()
    {
        OnLadder = false;
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MovimentHandler(horizontal, vertical);
        Flip(horizontal);
    }

    private void MovimentHandler(float horizontal, float vertical)
    {
        if (OnLadder)
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Escada")
        {
            myRigidBody.gravityScale = 0;
            OnLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Escada")
        {
            myRigidBody.gravityScale = 10;
            OnLadder = false;
        }
    }
}
