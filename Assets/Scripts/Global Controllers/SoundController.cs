using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController instance;

    public AudioClip pistonOnSound;
    public AudioClip pistonOffSound;

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
                audioSource.clip = pistonOnSound;
                break;
            case "pistonOff":
                audioSource.clip = pistonOffSound;
                break;
        }
        audioSource.Play();
    }
}
