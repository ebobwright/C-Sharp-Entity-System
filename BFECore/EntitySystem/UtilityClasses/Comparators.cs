using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem.UtilityClasses
{
    public class PositionSort : IComparer<Entity>
    {
        #region IComparer<Entity> Members

        public int Compare(Entity x, Entity y)
        {
            Position xPos = x.getAs<Position>();
            Position yPos = y.getAs<Position>();

            //if (xPos.height == yPos.height)
            //{
                return xPos.EntityPosition.Y.CompareTo(yPos.EntityPosition.Y);
            //}
            //else
            //{
            //    return xPos.height.CompareTo(yPos.height);
            //}
        }

        #endregion
    }

    public class WallSort : IComparable<WallSort>
    {
        public float Distance;
        public float PreDist;
        public Vector2 StartPoint;
        public Vector2 EndPoint;        

        public WallSort(float dist, float pDist, Vector2 sp, Vector2 ep)
        {
            Distance = dist;
            PreDist = pDist;
            StartPoint = sp;
            EndPoint = ep;
        }

        #region IComparable<WallSort> Members

        public int CompareTo(WallSort other)
        {
                return PreDist.CompareTo(other.PreDist);
        }

        #endregion
    }
}
