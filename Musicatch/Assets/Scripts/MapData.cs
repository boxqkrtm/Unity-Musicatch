using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public static MapData instance;
    AudioClip song;
    float[] mapTimings;
    float[] mapLocations;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(AudioClip song, float[] mapTimings, float[] mapLocations)
    {
        this.song = song;
        this.mapTimings = mapTimings;
        this.mapLocations = mapLocations;
    }

    public void GetData(GameObject target)
    {
        target.GetComponent<NoteSpawner>().SetData(song, mapTimings, mapLocations);
        instance = null;
        Destroy(gameObject);
    }
}
