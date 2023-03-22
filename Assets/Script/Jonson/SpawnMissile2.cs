using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile2 : MonoBehaviour
{
    public GameObject Missile;
    public float DestroyTime;
    public float Interval = 2.0f;   //�o������
    public float Interval2 = 8.0f;  //�o���Ԋu

    private float timer = 0.0f;
    private float timer2 = 0.0f;

    private bool wait = false;
    private GameObject targetObject; // �ΏۃI�u�W�F�N�g

    public string Key;              //�{�^����������o���@���Ԗ���

    GameObject obj;

    public float DelayTime = 0.0f;

    Vector3 ToPos;              //���ː�

    //�t�F�C�Y�؂�ւ��p
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position;

        targetObject = GameObject.Find("AttackPhase");

        // �t�F�C�Y�擾
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Invoke("InstantiateObject", DelayTime);
            ToPos = GameObject.Find("Player").transform.position;
        }
        if (targetObject == null)
        {
            return;
        }
        if (!wait)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;

            if (timer >= Interval)
            {
                // �t�F�C�Y�m�F
                AtkPhaseFlg = PhaseObj.activeSelf;

                // �t�F�C�Y�̊m�F
                if (AtkPhaseFlg == false)
                {
                    Invoke("InstantiateObject", DelayTime);
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

    void InstantiateObject()
    {
        obj = Instantiate(Missile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(obj, DestroyTime);
    }
}