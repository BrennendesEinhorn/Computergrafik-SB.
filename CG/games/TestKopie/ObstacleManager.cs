using DMS.Geometry;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpaceInvaders
{
    class ObstacleManager
    {
        public List<Box2D> obstacles = new List<Box2D>();
        private float obstacleSpeed = 0.3f;
        public float SpawnRate = 2f;
        private float delta = 0f;
        private Random ran = new Random(2000);

        public void SpawnObstacle(Vector2 pos)
        {
            obstacles.Add(new Box2D(pos.X, pos.Y, 0.4f, 0.18f));
        }
        public void RemoveObstacle(Box2D obstacle)
        {
            if (obstacles.Contains(obstacle)) obstacles.Remove(obstacle);
        }
        public void MoveObstacles(float timeDelta)
        {
            foreach (Box2D obstacle in obstacles)
            {
                obstacle.X -= timeDelta * obstacleSpeed;
            }
        }
        public void Update(float deltaTime)
        {
            delta += deltaTime;

            if(delta>=SpawnRate)
            {
                Random rand = new Random();
                var random = (float)(rand.NextDouble() * 1.5 - 1.0);
                SpawnObstacle(new Vector2(1f, random));
                delta = 0f;

                if (SpawnRate >= 2f)
                {
                    SpawnRate = SpawnRate - 0.06f;
                }
            }
        }
    }
}
