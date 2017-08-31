using DMS.TimeTools;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using DMS.OpenGL;
using DMS.Application;
using OpenTK;

namespace SpaceInvaders
{
    class MyWindow
    {
        private GameLogic gameLogic = new GameLogic();
        private Visual visual = new Visual(); 
        //private Sound sound = new Sound();
       
        public MyWindow()
        {
            Visual.LoadTextures();
        }
        public void Update(float updatePeriod)
        {
             /*if (gameLogic.Lost)
             {
                 return;
             }*/

            float axisUpDown = Keyboard.GetState()[Key.Down] ? -1.0f : Keyboard.GetState()[Key.Up] ? 1.0f : 0.0f;
            float axisLeftRight = Keyboard.GetState()[Key.Left] ? -1.0f : Keyboard.GetState()[Key.Right] ? 1.0f : 0.0f;
            bool shoot = Keyboard.GetState()[Key.Space];

            gameLogic.Update(axisUpDown, axisLeftRight, updatePeriod, shoot);
            visual.Update(updatePeriod);
        }

        public void Render()
        {
            Visual.Render(gameLogic.enemyManager.enemies, gameLogic.bullets, gameLogic.obstacleManager.obstacles, gameLogic.energyManager.energies, gameLogic.player, gameLogic.lives, gameLogic.Lost);
        }

        [STAThread]
        public static void Main()
        {
            var app = new ExampleApplication(1000, 1000);
            var window = new MyWindow();
            app.Render += window.Render;
            app.Update += window.Update;

            app.Run();
        }




    }  
}
