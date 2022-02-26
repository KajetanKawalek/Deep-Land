using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    class Entity : Cell
    {
        int heath;
        int maxHealth;
        int armour = 1;

        public override void PreUpdate()
        {

        }

        public override void Update()
        {

        }

        public override void PostUpdate()
        {

        }

        public void TakeDamage(int damage)
        {
            int damageToTake = damage - armour;
            if (damageToTake < 0)
                damageToTake = 0;

            heath -= damageToTake;
        }
    }
}
