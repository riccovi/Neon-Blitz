using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        if (tmp == null){
            tmp = GetComponent<TextMeshProUGUI>();
        }

        //https://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
        LeanTween.scale(tmp.gameObject, new Vector3(1.2f,1.2f,1f) // Scale the text by 1.2 in the x and y axis (and 1 for the z-axis).
        , 1f) // Duration is 1 second
        .setLoopPingPong();   // Continue the looping effect 
    }

}
