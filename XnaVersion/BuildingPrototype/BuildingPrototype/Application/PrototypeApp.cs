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

namespace MapEditor.Application
{
    class PrototypeApp : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;        
        private GameState currentState;

        public PrototypeApp()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            currentState = (GameState)new MapEditorState(this, graphics);
        }
        protected override void Initialize()
        {
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
            LoadContent( );
        }
    }
}
