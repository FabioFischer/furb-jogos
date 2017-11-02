using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    interface IPlayer
    {
        void MovementHandler();
        void CheckCollision();
        void KeyActionHandler();
        void MouseActionHandler();
    }
}
