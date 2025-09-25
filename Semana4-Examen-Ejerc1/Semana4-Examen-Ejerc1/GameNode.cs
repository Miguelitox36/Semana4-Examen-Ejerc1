using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class GameNode
    {
        private readonly int nodeId;
        private readonly string nodeType;
        private readonly List<GameEntity> entities;
        private readonly Player playerReference;

        public GameNode(int id, string type, Player player = null)
        {
            nodeId = id;
            nodeType = type;
            entities = new List<GameEntity>();
            playerReference = player;
        }

        public void AddEntity(GameEntity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(GameEntity entity)
        {
            entities.Remove(entity);
        }

        public List<GameEntity> GetEntities() => new List<GameEntity>(entities);
        public int GetNodeId() => nodeId;
        public string GetNodeType() => nodeType;

        public bool HasConflict()
        {            
            var playerEntities = entities.Where(e => IsPlayerEntity(e)).ToList();
            var enemyEntities = entities.Where(e => !IsPlayerEntity(e)).ToList();

            return playerEntities.Count > 0 && enemyEntities.Count > 0;
        }

        private bool IsPlayerEntity(GameEntity entity)
        {
            if (playerReference != null)
            {
                return playerReference.GetStructures().Contains(entity) ||
                       playerReference.GetUnits().Contains(entity);
            }
            return true; 
        }

        public void CleanupDeadEntities()
        {
            entities.RemoveAll(e => !e.IsAlive);
        }
    }
}
