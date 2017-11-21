using Assets.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    class Flask : MonoBehaviour, IUsable
    {
        private Rigidbody2D rigidBody;
        private Animator animator;
        private AudioSource audioSource;
        private RaycastHit2D hit { get; set; }

        private Vector3 direction { get; set; }
        private float throwSpeed { get; set; }

        public float grabDistance = 2f;
        public AudioClip explosionSound;

        private const string resourceName = "Frasco";
        public static string GetResourceName
        {
            get
            {
                return resourceName;
            }
        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {
            this.rigidBody = GetComponent<Rigidbody2D>();
            this.animator = GetComponent<Animator>();
            this.audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// Collide with actives object. When a object it's on player inventory, it will be desactive.
        /// </summary>
        /// <param name="collision"> Colision generated. </param>
        public void OnTriggerEnter2D(Collider2D collision)
        {
            animator.SetBool("isExploded", false);
            GameManager.PlaySoundOneShot(audioSource, explosionSound);
        }

        /// <summary>
        /// Removes objects collision.
        /// </summary>
        /// <param name="collision"> Collision between objects. </param>
        public void OnTriggerExit2D(Collider2D collision)
        {
        }

        public IEnumerator waitSeconds(int seconds)
        {
            yield return new WaitForSeconds(3);
        }
    }
}
