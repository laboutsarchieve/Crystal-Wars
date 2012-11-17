using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MapEditor.Data;
using Microsoft.Xna.Framework.Input;
using MapEditor.View.Repositories;

namespace MapEditor.View.Drawers
{
    class Drawer
    {
        protected SpriteBatch spriteBatch;
        protected Map map;        
        protected Vector2 upperLeftOfView;

        public Drawer(SpriteBatch spriteBatch, Map map)
        {
            this.spriteBatch = spriteBatch; 
            this.map = map;            
        }
        public void CenterView(Vector2 position)
        {
            upperLeftOfView = position - GlobalSettings.Resolution / 2;
            MoveView(Vector2.Zero);
        }
        public void MoveView(Vector2 movement)
        {
            upperLeftOfView += movement;

            if(map != null)
            {
                upperLeftOfView = Vector2.Clamp(upperLeftOfView,
                                                Vector2.Zero,
                                                GlobalSettings.ScaledTileSize * new Vector2(map.WidthInTiles - 1, map.HeightInTiles - 1) - GlobalSettings.Resolution);
            }
        }
        public void DrawStrings(string[] text, Vector2 upperRightPosition, Color color, Color backgroundColor)
        {
            spriteBatch.Begin();
            float longestString = 0;
            float tallestString = 0;
            foreach(string line in text)
            {
                Vector2 stringDimensions = FontRepository.debugFont.MeasureString(line);
                if(stringDimensions.X > longestString)
                    longestString = stringDimensions.X;

                if(stringDimensions.Y > tallestString)
                    tallestString = stringDimensions.Y;
            }

            Vector2 upperLeftPosition = upperRightPosition - longestString * Vector2.UnitX;
            DrawRectangle(new Rectangle((int)upperLeftPosition.X, (int)upperLeftPosition.Y, (int)longestString, (int)(text.Length * (tallestString))), backgroundColor);

            for(int index = 0; index < text.Length; index++)
            {
                spriteBatch.DrawString(FontRepository.debugFont, text[index], upperLeftPosition + index * tallestString * Vector2.UnitY, color);
            }
            spriteBatch.End();
        }
        internal void DrawString(string message, Vector2 position, Color color, Color backgroundColor)
        {
            spriteBatch.Begin();
            Vector2 stringDimensions = FontRepository.debugFont.MeasureString(message);
            DrawRectangle(new Rectangle((int)position.X, (int)position.Y, (int)stringDimensions.X, (int)stringDimensions.Y), backgroundColor);
            spriteBatch.DrawString(FontRepository.debugFont, message, position, color);
            spriteBatch.End();
        }
        protected void DrawRectangle(Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(TextureRepository.Pixel,
                             rectangle,
                             color);
        }
        public virtual void Draw( )
        {
            if(map != null)
            {
                spriteBatch.Begin();
                DrawMap();                        
                spriteBatch.End();
            }
        }
        private void DrawMap()
        {
            Vector2 upperLeftInTiles = upperLeftOfView / GlobalSettings.ScaledTileSize;
            for(int x = 0; x < GlobalSettings.TileResolution.X + 1 && upperLeftInTiles.X + x < map.WidthInTiles; x++)
            {
                for(int y = 0; y < GlobalSettings.TileResolution.Y + 1 && upperLeftInTiles.Y + y < map.HeightInTiles; y++)
                {
                    DrawTile(map[(int)upperLeftInTiles.X + x, (int)upperLeftInTiles.Y + y], new Point((int)upperLeftInTiles.X + x, (int)upperLeftInTiles.Y + y));
                }
            }
        }
        protected void DrawTile(TileType tileType, Point position)
        {
            if(tileType == TileType.Water)
                DrawWater(position);
            else if(tileType == TileType.MountainTall)
                DrawTallMountian(position);
            else
                DrawTextureAtMapLocation(TextureRepository.GetTileTexture(tileType), position, 0.0f);
        }
        private void DrawTallMountian(Point position)
        {
            DrawTexture(TextureRepository.GetTileTexture(TileType.MountainTall),
                        GlobalSettings.ScaledTileSize * new Vector2(position.X, position.Y) - GlobalSettings.ScaledTileSize * 0.3f * Vector2.UnitY - upperLeftOfView,
                        0.0f);
        }
        private void DrawWater(Point position)
        {
            Texture2D waterTexture = WaterLogic.GetTextureForWaterAt(map, position);
            DrawTextureAtMapLocation(waterTexture, position, 0.0f);
        }
        protected void DrawTextureAtMapLocation(Texture2D tileTexture, Point position, float rotation)
        {
            Vector2 drawLocation = GlobalSettings.ScaledTileSize * new Vector2(position.X, position.Y) - upperLeftOfView;            
            DrawTexture(tileTexture, drawLocation, rotation);
        }
        protected void DrawTexture(Texture2D tileTexture, Vector2 position, float rotation)
        {
            spriteBatch.Draw(tileTexture,
                             new Vector2(position.X, position.Y),
                             null,
                             Color.White,
                             rotation,
                             Vector2.Zero,
                             GlobalSettings.Scale,
                             SpriteEffects.None,
                             0);
        }
        protected void DrawUnscaledTexture(Texture2D tileTexture, Vector2 position, float rotation)
        {
            spriteBatch.Draw(tileTexture,
                             new Vector2(position.X, position.Y),
                             null,
                             Color.White,
                             rotation,
                             Vector2.Zero,
                             1.0f,
                             SpriteEffects.None,
                             0);
        }
        internal Map Map
        {
            get { return map; }
            set { map = value; }
        }
        public Vector2 UpperLeftOfView
        {
            get { return upperLeftOfView; }
        }        
    }
}