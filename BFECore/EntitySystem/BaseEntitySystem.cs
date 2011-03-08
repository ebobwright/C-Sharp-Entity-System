using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace BFECore.EntitySystem
{
	/// <summary>
	/// Base Entity System
	/// Handles storeage and retrieval of all entities and related components 
	/// </summary>
	[Serializable()]
    public class BaseEntitySystem : IDisposable 
	{
        public bool EditMode = false;
        public EditorType EditType = EditorType.EntityMode;
        public GridType GridType = GridType.GRID_NONE;

        public int NextEntityID
        {
            get { return m_NextEntityID; }
        }
        public Dictionary<int, Entity> EntityTable
        {
            get { return m_EntityTable; }
        }

        public Dictionary<Type, Dictionary<Entity, BaseComponent>> Stores
        {
            get { return componentStores; }
        }

		#region Constructor

		/// <summary>
		/// Constructor - I might consider making this a singleton, unless I can think of some reason 
		/// that you might want to have multiple entity systems within a single game.
		/// At this point, I'm also not sure why you would need to derive from this.
		/// </summary>
		public BaseEntitySystem()
		{
			m_EntityTable = new Dictionary<int, Entity>();

			if (Entity.defaultEntitySystem == null)
			{
				Entity.defaultEntitySystem = this;
			}
		}

		#endregion

		#region Private Members
		
		//EntityID incrementer - eventually we can do away with this and use guids
		private int m_NextEntityID = 0;

		//A master list of entity objects. We might not even need this.
		private Dictionary<int, Entity> m_EntityTable;

		//A place to store all of our component dictionaries	
		private Dictionary<Type, Dictionary<Entity, BaseComponent>> componentStores = new Dictionary<Type, Dictionary<Entity, BaseComponent>>();

		#endregion

		#region Entity Management Methods

		/// <summary>
		/// Just increments the id and sends a new one
		/// I think there is probably a better way to do this...
		/// but I'm a lazy thinker at the moment
		/// </summary>
		/// <returns>A "unique" entity ID</returns>
		public int CreateEntityID()
		{
			return m_NextEntityID++;
		}


		/// <summary>
		/// Puts the new entity into the entity dictionary
		/// </summary>
		/// <param name="e"></param>
		public void RegisterEntity(Entity e)
		{			
			m_EntityTable.Add(e.EntityID, e);
		}


		/// <summary>
		/// Retrieves and entity from the list by ID
		/// </summary>
		/// <param name="EntityID"></param>
		/// <returns></returns>
		public Entity GetEntity(int EntityID)
		{
			return m_EntityTable[EntityID];
		}


		/// <summary>
		/// Removes an entity from the master list and removes all of it's components
		/// </summary>
		/// <param name="e"></param>
		public void KillEntity(Entity e)
		{
			foreach (Dictionary<Entity, BaseComponent> store in componentStores.Values)
			{
				if (store.ContainsKey(e))
					store.Remove(e);
			}

			m_EntityTable.Remove(e.EntityID);		
		}

        /// <summary>
        /// Get a list of all types that this entity implements 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public List<Type> GetAllEntityTypes(Entity e)
        {
            List<Type> tempList = new List<Type>();

            foreach (Type compType in componentStores.Keys)
            {
                Dictionary<Entity, BaseComponent> compStore = componentStores[compType];

                if (compStore.ContainsKey(e))
                {
                    tempList.Add(compType);
                }
            }

            return tempList;
        }


		#endregion

		#region Component Management

		/// <summary>
		/// Add a component to the Entity System 		 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="component"></param>
		public void RegisterComponent(Entity e, BaseComponent component)
		{
			if (!componentStores.ContainsKey(component.GetType()))
			{				
				componentStores.Add(component.GetType(), new Dictionary<Entity, BaseComponent>());
			}

			Dictionary<Entity, BaseComponent> store = componentStores[component.GetType()];			
			store.Add(e, component);
				
		}


		/// <summary>
		/// Fetch a component of an entity by type (see getAs in Entity.cs as well)
		/// </summary>
		/// <param name="e"></param>
		/// <param name="T"></param>
		/// <returns></returns>
		public BaseComponent GetComponent(Entity e, Type T)
		{
            try
            {
                Dictionary<Entity, BaseComponent> store = componentStores[T];
                if (store != null)
                {
                    BaseComponent result = store[e];
                    if (result != null)
                        return result;
                }
            }
            catch(Exception)
            {
            }

			return null;
		}

		/// <summary>
		/// Removes an Entity/Component pair from the Entity System
		/// </summary>
		/// <param name="e"></param>
		/// <param name="component"></param>
		public void RemoveComponent(Entity e, BaseComponent component)
		{
            if (component != null)
            {
                if (componentStores.ContainsKey(component.GetType()))
                {
                    Dictionary<Entity, BaseComponent> store = componentStores[component.GetType()];
                    store.Remove(e);
                }
            }
		}

		/// <summary>
		/// Get a list of Entity objects that have a component for each passed in type
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		public List<Entity> GetAllEntitiesPossesing(params Type[] types)
		{
			List<Entity> tempEntityList = new List<Entity>();

			foreach(Entity e in m_EntityTable.Values)
			{
				bool entityExistsInAllComponentLists = true;
				foreach (Type T in types)
				{
					if (!componentStores.ContainsKey(T) || !componentStores[T].ContainsKey(e))
					{
						entityExistsInAllComponentLists = false;
						break;
					}
				}

				if (entityExistsInAllComponentLists)
					tempEntityList.Add(e);
			}
			
			return tempEntityList;
		}


        /// <summary>
        /// Check if a component already exists for an entity
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasComponent(Entity e, Type type)
        {
            bool bRet = false;
            if (componentStores.ContainsKey(type))
            {
                Dictionary<Entity, BaseComponent> store = componentStores[type];
                bRet = store.ContainsKey(e);
            }

            return bRet;
        }

		#endregion

		#region Load and Save

		public void Load(string entityFile)
		{
            this.Dispose();	            

			XmlDocument xdEntities = new XmlDocument();
			xdEntities.Load(entityFile);

			XmlNode xnEntities = xdEntities.SelectSingleNode(@"EntitySystem/EntityList");
			if (xnEntities != null)
			{
				foreach (XmlNode entity in xnEntities.ChildNodes)
				{
                    int entityID = int.Parse(entity.Name.TrimStart('E'));
					Entity e = new Entity(entityID);
					e.EntityName = entity.InnerText;

                    if (this.m_NextEntityID < entityID)
                        this.m_NextEntityID = entityID;
				}

                this.m_NextEntityID++;
			}

			XmlNode xnComponents = xdEntities.SelectSingleNode(@"EntitySystem/Components");
			if (xnComponents != null)
			{
				foreach (XmlNode componentDictionary in xnComponents.ChildNodes)
				{
					Type currentComponent = Type.GetType(componentDictionary.Name);

					foreach (XmlNode entity in componentDictionary.ChildNodes)
					{
						Entity tempE = GetEntity(int.Parse(entity.Name.TrimStart('E')));
						XmlSerializer x = new XmlSerializer(currentComponent);

						BaseComponent comp = (BaseComponent)x.Deserialize(new XmlNodeReader(entity.FirstChild));

						//foreach (XmlNode property in entity.ChildNodes)
						//{
						//    PropertyInfo pInfo = comp.GetType().GetProperty(property.Name);
						//    pInfo.SetValue(comp, System.Convert.ChangeType(property.InnerText, pInfo.PropertyType), null);
						//}

						RegisterComponent(tempE, comp);
					}
				}
			}
		}

		public bool Save(string entityFile)
		{
			XmlTextWriter textWriter = new XmlTextWriter(entityFile, null);

			// Opens the document
			textWriter.WriteStartDocument();
			textWriter.WriteStartElement("EntitySystem");
			
			// Write first element
			textWriter.WriteStartElement("EntityList");
			foreach (int key in m_EntityTable.Keys)
			{
				Entity e = m_EntityTable[key];
				textWriter.WriteStartElement("E" + key.ToString());
				textWriter.WriteString(e.EntityName);
				textWriter.WriteEndElement();
			}
			textWriter.WriteEndElement();

			textWriter.WriteStartElement("Components");

			List<Type> tempList = new List<Type>();
			foreach (Type compType in componentStores.Keys)
			{
				Dictionary<Entity, BaseComponent> compStore = componentStores[compType];

				textWriter.WriteStartElement(compType.ToString());

				foreach (Entity entity in compStore.Keys)
				{					
					textWriter.WriteStartElement("E" + entity.EntityID.ToString());

					BaseComponent comp = compStore[entity];

					//PropertyInfo[] propertyInfo = comp.GetType().GetProperties();

					//foreach (PropertyInfo pInfo in propertyInfo)
					//{
					//    textWriter.WriteStartElement(pInfo.Name);
					//    textWriter.WriteString(pInfo.GetValue(comp, null).ToString());
					//    textWriter.WriteEndElement();
					//}

					XmlSerializer x = new XmlSerializer(comp.GetType());
					x.Serialize(textWriter, comp);
					

					textWriter.WriteEndElement();
				}

				textWriter.WriteEndElement();
			}

			textWriter.WriteEndElement();
			textWriter.WriteEndElement();

			// Ends the document.
			textWriter.WriteEndDocument();

			// close writer
			textWriter.Close();

			return true;
		}

		#endregion

        #region IDisposable Members

        public void Dispose()
        {
            List<int> entList = new List<int>();
            foreach (Entity tempEntity in m_EntityTable.Values)
            {
                entList.Add(tempEntity.EntityID);
            }

            foreach (int EntID in entList)
            {
                Entity tempEntity = this.GetEntity(EntID);
                this.KillEntity(tempEntity);
            }
        }

        #endregion
    }

    public enum EditorType
    {
        EntityMode,
        WallMode
    }

    public enum GridType
    {
        GRID_NONE = 1,
        GRID_SIXTEEN = 16,
        GRID_THIRTYTWO = 32,
        GRID_SIXTYFOUR = 64
    }
}
