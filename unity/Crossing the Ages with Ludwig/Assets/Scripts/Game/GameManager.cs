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

        /// <summary>
        /// 
        /// </summary>
        public static class KeyPrefs
        {
            public static KeyCode jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
            public static KeyCode pickItem = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pickItemKey", "E"));
            public static KeyCode throwItem = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("throwItemKey", "R"));
        }

        /// <summary>
        /// 
        /// </summary>
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

            // control user preferences for key binding ~later~
            KeyPrefs.jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
            KeyPrefs.pickItem = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pickItemKey", "E"));
            KeyPrefs.throwItem = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("throwItemKey", "R"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteRenderer"></param>
        /// <param name="screenHoriz"></param>
        /// <param name="screenVert"></param>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        public static void GetCameraBoundaries(SpriteRenderer spriteRenderer, float screenHoriz, float screenVert, out float xMin, out float xMax, out float yMin, out float yMax)
        {
            xMin = (float)(screenHoriz - spriteRenderer.sprite.bounds.size.x / 2.0f);
            xMax = (float)(spriteRenderer.sprite.bounds.size.x / 2.0f - screenHoriz);
            yMin = (float)(screenVert - spriteRenderer.sprite.bounds.size.y / 2.0f);
            yMax = (float)(spriteRenderer.sprite.bounds.size.y / 2.0f - screenVert);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteRenderer"></param>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        /// <param name="margin"></param>
        public static void GetMovementBoundaries(SpriteRenderer spriteRenderer, out float xMin, out float xMax, out float yMin, out float yMax, float margin = 0.0f)
        {
            xMax = (float)((spriteRenderer.sprite.bounds.size.x / 2.0f) - margin);
            xMin = (float)(-(spriteRenderer.sprite.bounds.size.x / 2.0f) + margin);
            yMax = (float)((spriteRenderer.sprite.bounds.size.x / 2.0f) - margin);
            yMin = (float)(-(spriteRenderer.sprite.bounds.size.x / 2.0f) + margin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audioClip"></param>
        public static void PlaySoundOneShot(AudioSource source, AudioClip clip)
        {
            if (source != null && clip != null)
            {
                source.PlayOneShot(clip, 1);
            }
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
