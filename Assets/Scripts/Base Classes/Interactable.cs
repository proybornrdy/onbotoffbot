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
        glowShader = Shader.Find("Custom/SelectionGlow");
        EventManager.OnInteract += OnInteract;
    }

    public void Select(GameObject player)
    {
        if (selectedBy != null) return;
        selectedBy = player;
        r.material.shader = glowShader;
    }

    public void Deselect()
    {
        if (selectedBy == null) return;

        selectedBy = null;
        r.material.shader = initShader;
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
