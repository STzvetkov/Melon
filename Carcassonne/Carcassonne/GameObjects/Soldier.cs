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

        public Soldier()
        {

        }
        public Soldier(bool isActive)
        {
            this.isActive = IsActive;
        }
        public Soldier(bool isActive, bool isFermer, int playerColor)
        {
            this.isActive = IsActive;
            this.isFermer = IsFermer;
            this.playerColorId = PlayerColorID;
        }
    }
}
