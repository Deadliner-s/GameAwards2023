using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // プレハブオブジェクト    

    void Start()
    {        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Cキーが押されたら
        {
            Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
        }
    }
}

