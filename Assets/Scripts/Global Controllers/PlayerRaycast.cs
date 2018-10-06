using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

    public float rayDistance = 25;
    public Vector3 camPosition;
    public float alphaValue;
    private Color color;

    private List<GameObject> invisibleObjs = new List<GameObject>();

    private void Start()
    {
        camPosition = GameObject.FindWithTag("MainCamera").transform.position;

    }
    private void FixedUpdate()
    {
        Vector3 rayDirection = camPosition - transform.position;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit[] hits= Physics.RaycastAll(ray, rayDistance);
        Debug.DrawLine(transform.position, transform.position + rayDirection * rayDistance, Color.red);

        List<GameObject> currentInvisibleObjects = new List<GameObject>();

        foreach(RaycastHit hit in hits)
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.up * 5, Color.green);

            GameObject collidedObj = hit.collider.gameObject;
            color = collidedObj.GetComponent<Renderer>().material.color;
            color.a = alphaValue;
            collidedObj.GetComponent<Renderer>().material.color = color;


            if (!currentInvisibleObjects.Contains(collidedObj))
            {
                currentInvisibleObjects.Add(collidedObj);
            }
        }
        foreach (GameObject invisibleObj in invisibleObjs){
            if (!currentInvisibleObjects.Contains(invisibleObj))
            {
                color = invisibleObj.GetComponent<Renderer>().material.color;
                color.a = 1.0f;
                invisibleObj.GetComponent<Renderer>().material.color = color;
            }
        }
        invisibleObjs = currentInvisibleObjects;
    }
}
