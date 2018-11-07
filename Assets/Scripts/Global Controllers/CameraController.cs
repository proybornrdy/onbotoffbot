using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 newPos;
    public Vector3 currentPos;
    public Camera mainCamera;

    private Vector3 cameraPos; 

    //public GameObject[] rooms;

    public void changeCameraPos(GameObject room)
    {
        currentPos = mainCamera.transform.position;

        Transform roomCollider = room.transform.Find("RoomCollider");
        Vector3 roomSize = roomCollider.GetComponent<Collider>().bounds.size;
        Bounds roomBound = roomCollider.GetComponent<Collider>().bounds;
        Vector3 roomCenter = roomBound.center;
        roomCenter.x = roomCenter.x -roomSize.x *0.25f;
        roomCenter.z = roomCenter.z - roomSize.z * 0.25f;

        float[] lengths = new float[3];
        lengths[0] = roomSize.x;
        lengths[1] = roomSize.y;
        lengths[2] = roomSize.z;
        
        float longSide = Mathf.Max(lengths);

        float yAxis = Mathf.Tan(mainCamera.transform.eulerAngles.x) * Mathf.Sqrt(Mathf.Pow(longSide,2f)*2);

        cameraPos = new Vector3(roomCenter.x - longSide, roomCenter.y+yAxis, roomCenter.z - longSide) ;
        
        mainCamera.orthographicSize = longSide*0.65f;

        Debug.Log("~~~~~"+ room.name);
        Debug.Log(roomSize);
        Debug.Log(longSide);
        Debug.Log(cameraPos);
        Debug.Log(roomCenter);
        Debug.Log(roomCenter*0.5f);


        mainCamera.transform.position = Vector3.Lerp(currentPos, cameraPos, Time.deltaTime);

        Debug.DrawLine(mainCamera.transform.position, roomCenter, Color.red);
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
