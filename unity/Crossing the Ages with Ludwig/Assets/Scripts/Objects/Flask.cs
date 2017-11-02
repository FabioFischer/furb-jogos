using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    class Flask : MonoBehaviour, IUsable
    {
        private Vector3 direction { get; set; }
        private float throwSpeed { get; set; }

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
        }

        /// <summary>
        /// Removes objects collision.
        /// </summary>
        /// <param name="collision"> Collision between objects. </param>
        public void OnTriggerExit2D(Collider2D collision)
        {
        }
    }
}
