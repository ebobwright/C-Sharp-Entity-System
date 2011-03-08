using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BFECore.EntitySystem
{
	/// <summary>
	/// Not much to an entity
	/// It's a logical grouping of components
	/// It doesn't need to be anything but a number
	/// </summary>
	public class Entity
	{
		//The heart of the entity
		public int EntityID { get; set; }

        //Friendly Label :)
        public string EntityName { get; set; }

		//A reference to the entity system
		public static BaseEntitySystem defaultEntitySystem;

		//Constructor
		public Entity(int ID)
		{
			EntityID = ID;

			//Self Register!
			if (defaultEntitySystem != null)
			{
				defaultEntitySystem.RegisterEntity(this);	
			}
		}

		/// <summary>
		/// getAs is an IDE friendly way to get components and maintain strong typing 
		/// without going through the trouble of writing a get method for each component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T getAs<T>() where T:BaseComponent 
		{
			return (T)Entity.defaultEntitySystem.GetComponent(this, typeof(T));						
		}
	}
}
