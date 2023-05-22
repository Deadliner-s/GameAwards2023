using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Laser : MonoBehaviour
{    
    public GameObject prefab; // プレハブオブジェクト    

    [Header("何秒目に生成するか")]    
    public float time;
    [Header("何秒間")]
    public float Laser_time;
    [Header("向く方向")]
    public Vector3 TargetPos;    

    private float timer = 0.0f;
    private bool createFlg = false;
    private GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(TargetPos);
        Parent = GameObject.Find("Laser_hanger");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;  // タイマーを減算する                                                          

        if(timer >= time && !createFlg)
        {            
            GameObject spawnedObject = Instantiate(prefab, transform.position, transform.rotation);
            spawnedObject.transform.parent = Parent.transform;

            createFlg = true;
        }
    }
}
