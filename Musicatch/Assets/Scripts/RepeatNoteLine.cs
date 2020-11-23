using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNoteLine : MonoBehaviour
{
    float initSize = 2.369497f;
    float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myScale = gameObject.transform.localScale;
        myScale.x -= initSize * Time.deltaTime / delay;
        myScale.y -= initSize * Time.deltaTime / delay;
        gameObject.transform.localScale = myScale;
        if (myScale.x <= 0)
        {
            gameObject.transform.localScale = new Vector2(2.369497f, 2.369497f);
        }
    }
}
