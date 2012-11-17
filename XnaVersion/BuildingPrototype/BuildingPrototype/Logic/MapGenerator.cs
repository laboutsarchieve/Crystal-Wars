using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Data;
using GameTools.Noise2D;


namespace MapEditor.Logic
{
    class MapGenerator
    {
        private FastPerlinNoise2D noiseMaker;

        public MapGenerator()
        {
            noiseMaker = new FastPerlinNoise2D(DefaultSettings);
        }
        public Map GenerateMap(Point size)
        {
            Map map = new Map(size);
            noiseMaker.Settings.size = new Vector2((int)size.X, (int)size.Y);

            float[] noiseArray = new float[size.X * size.Y];

            noiseMaker.FillWithPerlinNoise2D(noiseArray);
            MakeMapFromNoise(map, noiseArray);

            return map;
        }
        private void MakeMapFromNoise(Map map, float[] noiseArray)
        {
            for(int x = 0; x < map.WidthInTiles; x++)
            {
                for(int y = 0; y < map.WidthInTiles; y++)
                {
                    int index = x * map.WidthInTiles + y % map.WidthInTiles;
                    TileType tile = GetTileFromNoise(noiseArray[index]);
                    map[x, y] = tile;
                }
            }
        }
        private TileType GetTileFromNoise(float noise)
        {
            if(noise < -0.3)
                return TileType.Water;
            else if(noise < 0.3)
                return TileType.Grass;
            else if(noise < 0.5)
                return TileType.MountainShort;
            else
                return TileType.MountainTall;
        }
        private PerlinNoiseSettings2D DefaultSettings
        {
            get
            {
                PerlinNoiseSettings2D settings = new PerlinNoiseSettings2D();
                settings.frequencyMulti = 2.0f;
                settings.persistence = 0.5f;
                settings.octaves = 6;
                settings.zoom = 10;
                return settings;
            }
        }
    }
}
