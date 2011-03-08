using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem
{
    public class BaseSystem
    {
        protected BaseEntitySystem es;

        public BaseSystem(BaseEntitySystem bes)
        {
            es = bes;
        }
    }
}
