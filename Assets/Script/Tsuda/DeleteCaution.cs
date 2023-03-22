using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCaution : MonoBehaviour
{    
    public float lifetime = 5f;  // オブジェクトの寿命（秒）

    private float timer;  // タイマー

    void Start()
    {        
        timer = lifetime;  // タイマーを設定する
    }

    void Update()
    {        
        timer -= Time.deltaTime;  // タイマーを減算する        

        if (timer <= 0)
        {
            Destroy(gameObject);  // オブジェクトを削除する
        }
    }
}
