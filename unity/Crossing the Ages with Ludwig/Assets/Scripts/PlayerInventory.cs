using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class PlayerInventory
    {
        public List<GameObject> items { get; set; }

        public void AddItem(GameObject obj)
        {
            if (obj == null)
            {
                throw new Exception();
            }

            items.Add(obj);
        }

        public GameObject GetItem(string name)
        {
            return items.Find(o => o.tag == name);
        }
    }
}
