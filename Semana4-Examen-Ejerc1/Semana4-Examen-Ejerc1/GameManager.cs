using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class GameManager
    {
        private readonly Player player;
        private readonly EnemyAI enemyAI;
        private readonly List<GameNode> gameNodes;
        private readonly List<Unit> enemyUnits;
        private int turnCount;
        private bool gameEnded;

        public GameManager()
        {
            player = new Player();
            enemyAI = new EnemyAI();
            gameNodes = InitializeNodes();
            enemyUnits = new List<Unit>();
            turnCount = 0;
            gameEnded = false;
        }

        private List<GameNode> InitializeNodes()
        {
            var nodes = new List<GameNode>
        {
        new GameNode(0, "Player Base", player),
        new GameNode(1, "Path Node"),
        new GameNode(2, "Path Node"),
        new GameNode(3, "Path Node"),
        new GameNode(4, "Enemy Base")
        };
            return nodes;
        }

        public void StartGame()
        {
            Console.WriteLine("=== Strategy Game Started ===");
            Console.WriteLine("Objective: Destroy enemy base or survive as long as possible!");
            Console.WriteLine();

            while (!gameEnded)
            {
                turnCount++;
                Console.WriteLine($"--- Turn {turnCount} ---");

                PlayerTurn();
                if (gameEnded) break;

                EnemyTurn();
                CheckGameState();
            }
        }

        private void PlayerTurn()
        {
            Console.WriteLine("\n=== Your Turn ===");

            bool turnEnded = false;
            while (!turnEnded && !gameEnded)
            {
                ShowPlayerMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowPlayerStructures();
                        break;
                    case "2":
                        ShowPlayerUnits();
                        break;
                    case "3":
                        CreateStructure();
                        break;
                    case "4":
                        CreateUnit();
                        break;
                    case "5":
                        ShowEnemies();
                        break;
                    case "6":
                        ShowNodes();
                        break;
                    case "7":
                        MovePlayerUnitsForward();
                        break;
                    case "8":                     
                        turnEnded = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
                       
            ExecuteStructureActions();
        }

        private void ShowPlayerMenu()
        {
            Console.WriteLine($"\nMoney: ${player.GetMoney()} | " +
                            $"Capacity: {player.GetUsedCapacity()}/{player.GetCapacity()}");
            Console.WriteLine("1. View Structures");
            Console.WriteLine("2. View Units");
            Console.WriteLine("3. Create Structure");
            Console.WriteLine("4. Create Unit");
            Console.WriteLine("5. View Enemies");
            Console.WriteLine("6. View Map Nodes");
            Console.WriteLine("7. Move Units Forward");
            Console.WriteLine("8. End Turn");
            Console.Write("Choose option: ");
        }

        private void ShowPlayerStructures()
        {
            var structures = player.GetStructures();
            Console.WriteLine("\n=== Your Structures ===");
            if (structures.Count == 0)
            {
                Console.WriteLine("No structures built.");
                return;
            }

            for (int i = 0; i < structures.Count; i++)
            {
                var s = structures[i];
                Console.WriteLine($"{i + 1}. {s.GetName()} - Health: {s.GetHealth()}");
            }
        }

        private void ShowPlayerUnits()
        {
            var units = player.GetUnits();
            Console.WriteLine("\n=== Your Units ===");
            if (units.Count == 0)
            {
                Console.WriteLine("No units created.");
                return;
            }

            for (int i = 0; i < units.Count; i++)
            {
                var u = units[i];
                Console.WriteLine($"{i + 1}. {u.GetName()} - Health: {u.GetHealth()}, " +
                                $"Damage: {u.GetDamage()}, Speed: {u.GetSpeed()}");
            }
        }

        private void CreateStructure()
        {
            Console.WriteLine("\n=== Create Structure ===");
            Console.WriteLine("1. Resource Collector ($100) - Generates money each turn");
            Console.WriteLine("2. Supply Depot ($150) - Increases unit/structure capacity");
            Console.WriteLine("3. Defense Turret ($200) - Attacks enemies");
            Console.Write("Choose structure type: ");

            string input = Console.ReadLine();
            Structure newStructure = null;

            switch (input)
            {
                case "1":
                    newStructure = new CollectionStructure();
                    break;
                case "2":
                    newStructure = new MaintenanceStructure();
                    break;
                case "3":
                    newStructure = new DefenseStructure();
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    return;
            }

            if (player.GetUsedCapacity() >= player.GetCapacity())
            {
                Console.WriteLine("Not enough capacity! Build Supply Depots to increase capacity.");
                return;
            }

            if (player.SpendMoney(newStructure.GetCost()))
            {
                player.AddStructure(newStructure);
                gameNodes[0].AddEntity(newStructure); 
                Console.WriteLine($"{newStructure.GetName()} built successfully!");
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
        }

        private void CreateUnit()
        {
            Console.WriteLine("\n=== Create Unit ===");
            Console.WriteLine("1. Infantry Soldier ($75) - Attacks helicopters and structures");
            Console.WriteLine("2. Battle Tank ($150) - Attacks soldiers and structures");
            Console.WriteLine("3. Combat Helicopter ($200) - Attacks tanks and structures");
            Console.Write("Choose unit type: ");

            string input = Console.ReadLine();
            Unit newUnit = null;

            switch (input)
            {
                case "1":
                    newUnit = new Soldier();
                    break;
                case "2":
                    newUnit = new Tank();
                    break;
                case "3":
                    newUnit = new Helicopter();
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    return;
            }

            if (player.GetUsedCapacity() >= player.GetCapacity())
            {
                Console.WriteLine("Not enough capacity! Build Supply Depots to increase capacity.");
                return;
            }

            if (player.SpendMoney(newUnit.GetCost()))
            {
                player.AddUnit(newUnit);
                gameNodes[0].AddEntity(newUnit);
                Console.WriteLine($"{newUnit.GetName()} recruited successfully!");
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
        }

        private void ShowEnemies()
        {
            Console.WriteLine("\n=== Enemy Forces ===");
            if (enemyUnits.Count == 0)
            {
                Console.WriteLine("No enemy units detected.");
                return;
            }

            for (int i = 0; i < enemyUnits.Count; i++)
            {
                var e = enemyUnits[i];
                Console.WriteLine($"{i + 1}. {e.GetName()} - Health: {e.GetHealth()}");
            }
        }

        private void ShowNodes()
        {
            Console.WriteLine("\n=== Map Status ===");
            for (int i = 0; i < gameNodes.Count; i++)
            {
                var node = gameNodes[i];
                var entities = node.GetEntities();
                Console.WriteLine($"Node {i}: {node.GetNodeType()} - {entities.Count} entities");

                foreach (var entity in entities)
                {
                    string owner = IsPlayerEntity(entity) ? "[PLAYER]" : "[ENEMY]";
                    Console.WriteLine($"  {owner} {entity.GetName()} (HP: {entity.GetHealth()})");
                }
            }
        }

        private void MovePlayerUnitsForward()
        {
            Console.WriteLine("\n=== Move Units Forward ===");
                       
            for (int nodeIndex = 0; nodeIndex < gameNodes.Count - 1; nodeIndex++)
            {
                var currentNode = gameNodes[nodeIndex];
                var nextNode = gameNodes[nodeIndex + 1];
                
                var playerUnits = currentNode.GetEntities()
                    .Where(e => e is Unit && IsPlayerEntity(e))
                    .Cast<Unit>()
                    .OrderByDescending(u => u.GetSpeed()) 
                    .ToList();
                                
                var enemyEntities = currentNode.GetEntities().Where(e => !IsPlayerEntity(e)).ToList();

                if (enemyEntities.Count == 0 && playerUnits.Count > 0)
                {
                    Console.WriteLine($"Moving {playerUnits.Count} units from {currentNode.GetNodeType()} forward...");

                    foreach (var unit in playerUnits.Take(1))
                    {
                        currentNode.RemoveEntity(unit);
                        nextNode.AddEntity(unit);
                        break;
                    }
                }
            }

            Console.WriteLine("Unit movement completed!");
        }

        private void ExecuteStructureActions()
        {
            foreach (var structure in player.GetStructures())
            {
                structure.ExecuteTurnAction(player);
            }
        }

        private void EnemyTurn()
        {
            Console.WriteLine("\n=== Enemy Turn ===");
                        
            var newEnemies = enemyAI.CreateEnemyUnits(enemyUnits);
            enemyUnits.AddRange(newEnemies);
                        
            foreach (var enemy in newEnemies)
            {
                gameNodes[gameNodes.Count - 1].AddEntity(enemy);
            }

            if (newEnemies.Count > 0)
            {
                Console.WriteLine($"Enemy reinforcements: {newEnemies.Count} units created!");
            }
                      
            MoveUnitsAndHandleCombat();
        }

        private void MoveUnitsAndHandleCombat()
        {       
            for (int nodeIndex = gameNodes.Count - 1; nodeIndex >= 0; nodeIndex--)
            {
                var node = gameNodes[nodeIndex];
                var entities = node.GetEntities();
                               
                var playerEntities = entities.Where(IsPlayerEntity).ToList();
                var enemyEntities = entities.Where(e => !IsPlayerEntity(e)).ToList();
                         
                if (playerEntities.Count > 0 && enemyEntities.Count > 0)
                {
                    HandleCombat(node, playerEntities, enemyEntities);
                }
                else if (enemyEntities.Count > 0 && nodeIndex > 0)
                {             
                    MoveEnemyUnits(nodeIndex);
                }

                node.CleanupDeadEntities();
            }

            player.CleanupDeadEntities();
            enemyUnits.RemoveAll(u => !u.IsAlive);
        }

        private void MoveEnemyUnits(int currentNodeIndex)
        {
            var currentNode = gameNodes[currentNodeIndex];
            var nextNode = gameNodes[currentNodeIndex - 1];

            var unitsToMove = currentNode.GetEntities()
                .Where(e => e is Unit && !IsPlayerEntity(e))
                .Cast<Unit>()
                .OrderBy(u => u.GetSpeed())
                .ToList();

            foreach (var unit in unitsToMove)
            {
                currentNode.RemoveEntity(unit);
                nextNode.AddEntity(unit);
            }
        }

        private void HandleCombat(GameNode node, List<GameEntity> playerEntities, List<GameEntity> enemyEntities)
        {
            Console.WriteLine($"Combat at {node.GetNodeType()}!");

            var allCombatants = new List<GameEntity>();
            allCombatants.AddRange(playerEntities);
            allCombatants.AddRange(enemyEntities);
                        
            foreach (var entity in allCombatants.Where(e => e.IsAlive))
            {
                if (entity is Unit attacker)
                {
                    var targets = IsPlayerEntity(entity)
                        ? enemyEntities.Where(e => e.IsAlive && attacker.CanAttack(e))
                         .OrderBy(e => e is Unit ? 1 : e is DefenseStructure ? 2 : 3)
                        : playerEntities.Where(e => e.IsAlive && attacker.CanAttack(e))
                         .OrderBy(e => e is Unit ? 1 : e is DefenseStructure ? 2 : 3);

                    var target = targets.FirstOrDefault();
                    if (target != null)
                    {
                        target.TakeDamage(attacker.GetDamage());
                        Console.WriteLine($"{attacker.GetName()} attacks {target.GetName()} for {attacker.GetDamage()} damage!");

                        if (!target.IsAlive)
                        {
                            Console.WriteLine($"{target.GetName()} destroyed!");
                        }
                    }
                }
                else if (entity is DefenseStructure defenseStruct && IsPlayerEntity(entity))
                {
                    var target = enemyEntities.Where(e => e.IsAlive).FirstOrDefault();
                    if (target != null)
                    {
                        target.TakeDamage(defenseStruct.GetDamage());
                        Console.WriteLine($"{defenseStruct.GetName()} attacks {target.GetName()} for {defenseStruct.GetDamage()} damage!");

                        if (!target.IsAlive)
                        {
                            Console.WriteLine($"{target.GetName()} destroyed!");
                        }
                    }
                }
            }
                        
            playerEntities.RemoveAll(e => !e.IsAlive);
            enemyEntities.RemoveAll(e => !e.IsAlive);
        }

        private bool IsPlayerEntity(GameEntity entity)
        {
            return player.GetStructures().Contains(entity) || player.GetUnits().Contains(entity);
        }

        private void CheckGameState()
        {    
            if (player.HasLost())
            {
                Console.WriteLine($"\n=== GAME OVER ===");
                Console.WriteLine($"You survived {turnCount} turns!");
                Console.WriteLine("All your structures have been destroyed.");
                gameEnded = true;
                return;
            }

            var enemyBase = gameNodes[gameNodes.Count - 1];
            var playerUnitsAtEnemyBase = enemyBase.GetEntities()
                .Where(e => e is Unit && IsPlayerEntity(e))
                .ToList();

            if (playerUnitsAtEnemyBase.Count > 0)
            {
                Console.WriteLine($"\n=== VICTORY ===");
                Console.WriteLine($"You conquered the enemy base in {turnCount} turns!");
                gameEnded = true;
            }
        }
    }

}
