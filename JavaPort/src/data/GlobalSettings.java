package data;

import org.lwjgl.util.vector.Vector2f;

public class GlobalSettings {
	private static Vector2 resolution;
	private static int tileSize;
	private static float scale = 1.0f;

	public static Vector2 getResolution() {
		return resolution;
	}

	public static void setResolution(Vector2 resolution) {
		GlobalSettings.resolution = resolution;
	}

	public static int getTileSize() {
		return tileSize;
	}

	public static void setTileSize(int tileSize) {
		GlobalSettings.tileSize = tileSize;
	}

	public static float getScale() {
		return scale;
	}

	public static void setScale(float scale) {
		GlobalSettings.scale = Math.max(0.1f, scale);
	}

	public static int getScaledTileSize() {
		return Math.max(1, (int) (scale * GlobalSettings.tileSize));
	}

	public static Vector2 getTileResolution() {
		return Vector2.vectorScale(resolution, 1.0f / getScaledTileSize());

	}
}