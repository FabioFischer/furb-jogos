using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    class PlayerInventory
    {
        public List<GameObject> items { get; set; }

        public PlayerInventory()
        {
            items = new List<GameObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void AddItem(GameObject obj)
        {
            if (obj == null)
            {
                throw new Exception();
            }

            items.Add(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveItem(GameObject obj)
        {
            this.items.Remove(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GameObject GetFirstItem()
        {
            return items.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetItem(string name)
        {
            return items.Find(o => o.tag == name);
        }
    }
}
