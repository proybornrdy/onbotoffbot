using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TagCatalogue : MonoBehaviour
{
    public static Dictionary<Tag, List<GameObject>> tagCatalogue;

    public static List<GameObject> FindAllWithTag(Tag t)
    {
        if (!tagCatalogue.ContainsKey(t)) Debug.LogError("No objects with tag " + t.ToString());
        return tagCatalogue[t];
    }

    public void Start()
    {
        //build catalogue

        var objs = FindObjectsOfType<GameObject>().Where(o => o.GetComponent<Tags>() != null);
        objs = objs.ToArray();
        foreach (var o in objs)
        {
            foreach (var t in o.GetComponent<Tags>().tags)
                tagCatalogue[t].Add(o);
        }
    }

    private void Awake()
    {
        tagCatalogue = new Dictionary<Tag, List<GameObject>>();
        foreach (Tag t in Enum.GetValues(typeof(Tag)))
            tagCatalogue[t] = new List<GameObject>();
    }


}
