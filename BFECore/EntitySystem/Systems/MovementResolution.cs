using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem.Systems
{
    public class MovementResolution : BaseSystem
    {
        public MovementResolution(BaseEntitySystem bes) : base(bes) { }

        public void Update(GameTime gameTime)
        {
            List<Entity> movables = es.GetAllEntitiesPossesing(typeof(Position), typeof(Movement), typeof(Solid));
            List<Entity> walls = es.GetAllEntitiesPossesing(typeof(Wall));

            foreach (Entity mover in movables)
            {
                if (mover.getAs<Movement>().DeltaVector != Vector2.Zero)
                {
                    foreach (Entity wall in walls)
                    {
                        if (wall != mover)
                        {
                            Wall wallComp = wall.getAs<Wall>();
                            Position wallPos = wall.getAs<Position>();
                            Solid moverSolid = mover.getAs<Solid>();

                            if (wallPos.RoomIndex == mover.getAs<Position>().RoomIndex)
                            {
                                if (wallComp.WallPoints != null && wallComp.WallPoints.Length > 1)
                                {
                                    for (int i = 0; i < wallComp.WallPoints.Length - 1; i++)
                                    {
                                        Vector2 positionVector = mover.getAs<Position>().EntityPosition + mover.getAs<Movement>().DeltaVector;
                                        Vector2 StartPoint = wallComp.WallPoints[i] + wallPos.EntityPosition;
                                        Vector2 EndPoint = wallComp.WallPoints[i + 1] + wallPos.EntityPosition;

                                        //get the normalized line segment vector
                                        Vector2 v = EndPoint - StartPoint;
                                        v.Normalize();

                                        //determine the point on the line segment nearest to the point p
                                        float distanceAlongLine = Vector2.Dot(positionVector, v) - Vector2.Dot(StartPoint, v);
                                        Vector2 nearestPoint;
                                        Vector2 originVector;
                                        if (distanceAlongLine < 0)
                                        {
                                            //closest point is A
                                            nearestPoint = StartPoint;

                                            //Calculate the distance between the two points            
                                            float actualDistance = Vector2.Distance(nearestPoint, positionVector);

                                            if (actualDistance < moverSolid.CylinderRadius)
                                            {
                                                originVector = positionVector - StartPoint;
                                                originVector.Normalize();

                                                float x = originVector.Y;
                                                float y = -originVector.X;
                                                originVector.X = x;
                                                originVector.Y = y;

                                                mover.getAs<Movement>().DeltaVector = originVector * Vector2.Dot(originVector, mover.getAs<Movement>().DeltaVector);
                                            }
                                        }
                                        else if (distanceAlongLine > Vector2.Distance(StartPoint, EndPoint))
                                        {
                                            //closest point is B
                                            nearestPoint = EndPoint;

                                            //Calculate the distance between the two points            
                                            float actualDistance = Vector2.Distance(nearestPoint, positionVector);

                                            if (actualDistance < moverSolid.CylinderRadius)
                                            {
                                                originVector = positionVector - EndPoint;
                                                originVector.Normalize();

                                                float x = originVector.Y;
                                                float y = -originVector.X;
                                                originVector.X = x;
                                                originVector.Y = y;

                                                mover.getAs<Movement>().DeltaVector = originVector * Vector2.Dot(originVector, mover.getAs<Movement>().DeltaVector);
                                            }
                                        }
                                        else
                                        {
                                            //closest point is between A and B... A + d  * ( ||B-A|| )
                                            nearestPoint = StartPoint + distanceAlongLine * v;

                                            //Calculate the distance between the two points            
                                            float actualDistance = Vector2.Distance(nearestPoint, positionVector);

                                            if (actualDistance < moverSolid.CylinderRadius)
                                            {
                                                originVector = EndPoint - StartPoint;
                                                originVector.Normalize();
                                                mover.getAs<Movement>().DeltaVector = originVector * Vector2.Dot(originVector, mover.getAs<Movement>().DeltaVector);
                                            }
                                        }

                                        //Now check to see if it's embedded
                                        positionVector = mover.getAs<Position>().EntityPosition + mover.getAs<Movement>().DeltaVector;
                                        distanceAlongLine = Vector2.Dot(positionVector, v) - Vector2.Dot(StartPoint, v);

                                        if (distanceAlongLine < 0)
                                        {
                                            //closest point is A
                                            nearestPoint = StartPoint;
                                        }
                                        else if (distanceAlongLine > Vector2.Distance(StartPoint, EndPoint))
                                        {
                                            //closest point is B
                                            nearestPoint = EndPoint;
                                        }
                                        else
                                        {
                                            //closest point is between A and B... A + d  * ( ||B-A|| )
                                            nearestPoint = StartPoint + distanceAlongLine * v;
                                        }

                                        //Calculate the distance between the two points            
                                        float actualDistance2 = Vector2.Distance(nearestPoint, positionVector);

                                        if (actualDistance2 < moverSolid.CylinderRadius)
                                        {
                                            float difference = moverSolid.CylinderRadius - actualDistance2;

                                            Vector2 surfacePerpendicular = positionVector - nearestPoint;
                                            surfacePerpendicular.Normalize();
                                            surfacePerpendicular *= difference;

                                            mover.getAs<Movement>().DeltaVector += surfacePerpendicular;                                           
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            List<Entity> allMovables = es.GetAllEntitiesPossesing(typeof(Position), typeof(Movement));
            foreach (Entity mover in allMovables)
            {
                mover.getAs<Position>().EntityPosition += mover.getAs<Movement>().DeltaVector;
                mover.getAs<Movement>().DeltaVector = Vector2.Zero;
            }
        }
    }
}
