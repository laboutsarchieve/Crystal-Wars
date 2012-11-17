using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MapEditor.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MapEditor.Logic
{
    static class MapFileManager
    {
        private static Map loadedMap;
        private static GraphicsDevice graphics;
        private static Texture2D mapTexture;

        private static Color waterColor = new Color(0, 0, 255);
        private static Color grassColor = new Color(0, 100, 0);
        private static Color forestColor = new Color(0, 255, 0);
        private static Color mountianShortColor = new Color(100, 0, 0);
        private static Color mountianTallColor = new Color(255, 0, 0);

        public static void Save(Map map, GraphicsDevice graphics)
        {
            mapTexture = new Texture2D(graphics, map.WidthInTiles, map.HeightInTiles);

            Color[] pixels = new Color[mapTexture.Width * mapTexture.Height];

            for(int x = 0; x < mapTexture.Width; x++)
            {
                for(int y = 0; y < mapTexture.Height; y++)
                {
                    int pointInArray = x % mapTexture.Width + y * mapTexture.Width;
                    TileFromColor(pixels[pointInArray] = GetColor(map[x, y]));
                }
            }

            mapTexture.SetData<Color>(pixels);
            Thread saveFileThread = new Thread(SaveDialog);
            saveFileThread.SetApartmentState(ApartmentState.STA);
            saveFileThread.Start();
        }
        public static Map Load(GraphicsDevice graphicsDevice)
        {
            graphics = graphicsDevice;
            loadedMap = new Map(Point.Zero);
            Thread loadFileThread = new Thread(LoadDialog);
            loadFileThread.SetApartmentState(ApartmentState.STA);
            loadFileThread.Start();
            loadFileThread.Join();

            return loadedMap;
        }
        private static void SaveDialog()
        {
            if(!Directory.Exists("maps"))
                Directory.CreateDirectory("maps");

            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Path.GetFullPath("maps");
            save.FileName = "new_map";
            save.AddExtension = true;
            save.DefaultExt = "png";
            save.Filter = "PNG(*.png)|*.*";

            DialogResult result = save.ShowDialog();

            if(result == DialogResult.OK)
            {
                using(Stream stream = File.OpenWrite(save.FileName))
                    mapTexture.SaveAsPng(stream, mapTexture.Width, mapTexture.Height);
            }
        }
        private static void LoadDialog()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Path.GetFullPath("maps");
            open.DefaultExt = "png";
            open.Filter = "PNG(*.png)|*.*";
            DialogResult result = open.ShowDialog();

            if(result == DialogResult.OK)
            {
                string toLoad = open.FileName;

                using(Stream stream = File.OpenRead(open.FileName))
                {
                    Texture2D mapTexture = Texture2D.FromStream(graphics, stream);
                    loadedMap = new Map(new Point(mapTexture.Width, mapTexture.Height));

                    Color[] pixels = new Color[mapTexture.Width * mapTexture.Height];
                    mapTexture.GetData<Color>(pixels);

                    for(int x = 0; x < mapTexture.Width; x++)
                    {
                        for(int y = 0; y < mapTexture.Height; y++)
                        {
                            int pointInArray = x % mapTexture.Width + y * mapTexture.Width;
                            TileType tile = TileFromColor(pixels[pointInArray]);
                            loadedMap[x, y] = tile;
                        }
                    }

                    RoomFinder.AddRoomsToMap(loadedMap, new Rectangle(0, 0, loadedMap.WidthInTiles, loadedMap.HeightInTiles));
                }
            }
        }
        private static Color GetColor(TileType tileType)
        {
            switch(tileType)
            {
                case TileType.Water:
                    return waterColor;
                case TileType.Grass:
                    return grassColor;
                case TileType.Tree:
                    return forestColor;
                case TileType.MountainShort:
                    return mountianShortColor;
                case TileType.MountainTall:
                    return mountianTallColor;
                default:
                    throw new ArgumentException("Unsupported Tile Type");
            }
        }
        private static TileType TileFromColor(Color color)
        {

            if(color == mountianShortColor)
                return TileType.MountainShort;
            else if(color == mountianTallColor)
                return TileType.MountainTall;
            else if(color == grassColor)
                return TileType.Grass;
            else if(color == forestColor)
                return TileType.Tree;
            else if(color == waterColor)
                return TileType.Water;
            if(color == new Color(0, 0, 0))
                return TileType.Wall;
            else if(color == new Color(100, 100, 100))
                return TileType.Floor;
            else if(color == new Color(255, 255, 255))
                return TileType.Door;
            else
                throw new ArgumentException("Unsupported Tile Type");
        }
    }
}
