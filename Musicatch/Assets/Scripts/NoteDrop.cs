using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDrop : MonoBehaviour
{
    public void SetNoteSpawner(GameObject spawner)
    {
        noteSpawner = spawner;
    }
    public void SetNoteDelay(float speed)
    {
        noteDelay = speed;
    }
    public void SetNoteEffect(GameObject effect1, GameObject effect2, GameObject effect3)
    {
        noteEffectGreat = effect1;
        noteEffectGood = effect2;
        noteEffectBad = effect3;
    }
    public void SetInputManager(InputManager i)
    {
        im = i;
    }

    public void SetSpawnerInteraction(bool i)
    {
        spawnerInteraction = i;
    }
    float noteDelay;
    Vector2 target = Vector2.zero;
    GameObject noteEffectGreat;
    GameObject noteEffectGood;
    GameObject noteEffectBad;
    GameObject noteSpawner;
    InputManager im;
    SpriteRenderer myRenderer;
    AudioSource ads;
    bool spawnerInteraction = true;

    float spawnRange = 12.30f;//스폰지점부터 중앙까지의 거리

    public int myIndex;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (GameObject.Find("hit") != null)
            ads = GameObject.Find("hit").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target, spawnRange * Time.deltaTime / noteDelay);
        if (spawnerInteraction == true)
        {
            float deltaHitIndex = (myIndex - noteSpawner.GetComponent<NoteSpawner>().hitIndex);
            myRenderer.color = Color.Lerp(myRenderer.color, Color.HSVToRGB(0.0138f + deltaHitIndex * 0.05f, 0.73f, 0.83f - deltaHitIndex * 0.3f)
            , 5f * Time.deltaTime);
        }
        if (Vector2.Distance(gameObject.transform.position, Vector2.zero) < 0.1f)
        {
            if (spawnerInteraction == true)
                noteSpawner.GetComponent<NoteSpawner>().MissNoteListener();
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //if (im.isLeft == true || im.isRight == true)
        if (true)
        {
            if (col.gameObject.tag == "Player" && noteSpawner.GetComponent<NoteSpawner>().hitIndex == myIndex)
            {
                //노트 딜레이 2초기준
                //2초동안 중앙으로 이동
                //1.8초 근처에 쳐야 퍼펙트
                float hitDistance = Vector3.Distance(transform.position, target);
                float DistancetoDeltaTime = ((spawnRange - hitDistance) / spawnRange) * noteDelay; //거리로 몇초가 지난 노트인지 분석
                float accuracy = Mathf.Abs(-NoteSpawner.perfactOffset - DistancetoDeltaTime);//0 is best
                print("hit " + accuracy * 1000 + "ms");
                noteSpawner.GetComponent<NoteSpawner>().HitNoteListener(accuracy);
                ads.Play();
                int msAcc = (int)Mathf.Round(accuracy * 1000); //밀리초 기반 정확도
                if (msAcc <= 80)
                {
                    Instantiate(noteEffectGreat, transform.position, Quaternion.identity);
                }
                else if (msAcc <= 130)
                {
                    Instantiate(noteEffectGood, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(noteEffectBad, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
        }
    }
}
