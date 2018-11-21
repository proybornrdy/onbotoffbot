using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    int playerOnEnters = 0;
    int playerOffEnters = 0;
    LevelController lc;
    TextTrigger text;
    public bool faded = false;
    public Door door;
    public GameObject floor;
    public GameObject wallsToHide;
    public GameObject walls;
    public GameObject cutawayWalls;

    void Awake()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    private void Start()
    {
        if (wallsToHide) wallsToHide.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.Player))
        {
            if (other.gameObject.HasTag(Tag.PlayerOn))
                playerOnEnters++;
            else if (other.gameObject.HasTag(Tag.PlayerOff))
                playerOffEnters++;

            if (playerOnEnters > 0 && playerOffEnters > 0)
            {
                if (door) PlayersInRoom();
                else lc.PlayersMovedToRoom(-1);
                if (text) text.TurnOn();
            }
        }
    }

    private void PlayersInRoom()
    {
        if (wallsToHide) wallsToHide.SetActive(true);
        lc.PlayersMovedToRoom(door.index);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(Tag.Player))
        {
            if (other.gameObject.HasTag(Tag.PlayerOn))
                playerOnEnters--;
            else if (other.gameObject.HasTag(Tag.PlayerOff))
                playerOffEnters--;

            if (playerOnEnters < 0) playerOnEnters = 0;
            if (playerOffEnters < 0) playerOffEnters = 0;
            if (playerOnEnters > 0 && playerOffEnters > 0)
            {
                if (text) text.TurnOff();
            }
        }
    }

    public void FadeIn()
    {
        SetRoomInvisible();
        StartCoroutine("Fade", false);
    }

    public void FadeOut()
    {
        StartCoroutine("Fade", true);
    }

    IEnumerator Fade(bool fadingOut)
    {
        /*Fade out : targetAlpha=0 < currentAlpha=1 (currentAlpha --0.1f)
        Fade in :  currentALpha=0 < targetAlpha=1 (currentAlpha ++0.1f)*/

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        if (!fadingOut)
        {
            foreach (var r in rends) r.enabled = true;
        }
        for (int i = 0; i < 100; i++)
        {

            foreach (Renderer r in rends)
            {
                changeMaterialModeToFadeMode(r);
                Color alpha = r.material.color;
                if (fadingOut)
                {
                    if (alpha.a > 0f) alpha.a -= 0.01f;
                    else alpha.a = 0.0f;
                }
                else
                {
                    if (alpha.a < 1)
                        alpha.a += 0.01f;
                }
                r.material.color = alpha;

            }
            yield return null;
        }

        if (fadingOut)
        {
            foreach (Renderer r in rends) r.enabled = false;

        }
        else /*since the faded out room needs to stay invisible require it to stay in Fade mode. So this only applies to room that is being faded in*/
        {
            foreach (Renderer r in rends) changeMaterialModeToOpaqueMode(r);
        }
    }


    private void SetRoomInvisible()
    {
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            changeMaterialModeToFadeMode(r);
            Color alpha = r.material.color;
            alpha.a = 0.0f;
            r.material.color = alpha;
        }
    }
    

    private void changeMaterialModeToFadeMode(Renderer rd)
    {

        rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        rd.material.SetInt("_ZWrite", 0);
        rd.material.EnableKeyword("_ALPHABLEND_ON");
    }


    private void changeMaterialModeToOpaqueMode(Renderer rd)
    {
        rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        rd.material.SetInt("_ZWrite", 1);
        rd.material.DisableKeyword("_ALPHABLEND_ON");
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
