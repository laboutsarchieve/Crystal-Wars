using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MapEditor.Data;
using Microsoft.Xna.Framework;

namespace MapEditor.View
{
    static class TextureRepository
    {
        private static Texture2D mountianShort;
        private static Texture2D mountianTall;
        private static Texture2D forest;
        private static Texture2D grass;
        private static Texture2D floor;
        private static Texture2D wall;
        private static Texture2D door;

        private static Texture2D waterCenter;
        private static Texture2D waterLowerLeft;
        private static Texture2D waterLowerMiddle;
        private static Texture2D waterLowerRight;
        private static Texture2D waterMiddleLeft;
        private static Texture2D waterMiddleRight;
        private static Texture2D waterUpperLeft;
        private static Texture2D waterUpperMiddle;
        private static Texture2D waterUpperRight;
        private static Texture2D waterLowerChannelEnd;
        private static Texture2D waterUpperChannelEnd;
        private static Texture2D waterRightChannelEnd;
        private static Texture2D waterLeftChannelEnd;

        private static Texture2D waterChannelHorizontal;
        private static Texture2D waterChannelVertical;

        private static Texture2D waterIsland;
        private static Texture2D waterUpperRightTip;
        private static Texture2D waterUpperLeftTip;
        private static Texture2D waterLowerRightTip;
        private static Texture2D waterLowerLeftTip;

        private static Texture2D soilder;
        private static Texture2D pixel;

        // TODO: Clean up the water texture initalizing
        public static void Initalize(ContentManager content, GraphicsDevice graphics)
        {
            grass = content.Load<Texture2D>(@"art/tiles/AWgrass");
            mountianShort = content.Load<Texture2D>(@"art/tiles/AWmountianShort");
            mountianTall = content.Load<Texture2D>(@"art/tiles/AWmountianTall");
            forest = content.Load<Texture2D>(@"art/tiles/AWforest");
            floor = content.Load<Texture2D>(@"art/tiles/Floor");
            wall = content.Load<Texture2D>(@"art/tiles/Wall");
            door = content.Load<Texture2D>(@"art/tiles/Door");

            waterIsland = content.Load<Texture2D>(@"art/tiles/AWWaterIsland");
            waterCenter = content.Load<Texture2D>(@"art/tiles/AWWaterCenter");
            waterLowerLeft = content.Load<Texture2D>(@"art/tiles/AWWaterLowerLeft");
            waterLowerMiddle = content.Load<Texture2D>(@"art/tiles/AWWaterLowerMiddle");
            waterLowerRight = content.Load<Texture2D>(@"art/tiles/AWWaterLowerRight");
            waterMiddleLeft = content.Load<Texture2D>(@"art/tiles/AWWaterMiddleLeft");
            waterMiddleRight = content.Load<Texture2D>(@"art/tiles/AWWaterMiddleRight");
            waterUpperLeft = content.Load<Texture2D>(@"art/tiles/AWWaterUpperLeft");
            waterUpperMiddle = content.Load<Texture2D>(@"art/tiles/AWWaterUpperMiddle");
            waterUpperRight = content.Load<Texture2D>(@"art/tiles/AWWaterUpperRight");

            waterLowerChannelEnd = content.Load<Texture2D>(@"art/tiles/AWWaterLowerChannelEnd");
            waterUpperChannelEnd = content.Load<Texture2D>(@"art/tiles/AWWaterUpperChannelEnd");
            waterRightChannelEnd = content.Load<Texture2D>(@"art/tiles/AWWaterRightChannelEnd");
            waterLeftChannelEnd = content.Load<Texture2D>(@"art/tiles/AWWaterLeftChannelEnd");

            waterChannelVertical = content.Load<Texture2D>(@"art/tiles/AWWaterChannelVertical");
            waterChannelHorizontal = content.Load<Texture2D>(@"art/tiles/AWWaterChannelHorizontal");

            waterUpperRightTip = content.Load<Texture2D>(@"art/tiles/AWWaterUpperRightTip"); ;
            waterUpperLeftTip = content.Load<Texture2D>(@"art/tiles/AWWaterUpperLeftTip"); ;
            waterLowerRightTip = content.Load<Texture2D>(@"art/tiles/AWWaterLowerRightTip"); ;
            waterLowerLeftTip = content.Load<Texture2D>(@"art/tiles/AWWaterLowerLeftTip");

            soilder = content.Load<Texture2D>(@"art/actors/Soldier");

            pixel = new Texture2D(graphics, 1, 1);
            Color[] whitePixel = {Color.White};
            pixel.SetData<Color>(whitePixel);


            GlobalSettings.TileSize = grass.Width;
        }
        public static Texture2D GetActorTexture(ActorType type)
        {
            switch(type)
            {
                case ActorType.SoldierOne:
                    return soilder;
                default:
                    throw new ArgumentException(); // TODO: make a more informative custom exception to throw
            }
        }
        public static Texture2D GetTileTexture(TileType tile)
        {
            switch(tile)
            {
                case TileType.Grass:
                    return grass;
                case TileType.MountainShort:
                    return mountianShort;
                case TileType.MountainTall:
                    return mountianTall;
                case TileType.Forest:
                    return forest;
                case TileType.Water:
                    return waterCenter;
                case TileType.Floor:
                    return floor;
                case TileType.Door:
                    return door;
                case TileType.Wall:
                    return wall;
                default:
                    throw new ArgumentException();
            }
        }
        public static Texture2D GetWater(WaterEdgeType edgeType)
        {
            switch(edgeType)
            {
                case WaterEdgeType.LowerLeft:
                    return waterLowerLeft;
                case WaterEdgeType.LowerMiddle:
                    return waterLowerMiddle;
                case WaterEdgeType.LowerRight:
                    return waterLowerRight;
                case WaterEdgeType.MiddleLeft:
                    return waterMiddleLeft;
                case WaterEdgeType.MiddleRight:
                    return waterMiddleRight;
                case WaterEdgeType.UpperLeft:
                    return waterUpperLeft;
                case WaterEdgeType.UpperMiddle:
                    return waterUpperMiddle;
                case WaterEdgeType.UpperRight:
                    return waterUpperRight;
                case WaterEdgeType.Center:
                    return waterCenter;
                case WaterEdgeType.LeftChannelEnd:
                    return waterLeftChannelEnd;
                case WaterEdgeType.RightChannelEnd:
                    return waterRightChannelEnd;
                case WaterEdgeType.UpperChannelEnd:
                    return waterUpperChannelEnd;
                case WaterEdgeType.LowerChannelEnd:
                    return waterLowerChannelEnd;
                case WaterEdgeType.ChannelHorizontal:
                    return waterChannelHorizontal;
                case WaterEdgeType.ChannelVertical:
                    return waterChannelVertical;
                case WaterEdgeType.WaterIsland:
                    return waterIsland;

                case WaterEdgeType.LowerLeftTip:
                    return waterLowerLeftTip;
                case WaterEdgeType.LowerRightTip:
                    return waterLowerRightTip;
                case WaterEdgeType.UpperLeftTip:
                    return waterUpperLeftTip;
                case WaterEdgeType.UpperRightTip:
                    return waterUpperRightTip;

                default:
                    throw new ArgumentException("Invalid Water Type");
            }
        }

        public static Texture2D Pixel
        {
            get { return pixel; }
        }
    }
}
