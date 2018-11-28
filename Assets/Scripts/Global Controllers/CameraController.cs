using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 newPos;
    public Vector3 currentPos;

    private Vector3 cameraPos;

    //public GameObject[] rooms;
    private bool onlyonce = false;

    public void changeCameraPos(Room room)
    {
        currentPos = Camera.main.transform.position;
        Vector3 roomSize = new Vector3(0, 0, 0);
        Vector3 roomCenter = new Vector3(0, 0, 0);
        float[] lengths = new float[3 * room.GetComponents(typeof(Collider)).Length];

        int i = 0;
        foreach (Collider col in room.GetComponents(typeof(Collider)))
        {
            Vector3 colSize = col.bounds.size;
            Bounds colBound = col.bounds;
            Vector3 colCenter = colBound.center;
            colCenter.x = colCenter.x - colSize.x * 0.25f;
            colCenter.z = colCenter.z - colSize.z * 0.25f;
            roomCenter.x += colCenter.x;
            roomCenter.z += colCenter.z;
            roomCenter.y += colCenter.y;

            Debug.Log(colSize + " " + i);

            lengths[0 + i * 3] = colSize.x;
            lengths[1 + i * 3] = colSize.y;
            lengths[2 + i * 3] = colSize.z;
            i++;
            Debug.Log("length/?? " + lengths[0] + " " + lengths[1] + " " + lengths[2]);

        }
        roomCenter.x = roomCenter.x / room.GetComponents(typeof(Collider)).Length;
        roomCenter.y = roomCenter.y / room.GetComponents(typeof(Collider)).Length;
        roomCenter.z = roomCenter.z / room.GetComponents(typeof(Collider)).Length;







        float longSide = Mathf.Max(lengths);
        Debug.Log("longside check " + lengths[0] + " " + lengths[1] + " " + lengths[2]);

        float yAxis = Mathf.Tan(Camera.main.transform.eulerAngles.x) * Mathf.Sqrt(Mathf.Pow(longSide, 2f) * 2);

        cameraPos = new Vector3(roomCenter.x - longSide, roomCenter.y + yAxis, roomCenter.z - longSide);

        //this below line deals how much screen is zoomed. Bigger the multiplier, lesser the screen zoomed.
        Camera.main.orthographicSize = longSide * 0.8f;

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
