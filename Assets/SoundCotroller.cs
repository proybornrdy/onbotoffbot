using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCotroller : MonoBehaviour {
    public AudioClip onButtonSound;
    private AudioSource audioSource;

    public static SoundCotroller instance;

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
    public void playSoundOnButton() {
        audioSource .clip = onButtonSound;
        audioSource.Play();
    }
}
