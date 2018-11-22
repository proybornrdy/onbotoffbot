using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CartTrack : Toggleable
{
    public float speed = 3f;
    public GameObject[] carts;
    public int[] ts;
    public GameObject[] keyPoints;
    public float[] pausePoints;
    bool on;
    int maxT;
    float currentT;
    bool paused = false;
    int currentPt;

    // Use this for initialization
    void Start () {
        maxT = keyPoints.Length;
        currentT = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (on && !paused)
        {
            var time = Time.deltaTime * speed;
            currentT += time;

            if (currentT > 1)
            {
                for (int i = 0; i < ts.Length; i++) {
                    ts[i]++;
                    if (ts[i] == maxT) ts[i] = 0;
                }
                currentT = 0;
            }

            for (int i = 0; i < ts.Length; i++)
            {
                Vector3 moveFrom = keyPoints[ts[i]].transform.position;
                Vector3 moveTo = keyPoints[(ts[i] + 1) % maxT].transform.position;
                
                carts[i].transform.position = Vector3.Lerp(moveFrom, moveTo, currentT);
                carts[i].transform.LookAt(moveTo, Vector3.up);
                if (!paused && currentT == 0 && pausePoints.Any(p => ts[i] == p))
                {
                    paused = true;
                    StartCoroutine("Pause");
                }
            }
        }
    }

    public override bool IsOn()
    {
        return on;
    }

    public override void TurnOff()
    {
        on = false;
    }

    public override void TurnOn()
    {
        on = true;
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        paused = false;
    }
}
