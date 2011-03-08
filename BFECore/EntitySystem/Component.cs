using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BFECore.EntitySystem
{
	/// <summary>
	/// Nobody should ever instantiate a BaseComponent 
	/// and they have nothing in common other than where they are stored.
	/// </summary>	
    [Serializable()]
    public class BaseComponent
    {
        public static List<Type> ComponentTypes;

        static BaseComponent()
        {
            ComponentTypes = new List<Type>();
            ComponentTypes.Add(typeof(Position));
            ComponentTypes.Add(typeof(Movement));
            ComponentTypes.Add(typeof(Player));
            ComponentTypes.Add(typeof(Renderable));
            ComponentTypes.Add(typeof(Wall));
            ComponentTypes.Add(typeof(Solid));
            ComponentTypes.Add(typeof(WarpPortal));
            ComponentTypes.Add(typeof(Animated));
        }

        public static Type GetType(string type) { return Type.GetType(type); }
    }

    public enum AnimationState
    {
        NONE,
        STAND_DOWN,
        STAND_UP,
        STAND_LEFT,
        STAND_RIGHT,
        WALK_DOWN,
        WALK_UP,
        WALK_LEFT,
        WALK_RIGHT
    }

    [Serializable()]
    public class Player : BaseComponent
    {
        public ushort PlayerIndex { get; set; }
    }

    [Serializable()]
	public class Position : BaseComponent
	{
        public Position()
        {
            EntityPosition = new Vector2();
            height = 0.0f;
            RoomIndex = 0;
        }

        public Vector2 EntityPosition { get; set; }
        public float height {get; set;}
        public int RoomIndex { get; set; }
	}

    [Serializable()]
    public class Movement : BaseComponent
    {
        public Movement()
        {
            DeltaVector = Vector2.Zero;
        }

        public Vector2 DeltaVector { get; set; }
    }

    [Serializable()]
	public class Renderable : BaseComponent
	{
        public Renderable()
        {            
            Tint = Color.White;
            Rotation = 0.0f;
            Scale = Vector2.One;
            CurrentFrameIndex = 0;
            Wrap = false;
            Vertical = false;
            HorizantalFlip = false;
        }

        public string image {get; set; }
        public Color Tint { get; set;  } //= Color.White;
        public float Rotation { get; set; } //= 0.0f;
        public Vector2 Scale { get; set; } //= 1.0f;
        public int CurrentFrameIndex { get; set; } //= 0;
        public DateTime lastFrameUpdate { get; set; } //;
        public Vector2 Offset { get; set; } //;
        public bool Wrap { get; set; }
        public bool Vertical { get; set; }
        public bool HorizantalFlip { get; set; }
	}

    public class Animated : BaseComponent
    {
        public Animated()
        {
            State = AnimationState.NONE;
            //AnimationLibrary = new Dictionary<int, string>();           
            AnimationLibrary = new List<KeyedAnim>();
        }

        public AnimationState State
        {
            get{ return m_State; }
            set 
            {
                m_State = value;
                Updated = true;
            }
        }
        //public Dictionary<int, string> AnimationLibrary { get; set; }
        public List<KeyedAnim> AnimationLibrary { get; set; }
        public bool Updated { get; set; }

        protected AnimationState m_State;        
    }

    public struct KeyedAnim
    {
        public AnimationState state { get; set; }
        public string anim {get; set; }
    }

    [Serializable()]
    public class Wall : BaseComponent
    {
        public Wall()
        {
        }

        public Vector2[] WallPoints { get; set; }        
    }

    [Serializable()]
    public class Solid : BaseComponent
    {
        public Solid()
        {
            CylinderRadius = 10.0f;
        }

        public float CylinderRadius { get; set; }
    }

    [Serializable()]
    public class WarpPortal : BaseComponent
    {
        public WarpPortal()
        {
            DestinationLocation = Vector2.Zero;
        }

        public float CylinderRadius { get; set; } //Centered at Position
        public int DestinationRoomIndex { get; set; } //What room are we moving to?
        public Vector2 DestinationLocation { get; set; } //Where in the room?
    }

  
}
