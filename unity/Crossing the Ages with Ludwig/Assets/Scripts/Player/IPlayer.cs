using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    interface IPlayer
    {
        void KeyActionHandler();
        void MouseActionHandler();
        void Movement(float horizontal, float vertical);
    }
}
