using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 newPos;
    public Vector3 currentPos;
    public Camera mainCamera;

    private Vector3 cameraPos = new Vector3(-7.0f, 8.5f, -7.0f); //fixed offset of the camera from the center of the level

    //public GameObject[] rooms;

    public void changeCameraPos(GameObject room)
    {
        currentPos = mainCamera.transform.position;

        Transform roomCollider = room.transform.Find("RoomCollider");




        Vector3 roomSize = roomCollider.GetComponent<Collider>().bounds.size;
        float zoomFactor;
        if (roomSize.y < 5) zoomFactor = roomSize.y;
        else zoomFactor = roomSize.y * 0.6f;
        mainCamera.orthographicSize = zoomFactor;
        Bounds parent = roomCollider.GetComponent<Collider>().bounds;

        Vector3 roomCenter = parent.center - new Vector3(0, parent.center.y, 0);

        //Debug.DrawLine(roomCenter, roomCenter + Vector3.up * 6, Color.red);


        //Renderer[] rends = room.GetComponentsInChildren<Renderer>();
        //if(rends.Length == 0)//error checking just in case if room element is empty gameobject
        //{
        //    newPos = room.transform.position;
        //}

        //Bounds parent = rends[0].bounds;
        //foreach (Renderer r in rends) //find center of the lelel and find suitable camera position from there
        //{
        //    parent.Encapsulate(r.bounds);
        //}

        newPos = roomCenter + new Vector3(roomSize.x * -0.5f, zoomFactor + 2f, roomSize.z * -0.5f);

        mainCamera.transform.position = Vector3.Lerp(currentPos, newPos, Time.deltaTime);
        //Debug.DrawLine(mainCamera.transform.position, newPos, Color.red);
    }
    public void zoomCamera(GameObject player1 , GameObject player2)
    {
        currentPos = mainCamera.transform.position;
        Vector3 player_1 = player1.transform.position;
        Vector3 player_2 = player2.transform.position;
        player_1 = player_1 * 0.5f;
        player_2 = player_2 * 0.5f;
        Vector3 newCamCenter = player_1 + player_2 +cameraPos ;
        mainCamera.transform.position = Vector3.Lerp(currentPos, newCamCenter, Time.deltaTime);
    }

}
