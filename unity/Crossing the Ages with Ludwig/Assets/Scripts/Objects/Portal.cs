using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Game;

namespace Assets.Scripts.Objects
{
    public class Portal : MonoBehaviour, IUsable
    {
        /// <summary>
        /// 
        /// </summary>
        private const string resourceName = "Portal";
        public static string GetResourceName
        {
            get
            {
                return resourceName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Use()
        {
            throw new NotImplementedException();
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

