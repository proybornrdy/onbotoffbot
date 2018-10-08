using System;
using UnityEngine;

public static class Utils {

	/**
	Takes vector v and returns the nearest cardinal vector.  For ties, precedence is i > j > k
	*/
	public static Vector3 NearestCardinal(Vector3 v){
        int largest = (int)LargestComponent(v);
        Vector3 cardinal = Vector3.zero;
        cardinal[largest] = Mathf.Sign(v[largest]);
        return cardinal;
	}

    public enum Coordinate { x, y, z }

    public static Coordinate LargestComponent(Vector3 v)
    {
        return
            Math.Abs(v.x) >= Math.Abs(v.y) && Math.Abs(v.x) >= Math.Abs(v.z) ? Coordinate.x :
            Math.Abs(v.y) >= Math.Abs(v.x) && Math.Abs(v.y) >= Math.Abs(v.z) ? Coordinate.y :
            Coordinate.z;
    }

    public static bool InRange(Vector3 onPlayerPos, Vector3 buttonPos)
    {
        return System.Math.Pow(onPlayerPos.x - buttonPos.x, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.y - buttonPos.y, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.z - buttonPos.z, 2) <= 1;
    }
    
    public static Vector3 NearestCubeCenter(Vector3 v)
    {
        return new Vector3(
            Mathf.Round(v.x),
            Mathf.Round(v.y),
            Mathf.Round(v.z)
            );
    }

	public static Vector3 closesCorner(GameObject query)
	{
		Vector3 globalPosition = query.GetComponent<Renderer>().bounds.min;
		globalPosition.x = (float)Math.Round((double)globalPosition.x);
		globalPosition.y = (float)Math.Round((double)globalPosition.y);
		globalPosition.z = (float)Math.Round((double)globalPosition.z);
		return globalPosition;
	}

    public static bool vectorEqual(Vector3 a, Vector3 b)
	{
		return a.x == b.x && a.y == b.y && a.z == b.z;
	}
}
