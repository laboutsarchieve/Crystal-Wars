using Microsoft.Xna.Framework;

namespace MapEditor.Application.GameStates
{
    interface GameState
    {
        void Initialize();
        void LoadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
