using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile2 : MonoBehaviour
{
    [Tooltip("�~�T�C���v���n�u")]
    public GameObject Missile;
    [Tooltip("������܂ł̎���")]
    public float DestroyTime;
    [Tooltip("�f�o�b�O�p�~�T�C�������L�[")]
    public string Key;

    // �i�s���ԗp
    private float timer;

    // ���ˊԊu�p
    private float IntervalTime;

    // ���Z�b�g�t���O
    private bool Reset_flg = false;

    [Tooltip("�����ꏊ�w��")]
    public GameObject SpawnPos;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    // ����R�s�y
    [Header("�~�T�C��1���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_1 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_1;
    [Tooltip("���ˊԊu")]
    public int Interval_1;   //�o������
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_1;
    // �m�F�p�t���O
    private bool Use_flg_1 = false;

    [Header("�~�T�C��2���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_2 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_2;
    [Tooltip("���ˊԊu")]
    public float Interval_2;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_2;
    // �m�F�p�t���O
    private bool Use_flg_2 = false;

    [Header("�~�T�C��3���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_3 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_3;
    [Tooltip("���ˊԊu")]
    public float Interval_3;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_3;
    // �m�F�p�t���O
    private bool Use_flg_3 = false;

    [Header("�~�T�C��4���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_4 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_4;
    [Tooltip("���ˊԊu")]
    public float Interval_4;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_4;
    // �m�F�p�t���O
    private bool Use_flg_4 = false;

    [Header("�~�T�C��5���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_5 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_5;
    [Tooltip("���ˊԊu")]
    public float Interval_5;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_5;
    // �m�F�p�t���O
    private bool Use_flg_5 = false;

    [Header("�~�T�C��6���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_6 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_6;
    [Tooltip("���ˊԊu")]
    public float Interval_6;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_6;
    // �m�F�p�t���O
    private bool Use_flg_6 = false;

    [Header("�~�T�C��7���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_7 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_7;
    [Tooltip("���ˊԊu")]
    public float Interval_7;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_7;
    // �m�F�p�t���O
    private bool Use_flg_7 = false;

    [Header("�~�T�C��8���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_8 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_8;
    [Tooltip("���ˊԊu")]
    public float Interval_8;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_8;
    // �m�F�p�t���O
    private bool Use_flg_8 = false;

    [Header("�~�T�C��9���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_9 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_9;
    [Tooltip("���ˊԊu")]
    public float Interval_9;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_9;
    // �m�F�p�t���O
    private bool Use_flg_9 = false;

    [Header("�~�T�C��10���")]
    [Tooltip("�A�^�b�N�t�F�C�Y���b��")]
    public float StartTime_10 = 100.0f;
    [Tooltip("���b��")]
    public float Timer_10;
    [Tooltip("���ˊԊu")]
    public float Interval_10;  //�o���Ԋu
    [Tooltip("�~�T�C���z��")]
    public List<GameObject> h_Missile_10;
    // �m�F�p�t���O
    private bool Use_flg_10 = false;

    // Start is called before the first frame update
    void Start()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // ���Ԃ̏�����
        timer = 0.0f;
        IntervalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player"))//�v���C���[�͐����Ă���i���݂���j
        {
            // �t�F�C�Y�m�F
            currentPhase = PhaseManager.instance.GetPhase();

            // �f�o�b�O�p
            if (Input.GetKeyDown(Key))
            {
                GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                Destroy(obj, DestroyTime);
            }

            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {// �A�^�b�N�t�F�C�Y
                // �e�t���O�Ȃǃ��Z�b�g
                if (Reset_flg == true)
                {
                    timer = 0.0f;

                    Use_flg_1 = false;
                    Use_flg_2 = false;
                    Use_flg_3 = false;
                    Use_flg_4 = false;
                    Use_flg_5 = false;
                    Use_flg_6 = false;
                    Use_flg_7 = false;
                    Use_flg_8 = false;
                    Use_flg_9 = false;
                    Use_flg_10 = false;

                    h_Missile_1 = new List<GameObject>();
                    h_Missile_2 = new List<GameObject>();
                    h_Missile_3 = new List<GameObject>();
                    h_Missile_4 = new List<GameObject>();
                    h_Missile_5 = new List<GameObject>();
                    h_Missile_6 = new List<GameObject>();
                    h_Missile_7 = new List<GameObject>();
                    h_Missile_8 = new List<GameObject>();
                    h_Missile_9 = new List<GameObject>();
                    h_Missile_10 = new List<GameObject>();


                    Reset_flg = false;
                }

                // ���ԍX�V
                timer += Time.deltaTime;
                IntervalTime += Time.deltaTime;

                // �C���^�[�o��


                // 1���
                if (Use_flg_1 == false && timer >= StartTime_1)
                {
                    if (IntervalTime >= Interval_1)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_1.Add(obj);
                        //GameObject obj1 = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        //h_Missile_1.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_1 + Timer_1 >= timer)
                    {
                        Use_flg_1 = true;
                    }
                }
                // 2���
                if (Use_flg_2 == false && timer >= StartTime_2)
                {
                    if (IntervalTime >= Interval_2)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_2.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_2 + Timer_2 >= timer)
                    {
                        Use_flg_2 = true;
                    }
                }
                // 3���
                if (Use_flg_3 == false && timer >= StartTime_3)
                {
                    if (IntervalTime >= Interval_3)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_3.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_3 + Timer_3 >= timer)
                    {
                        Use_flg_3 = true;
                    }
                }
                // 4���
                if (Use_flg_4 == false && timer >= StartTime_4)
                {
                    if (IntervalTime >= Interval_4)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_4.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_4 + Timer_4 >= timer)
                    {
                        Use_flg_4 = true;
                    }
                }
                // 5���
                if (Use_flg_5 == false && timer >= StartTime_5)
                {
                    if (IntervalTime >= Interval_5)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_5.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_5 + Timer_5 >= timer)
                    {
                        Use_flg_5 = true;
                    }
                }
                // 6���
                if (Use_flg_6 == false && timer >= StartTime_6)
                {
                    if (IntervalTime >= Interval_6)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_6.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_6 + Timer_6 >= timer)
                    {
                        Use_flg_6 = true;
                    }
                }
                // 7���
                if (Use_flg_7 == false && timer >= StartTime_7)
                {
                    if (IntervalTime >= Interval_7)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_7.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_7 + Timer_7 >= timer)
                    {
                        Use_flg_7 = true;
                    }
                }
                // 8���
                if (Use_flg_8 == false && timer >= StartTime_8)
                {
                    if (IntervalTime >= Interval_8)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_8 + Timer_8 >= timer)
                    {
                        Use_flg_8 = true;
                    }
                }
                // 9���
                if (Use_flg_9 == false && timer >= StartTime_9)
                {
                    if (IntervalTime >= Interval_9)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_9 + Timer_9 >= timer)
                    {
                        Use_flg_9 = true;
                    }
                }
                // 10���
                if (Use_flg_10 == false && timer >= StartTime_10)
                {
                    if (IntervalTime >= Interval_10)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // �^�C�}�[�����Z�b�g����
                    }
                    if (StartTime_10 + Timer_10 >= timer)
                    {
                        Use_flg_10 = true;
                    }
                }

            }
            else
            {// �n�C�X�s�[�h�t�F�C�Y
                Reset_flg = true;
            }
        }
    }
}