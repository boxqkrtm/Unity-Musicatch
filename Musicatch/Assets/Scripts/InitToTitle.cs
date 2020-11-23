using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitToTitle : MonoBehaviour
{
    GameObject screenHider;
    Image screenHiderImage;

    float fadeDelay = 0.5f;
    float waitDelay = 1.0f;
    float alpha;

    void Awake()
    {
        Application.targetFrameRate = 120;
    }

    void Start()
    {
        screenHider = GameObject.Find("ScreenHider");
        screenHiderImage = screenHider.GetComponent<Image>();
        alpha = 1f;
        StartCoroutine("fade");
    }

    void Update()
    {

    }

    IEnumerator fade()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            screenHiderImage.color = new Color(0f, 0f, 0f, alpha);
            alpha -= 1 / fadeDelay * 0.01f;
            if (alpha <= 0)
            {
                yield return new WaitForSeconds(waitDelay);
                break;
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            screenHiderImage.color = new Color(0f, 0f, 0f, alpha);
            alpha += 1 / fadeDelay * 0.01f;
            if (alpha >= 1f)
            {
                SceneManager.LoadScene("TitleScene");
                break;
            }
        }

    }
}
