using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class Player
    {
        private int money;
        private int capacity;
        private readonly List<Structure> structures;
        private readonly List<Unit> units;

        public Player()
        {
            money = 500; 
            capacity = 10;
            structures = new List<Structure>();
            units = new List<Unit>();
        }

        public void AddMoney(int amount) => money += amount;
        public bool SpendMoney(int amount)
        {
            if (money >= amount)
            {
                money -= amount;
                return true;
            }
            return false;
        }

        public void AddCapacity(int amount) => capacity += amount;
        public int GetMoney() => money;
        public int GetCapacity() => capacity;
        public int GetUsedCapacity() => structures.Count + units.Count;

        public void AddStructure(Structure structure)
        {
            structures.Add(structure);
            if (structure is MaintenanceStructure maintenance)
            {
                AddCapacity(maintenance.GetCapacityIncrease());
            }
        }

        public void AddUnit(Unit unit) => units.Add(unit);

        public List<Structure> GetStructures() => new List<Structure>(structures);
        public List<Unit> GetUnits() => new List<Unit>(units);

        public void CleanupDeadEntities()
        {
            structures.RemoveAll(s => !s.IsAlive);
            units.RemoveAll(u => !u.IsAlive);
        }

        public bool HasLost() => structures.Count == 0;
    }
}
