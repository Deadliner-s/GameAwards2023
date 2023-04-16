using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPhase : MonoBehaviour
{
    public GameObject Text;

    Vector3 move;

    private int num;

    private bool flg;
    // Start is called before the first frame update
    void Start()
    {
        flg = false;

        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //move = new Vector3(Text.transform.position.x, -180.0f, 0.0f);
            num--;
            flg = true;
            if (num < 0)
            {
                num = 0;
                flg = false;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //move = new Vector3(Text.transform.position.x, -180.0f, 0.0f);
            num++;
            flg = true;
            if (num > 2)
            {
                num = 2;

                flg = false;
            }
        }

        if (flg == true)
        {
            switch (num)
            {
                case(0):
                    move = new Vector3(0.0f, 180.0f, 0.0f);
                    Text.transform.position = move;
                    break;
                case (1):
                    move = new Vector3(0.0f, 0.0f, 0.0f);
                    Text.transform.position = move;
                    break;
                case (2):
                    move = new Vector3(0.0f, -180.0f, 0.0f);
                    Text.transform.position = move;
                    break;
            }
            flg = false;
        }


    }
}
