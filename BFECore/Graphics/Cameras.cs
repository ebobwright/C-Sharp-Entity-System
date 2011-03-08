using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BFECore.EntitySystem;

namespace BFECore.Graphics
{
    public class Camera2D
    {
        public Vector2 Center;
        public Vector2 Position;
        public int RoomIndex = 0;
        
        public Camera2D(GraphicsDevice graphics)
        {
            Center = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            Position = Vector2.Zero;       
        }

        public Vector2 RealCoordsFromScreen(int x, int y)
        {
            Vector2 realCoords = new Vector2();
            realCoords = new Vector2(x, y) + Position - Center;
            return realCoords;
        }

        public Vector2 ScreenCoordsFromWorldPosition(Vector2 EntityPosition)
        {
            Vector2 screenPosition = EntityPosition - Position + Center;
            return screenPosition;
        }

        public Rectangle EntityBoundaries(BFECore.EntitySystem.Entity entity)
        {
            Rectangle entRect = new Rectangle();

            Position entPos = entity.getAs<Position>();
            Renderable entRend = entity.getAs<Renderable>();
            if ( !string.IsNullOrEmpty(entRend.image) )
            {
                Sprite sprite = GraphicsLibrary.GraphicObjects[ entRend.image ];

                Vector2 screenPosition = ScreenCoordsFromWorldPosition(entPos.EntityPosition + entRend.Offset);
                if (entRend.Vertical)
                {
                    screenPosition.Y -= sprite.Center.Y * 2 * entRend.Scale.Y;
                }

                entRect.X = (int)(screenPosition.X - sprite.Center.X * entRend.Scale.X);
                entRect.Y = (int)screenPosition.Y;
                entRect.Width = (int)(sprite.Center.X * 2 * entRend.Scale.X);
                entRect.Height = (int)(sprite.Center.Y * 2 * entRend.Scale.Y);
            }
            return entRect;
        }
    }
}
