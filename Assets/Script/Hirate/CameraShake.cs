using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // オブジェクトの設定
    [Header("画ブレ設定")]
    [Tooltip("")]
    [SerializeField] float time;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] bool move = false;
    [Tooltip("フェードアウト")]
    [SerializeField] bool fadeOut = false;


    // 押した時の判定用
    private bool bInput = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                5f,
                0.3f,
                5,
                10,
                false,
                false
                ).OnComplete(() => bInput = false);
        }
    }
}
