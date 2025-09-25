using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public abstract class GameEntity
    {
        protected string name;
        protected int health;
        protected int cost;

        protected GameEntity(string name, int health, int cost)
        {
            this.name = name;
            this.health = health;
            this.cost = cost;
        }

        public virtual bool IsAlive => health > 0;
        public virtual string GetName() => name;
        public virtual int GetHealth() => health;
        public virtual int GetCost() => cost;

        public virtual void TakeDamage(int damage)
        {
            health = Math.Max(0, health - damage);
        }

        public abstract string GetEntityType();
    }    
}
