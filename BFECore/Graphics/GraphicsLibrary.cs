using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BFECore.Graphics
{
    public class GraphicsLibrary
    {
        public static void LoadGraphicsObjects(ContentManager Content)
        {
            GraphicObjects = new Dictionary<string, Sprite>();
            
            XmlDocument xdPartsList = new System.Xml.XmlDocument();
            xdPartsList.Load("parts.xml");
            
            XmlNodeList xnlParts = xdPartsList.SelectNodes("//parts/part");


            foreach (XmlNode xnPart in xnlParts)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D image = Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(xnPart.Attributes["image"].InnerText);
                string name = xnPart.Attributes["name"].InnerText;
                int width = int.Parse(xnPart.Attributes["width"].InnerText);
                int height = int.Parse(xnPart.Attributes["height"].InnerText);

                switch (xnPart.Attributes["type"].InnerText)
                {
                    case "sprite":
                        int x = int.Parse(xnPart.Attributes["x"].InnerText);
                        int y = int.Parse(xnPart.Attributes["y"].InnerText);
                        BFECore.Graphics.Sprite sprite = new Sprite(image, x, y, width, height);
                        GraphicObjects.Add(name, sprite);
                        break;

                    case "animation":
                        BFECore.Graphics.Animation anim = new BFECore.Graphics.Animation(image, width, height);
                        anim.m_dwFramesPerSecond = int.Parse(xnPart.Attributes["framerate"].InnerText);
                        XmlNodeList frames = xnPart.SelectNodes("frame");

                        foreach (XmlNode frame in frames)
                        {
                            int fx = int.Parse(frame.Attributes["x"].InnerText);
                            int fy = int.Parse(frame.Attributes["y"].InnerText);
                            anim.m_raFrames.Add(new Rectangle(fx, fy, width, height));
                        }

                        GraphicObjects.Add(name, anim);
                        break;
                }
            }
        }

        public static Dictionary<string, Sprite> GraphicObjects;
    }
}
