using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRank : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite rS;
    public Sprite rA;
    public Sprite rB;
    public Sprite rC;
    public Sprite rD;
    void Start()
    {

    }

    public void SetS()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rS;
    }
    public void SetA()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rA;
    }
    public void SetB()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rB;
    }
    public void SetC()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rC;
    }
    public void SetD()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rD;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
