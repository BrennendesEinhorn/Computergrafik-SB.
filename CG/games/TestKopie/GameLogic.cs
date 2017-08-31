using DMS.Geometry;
using DMS.TimeTools;
using MvcSpaceInvaders;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class GameLogic
    {
        private Box2D windowBorders = new Box2D(-1.0f, -1.0f, 2.0f, 1.7f);
        public Box2D player = new Box2D(-1.0f, 0.0f, 0.1f, 0.05f);
        public List<Box2D> bullets = new List<Box2D>();
        public ObstacleManager obstacleManager = new ObstacleManager();
        public EnemyManager enemyManager = new EnemyManager();
        public EnergyManager energyManager = new EnergyManager();
        private PeriodicUpdate shootCoolDown = new PeriodicUpdate(0.5f);
        private Stopwatch timeSource = new Stopwatch();
        public bool Lost;
        private int lifeCounter = 3;
        public List<Box2D> lives = new List<Box2D>();
        private float lifeXPos = -0.9f;

        public GameLogic()
        {
            shootCoolDown.PeriodElapsed += (s, t) => shootCoolDown.Stop();
            timeSource.Start();
            //CreateLives();

        }
        public void Update(float timeDelta)
        {
            obstacleManager.Update(timeDelta);
            energyManager.Update(timeDelta);
            enemyManager.Update(timeDelta);
        }
        public void Update(float axisUpDown, float axisLeftRight, float timeDelta, bool shoot)
        {
            shootCoolDown.Update((float)timeSource.Elapsed.TotalSeconds);
            if (Lost == true)
            {
                //timeDelta = 0f;
                return;
            } 
            //remove outside bullet
            foreach (Box2D bullet in bullets)
            {
                if (bullet.X > windowBorders.MaxX)
                {
                    bullets.Remove(bullet);
                    return;
                }
            }

            HandleCollisions();
            UpdatePlayer(timeDelta, axisUpDown, axisLeftRight, shoot);
            enemyManager.MoveEnemies(timeDelta);
            obstacleManager.MoveObstacles(timeDelta);
            energyManager.MoveEnergies(timeDelta);
            MoveBullets(timeDelta);
            Update(timeDelta);
            lifeManager(timeDelta);


        }

        private void HandleCollisions()
        {
            foreach (Box2D enemy in enemyManager.enemies)
            {
                if (enemy.X + enemy.SizeX < windowBorders.X)
                {
                    enemyManager.enemies.Remove(enemy);
                    return;
                }
                foreach (Box2D bullet in bullets)
                {
                    if (bullet.Intersects(enemy))
                    {
                        bullets.Remove(bullet);
                        enemyManager.enemies.Remove(enemy);
                        //need to return immediatly beause we change list
                        return;
                    }
                }
                if (enemy.Intersects(player))
                {
                    enemyManager.enemies.Remove(enemy);
                    lifeCounter_decrement();
                    return;
                }
            }
            foreach (Box2D obstacle in obstacleManager.obstacles)
            {
                if (player.Intersects(obstacle))
                {
                    lifeCounter_decrement();
                    //lives.Remove(lives.Last());
                    //ManageLifes();_________________________________________________________________________________
                    obstacleManager.RemoveObstacle(obstacle);
                    return;
                }
                //remove outside obstacles
                if (obstacle.X + obstacle.SizeX < windowBorders.X)
                {
                    obstacleManager.RemoveObstacle(obstacle);
                    return;
                }

                foreach (Box2D bullet in bullets)
                {
                    if (bullet.Intersects(obstacle))
                    {
                        bullets.Remove(bullet);
                        return;
                    }
                }
            } 
            foreach (Box2D energy in energyManager.energies)
            {
                if (player.Intersects(energy))
                {
                    energyManager.RemoveEnergy(energy);
                    lifeCounter_increase();
                    return;
                }
                //remove outside energíes
                else if (energy.X + energy.SizeX < windowBorders.X)
                {
                    energyManager.RemoveEnergy(energy);
                    return;
                }
            }
        }

        private void UpdatePlayer(float timeDelta, float axisUpDown, float axisLeftRight, bool shoot)
        {
            player.Y += timeDelta * axisUpDown;
            player.X += timeDelta * axisLeftRight;

            player.PushYRangeInside(windowBorders);
            player.PushXRangeInside(windowBorders);

            if (shoot && !shootCoolDown.Enabled)
            {
                bullets.Add(new Box2D(player.X, player.Y, 0.09f, 0.02f));
                bullets.Add(new Box2D(player.MaxX + 0.1f, player.Y, 0.09f, 0.02f));
                shootCoolDown.Start((float)timeSource.Elapsed.TotalSeconds);
            }
        }
        /*private void CreateLives()
        {
            lives.Add(new Box2D(-0.9f, 0.85f, 0.1f, 0.1f));
            lives.Add(new Box2D(-0.7f, 0.85f, 0.1f, 0.1f));
            lives.Add(new Box2D(-0.5f, 0.85f, 0.1f, 0.1f));

        }*/
        private void lifeManager(float timeDelta)
        {
            //lives.Clear();
            for (int i = 1; i <= 3; i++)
            {
                lives.Add(new Box2D(lifeXPos, 0.85f, 0.1f, 0.1f));
                lifeXPos = lifeXPos + 0.2f;
                return;
            }
        }

        /*private void AddLife()
        {
            if (lives.Count() == 1)
            {
                lives.Add(new Box2D(-0.7f, 0.85f, 0.1f, 0.1f));
            }
            else if (lives.Count() == 2)
            {
                lives.Add(new Box2D(-0.5f, 0.85f, 0.1f, 0.1f));
            }
        }*/
        public void lifeCounter_decrement()
        {
            if (lifeCounter <= 1)
            {
                Lost = true;
            }
            else
            {
                lifeCounter--;
            }
           // ManageLifes(timeDelta);
        }
        public void lifeCounter_increase()
        {
            if (lifeCounter < 3)
            {
                lifeCounter++;
            }
            //ManageLifes();
        }

        private void MoveBullets(float timeDelta)
        {
            foreach (Box2D bullet in bullets)
            {
                bullet.X += timeDelta;
            }
        }
    }

}
