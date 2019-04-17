using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failproof : MonoBehaviour
{

    public GameObject crate;
    public Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = crate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == crate.transform.name)
        {
            crate.transform.position = spawn;

        }
    }
}
