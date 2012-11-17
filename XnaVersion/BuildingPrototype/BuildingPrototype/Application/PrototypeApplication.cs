////////////////////////////////////////////////////////////////////
// Project: Map Editor          Author: Benjamin Sergent          //
// Last Edit: 11-8-2012         Email: BenjaminRSergent@gmail.com //
//                                                                //
// Description : A simple tiled based map editor prototype.       //
//               The tiles used are owned by Intelligent Systems. //
//               Do not release or modify for commerical purposes //
//               without replacing the tiles with your own work.  //
//                                                                //
////////////////////////////////////////////////////////////////////

using MapEditor.Application.GameStates;
using Microsoft.Xna.Framework;
using MapEditor.View;
using MapEditor.Data;
using MapEditor.View.Repositories;
using GameTools.Input;

namespace MapEditor.Application
{
    class PrototypeAppliction : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;        
        private GameState currentState;

        public PrototypeAppliction()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            currentState = (GameState)new MapEditorState(this, graphics);
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            GlobalSettings.Resolution = new Vector2(1200, 800);

            graphics.PreferredBackBufferWidth = (int)GlobalSettings.Resolution.X;
            graphics.PreferredBackBufferHeight = (int)GlobalSettings.Resolution.Y;
            graphics.ApplyChanges();

            currentState.Initialize();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            TextureRepository.Initalize(Content, GraphicsDevice);
            FontRepository.Initalize(Content);

            currentState.LoadContent();

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            ExtendedKeyboard.Update();
            ExtendedMouse.Update();

            currentState.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime);
            base.Draw(gameTime);
        }
        public void ChangeState( GameState newState )
        {
            currentState = newState;
            Initialize( );            
        }
    }
}
