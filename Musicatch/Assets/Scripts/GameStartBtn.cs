using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartBtn : MonoBehaviour
{
    public static bool isCloseAll = false;
    public AudioClip song;
    public string mapData;
    //
    string mapTimingsStr;
    string mapLocationsStr;
    float[] mapTimings;
    float[] mapLocations;
    public bool isOpen = false;
    public void StartBtn()
    {

        if (isOpen == true)
        {
            GameObject.Find("SE").GetComponent<SE>().start();
            mapTimingsStr = mapData.Split('A')[0];
            mapLocationsStr = mapData.Split('A')[1];
            int i = 0;
            mapTimings = new float[mapTimingsStr.Split(',').Length];
            foreach (var word in mapTimingsStr.Split(','))
            {
                mapTimings[i] = float.Parse(word);
                i++;
            }
            i = 0;
            mapLocations = new float[mapLocationsStr.Split(',').Length];
            foreach (var word in mapLocationsStr.Split(','))
            {
                mapLocations[i] = float.Parse(word);
                i++;
            }
            GameObject.Find("MapData").GetComponent<MapData>().SetData(song, mapTimings, mapLocations);
            GameObject.Find("Canvas").transform.Find("ScreenHider").gameObject.SetActive(true);
            GameObject.Find("ScreenHider").GetComponent<FadeScreen>().FadeWithScene("IngameScene");
        }
        else
        {
            GameObject.Find("SE").GetComponent<SE>().select();
            StartCoroutine("OpenOnlyThis");
            GameObject.Find("SoundPreview").GetComponent<AudioSource>().clip = song;
            GameObject.Find("SoundPreview").GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator OpenOnlyThis()
    {
        isCloseAll = true;
        yield return new WaitForSeconds(0.1f);
        isCloseAll = false;
        yield return new WaitForSeconds(0.1f);
        isOpen = true;
    }

    void Update()
    {
        if (isCloseAll == true)
        {
            isOpen = false;
        }
        if (isOpen == true)
        {
            GameObject parent = transform.parent.gameObject;
            GameObject info = parent.transform.Find("info").gameObject;
            Vector3 target = gameObject.transform.position;
            target.y -= Screen.height * 0.14f;

            info.transform.position = Vector3.Lerp(info.transform.position, target, 10f * Time.deltaTime);

        }
        else if (isOpen == false)
        {
            GameObject parent = transform.parent.gameObject;
            GameObject info = parent.transform.Find("info").gameObject;
            Vector3 target = gameObject.transform.position;
            target.y -= Screen.height * 0.05f;
            info.transform.position = Vector3.Lerp(info.transform.position, target, 10f * Time.deltaTime);
        }
    }
}
