using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour {

    void Start() {
        //FadeOut();
    }

    void FadeIn() {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine() {
        for (float i = GetComponent<CanvasGroup>().alpha; i < 1; i += (Time.deltaTime * 2)) {
            GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
    }

    void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine() {
        for (float i = GetComponent<CanvasGroup>().alpha; i > 0; i -= (Time.deltaTime * 2)) {
            GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
    }
}
