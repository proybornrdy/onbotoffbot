using UnityEngine;

[System.Serializable]
public class SerializableV3 {
	public float x, y, z;

	public SerializableV3(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public SerializableV3(Vector3 v3)
	{
		x = v3.x;
		y = v3.y;
		z = v3.z;
	}
}
