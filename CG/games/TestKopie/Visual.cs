using DMS.Geometry;
using DMS.OpenGL;
using MvcSpaceInvaders;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Visual
    {
        private static Texture texBackground;
        private static Texture texPlayer;
        private static Texture texEnemy;
        private static Texture texEnergy;
        private static Texture texObstacle;
        private static Texture texLife;
        private static Texture texBullet;
        private static Texture texEndscreen;
        private static Box2D texCoord = new Box2D(0, 0, 0.3f, 1);

        public static void LoadTextures()
        {
            texBackground = TextureLoader.FromBitmap(Resourcen.endscreen);
            texPlayer = TextureLoader.FromBitmap(Resourcen.forest);
            texEnemy = TextureLoader.FromBitmap(Resourcen.ufo_rot); 
            texEnergy = TextureLoader.FromBitmap(Resourcen.energy3);
            texObstacle = TextureLoader.FromBitmap(Resourcen.meteor_kurz);
            texLife = TextureLoader.FromBitmap(Resourcen.energy3);
            texBullet = TextureLoader.FromBitmap(Resourcen.laser1);
            texBackground.WrapMode(TextureWrapMode.MirroredRepeat);
            texEndscreen = TextureLoader.FromBitmap(Resourcen.endscreen1);
        }

        public void Update(float timeDelta)
        {
            texCoord.X += timeDelta * 0.03f;
        }

        public static void Render(List<Box2D> enemies, List<Box2D> bullets, List<Box2D> obstacles, List<Box2D> energies, Box2D player, List<Box2D> lives, bool Lost)
        {
            texBackground.WrapMode(TextureWrapMode.MirroredRepeat);
            GL.ClearColor(Color.White);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Color3(Color.White);
            texBackground.Activate();
            DrawTools.DrawTexturedRect(new Box2D(-1, -1, 2, 2), texCoord);
            texBackground.Deactivate();

         
            foreach (Box2D enemy in enemies)
            {
                DrawTexturedRect(enemy, texEnemy);
            }
            foreach (Box2D bullet in bullets)
            {
                DrawTexturedRect(bullet, texBullet);
            }
            foreach (Box2D obstacle in obstacles)
            {
                DrawTexturedRect(obstacle, texObstacle);
            }
            foreach (Box2D energy in energies)
            {
                DrawTexturedRect(energy, texEnergy);
            }
            foreach (Box2D life in lives)
            {
                DrawTexturedRect(life, texLife);
            }
            DrawTexturedRect(player, texPlayer);
            if (Lost == true)
            {
                DrawTexturedRect(new Box2D(-1, -1, 2, 2), texEndscreen);
            }
        }

        private static void DrawTexturedRect(Box2D Rect, Texture tex)
        {
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //the texture has to be enabled before use
            tex.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command
            
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rect.X, Rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rect.MaxX, Rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rect.MaxX, Rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rect.X, Rect.MaxY);
            GL.End();
            //the texture is disabled, so no other draw calls use this texture
            tex.Deactivate();
        }
        
    }
}
