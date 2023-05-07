using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // オブジェクトの設定
    [Header("画ブレ設定")]
    [Tooltip("時間")]
    [SerializeField] float time = 5.0f;
    [Tooltip("振動の強さ")]
    [SerializeField] float power = 0.3f;
    [Tooltip("振動数")]
    [SerializeField] int frequency = 5;
    [Tooltip("ランダム")]
    [SerializeField] int random = 10;
    [Tooltip("スナップ")]
    [SerializeField] bool snap = false;
    [Tooltip("フェードアウト")]
    [SerializeField] bool fadeOut = false;

    // 押した時の判定用
    private bool bInput = false;
    // 初期位置
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置代入
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bInput = true;
        }
        // 押された時に実行
        if (bInput)
        {
            gameObject.transform.DOShakePosition(
                time,
                power,
                frequency,
                random,
                snap,
                fadeOut
                ).OnComplete(() => { gameObject.transform.position = initPos; bInput = false; });
        }
    }
}
