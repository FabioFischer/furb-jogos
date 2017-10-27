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

        private const string FlaskRes = "Frasco";
        public static string FlaskResource
        {
            get
            {
                return FlaskRes;
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
    }
}
