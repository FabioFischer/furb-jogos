using Assets.Scripts;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player

{
    public class Player : MonoBehaviour, IPlayer
    {
        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;

        private string resourceName;
        private bool facingRight, onGround, onLadder, onTombStone1, onTombStone2, onTombStone3, isKeyMoved;
        private int level, actualPos;

        private Vector3 keyPosition;

        private Rigidbody2D rigidBody;
        private Animator animator;
        public GameObject textBox;
        private PlayerInventory inventory { get; set; }
        private RaycastHit2D hit { get; set; }
        private AudioSource audioSource;
        public GameObject TombStone1;
        public GameObject TombStone2;
        public GameObject TombStone3;
        public GameObject TombStoneActive1;
        public GameObject TombStoneActive2;
        public GameObject TombStoneActive3;
        public string correctSeq = "231";

        public GameObject restartText, restartButton;
        public AudioClip jumpSound, portalSound;
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
                // Precisa disso para não ferrar com as referências e nomes de gameobjects. Não remover.            
                if(SceneManager.GetActiveScene().name.Equals("Fase1"))
                    return "Sofie";
                else if(SceneManager.GetActiveScene().name.Equals("Fase2"))
                    return "Ludwig";
                else
                    return null;
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
            this.audioSource = GetComponent<AudioSource>();
            
            this.onLadder = false;
            string objectToFind = "";
            this.correctSeq = "132";
            this.actualPos = 0;

            restartText.SetActive(false);
            restartButton.SetActive(false);

            TombStone1 = GameObject.Find("TombStone1");
            TombStone2 = GameObject.Find("TombStone2");
            TombStone3 = GameObject.Find("TombStone3");
            TombStoneActive1 = GameObject.Find("TombStoneActive1");
            TombStoneActive2 = GameObject.Find("TombStoneActive2");
            TombStoneActive3 = GameObject.Find("TombStoneActive3");

            // Precisa disso para não ferrar com as referências e nomes de gameobjects. Não remover. 
            if(SceneManager.GetActiveScene().name.Equals("Fase1"))
            {
                level = 1;
                objectToFind = "Quarto do ludwig";
                keyPosition = GameObject.Find(Key.GetResourceName).transform.position;
                isKeyMoved = false;
                idlePortal();
            }
            else if(SceneManager.GetActiveScene().name.Equals("Fase2"))
            {
                level = 2;
                objectToFind = "Scene";
            }
            else
            {
                level = 0;
                objectToFind = "";
            }

            GameManager.GetMovementBoundaries
                (
                    GameObject.Find(objectToFind).GetComponentInChildren<SpriteRenderer>(), // Após a estruturação dos levels, utilizar o background
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
        /// 
        /// </summary>
        void idlePortal()
        {
            if(level == 1)
            {
                GameObject portal = GameObject.Find(Portal.GetResourceName);
                portal.transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, portal.transform.position.z * (-1));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool isOriginalPosition()
        {
            GameObject key = GameObject.Find(Key.GetResourceName);
            if (key != null) {
                return (this.keyPosition == key.transform.position);
            }
            return !inventory.items.Contains(key);
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            // If textbox its active, doesn't allow player to move
            if(!CheckTextBoxActive())
            {
                MovementHandler();
                CheckCollision();
                KeyActionHandler();
                MouseActionHandler();
                TombStoneHandler();
                IsLevelBeatable();
            }
        }

        /// <summary>
        /// Check if the level is still winnable
        /// </summary>
        public void IsLevelBeatable()
        {
            if (level == 1)
            {
                if (isOriginalPosition() && (!inventory.Contains(Flask.GetResourceName) && GameObject.Find(Flask.GetResourceName) == null))
                {
                    restartText.SetActive(true);
                    restartButton.SetActive(true);
                    animator.SetFloat("Speed", 0);
                }
            }
            else if (level == 2)
            {
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public bool isSeqCorrect(char sequence)
        {
            if(correctSeq[actualPos] == sequence)
            {
                actualPos++;
                return (actualPos == 3);
            }
            actualPos = 0;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void TombStoneHandler()
        {
            
            if(Input.GetKeyDown("e"))
            {
                bool won = false;
                if(onTombStone1)
                {
                    var otherPosn = TombStone1.transform.position;
                    TombStone1.transform.position = new Vector3(otherPosn.x, otherPosn.y, 1);
                    TombStoneActive1.transform.position = new Vector3(otherPosn.x, otherPosn.y, 0);
                    won = isSeqCorrect('1');
                } 
                else if(onTombStone2)
                {
                    var otherPosn = TombStone2.transform.position;
                    TombStone2.transform.position = new Vector3(otherPosn.x, otherPosn.y, 1);
                    TombStoneActive2.transform.position = new Vector3(otherPosn.x, otherPosn.y, 0);
                    won = isSeqCorrect('2');
                }
                else if (onTombStone3)
                {
                    var otherPosn = TombStone3.transform.position;
                    TombStone3.transform.position = new Vector3(otherPosn.x, otherPosn.y, 1);
                    TombStoneActive3.transform.position = new Vector3(otherPosn.x, otherPosn.y, 0);
                    won = isSeqCorrect('3');
                }
            }
        }

        /// <summary>
        /// Check if there is some text box active.
        /// </summary>
        public bool CheckTextBoxActive()
        {
            return textBox.active || restartText.active;
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
                GameManager.PlaySoundOneShot(audioSource, jumpSound);
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
            if(level == 1)
            {
                // Ladder collision
                if (collision.gameObject.Equals(GameObject.Find(Ladder.GetResourceName)))
                {
                    onLadder = true;
                }
                // Book collision
                else if (collision.gameObject.Equals(GameObject.Find(Book.GetResourceName)))
                {
                    if (this.inventory.Contains(Key.GetResourceName))
                    {
                        inventory.RemoveItem(Key.GetResourceName);
                        Destroy(collision.gameObject);
                        idlePortal();
                        GameManager.PlaySoundOneShot(audioSource, portalSound);
                    }
                }
                // Portal collision
                else if (collision.gameObject.Equals(GameObject.Find(Portal.GetResourceName)) && (GameObject.Find(Portal.GetResourceName).transform.position.z < 0))
                {
                    SceneManager.LoadScene("Fase2");
                }
            } 
            else if(level == 2)
            {
                if (collision.gameObject.Equals(GameObject.Find("TombStone1"))) {
         
                    onTombStone1 = true;
                }
                else if (collision.gameObject.Equals(GameObject.Find("TombStone2"))) {
                    onTombStone2 = true;
                }
                    
                else if (collision.gameObject.Equals(GameObject.Find("TombStone3"))) {
                    onTombStone3 = true;
  
                }
            }
        }

        /// <summary>
        /// Removes objects collision.
        /// </summary>
        /// <param name="collision"> Collision between objects. </param>
        public void OnTriggerExit2D(Collider2D collision)
        {
            // Ladder collision
            if (collision.gameObject.Equals(GameObject.Find(Ladder.GetResourceName)))
            {
                onLadder = false;
            }
            else if(level == 2)
            {
                if (collision.gameObject.Equals(GameObject.Find("TombStone1")))
                    onTombStone1 = false;
                else if (collision.gameObject.Equals(GameObject.Find("TombStone2")))
                    onTombStone2 = false;
                else if (collision.gameObject.Equals(GameObject.Find("TombStone3")))
                    onTombStone3 = false;
            }
        }
    }
}
