using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class DefenseStructure : Structure
    {
        private readonly int damage;

        public DefenseStructure() : base("Defense Turret", 100, 200)
        {
            damage = 40;
        }

        public override void ExecuteTurnAction(Player owner)
        {
            
        }

        public int GetDamage() => damage;
    }

}
