using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_collision : MonoBehaviour
{
    public GameObject LaserA;    

    private CapsuleCollider Col;
    private float timer = 0.0f;
    private float _cycle = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<CapsuleCollider>();
        Col.enabled = false;        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;        

        if(timer >= LaserA.GetComponent<LaserHead>().wait && timer <= LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime)
        {            
            // 0〜cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat((float)timer, _cycle);

                // 内部時刻timeにおける明滅状態を反映
            Col.enabled = repeatValue >= _cycle * 0.5f;                   
        }
        else
        {
            Col.enabled = false;
        }
    }
}
