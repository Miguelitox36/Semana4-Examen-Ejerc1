using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana4_Examen_Ejerc1
{
    public class EnemyAI
    {
        private readonly Random random;
        private int turnCount;
        private readonly List<int> fibonacciSequence;
        private int fibonacciIndex;

        public EnemyAI()
        {
            random = new Random();
            turnCount = 0;
            fibonacciSequence = new List<int> { 0, 1 };
            fibonacciIndex = 0;
        }

        public List<Unit> CreateEnemyUnits(List<Unit> existingEnemyUnits)
        {
            turnCount++;

            if (existingEnemyUnits.Count > 0)
            {
                return new List<Unit>();
            }

            while (fibonacciIndex >= fibonacciSequence.Count)
            {
                int nextFib = fibonacciSequence[fibonacciSequence.Count - 1] +
                             fibonacciSequence[fibonacciSequence.Count - 2];
                fibonacciSequence.Add(nextFib);
            }

            int unitsToCreate = fibonacciSequence[fibonacciIndex];
            fibonacciIndex++;

            var units = new List<Unit>();

            for (int i = 0; i < unitsToCreate; i++)
            {
                units.Add(CreateRandomUnit());
            }

            return units;
        }

        private Unit CreateRandomUnit()
        {
            double soldierProbability = turnCount <= 5 ? 0.5 : 0.33;
            double tankProbability = turnCount <= 5 ? 0.35 : 0.33;           

            double roll = random.NextDouble();

            if (roll < soldierProbability)
                return new Soldier();
            else if (roll < soldierProbability + tankProbability)
                return new Tank();
            else
                return new Helicopter();
        }
    }

}
