using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakerWithmouse : MonoBehaviour
{
    public string mapData;
    float time;
    NoteSpawnerEditor noteSpawner;
    List<float> map;
    List<float> mapLoc;
    float firstInput = 0f;
    public int bpm = 90;
    // Start is called before the first frame update
    void Start()
    {
        noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawnerEditor>();
        time = 0;
        map = new List<float>();
        mapLoc = new List<float>();

        if (mapData.IndexOf('A') != -1)
        {
            string mapTimingsStr = mapData.Split('A')[0];
            string mapLocationsStr = mapData.Split('A')[1];
            int i = 0;
            foreach (var word in mapTimingsStr.Split(','))
            {
                map.Add(float.Parse(word));
                i++;
            }
            i = 0;
            foreach (var word in mapLocationsStr.Split(','))
            {
                mapLoc.Add(float.Parse(word));
                i++;
            }
        }

        if (map.Count >= 1)
        {
            firstInput = map[0];
        }
        else
        {
            firstInput = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        noteSpawner.time = this.time;
        noteSpawner.musMap = this.map;
        noteSpawner.musMapLoc = this.mapLoc;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        //mwclick addnote
        {

            if (firstInput == -1) firstInput = time + NoteSpawner.GetGlobalSyncOffset();
            float guidedTime;
            //bpm 기준으로 1/4 박자 시간 계산
            float smallBeat = 1 / (bpm / 60f * 4f);//최소 초당 비트크기의 1/4
            float modv = (time + NoteSpawner.GetGlobalSyncOffset() - firstInput) % smallBeat;
            float divv = (time + NoteSpawner.GetGlobalSyncOffset() - firstInput) / smallBeat;
            if (modv > 0.5f)
            {
                //올림적용
                guidedTime = smallBeat * (divv - modv + 1) + firstInput;
            }
            else
            {
                //내림 적용
                guidedTime = smallBeat * (divv) + firstInput;
            }
            map.Insert(noteSpawner.mapIndex, guidedTime);
            Vector3 mouse2DPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = CalculateAngle(Vector3.zero, mouse2DPos);
            float guideAngle = Mathf.Round(angle * 10) / 10;
            mapLoc.Insert(noteSpawner.mapIndex, guideAngle);
            noteSpawner.mapIndex++;
            noteSpawner.SpawnNoteAngleWithTime(angle, 1.8f);//1.8=perfect delta
            print(guidedTime);
            print(time + NoteSpawner.GetGlobalSyncOffset());
        }
        if (Input.GetMouseButtonDown(2))
        {
            //mwrightclick removenote
        }
        if (Input.GetMouseButtonDown(1))
        //mwhell export
        {
            string result = "";
            for (int i = 0; i < map.Count; i++)
            {
                if (i == map.Count - 1)
                {
                    result += map[i].ToString();
                }
                else
                {
                    result += map[i].ToString() + ",";
                }
            }
            result += "A";
            for (int i = 0; i < mapLoc.Count; i++)
            {
                if (i == mapLoc.Count - 1)
                {
                    result += mapLoc[i].ToString();
                }
                else
                {
                    result += mapLoc[i].ToString() + ",";
                }
            }
            print(result);
        }
    }

    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

}
