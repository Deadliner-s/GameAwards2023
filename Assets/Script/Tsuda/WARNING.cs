using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WARNING : MonoBehaviour
{    
    // 点滅させる対象
    [SerializeField] private Image _target;

    // 点滅周期[s]
    private float _cycle = 0.7f;
    private double _time;    

    void Start()
    {
        
    }

    private void Update()
    {               
        // 内部時刻を経過させる
        _time += Time.deltaTime;

         // 周期cycleで繰り返す値の取得
         // 0〜cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat((float)_time, _cycle);

         // 内部時刻timeにおける明滅状態を反映
        _target.enabled = repeatValue >= _cycle * 0.5f;        
    }
}
