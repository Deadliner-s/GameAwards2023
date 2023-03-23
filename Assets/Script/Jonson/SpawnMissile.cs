using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile : MonoBehaviour
{  
    public GameObject Missile;
    public float DestroyTime;
    public string Key;
    public float Interval = 2.0f;   //�o������
    public float Interval2 = 8.0f;  //�o���Ԋu

    private float timer = 0.0f;
    private float timer2 = 0.0f;

    private bool wait = false;

    //�t�F�C�Y�؂�ւ��p
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // Start is called before the first frame update
    void Start()
    {
        // �t�F�C�Y�擾
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�C�Y�m�F
        AtkPhaseFlg = PhaseObj.activeSelf;

        if (Input.GetKeyDown(Key))
        {
            GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(obj, DestroyTime);
        }

        if (!wait)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;

            if (timer >= Interval)
            {
                // �t�F�C�Y�̊m�F
                if (AtkPhaseFlg == true)
                {
                    GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
                    Destroy(obj, DestroyTime);
                    timer = 0.0f; // �^�C�}�[�����Z�b�g����
                }
            }
            if (timer2 >= Interval2)
            {
                wait = true;
                timer2 = 0.0f;
            }
        }
        if (wait)
        {
            timer2 += Time.deltaTime;

            if (timer2 >= Interval2)
            {
                wait = false;
                timer2 = 0.0f;
            }
        }
    }
}