using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BFECore.Graphics
{
    public class SpriteBatchEX : SpriteBatch
    {
        private Texture2D pixel;

        public SpriteBatchEX(GraphicsDevice graphics) : base(graphics)
        {
            pixel = new Texture2D(graphics, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] pixels = new Color[1];
            pixels[0] = Color.White;
            pixel.SetData<Color>(pixels);
        }

        public void DrawLine(Vector2 StartPoint, Vector2 EndPoint, Color lineColor)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(StartPoint, EndPoint);

            // calculate the angle between the two vectors
            float angle = (float)Math.Atan2((double)(EndPoint.Y - StartPoint.Y),

            (double)(EndPoint.X - StartPoint.X));

            // stretch the pixel between the two vectors
            Draw(pixel,
                StartPoint,
                null,
                lineColor,
                angle,
                Vector2.Zero,
                new Vector2(distance, 1),
                SpriteEffects.None,
                0.0f);
        }

        public void DrawBox(Rectangle destRect, Color boxColor)
        {
            Draw(pixel,
                destRect,
                boxColor);
        }
    }
}
