using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace BFECore.Graphics
{
    public class Animation : Sprite
    {
        public Animation(Texture2D spriteTexture, int width, int height)
            : base(spriteTexture)
        {
            m_raFrames = new ArrayList();
            Center = new Vector2(width / 2, height / 2);
        }

        public ArrayList m_raFrames;
        public int m_dwFramesPerSecond = 12;

        public override Rectangle CurrentFrame(int frameIndex)
        {
            if (m_raFrames.Count > frameIndex)
                return (Rectangle)m_raFrames[frameIndex];
            else
                return (Rectangle)m_raFrames[0];
        }
   }
}
