using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Data;
using Microsoft.Xna.Framework;
using MapFileHandler;
using TurnBasedExample.View;

namespace MapEditor.View
{
    class WaterLogic
    {
        public static Texture2D GetTextureForWaterAt(Map map, Point position)
        {
            Texture2D waterTexture = TextureRepository.GetWater(WaterEdgeType.Center);

            // Surrounded by land
            if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
               !WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
               !WaterIsAt(map, new Point(position.X, position.Y - 1)) &&
               !WaterIsAt(map, new Point(position.X, position.Y + 1)))
                waterTexture = TextureRepository.GetWater(WaterEdgeType.WaterIsland);

            // Triple Boarder
            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y - 1)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Sticking out left
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LeftChannelEnd);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y - 1)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Sticking out right
                waterTexture = TextureRepository.GetWater(WaterEdgeType.RightChannelEnd);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Sticking out top
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerChannelEnd);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y - 1))) // Sticking out bottom
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperChannelEnd);


            // Double Boarder
            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X + 1, position.Y))) // Land to the left and right
                waterTexture = TextureRepository.GetWater(WaterEdgeType.ChannelVertical);

            else if(!WaterIsAt(map, new Point(position.X, position.Y - 1)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Land to the top and botom
                waterTexture = TextureRepository.GetWater(WaterEdgeType.ChannelHorizontal);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y - 1))) // Land to the left and top
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperLeft);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Land to the left and bottom
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerLeft);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y - 1))) // Land to the right and top
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperRight);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y)) &&
                    !WaterIsAt(map, new Point(position.X, position.Y + 1))) // Land to the right and bottom
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerRight);

            // Single Boarder
            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y))) // Land to the left
                waterTexture = TextureRepository.GetWater(WaterEdgeType.MiddleLeft);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y))) // Land to the right
                waterTexture = TextureRepository.GetWater(WaterEdgeType.MiddleRight);

            else if(!WaterIsAt(map, new Point(position.X, position.Y + 1))) // Land to the bottom
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerMiddle);

            else if(!WaterIsAt(map, new Point(position.X, position.Y - 1))) // Land to the top
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperMiddle);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y - 1))) // Land on the upper left diagonal
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperLeftTip);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y - 1))) // Land on the upper right diagonal
                waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperRightTip);

            else if(!WaterIsAt(map, new Point(position.X - 1, position.Y + 1))) // Land on the lower left diagonal
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerLeftTip);

            else if(!WaterIsAt(map, new Point(position.X + 1, position.Y + 1))) // Land on the lower right diagonal
                waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerRightTip);

            return waterTexture;
        }

        private static bool WaterIsAt(Map map, Point position)
        {
            if(!map.isOnMap(position))
                return true;
            else
                return map[position.X, position.Y] == TileType.Water;
        }
    }
}
