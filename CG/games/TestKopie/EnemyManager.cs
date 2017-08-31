using DMS.Geometry;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpaceInvaders
{
    class EnemyManager
    {
        private float enemySpeed = 0.35f;
        public List<Box2D> enemies = new List<Box2D>();
        public float SpawnRate = 5f;
        private float delta = 0f;
        private Random ran = new Random(2000);

        public EnemyManager()
        {
            SpawnEnemy(new Vector2(1f, 0.6f));
        }
        public void SpawnEnemy(Vector2 pos)
        {
            enemies.Add(new Box2D(pos.X, pos.Y, 0.22f, 0.2f));
        }
        public void MoveEnemies(float timeDelta)
        {
            foreach (Box2D enemy in enemies)
            {
                enemy.X -= enemySpeed * timeDelta;
            }
        }
        public void Update(float deltaTime)
        {
            delta += deltaTime;

            if (delta >= SpawnRate)
            {
                Random rand = new Random();
                var random = (float)(rand.NextDouble() * 1.6 - 1.0);
                SpawnEnemy(new Vector2(1f, random));
                delta = 0f;

                if (SpawnRate >= 2f)
                {
                    SpawnRate = SpawnRate - 0.5f;
                }
            }
        }
    }
}
