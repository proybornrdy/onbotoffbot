using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    int playerOnEnters = 0;
    int playerOffEnters = 0;
    LevelController lc;
    public bool faded = false;
    public Door door;
    public GameObject floor;
    public GameObject wallsToHide;
    public GameObject walls;
    public GameObject cutawayWalls;
    public GameObject backtrackBlocker;

    public GameObject onBotSpawnPoint;
    public GameObject offBotSpawnPoint;

    void Awake()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    private void Start()
    {
        GameObject.Find("TagCatalogue").GetComponent<TagCatalogue>().UpdateCatalogue(transform);
        var jumpPoints = TagCatalogue.FindAllWithTag(Tag.JumpPoint);
        foreach (var j in jumpPoints) j.gameObject.GetComponent<Renderer>().enabled = false;
        var colliders = TagCatalogue.FindAllWithTag(Tag.Collider);
        foreach (var j in colliders) j.gameObject.GetComponent<Renderer>().enabled = false;
        var spawnPoints = TagCatalogue.FindAllWithTag(Tag.SpawnPoint);
        foreach (var j in spawnPoints) j.gameObject.GetComponent<Renderer>().enabled = false;
        if (backtrackBlocker) backtrackBlocker.SetActive(false);
    }

    public void HideBlockingWalls()
    {
        if (wallsToHide) wallsToHide.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.Player))
        {
            Debug.Log("the room being triggered = " + gameObject.name);
            if (other.gameObject.HasTag(Tag.PlayerOn))
            {
                playerOnEnters++;
                other.gameObject.GetComponent<PlayerOn>().playerCurrentRoom = gameObject.name;
                other.gameObject.GetComponent<PlayerOn>().playerRoomCheck = gameObject.name;
            }
                
            else if (other.gameObject.HasTag(Tag.PlayerOff))
            {
                playerOffEnters++;
                other.gameObject.GetComponent<PlayerOff>().playerCurrentRoom = gameObject.name;
                other.gameObject.GetComponent<PlayerOff>().playerRoomCheck = gameObject.name;
            }
                

            if (playerOnEnters > 0 && playerOffEnters > 0)
            {
                if (door) PlayersInRoom();
                else lc.PlayersMovedToRoom(-1);
            }
        }
    }

    private void PlayersInRoom()
    {
        if (wallsToHide) wallsToHide.SetActive(true);
        if (door) lc.PlayersMovedToRoom(door.index);
    }

    public void ActivateBacktrackBlocker()
    {
        backtrackBlocker.SetActive(true);
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
        Light[] lights = GetComponentsInChildren<Light>();
        if (!fadingOut)
        {
            foreach (var r in rends)
                if (!r.gameObject.HasTag(Tag.JumpPoint) && !r.gameObject.HasTag(Tag.Collider) && !r.gameObject.HasTag(Tag.SpawnPoint))
                    r.enabled = true;
            foreach (Light l in lights) l.enabled = true;
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
            foreach (Light l in lights) l.enabled = false;

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
