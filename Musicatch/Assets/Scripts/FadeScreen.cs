using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    GameObject screenHider;
    Image screenHiderImage;

    float fadeDelay = 0.3f;
    //float waitDelay = 1.0f;
    float alpha;
    void Start()
    {
        screenHider = gameObject;
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
                //yield return new WaitForSeconds(waitDelay);
                break;
            }
        }
        gameObject.SetActive(false);
    }

    public void FadeWithScene(string sceneName)
    {
        StartCoroutine("FadeWithSceneSlow", sceneName);
    }

    IEnumerator FadeWithSceneSlow(string sceneName)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            screenHiderImage.color = new Color(0f, 0f, 0f, alpha);
            alpha += 1 / fadeDelay * 0.01f;
            if (alpha >= 1)
            {
                //yield return new WaitForSeconds(waitDelay);
                SceneManager.LoadScene(sceneName);
                break;
            }
        }
    }
}
