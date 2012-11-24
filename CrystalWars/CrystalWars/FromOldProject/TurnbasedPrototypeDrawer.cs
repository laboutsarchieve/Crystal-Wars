using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Shared.Data;
using Microsoft.Xna.Framework;
using Shared.View;
using Shared.View.Repositories;

namespace MapEditor.View.Drawers
{
    class TurnbasedPrototypeDrawer : Drawer
    {
        ActorDatabase actorDatabase;

        public TurnbasedPrototypeDrawer(SpriteBatch spriteBatch, Map map, ActorDatabase actorDatabase)
            : base(spriteBatch, map)
        {
            this.actorDatabase = actorDatabase;
        }
        public void HighlightPoints(List<Point> pointsToHighlight, Color color)
        {
            Color highlightColor = color * 0.3f;
            spriteBatch.Begin();
            foreach(Point highlightPoint in pointsToHighlight)
            {
                if(map.isOnMap(highlightPoint))
                    DrawRectangle(new Rectangle(highlightPoint.X*GlobalSettings.ScaledTileSize - (int)upperLeftOfView.X,
                                                highlightPoint.Y*GlobalSettings.ScaledTileSize - (int)upperLeftOfView.Y,
                                                GlobalSettings.ScaledTileSize,
                                                GlobalSettings.ScaledTileSize), 
                                  highlightColor);
            }
            spriteBatch.End();
        }
        public override void Draw()
        {
            base.Draw();
            spriteBatch.Begin();
            DrawActors();
            spriteBatch.End();
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
                             Vector2.Zero,
                             GlobalSettings.Scale,
                             SpriteEffects.None,
                             0);
            }
        }
    }
}
