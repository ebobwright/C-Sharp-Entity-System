using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BFECore.EntitySystem
{
	public static class Assemblages
	{
        static Assemblages()
        {
            AssemblageInstructions = new Dictionary<string, CreateAssemblage>();
            AssemblageInstructions.Add("Moveable", new CreateAssemblage(CreateMovable));
            AssemblageInstructions.Add("Player", new CreateAssemblage(CreatePlayer));
            AssemblageInstructions.Add("Drawable", new CreateAssemblage(CreateDrawable));
            AssemblageInstructions.Add("Wall", new CreateAssemblage(CreateWall));
            AssemblageInstructions.Add("Portal", new CreateAssemblage(CreatePortal));
        }

        public delegate Entity CreateAssemblage();
        public static Dictionary<String, CreateAssemblage> AssemblageInstructions;

        public static Entity CreatePlayer()
        {
            Entity newDrawableEntity = new Entity(Entity.defaultEntitySystem.CreateEntityID());
            Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Position());
            Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Renderable());
            Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Player());
            Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Movement());
            Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Animated());

            return newDrawableEntity;
        }

		public static Entity CreateMovable()
		{
			Entity newDrawableEntity = new Entity(Entity.defaultEntitySystem.CreateEntityID());
			Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Position());
			Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Movement());

			return newDrawableEntity;
		}

		public static Entity CreateDrawable()
		{
			Entity newDrawableEntity = new Entity(Entity.defaultEntitySystem.CreateEntityID());
			Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Position());
			Entity.defaultEntitySystem.RegisterComponent(newDrawableEntity, new Renderable());

			return newDrawableEntity;
		}

        public static Entity CreateWall()
        {
            Entity newWall = new Entity(Entity.defaultEntitySystem.CreateEntityID());
            Entity.defaultEntitySystem.RegisterComponent(newWall, new Position());
            Entity.defaultEntitySystem.RegisterComponent(newWall, new Wall());

            return newWall;
        }

        public static Entity CreatePortal()
        {
            Entity portal = new Entity(Entity.defaultEntitySystem.CreateEntityID());
            Entity.defaultEntitySystem.RegisterComponent(portal, new Position());
            Entity.defaultEntitySystem.RegisterComponent(portal, new WarpPortal());

            return portal;
        }
	}
}
