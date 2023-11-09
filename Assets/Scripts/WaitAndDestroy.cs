using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndDestroy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        Color c = spriteRenderer.material.color;
        c.a = 0f;
        spriteRenderer.material.color = c;
    }

    private void Awake()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color= c;
            yield return new WaitForSeconds(0.05f); 
        }
        Destroy(gameObject);
    }
}
