using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Toggleable {
    public bool on = false;
    public GameObject belt;
    public float scrollSpeed = 5f;
    public bool reverse;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            float offset = scrollSpeed * Time.deltaTime;
            Renderer mr = belt.GetComponent<Renderer>();
            string[] texProperties = mr.material.GetTexturePropertyNames();
            for (int i = 0; i < texProperties.Length; i++)
            {
                Vector2 pos = mr.material.GetTextureOffset(texProperties[i]);
                mr.material.SetTextureOffset(texProperties[i], pos + ((reverse ? Vector2.right : Vector2.left) * offset) * scrollSpeed / 20);
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
        if (!on) return;
        if (other.gameObject.HasTag(Tag.Pushable))
        {
            Vector3 direction = reverse?  Vector3.back : Vector3.forward;
            //other.gameObject.GetComponent<Rigidbody>().AddForce(scrollSpeed * direction * Time.deltaTime, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Rigidbody>().velocity = scrollSpeed * direction * Time.deltaTime * 20;

        }
    }
}
