using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tags : MonoBehaviour {

    public Tag[] tags;

    public bool HasTag(Tag s)
    {
        return tags.Any(t => t == s);
    }
}
