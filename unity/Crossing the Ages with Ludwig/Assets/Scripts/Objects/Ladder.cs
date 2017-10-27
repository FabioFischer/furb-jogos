using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Objects
{
    public class Ladder : MonoBehaviour, IUsable
    {
        /// <summary>
        /// 
        /// </summary>
        private const string LadderRes = "Escada";
        public static string LadderResource
        {
            get
            {
                return LadderRes;
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
