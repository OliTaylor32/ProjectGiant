using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeIn()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0f, 0f, 0f, img.color.a - 0.01f);
        }
    }

    private IEnumerator FadeOut()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0f, 0f, 0f, img.color.a + 0.01f);
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
