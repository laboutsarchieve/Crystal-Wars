package view;

import org.lwjgl.util.Point;

import data.TileMap;
import data.TileType;

public class WaterLogic {
	public static Texture GetTextureForWaterAt(TileMap map, Point position) {
        Texture waterTexture = TextureRepository.GetWater(WaterEdgeType.Center);

        // Surrounded by land
        if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
           !WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
           !WaterIsAt(map, new Point(position.getX(), position.getY() + 1)) &&
           !WaterIsAt(map, new Point(position.getX(), position.getY() - 1)))
            waterTexture = TextureRepository.GetWater(WaterEdgeType.WaterIsland);

        // Triple Boarder
        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() + 1)) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Sticking out left
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LeftChannelEnd);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() + 1)) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Sticking out right
            waterTexture = TextureRepository.GetWater(WaterEdgeType.RightChannelEnd);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Sticking out top
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerChannelEnd);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() + 1))) // Sticking out bottom
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperChannelEnd);


        // Double Boarder
        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX() + 1, position.getY()))) // Land to the left and right
            waterTexture = TextureRepository.GetWater(WaterEdgeType.ChannelVertical);

        else if(!WaterIsAt(map, new Point(position.getX(), position.getY() + 1)) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Land to the top and botom
            waterTexture = TextureRepository.GetWater(WaterEdgeType.ChannelHorizontal);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() + 1))) // Land to the left and top
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperLeft);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Land to the left and bottom
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerLeft);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() + 1))) // Land to the right and top
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperRight);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY())) &&
                !WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Land to the right and bottom
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerRight);

        // Single Boarder
        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY()))) // Land to the left
            waterTexture = TextureRepository.GetWater(WaterEdgeType.MiddleLeft);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY()))) // Land to the right
            waterTexture = TextureRepository.GetWater(WaterEdgeType.MiddleRight);

        else if(!WaterIsAt(map, new Point(position.getX(), position.getY() - 1))) // Land to the bottom
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerMiddle);

        else if(!WaterIsAt(map, new Point(position.getX(), position.getY() + 1))) // Land to the top
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperMiddle);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY() + 1))) // Land on the upper left diagonal
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperLeftTip);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY() + 1))) // Land on the upper right diagonal
            waterTexture = TextureRepository.GetWater(WaterEdgeType.UpperRightTip);

        else if(!WaterIsAt(map, new Point(position.getX() - 1, position.getY() - 1))) // Land on the lower left diagonal
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerLeftTip);

        else if(!WaterIsAt(map, new Point(position.getX() + 1, position.getY() - 1))) // Land on the lower right diagonal
            waterTexture = TextureRepository.GetWater(WaterEdgeType.LowerRightTip);

        return waterTexture;
    }

    private static boolean WaterIsAt(TileMap map, Point position)
    {
        if(!map.isOnMap(position))
            return true;
        else
            return map.getTileAt(position.getX(), position.getY()) == TileType.Water;
    }

}
