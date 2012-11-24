using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Data;
using GameTools.Noise2D;
using Shared.Data;

namespace MapEditor.Logic
{
    class MapGenerator
    {
        private FastPerlinNoise2D noiseMaker;
        private float waterLevel; 
        private float grassLevel;
        private float shortMountainLevel;
        private float treeLevel;

        public MapGenerator()
        {
            noiseMaker = new FastPerlinNoise2D(DefaultSettings);

            waterLevel = -0.3f;        
            grassLevel = 0.3f;        
            shortMountainLevel = 0.5f;
            treeLevel = 0.3f;
        }
        public Map GenerateMap(Point size)
        {
            Map map = new Map(size);
            noiseMaker.Settings.size = new Vector2((int)size.X, (int)size.Y);

            float[] heightNoise = new float[size.X * size.Y];
            float[] treeNoise = new float[size.X * size.Y];

            noiseMaker.FillWithPerlinNoise2D(heightNoise);
            noiseMaker.Settings.startingPoint = new Vector2(map.WidthInTiles, map.HeightInTiles);
            noiseMaker.FillWithPerlinNoise2D(treeNoise);
            MakeMapFromNoise(map, heightNoise, treeNoise);

            return map;
        }
        private void MakeMapFromNoise(Map map, float[] heightNoise, float[] treeNoise)
        {
            for(int x = 0; x < map.WidthInTiles; x++)
            {
                for(int y = 0; y < map.WidthInTiles; y++)
                {
                    int index = x * map.WidthInTiles + y % map.WidthInTiles;
                    TileType tile = GetTileFromNoise(heightNoise[index]);
                    if(HasTree(heightNoise[index], treeNoise[index]))
                        tile = TileType.Tree;

                    map[x, y] = tile;
                }
            }
        }
        private TileType GetTileFromNoise(float noise)
        {
            if(noise < waterLevel)
                return TileType.Water;
            else if(noise < grassLevel)
                return TileType.Grass;
            else if(noise < shortMountainLevel)
                return TileType.MountainShort;
            else
                return TileType.MountainTall;
        }
        private bool HasTree(float heightNoise, float treeNoise)
        {   
            treeNoise -= Math.Abs(heightNoise);

            return treeNoise > treeLevel;
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
        public float WaterLevel
        {
            get { return waterLevel; }
            set { waterLevel = value; }
        }
        public float GrassLevel
        {
            get { return grassLevel; }
            set { grassLevel = value; }
        }
        public float ShortMountainLevel
        {
            get { return shortMountainLevel; }
            set { shortMountainLevel = value; }
        }
        public float TreeLevel
        {
            get { return treeLevel; }
            set { treeLevel = value; }
        }

        public int Octaves { set { noiseMaker.Settings.octaves = value; } }        
        public float FrequencyMulti { set { noiseMaker.Settings.frequencyMulti = value; } }
        public float Persistence { set { noiseMaker.Settings.persistence = value; } }
        public float Zoom { set { noiseMaker.Settings.zoom = value; } }        
    }
}
