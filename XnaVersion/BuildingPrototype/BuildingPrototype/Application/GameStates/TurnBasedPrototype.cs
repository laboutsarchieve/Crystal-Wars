using MapEditor.Data;
using MapEditor.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Application.GameStates
{
    class TurnBasedPrototype : GameState
    {
        private GraphicsDeviceManager graphics;
        private Game mainGame;
        private Drawer drawer;
        private Map map;
        private SpriteBatch spriteBatch;
        private ActorDatabase actorDatabase;

        TurnBasedPrototype(Game mainGame, GraphicsDeviceManager graphics)
        {
            this.mainGame = mainGame;
            this.graphics = graphics; 

            map = new Map(Point.Zero);
            actorDatabase = new ActorDatabase();
        }
        public void Initialize()
        {
            spriteBatch = new SpriteBatch(mainGame.GraphicsDevice);
        }
        public void LoadContent()
        {
            drawer = new Drawer(spriteBatch, map, actorDatabase);
        }
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }
    }
}
