using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloorBlocks : MonoBehaviour {
    public GameObject plane;
    public GameObject floorBlock;

    public void Generate()
    {
        Vector3 position = plane.transform.position;
        Vector3 scale = plane.transform.localScale;
        for (int i = 0; i < (int)scale.x; i++)
        {
            for (int j = 0; j < (int)scale.z; j++)
            {
                for (int k = 0; k < (int)scale.y; k++)
                {
                    Instantiate(floorBlock, position + new Vector3(i - (scale.x / 2) + 0.5f, k - (scale.y / 2) + 0.5f, j - (scale.z / 2) + 0.5f), new Quaternion());
                }
            }
        }
        DestroyImmediate(plane);
    }
}
