using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BFECore.EntitySystem.Systems
{
    public class EditingSystem : BaseSystem
    {
        public EditingSystem(BaseEntitySystem bes) : base(bes) { }

        public static Entity selectedEntity;
        private Entity draggedEntity;
        private Vector2 MouseOffset;

        public static int selectedVertex;
        private int draggedVertex = -1;

        public void Update(GameTime gameTime)
        {
            if (es.EditMode)
            {
                if (es.EditType == EditorType.EntityMode)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (draggedEntity != null)
                        {
                            draggedEntity.getAs<Position>().EntityPosition = RenderingSystem.camera.RealCoordsFromScreen(Mouse.GetState().X, Mouse.GetState().Y) - MouseOffset;
                            draggedEntity.getAs<Position>().EntityPosition -= new Vector2(draggedEntity.getAs<Position>().EntityPosition.X % (int)es.GridType, draggedEntity.getAs<Position>().EntityPosition.Y % (int)es.GridType);

                            //draggedEntity.getAs<Position>().EntityPosition = RenderingSystem.camera.RealCoordsFromScreen(Mouse.GetState().X, Mouse.GetState().Y) - MouseOffset;
                        }
                        else
                        {
                            draggedEntity = TestForTargetedDrawable(Mouse.GetState().X, Mouse.GetState().Y);
                            if (draggedEntity != null)
                            {
                                MouseOffset = RenderingSystem.camera.RealCoordsFromScreen(Mouse.GetState().X, Mouse.GetState().Y) - draggedEntity.getAs<Position>().EntityPosition;
                            }
                        }

                        selectedEntity = TestForTargetedDrawable(Mouse.GetState().X, Mouse.GetState().Y);
                    }
                    else
                    {
                        draggedEntity = null;
                    }
                }
                else if (es.EditType == EditorType.WallMode)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        System.Threading.Thread.Sleep(100);

                        if (selectedEntity == null)
                        {
                            selectedEntity = new Entity(es.CreateEntityID());
                        }

                        Wall entWall = selectedEntity.getAs<Wall>();

                        if (entWall == null)
                        {
                            entWall = new Wall();
                            es.RegisterComponent(selectedEntity, entWall);
                        }

                        System.Collections.ArrayList wallPoints = new System.Collections.ArrayList();
                        if (entWall.WallPoints != null)
                        {
                            wallPoints.AddRange(entWall.WallPoints);
                        }

                        if (wallPoints.Count == 0)
                        {
                            wallPoints.Add(new Vector2(0, 0));
                        }
                        Vector2 lastPoint = (Vector2)wallPoints[wallPoints.Count - 1];
                        wallPoints.Add(new Vector2(lastPoint.X + 30, lastPoint.Y + 30));

                        entWall.WallPoints = (Vector2[])wallPoints.ToArray(typeof(Vector2));
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        if (selectedEntity != null)
                        {
                            Wall entWall = selectedEntity.getAs<Wall>();

                            if (entWall != null)
                            {
                                if (entWall.WallPoints != null)
                                {
                                    System.Collections.ArrayList wallPoints = new System.Collections.ArrayList();
                                    wallPoints.AddRange(entWall.WallPoints);

                                    for (int i = 0; i < wallPoints.Count; i++)
                                    {
                                        if (selectedVertex == i)
                                        {
                                            wallPoints.RemoveAt(i);
                                            break;
                                        }
                                    }

                                    entWall.WallPoints = (Vector2[])wallPoints.ToArray(typeof(Vector2));
                                }
                            }
                        }
                    }

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (selectedEntity != null)
                        {
                            Wall entWall = selectedEntity.getAs<Wall>();
                            if (entWall != null)
                            {
                                if (draggedVertex != -1)
                                {
                                    entWall.WallPoints[draggedVertex] = RenderingSystem.camera.RealCoordsFromScreen(Mouse.GetState().X, Mouse.GetState().Y) - selectedEntity.getAs<Position>().EntityPosition;
                                }
                                else
                                {
                                    for (int i = 0; i < entWall.WallPoints.Length; i++)// (Vector2 wallPoint in entWall.WallPoints)
                                    {
                                        Vector2 rectPos = RenderingSystem.camera.ScreenCoordsFromWorldPosition(entWall.WallPoints[i] + selectedEntity.getAs<Position>().EntityPosition);
                                        Rectangle entRect = new Rectangle((int)(rectPos.X - 15), (int)(rectPos.Y - 15), 30, 30);

                                        if (entRect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                                        {
                                            draggedVertex = i;
                                        }
                                    }
                                }

                                for (int i = 0; i < entWall.WallPoints.Length; i++)// (Vector2 wallPoint in entWall.WallPoints)
                                {
                                    Vector2 rectPos = RenderingSystem.camera.ScreenCoordsFromWorldPosition(entWall.WallPoints[i] + selectedEntity.getAs<Position>().EntityPosition);
                                    Rectangle entRect = new Rectangle((int)(rectPos.X - 15), (int)(rectPos.Y - 15), 30, 30);

                                    if (entRect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                                    {
                                        selectedVertex = i;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        draggedVertex = -1;
                    }
                }
            }
        }

        public Entity TestForTargetedDrawable(int X, int Y)
        {
            List<Entity> selectedEntities = new List<Entity>();
            Entity selectedEntity = null;
            List<Entity> targetables = es.GetAllEntitiesPossesing(typeof(Position), typeof(Renderable));
            foreach (Entity target in targetables)
            {
                Rectangle newRect = RenderingSystem.camera.EntityBoundaries(target);
                if (newRect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    selectedEntities.Add(target);
                }
            }

            selectedEntities.Sort(new BFECore.EntitySystem.UtilityClasses.PositionSort());

            if (selectedEntities.Count > 0)
            {
                selectedEntity = selectedEntities[selectedEntities.Count - 1];
            }

            return selectedEntity;
        }
    }
}
