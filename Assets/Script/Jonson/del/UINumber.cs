using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINumber : MonoBehaviour
{
    private int Number;
    public int Timer;
    // Start is called before the first frame update
    void Start()
    {
        Number = 0;
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer--;
        if (Timer <= 0)
            Timer = 0;

        Number = (Timer / 120);
        if (Number >= 1)
        {
            //‰½‚©•\Ž¦
        }





    }
}
