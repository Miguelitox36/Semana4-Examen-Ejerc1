using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public abstract class Structure : GameEntity
    {
        protected Structure(string name, int health, int cost) : base(name, health, cost) { }

        public override string GetEntityType() => "Structure";
        public abstract void ExecuteTurnAction(Player owner);
    }
}
