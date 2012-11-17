import input.ExtendedKeyboard;

import org.lwjgl.*;
import org.lwjgl.opengl.*;
import org.lwjgl.util.Point;
import org.lwjgl.input.*;

import data.GlobalSettings;
import data.TileMap;
import data.TileType;
import data.Vector2;

import view.Drawer;
import view.TextureRepository;

import static org.lwjgl.opengl.GL11.*;

public class MapEditor {

	private TileMap map;
	private Drawer drawer;
	
	public void start() {
		try {
			Display.setDisplayMode(new DisplayMode(800, 600));
			Display.create();
		} catch (LWJGLException e) {
			e.printStackTrace();
			System.exit(0);
		}
		
		map = new TileMap(new Point(100,100));
		
		map.setAllTilesTo(TileType.Grass);
		map.setTileAt(1, 1, TileType.Water);
		map.setTileAt(1, 2, TileType.Water);
		map.setTileAt(1, 3, TileType.Water);		
		map.setTileAt(2, 3, TileType.Water);
		map.setTileAt(2, 2, TileType.Water);
		map.setTileAt(3, 3, TileType.Water);
		map.setTileAt(3, 2, TileType.Water);
		map.setTileAt(3, 1, TileType.Water);

		drawer = new Drawer(map);

		glEnable(GL_TEXTURE_2D);

		TextureRepository.initalize();

		glMatrixMode(GL11.GL_PROJECTION);
		glLoadIdentity();
		glOrtho(0, 800, 0, 600, 1, -1);
		glMatrixMode(GL11.GL_MODELVIEW);

		while (!Display.isCloseRequested()) {
			pollInput();
			
			GL11.glClear(GL11.GL_COLOR_BUFFER_BIT | GL11.GL_DEPTH_BUFFER_BIT);

			drawer.Draw();
			Display.update();
			Display.sync(60);
		}

		Display.destroy();
	}

	private void pollInput() {
		ProcessMovementInput( );
	}
	
	private void ProcessMovementInput()
    {
        float MOVE_SCALE = GlobalSettings.getScaledTileSize( ) / 2;
        Vector2 movement = Vector2.getZero( );

        if(ExtendedKeyboard.IsKeyDown(Keyboard.KEY_W))
        	movement.add(Vector2.getUnitY());
        if(ExtendedKeyboard.IsKeyDown(Keyboard.KEY_A))
        	movement.add(Vector2.getUnitX().scale(-1));
        if(ExtendedKeyboard.IsKeyDown(Keyboard.KEY_S))
        	movement.add(Vector2.getUnitY().scale(-1));
        if(ExtendedKeyboard.IsKeyDown(Keyboard.KEY_D))
            movement.add(Vector2.getUnitX());

        movement.scale(MOVE_SCALE);
        drawer.MoveView(movement);
    }

	public static void main(String arg[]) {
		MapEditor game = new MapEditor();
		
		GlobalSettings.setResolution(new Vector2(800,600));
		GlobalSettings.setTileSize(32);
		GlobalSettings.setScale(1);
		
		game.start();
	}

}
