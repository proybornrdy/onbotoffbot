using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public int count = 0;
    LevelController lc;

    public void Start()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        if (LevelController.startInStatic >= 6|| lc.startIn >= 6)
        {
            Destroy(GameObject.Find("sect1_clear"));
        }
        if (LevelController.startInStatic >= 12 || lc.startIn >= 12)
        {
            Destroy(GameObject.Find("sect2_clear"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "PlayerOn" || other.transform.name == "PlayerOff")
        {
            other.gameObject.SetActive(false);
            count++;
        }
        if (count == 2)
        {
            if (transform.name == "sect1_clear")
            {
                lc.sectionCleared(1, this.gameObject);
            }
            if (transform.name == "sect2_clear")
            {
                lc.sectionCleared(2, this.gameObject);
            }
        }
    }
}
