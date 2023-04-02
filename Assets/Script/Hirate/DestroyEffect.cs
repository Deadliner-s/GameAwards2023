using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // エフェクトが終了した時
        if (ps.isStopped)
        {
            // エフェクトのオブジェクトを削除
            Destroy(gameObject);
        }
    }
}
