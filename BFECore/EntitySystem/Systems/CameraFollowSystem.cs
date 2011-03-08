using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem.Systems
{
    public class CameraFollowSystem : BaseSystem
    {
        public CameraFollowSystem(BaseEntitySystem bes) : base(bes) { }

        public void Update(GameTime gameTime)
        {
            List<Entity> players = es.GetAllEntitiesPossesing(typeof(Player));

            if (players.Count > 0)
            {
                Position playerPos = players[0].getAs<Position>();

                if (RenderingSystem.camera.RoomIndex != playerPos.RoomIndex)
                    RenderingSystem.camera.RoomIndex = playerPos.RoomIndex;

                float distanceToFollow = 200.0f;
                float actualDistance = Vector2.Distance(playerPos.EntityPosition, RenderingSystem.camera.Position);

                if (actualDistance > distanceToFollow)
                {
                    Vector2 cameraVector = RenderingSystem.camera.Position - playerPos.EntityPosition;
                    cameraVector.Normalize();
                    cameraVector *= distanceToFollow;

                    RenderingSystem.camera.Position = cameraVector + playerPos.EntityPosition;
                }
            }
        }
    }
}
