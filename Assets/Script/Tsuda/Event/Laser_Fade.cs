using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Fade : MonoBehaviour
{
    public bool Boss;
    public float scaleSpeed = 0.01f;
    public float scaleSpeed2 = 0.01f;
    public float wait;

    private float timer = 0.0f;
    private GameObject EV_Laser;
    private bool flg = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Boss)
        {
            EV_Laser = GameObject.Find("Laser_Boss");
        }
        else
        {
            EV_Laser = GameObject.Find("Laser_Squad");
            transform.localScale = new Vector3(0.1f, 0.1f, 3.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

       if(!Boss)
       {
           if(transform.localScale.x <= 3.5f && timer >= wait && !flg)
            {
                transform.localScale += new Vector3(scaleSpeed2, scaleSpeed2, 0);

                if(transform.localScale.x >= 3.5f)
                {
                    flg = true;
                }
            }
       }

        if (timer >= wait + EV_Laser.GetComponent<Event_Laser>().Laser_time)
        {
            if (Boss)
            {
                // スケールを減らす
                transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            }
            else
            {
                // スケールを減らす
                transform.localScale -= new Vector3(scaleSpeed, 0, 0);
            }
            
        }

        // スケールが0以下になったらオブジェクトを削除する
        if (transform.localScale.x <= 0 || transform.localScale.y <= 0)
        {
            Destroy(gameObject);
        }
    }
}
