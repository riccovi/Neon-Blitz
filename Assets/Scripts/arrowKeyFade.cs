using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowKeyFade : MonoBehaviour
{   
    public Image arrowKeyImage; 
    public float fadeDuration = 2f; 
    public float showDuration = 2f; 
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = arrowKeyImage.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        StartCoroutine(FadeOutHint());
    }

    private IEnumerator FadeOutHint()
    {
        yield return new WaitForSeconds(showDuration);

        float timeElapsed = 0;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
