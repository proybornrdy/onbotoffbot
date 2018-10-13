using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 newPos;
    public Vector3 currentPos;
    public GameObject mainCamera;

    private Vector3 cameraPos = new Vector3(-7.0f, 8.5f, -7.0f); //fixed offset of the camera from the center of the level

    //public GameObject[] rooms;

    public void changeCameraPos(GameObject room)
    {
        currentPos = mainCamera.transform.position;

        Renderer[] rends = room.GetComponentsInChildren<Renderer>();
        if(rends.Length == 0)//error checking just in case if room element is empty gameobject
        {
            newPos = room.transform.position;
        }

        Bounds parent = rends[0].bounds;
        foreach(Renderer r in rends) //find center of the lelel and find suitable camera position from there
        {
            parent.Encapsulate(r.bounds);
        }

        newPos = parent.center + cameraPos;  
        mainCamera.transform.position = Vector3.Lerp(currentPos, newPos, Time.deltaTime);
    }

}
