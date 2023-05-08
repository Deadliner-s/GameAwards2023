using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Laser : MonoBehaviour
{    
    public GameObject prefab; // プレハブオブジェクト    

    [Tooltip("何秒目に生成するか")]
    public float time;

    [Tooltip("向く方向")]
    public Vector3 TargetPos;    

    private float timer = 0.0f;
    private bool createFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(TargetPos);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;  // タイマーを減算する                                                          

        if(timer >= time && !createFlg)
        {
            Instantiate(prefab, transform.position, transform.rotation);

            createFlg = true;
        }
    }
}
