using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorRandomizer : MonoBehaviour
{
    private TextMeshProUGUI letter;

    // Start is called before the first frame update
    void Start()
    {
        letter = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("ChangeColor", 0.0f, 1.0f);
    }

    void ChangeColor()
    {
        letter.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

}
