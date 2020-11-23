using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 50f;
    InputManager im;
    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myRotation = gameObject.transform.eulerAngles;
        if (im.isLeft == true)
        {
            myRotation.z += speed * Time.deltaTime;
        }
        if (im.isRight == true)
        {
            myRotation.z -= speed * Time.deltaTime;
        }
        gameObject.transform.rotation = Quaternion.Euler(myRotation);
    }
}
