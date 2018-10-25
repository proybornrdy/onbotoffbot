using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour {
    public float fadeSpeed = 2;
    private bool isFading = false;

    public void FadeIn() {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine() {
        if (!isFading) {
            while (GetComponent<CanvasGroup>().alpha < 1) {
                isFading = true;
                GetComponent<CanvasGroup>().alpha += Time.deltaTime * fadeSpeed;
                Debug.Log("a");
                yield return null;
            }
            isFading = false;
        }
    }

    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine() {
        if (!isFading) {
            while (GetComponent<CanvasGroup>().alpha > 0) {
                isFading = true;
                GetComponent<CanvasGroup>().alpha -= Time.deltaTime * fadeSpeed;
                Debug.Log("b");
                yield return null;
            }
            isFading = false;
        }
    }
}
