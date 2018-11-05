using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController instance;

    public AudioClip pistonOnSound;
    public AudioClip pistonOffSound;
    public AudioClip switchOnSound;
    public AudioClip switchOffSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playSoundEffect(string typeOfSound) {
        switch (typeOfSound) {
            case "pistonOn":
                audioSource.PlayOneShot(pistonOnSound);
                break;
            case "pistonOff":
                audioSource.PlayOneShot(pistonOffSound);
                break;
            case "DoorOn":
                audioSource.PlayOneShot(doorOpenSound);
                break;
            case "DoorOff":
                audioSource.PlayOneShot(doorCloseSound);
                break;
            case "SwitchOn":
                audioSource.PlayOneShot(switchOnSound);
                break;
            case "SwitchOff":
                audioSource.PlayOneShot(switchOffSound);
                break;
        }
    }
}
