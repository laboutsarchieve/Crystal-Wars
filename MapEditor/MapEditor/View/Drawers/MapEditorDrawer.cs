using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Data;
using Shared.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shared.View;
using Shared.View.Repositories;

namespace MapEditor.View.Drawers
{
    class MapEditorDrawer : Drawer
    {
        public MapEditorDrawer(SpriteBatch spriteBatch, Map map) : base(spriteBatch, map)
        {

        }       
        internal void DrawTileSelection(Dictionary<Keys, TileType> tileSelectionNumbers, TileType tileType)
        {
            spriteBatch.Begin( );
            DrawSelectedTile(tileType);

            float left = GlobalSettings.Resolution.X / 2 - tileSelectionNumbers.Count/2.0f * GlobalSettings.TileSize;
            float top = GlobalSettings.Resolution.Y - GlobalSettings.TileSize;
            Vector2 topLeft = new Vector2(left, top);

            Keys[] selectionKeys = tileSelectionNumbers.Keys.ToArray( );
            
            for(int index = 0; index < selectionKeys.Length; index++)
            {
                Keys key = selectionKeys[index];
                TileType tile = tileSelectionNumbers[key];
                DrawRectangle(new Rectangle((int)left + index * (GlobalSettings.TileSize + 2) - 1,
                                            (int)top - 1,
                                            (int)GlobalSettings.TileSize + 2,
                                            (int)GlobalSettings.TileSize + 2),
                              Color.Black * 0.8f);
                DrawUnscaledTexture(TextureRepository.GetTileTexture(tile), topLeft + index * (GlobalSettings.TileSize + 2) * Vector2.UnitX, 0.0f);
            }
            spriteBatch.End();
        }
        private void DrawSelectedTile(TileType selectedTile)
        {
            DrawRectangle(new Rectangle(0, 0, GlobalSettings.TileSize + 2, GlobalSettings.TileSize + 2), Color.Black * 0.8f);

            DrawUnscaledTexture(TextureRepository.GetTileTexture(selectedTile), Vector2.Zero, 0.0f);
        }
    }
}
