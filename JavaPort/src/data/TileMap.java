package data;

import org.lwjgl.util.Point;

public class TileMap {
	private TileType[][] tiles;

	public TileMap(Point size) {
		tiles = new TileType[size.getX()][size.getY()];
	}

	public TileMap(int x, int y) {
		tiles = new TileType[x][y];
	}

	public void setAllTilesTo(TileType tile) {
		setTileInAreaTo(tile, new Point(0, 0), new Point(getWidthInTiles() - 1, getHeightInTiles() - 1));
	}

	public void setTileInAreaTo(TileType tile, Point upperLeft, Point lowerRight) {
		Point size = new Point(lowerRight.getX() - upperLeft.getX(), lowerRight.getY() - upperLeft.getY());

		for (int x = 0; x < size.getX() + 1; x++) {
			for (int y = 0; y < size.getY() + 1; y++) {
				tiles[upperLeft.getX() + x][upperLeft.getY() + y] = tile;
			}
		}
	}

	public boolean isOnMap(Point position) {
		return (position.getX() > -1 && position.getY() > -1 && position.getX() < getWidthInTiles() && position.getY() < getHeightInTiles());
	}

	public TileType getTileAt(int x, int y) {
		return tiles[x][y];
	}

	public TileType setTileAt(int x, int y, TileType tile) {
		return tiles[x][y] = tile;
	}

	public int getWidthInPixels() {
		return GlobalSettings.getScaledTileSize() * tiles.length;
	}

	public int getHeightInPixels() {
		return GlobalSettings.getScaledTileSize() * tiles[0].length;
	}

	public int getWidthInTiles() {
		return tiles.length;
	}

	public int getHeightInTiles() {
		return tiles[0].length;
	}
}