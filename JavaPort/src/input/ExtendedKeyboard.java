package input;

import org.lwjgl.input.Keyboard;

public class ExtendedKeyboard {

	public static boolean IsKeyDown(int key) {
		return Keyboard.isKeyDown(key);
	}

}
