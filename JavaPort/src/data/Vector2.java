package data;

import org.lwjgl.util.Point;

public class Vector2 {
	private float x;
	private float y;

	public Vector2(float x, float y) {
		this.x = x;
		this.y = y;
	}
	public Vector2(Point sourcePoint) {
		x = sourcePoint.getX();
		y = sourcePoint.getY();
	}
	public Vector2(Vector2 source) {
		x = source.x;
		y = source.y;
	}

	public Vector2 add(Vector2 toAdd) {
		x += toAdd.x;
		y += toAdd.y;
		return this;
	}

	public Vector2 sub(Vector2 toSub) {
		x -= toSub.x;
		y -= toSub.y;
		return this;
	}

	public Vector2 scale(float scaleFactor) {
		x *= scaleFactor;
		y *= scaleFactor;
		return this;
	}

	public void face(Vector2 location) {
		float origLength = length();
		Vector2 toward = vectorSub(location, this);
		toward.normalize();
		Vector2 facingVector = vectorScale(toward, origLength);
		x = facingVector.x;
		y = facingVector.y;
	}

	public void setAngle(float angle) {
		rotate(angle - getAngle());
	}

	public void rotate(float rotation) {
		float length = length();
		float angle = getAngle();
		x = (length) * (float) Math.cos(angle + rotation);
		y = (length) * (float) Math.sin(angle + rotation);
	}

	public void limit(float maxLength) {
		if (lengthSq() > maxLength * maxLength) {
			normalize();
			scale(maxLength);
		}
	}

	public void normalize() {
		float length = length();
		if (length > 0) {
			scale(1 / length);
		}
	}

	public float dot(Vector2 otherVector) {
		return x * otherVector.x + y * otherVector.y;
	}

	public float getAngle() {
		return (float) Math.atan2(y, x);
	}

	public float angleBetween(Vector2 otherVector) {
		float dotProduct = dot(otherVector);
		float angle = (float) Math.acos(dotProduct / (length() * otherVector.length()));
		return angle;
	}

	public float length() {
		return (float) Math.sqrt(lengthSq());
	}

	public float lengthSq() {
		return x * x + y * y;
	}

	public float getX() {
		return x;
	}

	public void setX(float x) {
		this.x = x;
	}

	public float getY() {
		return y;
	}

	public void setY(float y) {
		this.y = y;
	}

	public static Vector2 getZero() {
		return new Vector2(0, 0);
	}

	public static Vector2 getUnitX() {
		return new Vector2(1, 0);
	}

	public static Vector2 getUnitY() {
		return new Vector2(0, 1);
	}

	public static Vector2 getOne() {
		return new Vector2(1, 1);
	}

	public static Vector2 vectorAdd(Vector2 v1, Vector2 v2) {
		return new Vector2(v1.x + v2.x, v1.y + v2.y);
	}

	public static Vector2 vectorSub(Vector2 v1, Vector2 v2) {
		return new Vector2(v1.x - v2.x, v1.y - v2.y);
	}

	public static Vector2 vectorScale(Vector2 v1, float scaleFactor) {
		return new Vector2(v1.x * scaleFactor, v1.y * scaleFactor);
	}

	public static Vector2 clamp(Vector2 vector, Vector2 min, Vector2 max) {
		Vector2 clampedVector = new Vector2(vector);

		if (clampedVector.x < min.x)
			clampedVector.x = min.x;

		if (clampedVector.y < min.y)
			clampedVector.y = min.y;

		if (clampedVector.x > max.x)
			clampedVector.x = max.x;

		if (clampedVector.y > max.y)
			clampedVector.y = max.y;

		return clampedVector;
	}
}