using MapEditor.Data;
using MapEditor.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Logic;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MapEditor.View.Drawers;

namespace MapEditor.Application.GameStates
{
    class TurnbasedPrototypeState : GameState
    {
        private GraphicsDeviceManager graphics;        
        private TurnbasedPrototypeDrawer drawer;
        private Map map;
        private SpriteBatch spriteBatch;
        private ActorDatabase actorDatabase;
        private Actor selectedActor;
        private List<Point> movementHighlight;

        private PrototypeAppliction mainGame;

        public TurnbasedPrototypeState(PrototypeAppliction mainGame, GraphicsDeviceManager graphics, Map map)
        {
            this.mainGame = mainGame;
            this.graphics = graphics; 
            this.map = map;        
        }
        public TurnbasedPrototypeState(PrototypeAppliction mainGame, GraphicsDeviceManager graphics)
        {
            this.mainGame = mainGame;
            this.graphics = graphics; 
        }
        public void Initialize()
        {
            selectedActor = Actor.DummyActor;
            movementHighlight = new List<Point>( );
            actorDatabase = new ActorDatabase();
            actorDatabase.AddActor(new Actor(Vector2.One, new Vector2(32,32), 10, ActorType.SoldierOne));

            spriteBatch = new SpriteBatch(mainGame.GraphicsDevice);            
        }
        public void LoadContent()
        {
            if(map == null)
                map = MapFileManager.Load(mainGame.GraphicsDevice);

            if(map.WidthInTiles == 0)
                mainGame.Exit( );

            drawer = new TurnbasedPrototypeDrawer(spriteBatch, map, actorDatabase);
        }
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(mainGame.IsActive)
            {
                PollKeyboard(gameTime);
                PollMouse();
            }   
        }        
        private void PollKeyboard(GameTime gameTime)
        {
            ProcessSystemInput( );
            ProcessMovementInput( );            
        }        
        private void PollMouse()
        {
            ProcessUnitSelection( );
        }       
        private void UpdateMovementHighlight( )
        {
            movementHighlight = Pathfinder.GetPointsInRange(map, selectedActor);
        }
        private void ProcessSystemInput()
        {
            if(ExtendedKeyboard.IsKeyDown(Keys.LeftControl) && ExtendedKeyboard.IsKeyDownAfterUp(Keys.U))
            { 
                MapEditorState mapEditor = new MapEditorState(mainGame, graphics, map);
                mainGame.ChangeState(mapEditor);
                mapEditor.UpperLeftOfView = drawer.UpperLeftOfView;
            }
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
        private void ProcessUnitSelection()
        {
            Point mousePixelPosition = ExtendedMouse.GetMousePosition( );
            Point mouseTilePoint = new Point((mousePixelPosition.X + (int)drawer.UpperLeftOfView.X) / GlobalSettings.ScaledTileSize,
                                                (mousePixelPosition.Y + (int)drawer.UpperLeftOfView.Y) / GlobalSettings.ScaledTileSize);

            Vector2 mouseTileVector = new Vector2(mouseTilePoint.X, mouseTilePoint.Y);

            if(ExtendedMouse.LeftClickDownAfterUp())
            { 
                selectedActor = actorDatabase.GetActorAt(mouseTileVector);   
                UpdateMovementHighlight( );
            }
            if(ExtendedMouse.MiddleClickDownAfterUp())
            { 
                MoveActor(selectedActor, mouseTileVector);
            }
            if(selectedActor != null && ExtendedMouse.RightClickDownAfterUp() && movementHighlight.Contains(mouseTilePoint))
            { 
                MoveActor(selectedActor, mouseTileVector);
            }
        }
        private void MoveActor(Actor actor, Vector2 newLocation)
        {
            selectedActor.Position = newLocation;
            UpdateMovementHighlight( );
        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            drawer.Draw();
            drawer.HighlightPoints(movementHighlight, Color.Blue);

            string positionString = "Actor: " + actorDatabase.Actors[0].Position.X + ", " + actorDatabase.Actors[0].Position.Y;
            drawer.DrawString(positionString, Vector2.Zero, Color.Black, Color.Red);
        }
        public Vector2 UpperLeftOfView
        {
            set{ drawer.MoveView( value - drawer.UpperLeftOfView); }
        }
    }
}
