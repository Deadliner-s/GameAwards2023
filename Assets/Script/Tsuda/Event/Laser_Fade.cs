using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Fade : MonoBehaviour
{
    public bool Boss;
    public float scaleSpeed = 0.01f;
    public float wait;

    private float timer = 0.0f;
    private GameObject EV_Laser;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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
