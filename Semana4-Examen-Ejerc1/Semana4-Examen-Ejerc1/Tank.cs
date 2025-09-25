using Semana4_Examen_Ejerc1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{ 
public class Tank : Unit
{
    public Tank() : base("Battle Tank", 120, 150, 50, 2) { }

    public override bool CanAttack(GameEntity target)
    {
        return target is Soldier || target is Structure;
    }
}
}
