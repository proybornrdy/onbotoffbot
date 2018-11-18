using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 newPos;
    public Vector3 currentPos;

    private Vector3 cameraPos; 

    //public GameObject[] rooms;

    public void changeCameraPos(Room room)
    {
        currentPos = Camera.main.transform.position;
        
        Vector3 roomSize = room.GetComponent<Collider>().bounds.size;
        Bounds roomBound = room.GetComponent<Collider>().bounds;
        Vector3 roomCenter = roomBound.center;
        roomCenter.x = roomCenter.x -roomSize.x *0.25f;
        roomCenter.z = roomCenter.z - roomSize.z * 0.25f;

        float[] lengths = new float[3];
        lengths[0] = roomSize.x;
        lengths[1] = roomSize.y;
        lengths[2] = roomSize.z;
        
        float longSide = Mathf.Max(lengths);

        float yAxis = Mathf.Tan(Camera.main.transform.eulerAngles.x) * Mathf.Sqrt(Mathf.Pow(longSide,2f)*2);

        cameraPos = new Vector3(roomCenter.x - longSide, roomCenter.y+yAxis, roomCenter.z - longSide) ;

        //this below line deals how much screen is zoomed. Bigger the multiplier, lesser the screen zoomed.
        Camera.main.orthographicSize = longSide*0.8f;

        //Debug.Log("~~~~~"+ room.name);
        //Debug.Log(roomSize);
        //Debug.Log(longSide);
        //Debug.Log(cameraPos);
        //Debug.Log(roomCenter);
        //Debug.Log(roomCenter*0.5f);


        Camera.main.transform.position = Vector3.Lerp(currentPos, cameraPos, Time.deltaTime);

        Debug.DrawLine(Camera.main.transform.position, roomCenter, Color.red);
    }


    //public void zoomCamera(GameObject player1 , GameObject player2)
    //{
    //    currentPos = Camera.main.transform.position;
    //    Vector3 player_1 = player1.transform.position;
    //    Vector3 player_2 = player2.transform.position;
    //    player_1 = player_1 * 0.5f;
    //    player_2 = player_2 * 0.5f;
    //    Vector3 newCamCenter = player_1 + player_2 +cameraPos ;
    //    Camera.main.transform.position = Vector3.Lerp(currentPos, newCamCenter, Time.deltaTime);
    //}

}
