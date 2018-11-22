using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class RepeatTextureMaterial : MonoBehaviour {
    public Coordinate textureXCoordinate = Coordinate.x;
    public Coordinate textureYCoordinate = Coordinate.z;
    public int scaleX = 1;
    public int scaleY = 1;
    public float offsetX = 0;
    public float offsetY = 0;

	// Use this for initialization
	void Start () {
        var renderer = GetComponent<Renderer>();
        float x;
        switch (textureXCoordinate)
        {
            case Coordinate.y:
                x = transform.lossyScale.y * 10 / scaleX;
                break;
            case Coordinate.z:
                x = transform.lossyScale.z * 10 / scaleX;
                break;
            default:
                x = transform.lossyScale.x * 10 / scaleX;
                break;
        }
        float y;
        switch (textureYCoordinate)
        {
            case Coordinate.x:
                y = transform.lossyScale.x * 10 / scaleY;
                break;
            case Coordinate.y:
                y = transform.lossyScale.y * 10 / scaleY;
                break;
            default:
                y = transform.lossyScale.z * 10 / scaleY;
                break;
        }
        renderer.material.mainTextureScale = new Vector2(x, y);
        renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
	
	// Update is called once per frame
	void OnValidate ()
    {

        var renderer = GetComponent<Renderer>();
        float x;
        switch (textureXCoordinate)
        {
            case Coordinate.y:
                x = transform.lossyScale.y * 10 / scaleX;
                break;
            case Coordinate.z:
                x = transform.lossyScale.z * 10 / scaleX;
                break;
            default:
                x = transform.lossyScale.x * 10 / scaleX;
                break;
        }
        float y;
        switch (textureYCoordinate)
        {
            case Coordinate.x:
                y = transform.lossyScale.x * 10 / scaleY;
                break;
            case Coordinate.y:
                y = transform.lossyScale.y * 10 / scaleY;
                break;
            default:
                y = transform.lossyScale.z * 10 / scaleY;
                break;
        }
        renderer.material.mainTextureScale = new Vector2(x, y);
        renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
