using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 175, 0);
    public float fadeTime = 0.5f;
    private float elapsedTime = 0f;
    private Color startColor;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private void Awake() 
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        elapsedTime += Time.deltaTime;

        if(elapsedTime < fadeTime)
        {
            float fadeAlpha = startColor.a * (1 - (elapsedTime / fadeTime));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
