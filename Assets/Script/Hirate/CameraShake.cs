using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // オブジェクトの設定
    [Header("画ブレ設定")]
    [Tooltip("＜時間＞\n・画面が揺れる時間")]
    [SerializeField] float time = 0.5f;
    [Tooltip("＜振動の強さ＞\n・動く座標量")]
    [SerializeField] float power = 0.1f;
    [Tooltip("＜振動数＞\n・大きいと激しく揺れます\n・小さいとゆっくり揺れます)")]
    [SerializeField] int frequency = 15;
    [Tooltip("＜ランダム量＞\n・多いと揺れる方向が増えます\n・少ないと揺れる方向が少なくなります")]
    [SerializeField] int random = 100;
    //[Tooltip("スナップ")]
    private bool snap = false;
    [Tooltip("＜フェードアウト＞\n・揺れがフェードアウト(収縮)させるかどうか")]
    [SerializeField] bool fadeOut = false;

    // 押した時の判定用
    private bool bInput = false;
    // 初期位置
    private Vector3 initPos;
    // 揺らす処理の代入用
    private Tweener tweener;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置代入
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 押された時に実行
        if (Input.GetKeyDown(KeyCode.O) && !bInput)
        {
            // もう一度押せないようにする
            bInput = true;

            // 直前の処理が残っている場合、初期座標に戻す
            if (tweener != null)
            {
                tweener.Kill();
                gameObject.transform.position = initPos;
            }

            // 画面を揺らす処理
            tweener =
                gameObject.transform.DOShakePosition(
                time,      // 時間
                power,     // 振動の強さ
                frequency, // 振動数
                random,    // ランダム量
                snap,      // スナップの有無
                fadeOut    // フェードアウトの有無
                ).OnComplete(() => bInput = false);
        }
    }
}
