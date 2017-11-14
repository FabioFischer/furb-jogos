using Assets.Scripts;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;

namespace Assets.Scripts.Player

{
    public class Player : MonoBehaviour, IPlayer
    {
        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;

        private const string resourceName = "Sofie";
        private bool facingRight, onGround, onLadder;

        private Rigidbody2D rigidBody;
        private Animator animator;
        private AudioSource audio;
        private PlayerInventory inventory { get; set; }
        private RaycastHit2D hit { get; set; }

        public AudioClip jumpSound;
        public Transform ground;
        public Transform holdPoint;
        public LayerMask groundObjects;
        public float walkSpeed = 10.0f;
        public float groundRadius = 0.2f;
        public float jumpForce = 10000f;
        public float grabDistance = 2f;
        public float throwForce = 10f;

        /// <summary>
        /// 
        /// </summary>
        public static string GetResourceName
        {
            get
            {
                return resourceName;
            }
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            this.rigidBody = GetComponent<Rigidbody2D>();
            this.animator = GetComponent<Animator>();
            this.inventory = new PlayerInventory();

            this.onLadder = false;

            GameManager.GetMovementBoundaries
                (
                    GameObject.Find("Quarto do ludwig").GetComponentInChildren<SpriteRenderer>(), // Após a estruturação dos levels, utilizar o background
                    out this.xMin,
                    out this.xMax,
                    out this.yMin,
                    out this.yMax,
                    2.0f // margin value
                );

            //Init scene facing left
            ChangeDirection(1);
            facingRight = !facingRight;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            MovementHandler();
            CheckCollision();
            KeyActionHandler();
            MouseActionHandler();
        }

        /// <summary>
        /// Move player.
        /// </summary>
        public void MovementHandler()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            ChangeDirection(horizontal);

            if (onLadder)
            {
                rigidBody.velocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
            }
            else if (CheckBoundaries(transform.position, horizontal, vertical))
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontal));

                rigidBody.velocity = new Vector2
                    (
                        horizontal * walkSpeed,
                        rigidBody.velocity.y
                    );
            }
            else
            {
                animator.SetFloat("Speed", Mathf.Abs(0));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool CheckBoundaries(Vector3 pos, float horizontal, float vertical)
        {
            return (facingRight)
                ? pos.x + horizontal <= xMax && pos.y + vertical <= yMax && pos.y + vertical >= yMin
                : pos.x + horizontal >= xMin && pos.y + vertical <= yMax && pos.y + vertical >= yMin;
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
            onGround = onGroundCheck(ground.position, groundRadius, groundObjects);
            //myAnimator.SetBool("Ground", onGround); Descomentar ao adicionar sprite de pulo do personagem

            Physics2D.queriesStartInColliders = false;
            hit = Physics2D.Raycast
                (
                    transform.position,
                    Vector2.right * transform.localScale.x,
                    grabDistance
                );
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
            if (Input.GetKeyDown(GameManager.KeyPrefs.pickItem) && hit != null)
            {
                if (hit.collider != null && hit.collider.tag == "Grabbable")
                {
                    GameObject obj = hit.collider.gameObject;

                    inventory.AddItem(obj);
                    obj.SetActive(false);
                }
            }

            // If R key pressed, throw first object on inventory.
            if (Input.GetKeyDown(GameManager.KeyPrefs.throwItem) && inventory.items.Count > 0)
            {
                GameObject obj = inventory.GetFirstItem();

                if (obj != null)
                {
                    inventory.RemoveItem(obj);
                    obj.SetActive(true);
                    ThrowObject(obj);
                    hit = new RaycastHit2D();
                }
            }

            // If space pressed, player jumps
            if (onGround && Input.GetKeyDown(GameManager.KeyPrefs.jump))
            {
                // Jump
                rigidBody.AddForce(new Vector2(0, jumpForce));

                // Play jump sound
                Debug.Log("pulano");
                float vol = Random.Range (70, 100);
                audio.PlayOneShot(jumpSound, vol);
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
            obj.transform.localScale = transform.localScale;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
        }

        /// <summary>
        /// Collide with actives object. When a object it's on player inventory, it will be desactive.
        /// </summary>
        /// <param name="collision"> Colision generated. </param>
        public void OnTriggerEnter2D(Collider2D collision)
        {
            onLadder = collision.gameObject.Equals(GameObject.Find(Ladder.GetResourceName));
        }

        /// <summary>
        /// Removes objects collision.
        /// </summary>
        /// <param name="collision"> Collision between objects. </param>
        public void OnTriggerExit2D(Collider2D collision)
        {
            onLadder = !collision.gameObject.Equals(GameObject.Find(Ladder.GetResourceName));
        }
    }
}
