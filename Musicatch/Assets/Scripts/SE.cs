using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    public AudioClip startSE;
    public AudioClip selectSE;

    public void start()
    {
        gameObject.GetComponent<AudioSource>().clip = startSE;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void select()
    {
        gameObject.GetComponent<AudioSource>().clip = selectSE;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
