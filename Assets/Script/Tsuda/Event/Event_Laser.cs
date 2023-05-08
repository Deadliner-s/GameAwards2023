using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Laser : MonoBehaviour
{    
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g    

    [Tooltip("���b�ڂɐ������邩")]
    public float time;

    [Tooltip("��������")]
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
        timer += Time.deltaTime;  // �^�C�}�[�����Z����                                                          

        if(timer >= time && !createFlg)
        {
            Instantiate(prefab, transform.position, transform.rotation);

            createFlg = true;
        }
    }
}
