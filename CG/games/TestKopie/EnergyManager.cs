using DMS.Geometry;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpaceInvaders
{
    class EnergyManager
    {
        public List<Box2D> energies = new List<Box2D>();
        public float SpawnRate = 9f;
        private float delta = 0f;
        private Random ran = new Random(2000);

        public EnergyManager()
        {
            SpawnEnergy(new Vector2(1f, 0.7f));
        }
        public void SpawnEnergy(Vector2 pos)
        {
            energies.Add(new Box2D(pos.X, pos.Y, 0.1f, 0.1f));
        }
        public void MoveEnergies(float timeDelta)
        {
            foreach (Box2D energy in energies)
            {
                energy.X -= timeDelta * 0.8f;
            }
        }
        public void RemoveEnergy(Box2D energy)
        {
            if (energies.Contains(energy)) energies.Remove(energy);
        }
        public void Update(float deltaTime)
        {
            delta += deltaTime;

            if (delta >= SpawnRate)
            {
                Random rand = new Random();
                var random = (float)(rand.NextDouble() * 1.7 - 1.0);
                SpawnEnergy(new Vector2(1f, random));
                delta = 0f;
            }
        }
    }
}
