using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem.Systems
{
    public class AnimationSystem : BaseSystem
    {
        public AnimationSystem(BaseEntitySystem bes) : base(bes) { }

        public void Update(GameTime gameTime)
        {
            List<Entity> animations = es.GetAllEntitiesPossesing(typeof(Animated));

            foreach (Entity animated in animations)
            {
                if (animated.getAs<Animated>().Updated)
                {
                    foreach(KeyedAnim ka in animated.getAs<Animated>().AnimationLibrary)
                    {
                        if (ka.state == animated.getAs<Animated>().State)
                        {
                            animated.getAs<Renderable>().image = ka.anim;
                            animated.getAs<Animated>().Updated = false;
                        }
                    }
                }
            }
        }
    }
}
