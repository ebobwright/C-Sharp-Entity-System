using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BFECore.Graphics
{
    public class Sprite
    {
        public Sprite(Texture2D spriteTexture)
        {
            texture = spriteTexture;

            Center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
            m_FrameRect = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
        }

        public Sprite(Texture2D spriteTexture, int x, int y, int width, int height)
        {            
            texture = spriteTexture;

            Center = new Vector2(width / 2, height / 2);
            m_FrameRect = new Rectangle(x, y, width, height);
        }       

        public Vector2 Center;

        protected Texture2D texture = null;
        protected Rectangle m_FrameRect;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public virtual Rectangle CurrentFrame(int frameIndex)
        {
            return m_FrameRect;
        }
    }
}
