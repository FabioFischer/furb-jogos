﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Objects
{
    public class Ladder : MonoBehaviour, IUsable
    {
        /// <summary>
        /// 
        /// </summary>
        private const string resourceName = "Escada";
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
    }
}
