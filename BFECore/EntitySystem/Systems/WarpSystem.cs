using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem.Systems
{
    public class WarpSystem : BaseSystem
    {
        public WarpSystem(BaseEntitySystem bes) : base(bes) { }

        public void Update(GameTime gameTime)
        {
            //Get all solid objects
            List<Entity> solidObjects = es.GetAllEntitiesPossesing(typeof(Position), typeof(Solid));
            //Get all warp portals
            List<Entity> portals = es.GetAllEntitiesPossesing(typeof(Position), typeof(WarpPortal));

            //For each solid object
            foreach (Entity solid in solidObjects)
            {
                foreach (Entity portal in portals)
                {
                    if (portal.getAs<Position>().RoomIndex == solid.getAs<Position>().RoomIndex)
                    {
                        float distance = Vector2.Distance(portal.getAs<Position>().EntityPosition, solid.getAs<Position>().EntityPosition);

                        //If it overlaps any portal
                        if (distance < portal.getAs<WarpPortal>().CylinderRadius + solid.getAs<Solid>().CylinderRadius)
                        {                            
                            //Then set its room and location equal to whatever the portal says
                            solid.getAs<Position>().RoomIndex = portal.getAs<WarpPortal>().DestinationRoomIndex;
                            solid.getAs<Position>().EntityPosition = portal.getAs<WarpPortal>().DestinationLocation;
                        }                        
                    }
                }
            }
        }
    }
}
