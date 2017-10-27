using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    class GameManager : MonoBehaviour
    {
        public static GameManager GM;

        public KeyCode jump { get; set; }
        public KeyCode pickItem { get; set; }

        void Awake()
        {
            // Singleton
            if (GM == null)
            {
                DontDestroyOnLoad(gameObject);
                GM = this;
            }
            else if (GM != this)
            {
                DestroyObject(gameObject);
            }
            
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
            pickItem = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pickItemKey", "E"));
        }

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
        }
    }
}
