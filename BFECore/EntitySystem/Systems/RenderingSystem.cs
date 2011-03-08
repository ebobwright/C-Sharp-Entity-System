using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BFECore.Graphics;
namespace BFECore.EntitySystem.Systems
{
    public class RenderingSystem : BaseSystem 
    {
        public static BFECore.Graphics.Camera2D camera;

        private SpriteBatchEX m_spriteBatch;
        private GraphicsDevice m_graphics;        
        
        public RenderingSystem(BaseEntitySystem bes, GraphicsDevice graphics)
            : base(bes)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatchEX(graphics);
            camera = new Camera2D(graphics);            
        }

        public Effect filterEffect;

        public void Draw(GameTime gameTime)
        {            
            /* original draw method from before the entity system            
            Vector2 screenPosition = gob.Position + (gob.Center * (gob.CompositeScale)) - (Position * gob.Depth);
            screenPosition -= Center;
            screenPosition *= gob.Depth * Depth;
            screenPosition += Center;

            spriteBatch.Draw(gob.Texture, screenPosition, gob.CurrentFrame, gob.Tint, gob.Rotation, gob.Center, Depth * gob.CompositeScale, gob.SpriteEffects, gob.Depth);        
            */


            //EffectPass pass;

            //filterEffect.Parameters["burn"].SetValue(.15f);

            //filterEffect.Parameters["saturation"].SetValue(1.0f);
            //filterEffect.Parameters["r"].SetValue(1.00f);
            //filterEffect.Parameters["g"].SetValue(1.00f);
            //filterEffect.Parameters["b"].SetValue(1.00f);
            //filterEffect.Parameters["brite"].SetValue(0.15f);

            //filterEffect.Begin();
                                            
            m_spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            m_graphics.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            m_graphics.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

            //pass = filterEffect.CurrentTechnique.Passes[0];
            //pass.Begin();

            #region Render Game Objects

            List<Entity> renderableEntities = es.GetAllEntitiesPossesing(typeof(Position), typeof(Renderable));
            
            //Filter List before sorting
            List<Entity> entitiesToDraw = new List<Entity>();
            foreach (Entity renderableEntity in renderableEntities)
            {
                Position ePos = renderableEntity.getAs<Position>();
                if (ePos.RoomIndex == camera.RoomIndex)
                {
                    //TODO: Filter based on location as well
                    entitiesToDraw.Add(renderableEntity);
                }
            }

            //Sort the filtered list
            entitiesToDraw.Sort(new BFECore.EntitySystem.UtilityClasses.PositionSort());

            //Second pass, actually draw everything.
            foreach (Entity renderableEntity in entitiesToDraw)
            {
                Position ePos = renderableEntity.getAs<Position>();                
                Renderable eRend = renderableEntity.getAs<Renderable>();

                //int GraphicIndex = -1;                    
                //if(eRend.animationLibrary.Count > (int)eRend.State )
                //    GraphicIndex = eRend.animationLibrary[(int)eRend.State];

                if (!string.IsNullOrEmpty(eRend.image))
                {
                    Sprite sprite = GraphicsLibrary.GraphicObjects[eRend.image];

                    if (sprite is Animation)
                    {
                        DateTime dtNow = DateTime.Now;
                        TimeSpan ts = dtNow - eRend.lastFrameUpdate;

                        if (ts.TotalMilliseconds > 1000 / ((Animation)sprite).m_dwFramesPerSecond)
                        {
                            eRend.CurrentFrameIndex++;
                            eRend.lastFrameUpdate = dtNow;
                        }

                        if (eRend.CurrentFrameIndex >= ((Animation)sprite).m_raFrames.Count)
                            eRend.CurrentFrameIndex = 0;
                    }

                    Vector2 screenPosition = camera.ScreenCoordsFromWorldPosition(ePos.EntityPosition + eRend.Offset);
                    if (eRend.Vertical)
                    {
                        screenPosition.Y -= sprite.Center.Y * 2 * eRend.Scale.Y;
                    }
                    screenPosition.X -= sprite.Center.X * eRend.Scale.X;
                    screenPosition.Y -= ePos.height;

                    if (eRend.Wrap)
                    {
                        Rectangle sourcePosition = sprite.CurrentFrame(eRend.CurrentFrameIndex);
                        int xRepeat = (int)(sprite.Center.X * 2 * eRend.Scale.X) / sourcePosition.Width;
                        int yRepeat = (int)(sprite.Center.Y * 2 * eRend.Scale.Y) / sourcePosition.Height;

                        for (int ix = 0; ix < xRepeat; ix++)
                        {
                            for (int iy = 0; iy < yRepeat; iy++)
                            {
                                m_spriteBatch.Draw(sprite.Texture, screenPosition + new Vector2(ix * sourcePosition.Width, iy * sourcePosition.Height), sourcePosition, eRend.Tint, eRend.Rotation, Vector2.Zero, 1.0f, eRend.HorizantalFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                            }
                        }
                    }
                    else
                    {
                        screenPosition += sprite.Center * eRend.Scale;
                        m_spriteBatch.Draw(sprite.Texture, screenPosition, sprite.CurrentFrame(eRend.CurrentFrameIndex), eRend.Tint, eRend.Rotation, sprite.Center, eRend.Scale, eRend.HorizantalFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    }

                    if (es.EditMode)
                    {
                        Color highlight = Color.Gray;

                        if (renderableEntity == EditingSystem.selectedEntity)
                            highlight = Color.Yellow;

                        Rectangle boundRect = camera.EntityBoundaries(renderableEntity);
                        Vector2 topLeft = new Vector2(boundRect.Left, boundRect.Top);
                        Vector2 topRight = new Vector2(boundRect.Right, boundRect.Top);
                        Vector2 bottomLeft = new Vector2(boundRect.Left, boundRect.Bottom);
                        Vector2 bottomRight = new Vector2(boundRect.Right, boundRect.Bottom);

                        m_spriteBatch.DrawLine(topLeft, bottomRight, highlight);
                        m_spriteBatch.DrawLine(topLeft, topRight, highlight);
                        m_spriteBatch.DrawLine(topLeft, bottomLeft, highlight);
                        m_spriteBatch.DrawLine(bottomLeft, bottomRight, highlight);
                        m_spriteBatch.DrawLine(topRight, bottomRight, highlight);

                        Vector2 entPos = camera.ScreenCoordsFromWorldPosition(ePos.EntityPosition);
                        Rectangle entRect = new Rectangle((int)(entPos.X - 5), (int)(entPos.Y - 5), 10, 10);
                        m_spriteBatch.DrawBox(entRect, highlight);
                    }
                }                
            }

            #endregion

            #region Render Editor Objects

            if (es.EditMode)
            {

                List<Entity> Walls = es.GetAllEntitiesPossesing(typeof(Wall));


                foreach (Entity wall in Walls)
                {
                    Wall wallComp = wall.getAs<Wall>();
                    Position wallPos = wall.getAs<Position>();

                    if (wallPos.RoomIndex == camera.RoomIndex)
                    {                        
                        if (wallComp.WallPoints != null && wallComp.WallPoints.Length > 1)
                        {
                            Color vertexColor = Color.Red;

                            Vector2 rectPos = camera.ScreenCoordsFromWorldPosition(wallComp.WallPoints[0] + wallPos.EntityPosition);
                            Rectangle entRect = new Rectangle((int)(rectPos.X - 5), (int)(rectPos.Y - 5), 10, 10);

                            if (0 == EditingSystem.selectedVertex)
                                vertexColor = Color.Yellow;
                            else
                                vertexColor = Color.Red;

                            m_spriteBatch.DrawBox(entRect, vertexColor);

                            for (int i = 0; i < wallComp.WallPoints.Length - 1; i++)
                            {
                                Vector2 StartPoint = wallComp.WallPoints[i] + wallPos.EntityPosition;
                                Vector2 EndPoint = wallComp.WallPoints[i + 1] + wallPos.EntityPosition;
                                m_spriteBatch.DrawLine(camera.ScreenCoordsFromWorldPosition(StartPoint), camera.ScreenCoordsFromWorldPosition(EndPoint), Color.Red);

                                rectPos = camera.ScreenCoordsFromWorldPosition(EndPoint);
                                entRect = new Rectangle((int)(rectPos.X - 5), (int)(rectPos.Y - 5), 10, 10);

                                if ((i + 1) == EditingSystem.selectedVertex)
                                    vertexColor = Color.Yellow;
                                else
                                    vertexColor = Color.Red;
                                m_spriteBatch.DrawBox(entRect, vertexColor);
                            }
                        }
                    }
                }

            }
            #endregion

            //pass.End();
            m_spriteBatch.End();
            //filterEffect.End();
        }
    }
}
