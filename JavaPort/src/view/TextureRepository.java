package view;

import java.io.*;
import java.nio.ByteBuffer;

import data.TileType;
import de.matthiasmann.twl.utils.PNGDecoder;
import de.matthiasmann.twl.utils.PNGDecoder.Format;
import static org.lwjgl.opengl.GL11.*;

public class TextureRepository {
	private static int nextTextureNumber = 1;

	private static Texture mountianShort;
	private static Texture mountianTall;
	private static Texture forest;
	private static Texture grass;

	private static Texture waterCenter;
	private static Texture waterLowerLeft;
	private static Texture waterLowerMiddle;
	private static Texture waterLowerRight;
	private static Texture waterMiddleLeft;
	private static Texture waterMiddleRight;
	private static Texture waterUpperLeft;
	private static Texture waterUpperMiddle;
	private static Texture waterUpperRight;
	private static Texture waterLowerChannelEnd;
	private static Texture waterUpperChannelEnd;
	private static Texture waterRightChannelEnd;
	private static Texture waterLeftChannelEnd;

	private static Texture waterChannelHorizontal;
	private static Texture waterChannelVertical;

	private static Texture waterIsland;
	private static Texture waterUpperRightTip;
	private static Texture waterUpperLeftTip;
	private static Texture waterLowerRightTip;
	private static Texture waterLowerLeftTip;

	public static void initalize() {
		grass = bindTexture("Art/Tiles/AW/AWgrass.png");
		mountianShort = bindTexture("Art/Tiles/AW/AWmountianShort.png");
		mountianTall = bindTexture("Art/Tiles/AW/AWmountianTall.png");
		forest = bindTexture("Art/Tiles/AW/AWforest.png");

		waterIsland = bindTexture("Art/Tiles/AW/Water/AWWaterIsland.png");
		waterCenter = bindTexture("Art/Tiles/AW/Water/AWWaterCenter.png");
		waterLowerLeft = bindTexture("Art/Tiles/AW/Water/AWWaterLowerLeft.png");
		waterLowerMiddle = bindTexture("Art/Tiles/AW/Water/AWWaterLowerMiddle.png");
		waterLowerRight = bindTexture("Art/Tiles/AW/Water/AWWaterLowerRight.png");
		waterMiddleLeft = bindTexture("Art/Tiles/AW/Water/AWWaterMiddleLeft.png");
		waterMiddleRight = bindTexture("Art/Tiles/AW/Water/AWWaterMiddleRight.png");
		waterUpperLeft = bindTexture("Art/Tiles/AW/Water/AWWaterUpperLeft.png");
		waterUpperMiddle = bindTexture("Art/Tiles/AW/Water/AWWaterUpperMiddle.png");
		waterUpperRight = bindTexture("Art/Tiles/AW/Water/AWWaterUpperRight.png");

		waterLowerChannelEnd = bindTexture("Art/Tiles/AW/Water/AWWaterLowerChannelEnd.png");
		waterUpperChannelEnd = bindTexture("Art/Tiles/AW/Water/AWWaterUpperChannelEnd.png");
		waterRightChannelEnd = bindTexture("Art/Tiles/AW/Water/AWWaterRightChannelEnd.png");
		waterLeftChannelEnd = bindTexture("Art/Tiles/AW/Water/AWWaterLeftChannelEnd.png");

		waterChannelVertical = bindTexture("Art/Tiles/AW/Water/AWWaterChannelVertical.png");
		waterChannelHorizontal = bindTexture("Art/Tiles/AW/Water/AWWaterChannelHorizontal.png");

		waterUpperRightTip = bindTexture("Art/Tiles/AW/Water/AWWaterUpperRightTip.png");
		;
		waterUpperLeftTip = bindTexture("Art/Tiles/AW/Water/AWWaterUpperLeftTip.png");
		;
		waterLowerRightTip = bindTexture("Art/Tiles/AW/Water/AWWaterLowerRightTip.png");
		;
		waterLowerLeftTip = bindTexture("Art/Tiles/AW/Water/AWWaterLowerLeftTip.png");
	}

	public static Texture GetTileTexture(TileType tile) {
		switch (tile) {
		case Grass:
			return grass;
		case MountainShort:
			return mountianShort;
		case MountainTall:
			return mountianTall;
		case Forest:
			return forest;
		case Water:
			return waterCenter;
		default:
			return new Texture(0, 0, 0);
		}
	}

	public static Texture GetWater(WaterEdgeType edgeType) {
		switch (edgeType) {
		case LowerLeft:
			return waterLowerLeft;
		case LowerMiddle:
			return waterLowerMiddle;
		case LowerRight:
			return waterLowerRight;
		case MiddleLeft:
			return waterMiddleLeft;
		case MiddleRight:
			return waterMiddleRight;
		case UpperLeft:
			return waterUpperLeft;
		case UpperMiddle:
			return waterUpperMiddle;
		case UpperRight:
			return waterUpperRight;
		case Center:
			return waterCenter;
		case LeftChannelEnd:
			return waterLeftChannelEnd;
		case RightChannelEnd:
			return waterRightChannelEnd;
		case UpperChannelEnd:
			return waterUpperChannelEnd;
		case LowerChannelEnd:
			return waterLowerChannelEnd;
		case ChannelHorizontal:
			return waterChannelHorizontal;
		case ChannelVertical:
			return waterChannelVertical;
		case WaterIsland:
			return waterIsland;

		case LowerLeftTip:
			return waterLowerLeftTip;
		case LowerRightTip:
			return waterLowerRightTip;
		case UpperLeftTip:
			return waterUpperLeftTip;
		case UpperRightTip:
			return waterUpperRightTip;

		default:
			return new Texture(0, 0, 0);
		}
	}

	private static Texture bindTexture(String fileName) {

		int currentTextureNumber = nextTextureNumber;
		nextTextureNumber++;
		try {
			InputStream in = new FileInputStream(fileName);
			PNGDecoder decoder = new PNGDecoder(in);

			int imageDataSize = 4 * decoder.getWidth() * decoder.getHeight();
			ByteBuffer imageDataBuffer = ByteBuffer.allocateDirect(imageDataSize);
			decoder.decode(imageDataBuffer, 4 * decoder.getWidth(), Format.RGBA);
			imageDataBuffer.flip();
			in.close();

			glBindTexture(GL_TEXTURE_2D, currentTextureNumber);
			glPixelStorei(GL_UNPACK_ALIGNMENT, 1);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

			glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);

			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, decoder.getWidth(), decoder.getHeight(), 0, GL_RGBA,
					GL_UNSIGNED_BYTE, imageDataBuffer);

			return new Texture(currentTextureNumber, decoder.getWidth(), decoder.getHeight());

		} catch (Exception ex) {
			System.out.println("Error Loading texture named " + fileName);
			ex.printStackTrace();

			return new Texture(0, 0, 0);
		}

	}
}
