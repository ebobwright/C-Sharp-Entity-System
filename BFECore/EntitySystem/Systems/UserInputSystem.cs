using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using BFECore.Graphics;

namespace BFECore.EntitySystem.Systems
{
    public class UserInputSystem : BaseSystem
    {
        public UserInputSystem(BaseEntitySystem bes) : base(bes) { }

        public void Update(GameTime gameTime)
        {            
            //if (!es.EditMode)
            {
                List<Entity> players = es.GetAllEntitiesPossesing(typeof(Player));

                foreach (Entity player in players)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        player.getAs<Movement>().DeltaVector -= Vector2.UnitY;
                        player.getAs<Animated>().State = AnimationState.WALK_UP;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        player.getAs<Movement>().DeltaVector += Vector2.UnitY;
                        player.getAs<Animated>().State = AnimationState.WALK_DOWN;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        player.getAs<Movement>().DeltaVector -= Vector2.UnitX;
                        player.getAs<Animated>().State = AnimationState.WALK_LEFT;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        player.getAs<Movement>().DeltaVector += Vector2.UnitX;
                        player.getAs<Animated>().State = AnimationState.WALK_RIGHT;
                    }

                    //Normalize Diagonals for speed
                    if (player.getAs<Movement>().DeltaVector != Vector2.Zero)
                    {
                        Vector2 delta = player.getAs<Movement>().DeltaVector;
                        delta.Normalize();
                        delta *= 3.0f;
                        player.getAs<Movement>().DeltaVector = delta;


                        //player.getAs<Movement>().DeltaVector.Normalize();
                        //player.getAs<Movement>().DeltaVector *= 3.0f;
                    }
                    else //No movement is happening, so reset to standing...
                    {
                        if (player.getAs<Animated>().State == AnimationState.WALK_UP)
                            player.getAs<Animated>().State = AnimationState.STAND_UP;

                        if (player.getAs<Animated>().State == AnimationState.WALK_DOWN)
                            player.getAs<Animated>().State = AnimationState.STAND_DOWN;

                        if (player.getAs<Animated>().State == AnimationState.WALK_LEFT)
                            player.getAs<Animated>().State = AnimationState.STAND_LEFT;

                        if (player.getAs<Animated>().State == AnimationState.WALK_RIGHT)
                            player.getAs<Animated>().State = AnimationState.STAND_RIGHT;
                    }                    
                }
            }
        }
    }
}
