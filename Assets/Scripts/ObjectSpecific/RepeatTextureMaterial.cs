using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTextureMaterial : MonoBehaviour {
    public Coordinate textureXCoordinate = Coordinate.x;
    public Coordinate textureYCoordinate = Coordinate.z;

	// Use this for initialization
	void Start () {
        var renderer = GetComponent<Renderer>();
        float x;
        switch (textureXCoordinate)
        {
            case Coordinate.y:
                x = transform.lossyScale.y * 10;
                break;
            case Coordinate.z:
                x = transform.lossyScale.z * 10;
                break;
            default:
                x = transform.lossyScale.x * 10;
                break;
        }
        float y;
        switch (textureYCoordinate)
        {
            case Coordinate.x:
                y = transform.lossyScale.x * 10;
                break;
            case Coordinate.y:
                y = transform.lossyScale.y * 10;
                break;
            default:
                y = transform.lossyScale.z * 10;
                break;
        }
        renderer.material.mainTextureScale = new Vector2(x, y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
