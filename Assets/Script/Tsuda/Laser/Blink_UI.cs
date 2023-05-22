using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink_UI : MonoBehaviour
{
    public GameObject Laser;
    // 点滅させる対象
    [SerializeField] private Image _target;

    // 点滅周期[s]
    private float _cycle = 1.0f;
    private double _time;

    private float timer = 0.0f;
    private Camera mainCamera;  // メインカメラ

    void Start()
    {        
        mainCamera = Camera.main;  // メインカメラを取得する
        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);

        if (timer <= Laser.GetComponent<LaserHead>().wait)
        {
            if (timer >= Laser.GetComponent<LaserHead>().charge) { _cycle = 0.2f; }

            // 内部時刻を経過させる
            _time += Time.deltaTime;

            // 周期cycleで繰り返す値の取得
            // 0～cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat((float)_time, _cycle);

            // 内部時刻timeにおける明滅状態を反映
            _target.enabled = repeatValue >= _cycle * 0.5f;
        }
        else
        {
            _target.enabled = false;
        }
    }
}
