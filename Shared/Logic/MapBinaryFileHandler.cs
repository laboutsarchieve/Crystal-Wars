using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Shared.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System;

namespace Shared.Logic
{
    static public class MapBinaryFileManager
    {
        private static Map mapToSave;
        private static Map loadedMap;

        public static void Save(Map map)
        {
            mapToSave = map;
            Thread saveFileThread = new Thread(SaveDialog);
            saveFileThread.SetApartmentState(ApartmentState.STA);
            saveFileThread.Start();
            saveFileThread.Join( );
            mapToSave = null;
        }
        public static Map Load()
        {
            loadedMap = new Map(Point.Zero);
            Thread loadFileThread = new Thread(LoadDialog);
            loadFileThread.SetApartmentState(ApartmentState.STA);
            loadFileThread.Start();
            loadFileThread.Join();

            Map toReturn = loadedMap;
            loadedMap = null;

            return toReturn;
        }
        private static void SaveDialog()
        {
            if(!Directory.Exists("maps"))
                Directory.CreateDirectory("maps");

            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Path.GetFullPath("maps");
            save.FileName = "new_map";
            save.AddExtension = true;
            save.DefaultExt = "map";
            save.Filter = "Map Data(*.map)|*.*";

            DialogResult result = save.ShowDialog();

            if(result == DialogResult.OK)
            {
                using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(save.FileName)))
                {
                    writer.Write((Int32)mapToSave.WidthInTiles);
                    writer.Write((Int32)mapToSave.HeightInTiles);

                    for(int x = 0; x < mapToSave.WidthInTiles; x++)
                    {
                        for(int y = 0; y < mapToSave.HeightInTiles; y++)
                        {
                            writer.Write((Int32)mapToSave[x,y]);
                        }
                    }
                }
            }
        }
        private static void LoadDialog()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Path.GetFullPath("maps");
            open.DefaultExt = "map";
            open.Filter = "Map Data(*.map)|*.*";
            DialogResult result = open.ShowDialog();

            if(result == DialogResult.OK)
            {
                string toLoad = open.FileName;

                using(BinaryReader reader = new BinaryReader(File.OpenRead(open.FileName)))
                {                    
                    int width = reader.ReadInt32( );
                    int height = reader.ReadInt32( );

                    loadedMap = new Map(new Point(width, height));

                    for(int x = 0; x < width; x++)
                    {
                        for(int y = 0; y < height; y++)
                        {
                            TileType tile = (TileType)reader.ReadInt32( );
                            loadedMap[x,y] = tile;
                        }
                    }
                }
            }
        }
    }
}
