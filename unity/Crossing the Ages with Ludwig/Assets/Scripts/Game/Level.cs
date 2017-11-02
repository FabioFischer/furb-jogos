using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game
{
    class Level
    {
        public string backgroundResourceName { get; set; }

        public Level(string backgroundResourceName)
        {
            this.backgroundResourceName = backgroundResourceName;
        }
    }
}
