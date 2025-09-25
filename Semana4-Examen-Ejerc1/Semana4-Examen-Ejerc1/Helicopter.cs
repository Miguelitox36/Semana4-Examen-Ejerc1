using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class Helicopter : Unit
    {
        public Helicopter() : base("Combat Helicopter", 60, 200, 40, 3) { }

        public override bool CanAttack(GameEntity target)
        {
            return target is Tank || target is Structure;
        }
    }
}
