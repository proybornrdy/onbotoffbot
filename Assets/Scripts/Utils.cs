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

    public static bool InRange(Vector3 onPlayerPos, Vector3 buttonPos)
    {
        return System.Math.Pow(onPlayerPos.x - buttonPos.x, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.y - buttonPos.y, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.z - buttonPos.z, 2) <= 1;
    }
}
