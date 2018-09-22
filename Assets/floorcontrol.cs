using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorcontrol : MonoBehaviour {

    public GameObject floor;
    public int width;
    public int length;

	// Use this for initialization
	void Start () {
        for (int x = 0; x < width; x++){
            for (int y = 0; y < length; y++){
                Instantiate(floor, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
