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


using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MapEditor.Data;
using MapEditor.View;
using MapEditor.Logic;

namespace MapEditor.Application
{
    public class PrototypeApp : Microsoft.Xna.Framework.Game
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
    }
}
