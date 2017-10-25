using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    interface IPlayer
    {
        void KeyboardHandler();
        void MouseClickHandler();
        void Movement(float horizontal, float vertical);
    }
}
