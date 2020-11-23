using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//spawning node and managing
public class NoteSpawnerEditor : MonoBehaviour
{
    //Get in inspector
    public GameObject note;
    public GameObject noteEffect;

    //global offset
    float syncOffset = NoteSpawner.GetGlobalSyncOffset();
    float noteDelay = NoteSpawner.GetGlobalNoteDelay();
    //private
    public float time;
    GameObject music;
    //for map reading
    public List<float> musMap;
    public List<float> musMapLoc;
    public int mapIndex = 0;
    public int hitIndex = 0;
    bool isPlaying = false;
    //for spawn location
    float spawnRange = 12.30f;//스폰지점부터 중앙까지의 거리

    void Start()
    {
        music = GameObject.Find("Music");
        mapIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        hitIndex = 0;
        if (time + syncOffset >= 0 && isPlaying == false)
        {
            music.GetComponent<AudioSource>().Play();
            isPlaying = true;
        }
        for (int i = mapIndex; i < musMap.Count; i++)
        {
            if (mapIndex < musMap.Count)
            {
                if (musMap[mapIndex] <= time)
                {
                    //noteDelay;
                    SpawnNoteAngle(musMapLoc[mapIndex]);
                    mapIndex++;
                    break;
                }
            }
        }
    }
    public void HitNoteListener(float accuracy)
    {

    }

    public void MissNoteListener()
    {

    }

    void SpawnNoteAngle(float targetAngle)
    {
        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));
        Vector3 spawnPos = new Vector3(0, spawnRange, 0);
        spawnPos = angle * spawnPos;
        GameObject tmp = Instantiate(note, spawnPos, Quaternion.identity);
        tmp.GetComponent<NoteDrop>().myIndex = 0;
        tmp.GetComponent<NoteDrop>().SetSpawnerInteraction(false);
        tmp.GetComponent<NoteDrop>().SetNoteDelay(noteDelay);
        tmp.GetComponent<NoteDrop>().SetNoteEffect(noteEffect, noteEffect, noteEffect);
        tmp.GetComponent<NoteDrop>().SetNoteSpawner(gameObject);
    }

    //남은 시간 기준 노트 생성
    public void SpawnNoteAngleWithTime(float targetAngle, float delta)
    {
        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));
        Vector3 spawnPos = new Vector3(0, spawnRange * ((noteDelay - delta) / noteDelay), 0);
        spawnPos = angle * spawnPos;
        GameObject tmp = Instantiate(note, spawnPos, Quaternion.identity);
        tmp.GetComponent<NoteDrop>().myIndex = 0;
        tmp.GetComponent<NoteDrop>().SetSpawnerInteraction(false);
        tmp.GetComponent<NoteDrop>().SetNoteDelay(noteDelay);
        tmp.GetComponent<NoteDrop>().SetNoteEffect(noteEffect, noteEffect, noteEffect);
        tmp.GetComponent<NoteDrop>().SetNoteSpawner(gameObject);
    }

}
