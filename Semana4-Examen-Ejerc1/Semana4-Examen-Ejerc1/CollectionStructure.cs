using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    internal class CollectionStructure : Structure
    {
        private readonly int incomePerTurn;

        public CollectionStructure() : base("Resource Collector", 50, 100)
        {
            incomePerTurn = 25;
        }

        public override void ExecuteTurnAction(Player owner)
        {
            owner.AddMoney(incomePerTurn);
        }
    }
}
