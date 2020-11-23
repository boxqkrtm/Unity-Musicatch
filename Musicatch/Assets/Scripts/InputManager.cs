using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isLeft;
    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isLeft = false;
        isRight = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeft = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2f)
            {
                isLeft = true;
            }
            else
            {
                isRight = true;
            }
        }
    }
}
