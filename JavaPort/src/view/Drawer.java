package view;

import org.lwjgl.*;
import org.lwjgl.opengl.*;
import org.lwjgl.util.Point;
import org.lwjgl.util.vector.Vector2f;

import static org.lwjgl.opengl.GL11.*;

import data.*;

public class Drawer {
	private TileMap map;
	private Vector2 upperLeftOfView;

	public Drawer(TileMap map) {
		this.map = map;
		upperLeftOfView = Vector2.getZero();
	}

	public void CenterView(Vector2 position) {
		upperLeftOfView = Vector2.vectorSub(position, Vector2.vectorScale(GlobalSettings.getResolution(), 0.5f));
		MoveView(new Vector2(0, 0));
	}

	public void MoveView(Vector2 movement) {
		upperLeftOfView.add(movement);

		if (map != null) {
			Vector2 maxVector = new Vector2(map.getWidthInTiles() - 1, map.getHeightInTiles() - 1);
			maxVector.scale(GlobalSettings.getScaledTileSize());
			maxVector.sub(GlobalSettings.getResolution());

			upperLeftOfView = Vector2.clamp(upperLeftOfView, Vector2.getZero(), maxVector);
		}
	}

	public void Draw() {
		drawMap();
	}

	private void drawMap() {
		Vector2 upperLeftInTiles = Vector2.vectorScale(upperLeftOfView, 1.0f/GlobalSettings.getScaledTileSize());

		for (int x = 0; x < GlobalSettings.getTileResolution().getX() + 1
				&& upperLeftInTiles.getX() + x < map.getWidthInTiles(); x++) {
			for (int y = 0; y < GlobalSettings.getTileResolution().getY() + 1
					&& upperLeftInTiles.getY() + y < map.getHeightInTiles(); y++) {

				DrawTile(map.getTileAt((int) upperLeftInTiles.getX() + x, (int) upperLeftInTiles.getY() + y),
						new Point((int) upperLeftInTiles.getX() + x, (int) upperLeftInTiles.getY() + y));
			}
		}
	}

	private void DrawTile(TileType tileType, Point position) {
		if (tileType == TileType.Water)
			DrawWater(position);
		else if (tileType == TileType.MountainTall)
			DrawTallMountian(position);
		else
			DrawTextureAtMapLocation(TextureRepository.GetTileTexture(tileType), position);
	}

	private void DrawWater(Point position) {
		Texture waterTexture = WaterLogic.GetTextureForWaterAt(map, position);
		DrawTextureAtMapLocation(waterTexture, position);
	}

	private void DrawTallMountian(Point position) {
		Vector2 adjustedPosition = new Vector2(position).scale(GlobalSettings.getScaledTileSize());
		adjustedPosition.sub(Vector2.getUnitY().scale(GlobalSettings.getScaledTileSize() * 0.3f));
		adjustedPosition.sub(upperLeftOfView);

		DrawTexture(TextureRepository.GetTileTexture(TileType.MountainTall), adjustedPosition);
	}

	private void DrawTextureAtMapLocation(Texture tileTexture, Point position) {
		Vector2 drawLocation = new Vector2(position.getX(), position.getY()).scale(GlobalSettings.getScaledTileSize()).sub(upperLeftOfView);
		DrawTexture(tileTexture, drawLocation);
	}

	private void DrawTexture(Texture texture, Vector2 position) {
		DrawUnscaledTexture(texture, position, GlobalSettings.getScale()*texture.getWidth(), GlobalSettings.getScale()*texture.getHeight());
	}

	private void DrawUnscaledTexture(Texture texture, Vector2 position, float width, float height) {
		texture.bind();
		glBegin(GL_QUADS);

		glTexCoord2f(0.0f, 1.0f);
		glVertex2f(position.getX(), position.getY());

		glTexCoord2f(1.0f, 1.0f);
		glVertex2f(position.getX() + width, position.getY());

		glTexCoord2f(1.0f, 0.0f);
		glVertex2f(position.getX() + width, position.getY() + height);

		glTexCoord2f(0.0f, 0.0f);
		glVertex2f(position.getX(), position.getY() + height);

		glEnd();
	}
}
