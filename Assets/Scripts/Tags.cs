using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tags : MonoBehaviour {

    public List<string> tags;

    public bool HasTag(string s)
    {
        return tags.Any(t => t == s);
    }
}
