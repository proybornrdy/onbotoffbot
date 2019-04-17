using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Interactable : MonoBehaviour
{
    Shader initShader;
    Shader glowShader;
    Renderer r;
    GameObject selectedBy = null;

    public delegate void Action(GameObject player);

    public Action InteractAction;

    // Use this for initialization
    void Start () {
        r = GetComponent<Renderer>();
        initShader = r.material.shader;
        //glowShader = Shader.Find("OutlineShader");
        glowShader = Shader.Find("SelectionGlow");
        EventManager.OnInteract += OnInteract;

    }

    public void Select(GameObject player)
    {
        if (selectedBy != null) return;
        selectedBy = player;
        GetComponent<Renderer>().material.shader = glowShader;
        //r.material.SetColor("_OutlineColor", Color.white);
    }

    public void Deselect()
    {
        if (selectedBy == null) return;

        selectedBy = null;
        GetComponent<Renderer>().material.shader = initShader;
    }

    public void OnInteract(GameObject player)
    {
        if (selectedBy == player)
            InteractAction(player);
    }

    public GameObject SelectedBy()
    {
        return selectedBy;
    }
}
