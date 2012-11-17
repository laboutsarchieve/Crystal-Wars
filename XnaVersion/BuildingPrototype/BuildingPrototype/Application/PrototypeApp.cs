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
        private SpriteBatch spriteBatch;
        private ActorDatabase actorDatabase;
        private Map map;
        private Drawer drawer;
        private TileType selectedTile;
        private Dictionary<Keys, TileType> tileSelectionNumbers;
        private ActionHistoryData history;
        private int brushSize = 1;
        private int tillNextHistoryAction;
        private int betweenHistoryAction = 60;
        private bool showHelp;

        public PrototypeApp()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            GlobalSettings.Resolution = new Vector2(1200, 800);

            graphics.PreferredBackBufferWidth = (int)GlobalSettings.Resolution.X;
            graphics.PreferredBackBufferHeight = (int)GlobalSettings.Resolution.Y;
            graphics.ApplyChanges();

            selectedTile = TileType.Grass;

            tileSelectionNumbers = new Dictionary<Keys, TileType>();
            tileSelectionNumbers.Add(Keys.D1, TileType.Grass);
            tileSelectionNumbers.Add(Keys.D2, TileType.Water);
            tileSelectionNumbers.Add(Keys.D3, TileType.Forest);
            tileSelectionNumbers.Add(Keys.D4, TileType.MountainShort);
            tileSelectionNumbers.Add(Keys.D5, TileType.MountainTall);

            history = new ActionHistoryData();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureRepository.Initalize(Content, GraphicsDevice);
            FontRepository.Initalize(Content);

            map = new Map(Point.Zero);
            actorDatabase = new ActorDatabase();
            drawer = new Drawer(spriteBatch, map, actorDatabase);

            bool enteredSize = NewMap();
            if(!enteredSize)
                Exit();
        }
        private void RandomMap()
        {
            actorDatabase.Clear();

            MapGenerator mapGenerator = new MapGenerator();
            MapSizeForm mapSizeForm = new MapSizeForm();
            System.Windows.Forms.DialogResult result = mapSizeForm.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                map = mapGenerator.GenerateMap(mapSizeForm.MapSize);
                drawer = new Drawer(spriteBatch, map, actorDatabase);
                history = new ActionHistoryData();
            }
        }
        private bool NewMap()
        {
            actorDatabase.Clear();

            MapSizeForm mapSizeForm = new MapSizeForm();
            System.Windows.Forms.DialogResult result = mapSizeForm.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                map = new Map(mapSizeForm.MapSize);
                map.SetAllTilesTo(TileType.Grass);
                drawer = new Drawer(spriteBatch, map, actorDatabase);
                history = new ActionHistoryData();

                return true;
            }

            return false;
        }
        private void LoadMap()
        {
            Map loadedMap = MapFileManager.Load(GraphicsDevice);
            if(loadedMap.WidthInTiles > 1 && loadedMap.HeightInTiles > 1)
            {
                actorDatabase.Clear();
                map = loadedMap;
                drawer.Map = map;
                history = new ActionHistoryData();
            }
        }
        private void SaveMap()
        {
            MapFileManager.Save(map, GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            ExtendedKeyboard.Update();
            ExtendedMouse.Update();
            if(IsActive)
            {
                PollKeyboard(gameTime);
                PollMouse();
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            drawer.Draw(selectedTile);
            drawer.DrawString("Brush Size: " + brushSize, new Vector2(0, GlobalSettings.TileSize), Color.Black, Color.White * 0.5f);

            drawer.DrawTileSelection(tileSelectionNumbers);
            if(showHelp)
            {
                DrawHelpText();
            }

            base.Draw(gameTime);
        }
        private void PollKeyboard(GameTime gameTime)
        {
            ProcessSystemInput();
            ProcessMapActionInput(gameTime);
            ProcessMovementInput();
        }
        private void PollMouse()
        {
            ProcessMapChanges();
        }
        private void ProcessSystemInput()
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                EscapeExit();

            if(ExtendedKeyboard.IsKeyDown(Keys.H))
                showHelp = true;
            else
                showHelp = false;
        }
        private void ProcessMapActionInput(GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.N))
                NewMap();
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.R))
                RandomMap();
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.L))
                LoadMap();
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.P))
                SaveMap();

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.OemPlus))
                brushSize++;
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.OemMinus) && brushSize > 1)
                brushSize--;

            if(ExtendedKeyboard.IsKeyDown(Keys.LeftControl) && ExtendedKeyboard.IsKeyDownAfterUp(Keys.Z))
                Undo();
            if(ExtendedKeyboard.IsKeyDown(Keys.LeftControl) && ExtendedKeyboard.IsKeyDownAfterUp(Keys.Y))
                Redo();

            if(ExtendedKeyboard.IsKeyDown(Keys.LeftAlt) &&
               ExtendedKeyboard.IsKeyDown(Keys.LeftControl) &&
               ExtendedKeyboard.IsKeyDown(Keys.Z) &&
               tillNextHistoryAction < 0)
            {
                Undo();
                tillNextHistoryAction = betweenHistoryAction;
            }
            if(ExtendedKeyboard.IsKeyDown(Keys.LeftAlt) &&
               ExtendedKeyboard.IsKeyDown(Keys.LeftControl) &&
               ExtendedKeyboard.IsKeyDown(Keys.Y) &&
               tillNextHistoryAction < 0)
            {
                Redo();
                tillNextHistoryAction = betweenHistoryAction;
            }

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Up))
                GlobalSettings.Scale += 0.05f;
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Down))
                GlobalSettings.Scale -= 0.05f;

            if(ExtendedKeyboard.IsKeyDown(Keys.D1))
                selectedTile = tileSelectionNumbers[Keys.D1];
            if(ExtendedKeyboard.IsKeyDown(Keys.D2))
                selectedTile = tileSelectionNumbers[Keys.D2];
            if(ExtendedKeyboard.IsKeyDown(Keys.D3))
                selectedTile = tileSelectionNumbers[Keys.D3];
            if(ExtendedKeyboard.IsKeyDown(Keys.D4))
                selectedTile = tileSelectionNumbers[Keys.D4];
            if(ExtendedKeyboard.IsKeyDown(Keys.D5))
                selectedTile = tileSelectionNumbers[Keys.D5];

            tillNextHistoryAction -= gameTime.ElapsedGameTime.Milliseconds;
        }
        private void ProcessMovementInput()
        {
            float MOVE_SCALE = GlobalSettings.ScaledTileSize / 2;
            Vector2 movement = Vector2.Zero;

            if(ExtendedKeyboard.IsKeyDown(Keys.W))
                movement.Y--;
            if(ExtendedKeyboard.IsKeyDown(Keys.A))
                movement.X--;
            if(ExtendedKeyboard.IsKeyDown(Keys.S))
                movement.Y++;
            if(ExtendedKeyboard.IsKeyDown(Keys.D))
                movement.X++;            

            drawer.MoveView(GlobalSettings.ScaledTileSize * movement);
        }
        private void EscapeExit()
        {
            switch(System.Windows.Forms.MessageBox.Show(null, "Are you sure you want to quit?", "Quit", System.Windows.Forms.MessageBoxButtons.YesNo))
            {
                case System.Windows.Forms.DialogResult.No:
                    break;
                case System.Windows.Forms.DialogResult.Yes:
                    Exit();
                    break;
            }
        }
        private void ProcessMapChanges()
        {
            Point mousePixelPosition = ExtendedMouse.GetMousePosition();
            Point mouseTilePosition = new Point((mousePixelPosition.X + (int)drawer.UpperLeftOfView.X) / GlobalSettings.ScaledTileSize,
                                                (mousePixelPosition.Y + (int)drawer.UpperLeftOfView.Y) / GlobalSettings.ScaledTileSize);

            if(ExtendedMouse.LeftClickDown())
            {
                if(map.isOnMap(mouseTilePosition) && !(map[mouseTilePosition.X, mouseTilePosition.Y] == selectedTile))
                {
                    ChangeTile(mouseTilePosition, selectedTile);
                    history.numActionsToUndo.Push(1);
                    history.numActionsToRedo.Clear();
                    history.undoHistory.Clear();
                }
            }

            if(ExtendedMouse.RightClickDown())
            {
                Point toChange;
                int numTilesChanged = 0;
                for(int x = -brushSize; x < brushSize + 1; x++)
                {
                    for(int y = -brushSize; y < brushSize + 1; y++)
                    {
                        toChange = new Point(mouseTilePosition.X - x, mouseTilePosition.Y - y);
                        if(map.isOnMap(toChange) && map[toChange.X, toChange.Y] != selectedTile)
                        {
                            if(map[toChange.X, toChange.Y] == selectedTile)
                                continue;

                            ChangeTile(toChange, selectedTile);
                            numTilesChanged++;
                        }
                    }
                }

                if(numTilesChanged > 0)
                {
                    history.numActionsToRedo.Clear();
                    history.undoHistory.Clear();
                    history.numActionsToUndo.Push(numTilesChanged);
                }
            }

            float scaleChange = ExtendedMouse.GetWheelMovement() / 10000.0f;

            GlobalSettings.Scale += scaleChange;
        }
        private void ChangeTile(Point position, TileType newTileType)
        {
            if(map.isOnMap(position))
            {
                MapEditAction action = new MapEditAction(position, map[position.X, position.Y]);
                history.actionHistory.Push(action);
                map[position.X, position.Y] = newTileType;
            }
        }
        private void Undo()
        {
            if(history.numActionsToUndo.Count > 0)
            {
                int toUndo = history.numActionsToUndo.Pop();
                history.numActionsToRedo.Push(toUndo);
                for(int numUndone = 0; numUndone < toUndo; numUndone++)
                {
                    MapEditAction actionToUndo = history.actionHistory.Pop();
                    history.undoHistory.Push(new MapEditAction(actionToUndo.Position, map[actionToUndo.Position.X, actionToUndo.Position.Y]));
                    map[actionToUndo.Position.X, actionToUndo.Position.Y] = actionToUndo.OldTileType;
                }
            }
        }
        private void Redo()
        {
            if(history.numActionsToRedo.Count > 0)
            {
                int toRedo = history.numActionsToRedo.Pop();
                history.numActionsToUndo.Push(toRedo);
                for(int numUndone = 0; numUndone < toRedo; numUndone++)
                {
                    MapEditAction action = history.undoHistory.Pop();
                    ChangeTile(action.Position, action.OldTileType);
                }
            }
        }
        public void DrawHelpText()
        {
            string[] helpText = {"1 - 5 Select Tile Type",
                                 "L = load",
                                 "P = save",
                                 "R = Random Map",
                                 "N = New Empty Map",
                                 "W,A,S,D = Move View",
                                 "Left Click = place tile",
                                 "Right Click = Use Brush",
                                 "-/+ = change brush size" ,
                                 "Ctrl-Z = Undo Once",
                                 "Hold Ctrl-Alt-Z = Undo several",
                                 "Ctrl-Y = Redo Once",
                                 "Hold Ctrl-Alt-Y = Redo several",
                                 "Mouse Wheel/ Up+Down Arrow Keys = Zoom"};



            drawer.DrawStrings(helpText, new Vector2(GlobalSettings.Resolution.X, 0), Color.Black, Color.White * 0.8f);
        }
    }
}
