using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Toggleable {
    public bool on = false;
    public GameObject beltFront;
    public GameObject beltMiddle;
    public GameObject beltBack;
    public float scrollSpeed = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            float offset = scrollSpeed * Time.deltaTime;
            Renderer mr = beltMiddle.GetComponent<Renderer>();
            string[] texProperties = mr.material.GetTexturePropertyNames();
            for (int i = 0; i < texProperties.Length; i++)
            {
                Vector2 pos = mr.material.GetTextureOffset(texProperties[i]);
                mr.material.SetTextureOffset(texProperties[i], pos + (Vector2.left * offset));
            }
        }
	}

    public override void TurnOn()
    {
        on = true;
    }

    public override void TurnOff()
    {
        on = false;
    }

    public override bool IsOn()
    {
        return on;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!on || !other.gameObject.GetComponent<Tags>()) return;
        if (other.gameObject.GetComponent<Tags>().HasTag("Pushable"))
        {
            Vector3 direction = (beltFront.transform.position - beltBack.transform.position).normalized;
            //other.gameObject.GetComponent<Rigidbody>().AddForce(scrollSpeed * direction * Time.deltaTime, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Rigidbody>().velocity = scrollSpeed * direction * Time.deltaTime * 20;

        }
    }
}
