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

        public Soldier()
        {
            this.isActive = false;
            this.isFermer = false;
            this.playerColorId = 0;
        }
        public Soldier(bool initIsActive)
        {
            this.isActive = initIsActive;
            this.isFermer = false;
            this.playerColorId = 0;
        }
        public Soldier(bool initIsActive, bool initIsFermer, int initPlayerColorID)
        {
            this.isActive = initIsActive;
            this.isFermer = initIsFermer;
            this.playerColorId = initPlayerColorID;
        }
    }
}
