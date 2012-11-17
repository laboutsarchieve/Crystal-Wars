package view;

import static org.lwjgl.opengl.GL11.GL_TEXTURE_2D;
import static org.lwjgl.opengl.GL11.glBindTexture;

public class Texture {
	private int glNumber;
	private int width;
	private int height;

	public Texture(int glNumber, int width, int height) {
		this.glNumber = glNumber;
		this.width = width;
		this.height = height;
	}

	public void bind() {
		glBindTexture(GL_TEXTURE_2D, glNumber);
	}

	public int getGlNumber() {
		return glNumber;
	}

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}
}
