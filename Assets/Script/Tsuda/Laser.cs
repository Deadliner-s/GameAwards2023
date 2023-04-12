using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float decrease = 0.1f;
    [SerializeField] private float delay = 2.0f; // 再生までの待機時間

    private float timer = 0.0f; // 経過時間
    private bool played = false; // 再生したかどうかのフラグ
    //private bool stopped = false;
    
    private Vector3 currentScale;

    [SerializeField] private ParticleSystem particleSystem; // 再生するパーティクルオブジェクト

    void Start()
    {
        currentScale = transform.localScale;
        particleSystem.Stop();
    }

    private void Update()
    {
        // 再生済みであれば何もしない
//        if (stopped) return;

        // 経過時間を加算
        timer += Time.deltaTime;

        // 指定した時間が経過したら再生
        if (timer >= delay && !played)
        {
            particleSystem.Play();
            played = true;
        }

        if(timer >= delay + 4.0f)
        {            
            // scaleを減少させる
            currentScale.x -= decrease;
            currentScale.y -= decrease;
            // 変更後のscaleを設定
            transform.localScale = currentScale;            
        }

        if(currentScale.x <= 0.0f && currentScale.y <= 0.0f)  // && !stopped)
        {
            Destroy(this.gameObject);
//            particleSystem.Stop();
//            stopped = true;
        }
    }
}


