using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MapEditor.Data;
using Microsoft.Xna.Framework.Input;

namespace MapEditor.View
{
    class Drawer
    {
        private SpriteBatch spriteBatch;
        private Map map;
        private ActorDatabase actorDatabase;
        private Vector2 upperLeftOfView;

        public Drawer(SpriteBatch spriteBatch, Map map, ActorDatabase actorDatabase)
        {
            this.spriteBatch = spriteBatch;
            this.map = map;
            this.actorDatabase = actorDatabase;
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
        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(TextureRepository.Pixel,
                             rectangle,
                             color);
        }
        internal void DrawTileSelection(Dictionary<Keys, TileType> tileSelectionNumbers)
        {
            float left = GlobalSettings.Resolution.X / 2 - tileSelectionNumbers.Count/2.0f * GlobalSettings.TileSize;
            float top = GlobalSettings.Resolution.Y - GlobalSettings.TileSize;
            Vector2 topLeft = new Vector2(left, top);

            Keys[] selectionKeys = tileSelectionNumbers.Keys.ToArray( );
            spriteBatch.Begin();
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
        public void Draw(TileType selectedTile)
        {
            if(map != null)
            {
                spriteBatch.Begin();
                DrawMap();
                DrawActors();
                DrawHud(selectedTile);
                spriteBatch.End();
            }
        }
        private void DrawActors()
        {
            List<Actor> actorsOnMap = actorDatabase.Actors;

            foreach(Actor actor in actorsOnMap)
            {
                Texture2D actorTexture = TextureRepository.GetActorTexture(actor.Type);
                spriteBatch.Draw(actorTexture,
                             GlobalSettings.ScaledTileSize * actor.Position - upperLeftOfView,
                             null,
                             Color.White,
                             0.0f,
                             GlobalSettings.ScaledTileSize * new Vector2(0.5f, 0.5f),
                             GlobalSettings.Scale,
                             SpriteEffects.None,
                             0);
            }
        }
        private void DrawHud(TileType selectedTile)
        {
            DrawRectangle(new Rectangle(0, 0, GlobalSettings.TileSize + 2, GlobalSettings.TileSize + 2), Color.Black * 0.8f);

            DrawUnscaledTexture(TextureRepository.GetTileTexture(selectedTile), Vector2.Zero, 0.0f);
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
        private void DrawTile(TileType tileType, Point position)
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
        private void DrawTextureAtMapLocation(Texture2D tileTexture, Point position, float rotation)
        {
            Vector2 posAsVector = GlobalSettings.ScaledTileSize * new Vector2(position.X, position.Y) - upperLeftOfView;
            DrawTexture(tileTexture, posAsVector, rotation);
        }
        private void DrawTexture(Texture2D tileTexture, Vector2 position, float rotation)
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
        private void DrawUnscaledTexture(Texture2D tileTexture, Vector2 position, float rotation)
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
