using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class Soldier : Unit
    {
        public Soldier() : base("Infantry Soldier", 80, 75, 30, 1) { }

        public override bool CanAttack(GameEntity target)
        {
            return target is Helicopter || target is Structure;
        }
    }
}
