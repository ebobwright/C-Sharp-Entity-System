using System;
using System.Collections.Generic;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using BFECore.EntitySystem;
using BFECore.EntitySystem.Systems;
using BFECore.Graphics;



namespace GameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private int lastMouseWheel;

        BaseEntitySystem mainEntitySystem;

        RenderingSystem renderer;

        UserInputSystem userInput;
        MovementResolution movementSystem;
        EditingSystem editingSystem;
        WarpSystem warpSystem;
        CameraFollowSystem cameraSystem;
        AnimationSystem animationSystem;

#if DEBUG

        EntityEditor.EntityEditor frmEntityEditor = null;

#endif

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;    
               
            Content.RootDirectory = "Content";

            //Create Entity System
            mainEntitySystem = new BFECore.EntitySystem.BaseEntitySystem();          
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>        
        protected override void Initialize()
        {
            //Create Systems
            renderer = new RenderingSystem(mainEntitySystem, graphics.GraphicsDevice);
            renderer.filterEffect = Content.Load<Effect>("filter");
            userInput = new UserInputSystem(mainEntitySystem);
            movementSystem = new MovementResolution(mainEntitySystem);
            editingSystem = new EditingSystem(mainEntitySystem);
            warpSystem = new WarpSystem(mainEntitySystem);
            cameraSystem = new CameraFollowSystem(mainEntitySystem);
            animationSystem = new AnimationSystem(mainEntitySystem);

            base.Initialize();

            lastMouseWheel = Mouse.GetState().ScrollWheelValue;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GraphicsLibrary.LoadGraphicsObjects(this.Content);      

            //Entity starryBackground = Assemblages.CreateDrawable();
            //starryBackground.EntityName = "STARS";
            //starryBackground.getAs<Renderable>().GraphicIndex = 1;
            //starryBackground.getAs<Renderable>().Scale = Vector2.One * 1.56f;
            //starryBackground.getAs<Position>().height = 0.0f;

            //Entity mario1 = Assemblages.CreatePlayer();
            //mario1.EntityName = "PLAYER1";
            //mario1.getAs<Renderable>().GraphicIndex = 0;
            //mario1.getAs<Renderable>().Scale = Vector2.One * 2.0f;
            //mario1.getAs<Position>().EntityPosition = new Vector2(100, 100);
            //mario1.getAs<Position>().height = 0.0f;

            //Entity mario2 = Assemblages.CreateDrawable();
            //mario2.EntityName = "NPC1";
            //mario2.getAs<Renderable>().GraphicIndex = 0;
            //mario2.getAs<Position>().EntityPosition = new Vector2(200, 100);
            //mario2.getAs<Position>().height = 0.0f;
            //mario2.getAs<Renderable>().Scale = Vector2.One * 2.0f;

            //Entity WALL = new Entity(mainEntitySystem.CreateEntityID());
            //mainEntitySystem.RegisterComponent(WALL, new Wall());
            //WALL.EntityName = "WALL1";
            //WALL.getAs<Wall>().EndPoint = new Vector2(100, 200);

            //Entity WALL2 = new Entity(mainEntitySystem.CreateEntityID());
            //mainEntitySystem.RegisterComponent(WALL2, new Wall());
            //WALL2.EntityName = "WALL2";
            //WALL2.getAs<Wall>().StartPoint = new Vector2(100, 200);
            //WALL2.getAs<Wall>().EndPoint = new Vector2(300, 200);            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.Content.Unload();
        }


        DateTime dtLastUpdate = new DateTime(1980, 1, 25);
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                mainEntitySystem.EditMode = !mainEntitySystem.EditMode;

                if (frmEntityEditor == null)
                {
                    frmEntityEditor = new EntityEditor.EntityEditor(mainEntitySystem);                   
                }
                frmEntityEditor.Show();
                
                System.Threading.Thread.Sleep(500);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                RenderingSystem.camera.Position.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                RenderingSystem.camera.Position.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                RenderingSystem.camera.Position.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                RenderingSystem.camera.Position.Y += 1;            

            userInput.Update(gameTime);           
            movementSystem.Update(gameTime);
            editingSystem.Update(gameTime);
            warpSystem.Update(gameTime);
            cameraSystem.Update(gameTime);
            animationSystem.Update(gameTime);

            //if (mainEntitySystem.EditMode)
            //{
            //    if (Keyboard.GetState().IsKeyDown(Keys.F11))
            //        mainEntitySystem.Save();
            //    if (Keyboard.GetState().IsKeyDown(Keys.F5))
            //        mainEntitySystem = BaseEntitySystem.Load();
            //}
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);     
       
            renderer.Draw(gameTime);
     
            graphics.GraphicsDevice.RenderState.DepthBufferEnable = true;

            base.Draw(gameTime);
        }
    }
}
