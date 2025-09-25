
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public abstract class Unit : GameEntity
    {
        protected int damage;
        protected int speed;

        protected Unit(string name, int health, int cost, int damage, int speed)
            : base(name, health, cost)
        {
            this.damage = damage;
            this.speed = speed;
        }

        public override string GetEntityType() => "Unit";
        public virtual int GetDamage() => damage;
        public virtual int GetSpeed() => speed;

        public abstract bool CanAttack(GameEntity target);
    }
}
