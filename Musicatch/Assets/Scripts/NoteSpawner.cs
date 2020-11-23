using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//spawning node and managing
public class NoteSpawner : MonoBehaviour
{
    //Get in inspector
    InputManager im;
    public GameObject note;
    public GameObject noteEffectGreat;
    public GameObject noteEffectGood;
    public GameObject noteEffectBad;
    public static float perfactOffset = -1.7f;//퍼펙트 판정나는 노트의 위치
    public static float soundOffset = -0.1f; //적을수록 노트가 빨리옴
    static float syncOffset = perfactOffset + soundOffset;
    static float noteDelay = 2f;
    //private
    float time;
    GameObject music;
    //for map reading
    float[] musMap;
    float[] musMapLoc;
    int mapIndex;
    public int hitIndex;
    bool isPlaying = false;
    //for spawn location
    float spawnRange = 12.30f;//스폰지점부터 중앙까지의 거리
    Text comboTxt;
    Text statTxt;
    Text scoreTxt;
    int great;
    int good;
    int bad;
    int miss;
    int combo;
    float score;
    int scoreBefore;
    const int maxScore = 1000000;
    bool isGameEnd;
    private static NoteSpawner instance; //중복 인스턴스 방지
    public static float GetGlobalSyncOffset()
    {
        return syncOffset;
    }
    public static float GetGlobalNoteDelay()
    {
        return noteDelay;
    }

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetData(AudioClip song, float[] mapTimings, float[] mapLocations)
    {
        music.GetComponent<AudioSource>().clip = song;
        this.musMap = mapTimings;
        this.musMapLoc = mapLocations;
    }

    void Start()
    {
        hitIndex = 0;
        great = 0;
        good = 0;
        bad = 0;
        miss = 0;
        combo = 0;
        score = 0;
        scoreBefore = 0;
        time = syncOffset;
        mapIndex = 0;
        isPlaying = false;
        isGameEnd = false;
        music = GameObject.Find("Music");
        //request map
        GameObject.Find("MapData").GetComponent<MapData>().GetData(gameObject);

        //getGameObjects
        comboTxt = GameObject.Find("Combo").GetComponent<Text>();
        statTxt = GameObject.Find("Stat").GetComponent<Text>();
        scoreTxt = GameObject.Find("Score").GetComponent<Text>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd == false)
        {
            if (time >= 0 && isPlaying == false)
            {
                music.GetComponent<AudioSource>().Play();
                DontDestroyOnLoad(music);
                isPlaying = true;
            }

            time += Time.deltaTime;
            if (mapIndex < musMap.Length)
            {
                if (musMap[mapIndex] <= time - syncOffset)
                {
                    SpawnNoteAngle(musMapLoc[mapIndex]);
                    mapIndex++;
                }
            }
            else
            {
                isGameEnd = true;
                StartCoroutine("GameEnd");
            }
        }

        if (scoreTxt != null)
        {
            comboTxt.text = combo.ToString();
            string statStr = "";
            statStr += "Great\t: " + great + "\n";
            statStr += "Good\t: " + good + "\n";
            statStr += "Bad\t\t: " + bad + "\n";
            statStr += "Miss\t\t: " + miss + "\n";
            statTxt.text = statStr;

            scoreBefore = (int)Mathf.Lerp(scoreBefore, score, 50f * Time.deltaTime);
            scoreTxt.text = scoreBefore.ToString();
        }

    }
    int CalculateScore()
    {
        if (musMap.Length == great) return maxScore;
        float result = (maxScore / musMap.Length * great);
        result += (maxScore / musMap.Length * 0.7f * good);
        result += (maxScore / musMap.Length * 0.5f * bad);
        print(great + "/" + musMap.Length);
        return (int)Mathf.Ceil(result);
    }

    public void HitNoteListener(float accuracy)
    {
        int msAcc = (int)Mathf.Round(accuracy * 1000); //밀리초 기반 정확도
        if (msAcc <= 80)
        {
            great++;
            score = CalculateScore();
        }
        else if (msAcc <= 130)
        {
            good++;
            score = CalculateScore();

        }
        else
        {
            bad++;
            score = CalculateScore();

        }
        hitIndex++;
        combo++;
    }

    public void MissNoteListener()
    {
        hitIndex++;
        combo = 0;
        miss++;
    }


    void SpawnNoteRandom()
    {
        Quaternion angle = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        Vector3 spawnPos = new Vector3(0, spawnRange, 0);
        spawnPos = angle * spawnPos;
        GameObject tmp = Instantiate(note, spawnPos, Quaternion.identity);
        tmp.GetComponent<NoteDrop>().myIndex = mapIndex;
        tmp.GetComponent<NoteDrop>().SetNoteDelay(noteDelay);
        tmp.GetComponent<NoteDrop>().SetNoteEffect(noteEffectGreat, noteEffectGood, noteEffectBad);
        tmp.GetComponent<NoteDrop>().SetNoteSpawner(gameObject);
        tmp.GetComponent<NoteDrop>().SetInputManager(im);
    }

    void SpawnNoteAngle(float targetAngle)
    {
        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));
        Vector3 spawnPos = new Vector3(0, spawnRange, 0);
        spawnPos = angle * spawnPos;
        GameObject tmp = Instantiate(note, spawnPos, Quaternion.identity);
        tmp.GetComponent<NoteDrop>().myIndex = mapIndex;
        tmp.GetComponent<NoteDrop>().SetNoteDelay(noteDelay);
        tmp.GetComponent<NoteDrop>().SetNoteEffect(noteEffectGreat, noteEffectGood, noteEffectBad);
        tmp.GetComponent<NoteDrop>().SetNoteSpawner(gameObject);
        tmp.GetComponent<NoteDrop>().SetInputManager(im);
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("Canvas").transform.Find("ScreenHider").gameObject.SetActive(true);
        GameObject.Find("ScreenHider").GetComponent<FadeScreen>().FadeWithScene("ResultScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ResultScene")
        {
            string statStr = "";
            statStr += "Great\t: " + great + "\n";
            statStr += "Good\t: " + good + "\n";
            statStr += "Bad\t\t: " + bad + "\n";
            statStr += "Miss\t\t: " + miss + "\n";
            GameObject.Find("Stat").GetComponent<Text>().text = statStr;
            GameObject.Find("Score").GetComponent<Text>().text = Mathf.Round(score).ToString();
            if (score >= maxScore / 100 * 95)
                GameObject.Find("Rank").GetComponent<SetRank>().SetS();
            else if (score >= maxScore / 100 * 90)
                GameObject.Find("Rank").GetComponent<SetRank>().SetA();
            else if (score >= maxScore / 100 * 85)
                GameObject.Find("Rank").GetComponent<SetRank>().SetB();
            else if (score >= maxScore / 100 * 80)
                GameObject.Find("Rank").GetComponent<SetRank>().SetC();
            else
                GameObject.Find("Rank").GetComponent<SetRank>().SetD();
        }
        else if (scene.name == "IngameScene")
        {
            print("reset");
            Start();
        }
    }
}
