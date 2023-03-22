using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f; // 再生までの待機時間
    private float timer = 0.0f; // 経過時間
    private bool played = false; // 再生したかどうかのフラグ
//    private CapsuleCollider Col;

    [SerializeField] private ParticleSystem particleSystem; // 再生するパーティクルオブジェクト

    void Start()
    {
//        Col = GetComponent<CapsuleCollider>();
//        Col.enabled = false;
        particleSystem.Stop();
    }

    private void Update()
    {
        // 再生済みであれば何もしない
        if (played) return;

        // 経過時間を加算
        timer += Time.deltaTime;

        // 指定した時間が経過したら再生
        if (timer >= delay)
        {
//            Col.enabled = true;
            particleSystem.Play();
            played = true;
        }
    }
}


