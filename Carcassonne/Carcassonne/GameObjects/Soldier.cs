using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.GameObjects
{
    public class Soldier :Creature
    {
        private bool isActive;
        private bool isFermer;
        private int playerColorId;

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
        }

        public bool IsFermer
        {
            get
            {
                return this.isFermer;
            }
        }

        public int PlayerColorID
        {
            get
            {
                return this.PlayerColorID;
            }
        }

        //TODO: Add constructor
    }
}
