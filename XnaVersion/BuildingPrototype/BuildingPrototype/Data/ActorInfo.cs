using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Data
{
    class ActorInfo
    {
        internal static Dictionary<TileType, int> GetTileCosts(ActorType actorType)
        {
            Dictionary<TileType,int> costs = new Dictionary<TileType,int>( );
            switch(actorType)
            {
                case ActorType.Dummy:
                    return costs;
                case ActorType.SoldierOne:
                {
                    costs.Add(TileType.Grass, 2);
                    costs.Add(TileType.Tree, 4);
                    costs.Add(TileType.MountainShort, 4);
                    costs.Add(TileType.MountainTall, 7);
                    return costs;
                }
                default:
                    return costs;

            }
        }
    }
}
