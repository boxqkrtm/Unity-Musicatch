using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
    public void GoTitle()
    {
        GameObject music = GameObject.Find("Music");
        if (music != null)
        {
            Destroy(music);
        }
        GameObject.Find("Canvas").transform.Find("ScreenHider").gameObject.SetActive(true);
        GameObject.Find("ScreenHider").GetComponent<FadeScreen>().FadeWithScene("TitleScene");
    }
}
