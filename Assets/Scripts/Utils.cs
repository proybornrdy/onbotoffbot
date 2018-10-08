using System;
using UnityEngine;

public static class Utils {

	/**
	Takes vector v and returns the nearest cardinal vector.  For ties, precedence is i > j > k
	*/
	public static Vector3 NearestCardinal(Vector3 v){
		return Math.Abs(v.x) >= Math.Abs(v.y) && Math.Abs(v.x) >= Math.Abs(v.z) ?
					new Vector3(v.x / Math.Abs(v.x), 0, 0) :
				Math.Abs(v.y) >= Math.Abs(v.x) && Math.Abs(v.y) >= Math.Abs(v.z) ?
					new Vector3(0, v.y / Math.Abs(v.y), 0) :
					new Vector3(0, 0, v.z / Math.Abs(v.z));
	}

	public static bool vectorEqual(Vector3 a, Vector3 b)
	{
		return a.x == b.x && a.y == b.y && a.z == b.z;
	}

	public static Vector3 closesCorner(GameObject query)
	{
		Vector3 globalPosition = query.GetComponent<Renderer>().bounds.min;
		globalPosition.x = (float)Math.Round((double)globalPosition.x);
		globalPosition.y = (float)Math.Round((double)globalPosition.y);
		globalPosition.z = (float)Math.Round((double)globalPosition.z);
		return globalPosition;
	}
}
