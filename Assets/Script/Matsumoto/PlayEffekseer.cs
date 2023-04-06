using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Effekseer;

public class PlayEffekseer : MonoBehaviour
{
    public GameObject gameobj;              // 何が当たったら
    public EffekseerEffectAsset effect;     // 再生するエフェクト

    // Start is called before the first frame update
    void Start()
    {
        // エフェクトを取得する。
       effect = Resources.Load<EffekseerEffectAsset>(effect.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


            // transformの位置でエフェクトを再生する
            EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
            // transformの回転を設定する。
            handle.SetRotation(transform.rotation);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == gameobj.name)
        {
            // transformの位置でエフェクトを再生する
            EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
            // transformの回転を設定する。
            handle.SetRotation(transform.rotation);

            Debug.Log("effect");
        }
    }
}
