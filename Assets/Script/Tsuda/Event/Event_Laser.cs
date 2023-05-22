using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Laser : MonoBehaviour
{    
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g    

    [Header("���b�ڂɐ������邩")]    
    public float time;
    [Header("���b��")]
    public float Laser_time;
    [Header("��������")]
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
        timer += Time.deltaTime;  // �^�C�}�[�����Z����                                                          

        if(timer >= time && !createFlg)
        {            
            GameObject spawnedObject = Instantiate(prefab, transform.position, transform.rotation);
            spawnedObject.transform.parent = Parent.transform;

            createFlg = true;
        }
    }
}
