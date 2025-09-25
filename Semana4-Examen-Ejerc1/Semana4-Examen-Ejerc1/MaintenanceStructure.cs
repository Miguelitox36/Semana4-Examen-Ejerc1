using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class MaintenanceStructure : Structure
    {
        private readonly int capacityIncrease;

        public MaintenanceStructure() : base("Supply Depot", 75, 150)
        {
            capacityIncrease = 5;
        }

        public override void ExecuteTurnAction(Player owner)
        {            
        }

        public int GetCapacityIncrease() => capacityIncrease;
    }
}
