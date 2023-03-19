using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // プレハブオブジェクト

    void Update()
    {
        // もし'C'キーが押されたら
        if (Input.GetKeyDown(KeyCode.C))
        {
            // プレハブオブジェクトを生成する
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
