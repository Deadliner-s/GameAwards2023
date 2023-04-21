using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissileScreen : MonoBehaviour
{
    static int FlagMax = 3;
    public GameObject SpeedMissile;
    public float SpeedStraight;
    public GameObject ClusterMissile;
    public float SpeedCluster;
    public int ClusterNumber;

    public string KeyCluster;
    public GameObject SpawnTop;
    public string KeyTop;
    public GameObject SpawnTopLeft;
    public string KeyTopLeft;
    public GameObject SpawnLeft;
    public string KeyLeft;
    public GameObject SpawnBotLeft;
    public string KeyBotLeft;
    public GameObject SpawnBot;
    public string KeyBot;
    public float DestroyTime;

    GameObject player;

    private float timer;

    // ���ˊԊu�p
    private float IntervalTimeTop;
    private float IntervalTimeTopLeft;
    private float IntervalTimeLeft;
    private float IntervalTimeBotLeft;
    private float IntervalTimeBot;
    private float IntervalTimeCluster;

    // ���Z�b�g�t���O
    private bool Reset_flg = false;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    [Header("AttackPhase���牽�b��������o�����i�N���X�^�[�j")]
    public float[] StartTimeCluster = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i�N���X�^�[�j")]
    public float[] DurationCluster = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i�N���X�^�[�j")]
    public float[] IntervalTriggerCluster = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i��j")]
    public float[] StartTimeTop = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i��j")]
    public float[] DurationTop = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i��j")]
    public float[] IntervalTriggerTop = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i����j")]
    public float[] StartTimeTopLeft = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i����j")]
    public float[] DurationTopLeft = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i����j")]
    public float[] IntervalTriggerTopLeft = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i���j")]
    public float[] StartTimeLeft = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i���j")]
    public float[] DurationLeft = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i���j")]
    public float[] IntervalTriggerLeft = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i�����j")]
    public float[] StartTimeBotLeft = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i�����j")]
    public float[] DurationBotLeft = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i�����j")]
    public float[] IntervalTriggerBotLeft = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i���j")]
    public float[] StartTimeBot = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i���j")]
    public float[] DurationBot = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i���j")]
    public float[] IntervalTriggerBot = new float[FlagMax];   //�o���Ԋu

    // �m�F�p�t���O
    private bool[] UseFlagCluster = new bool[FlagMax];
    private bool[] UseFlagTop = new bool[FlagMax];
    private bool[] UseFlagTopLeft = new bool[FlagMax];
    private bool[] UseFlagLeft = new bool[FlagMax];
    private bool[] UseFlagBotLeft = new bool[FlagMax];
    private bool[] UseFlagBot = new bool[FlagMax];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // ���Ԃ̏�����
        timer = 0.0f;
        IntervalTimeCluster = 0.0f;
        IntervalTimeTop = 0.0f;
        IntervalTimeTopLeft = 0.0f;
        IntervalTimeLeft = 0.0f;
        IntervalTimeBotLeft = 0.0f;
        IntervalTimeBot = 0.0f;
        for (int i = 0; i < FlagMax; i++)
        {
            UseFlagCluster[i] = false;
            UseFlagTop[i] = false;
            UseFlagTopLeft[i] = false;
            UseFlagLeft[i] = false;
            UseFlagBotLeft[i] = false;
            UseFlagBot[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)//�v���C���[�͐����Ă���i���݂���j
        {
            // �f�o�b�O�p
            if (Input.GetKeyDown(KeyCluster))
            {
                GameObject obj = Instantiate(ClusterMissile, SpawnLeft.transform.position, Quaternion.identity);
                obj.GetComponent<MissileBossCluster>().Speed = SpeedCluster;
                obj.GetComponent<MissileBossCluster>().ClusterNumber = ClusterNumber;
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyTop))
            {
                GameObject obj = Instantiate(SpeedMissile, SpawnTop.transform.position, Quaternion.identity);
                obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyTopLeft))
            {
                GameObject obj = Instantiate(SpeedMissile, SpawnTopLeft.transform.position, Quaternion.identity);
                obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyLeft))
            {
                GameObject obj = Instantiate(SpeedMissile, SpawnLeft.transform.position, Quaternion.identity);
                obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyBotLeft))
            {
                GameObject obj = Instantiate(SpeedMissile, SpawnBotLeft.transform.position, Quaternion.identity);
                obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyBot))
            {
                GameObject obj = Instantiate(SpeedMissile, SpawnBot.transform.position, Quaternion.identity);
                obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                Destroy(obj, DestroyTime);
            }

            // �t�F�C�Y�m�F
            currentPhase = PhaseManager.instance.GetPhase();

            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                // �A�^�b�N�t�F�C�Y
                // �e�t���O�A�ϐ��Ȃǃ��Z�b�g
                if (Reset_flg)
                {
                    timer = 0.0f;
                    IntervalTimeCluster = 0.0f;
                    IntervalTimeTop = 0.0f;
                    IntervalTimeTopLeft = 0.0f;
                    IntervalTimeLeft = 0.0f;
                    IntervalTimeBotLeft = 0.0f;
                    IntervalTimeBot = 0.0f;
                    for (int i = 0; i < FlagMax; i++)
                    {
                        UseFlagCluster[i] = false;
                        UseFlagTop[i] = false;
                        UseFlagTopLeft[i] = false;
                        UseFlagLeft[i] = false;
                        UseFlagBotLeft[i] = false;
                        UseFlagBot[i] = false;
                    }
                    Reset_flg = false;
                }
                // ���ԍX�V
                timer += Time.deltaTime;
                IntervalTimeCluster += Time.deltaTime;
                IntervalTimeTop += Time.deltaTime;
                IntervalTimeTopLeft += Time.deltaTime;
                IntervalTimeLeft += Time.deltaTime;
                IntervalTimeBotLeft += Time.deltaTime;
                IntervalTimeBot += Time.deltaTime;
                // i��ڃC���^�[�o��
                for (int i = 0; i < FlagMax; i++)
                {
                    if (!UseFlagCluster[i] && timer >= StartTimeCluster[i])
                    {
                        if (IntervalTimeCluster >= IntervalTriggerCluster[i])
                        {
                            GameObject obj = Instantiate(ClusterMissile, SpawnLeft.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileBossCluster>().Speed = SpeedCluster;
                            obj.GetComponent<MissileBossCluster>().ClusterNumber = ClusterNumber;
                            Destroy(obj, DestroyTime);
                            IntervalTimeCluster = 0.0f;
                        }
                        if (StartTimeCluster[i] + DurationCluster[i] <= timer)
                        {
                            UseFlagCluster[i] = true;
                        }
                    }
                    if (!UseFlagTop[i] && timer >= StartTimeTop[i])
                    {
                        if (IntervalTimeTop >= IntervalTriggerTop[i])
                        {
                            GameObject obj = Instantiate(SpeedMissile, SpawnTop.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                            Destroy(obj, DestroyTime);
                            IntervalTimeTop = 0.0f;
                        }
                        if (StartTimeTop[i] + DurationTop[i] <= timer)
                        {
                            UseFlagTop[i] = true;
                        }
                    }
                    if (!UseFlagTopLeft[i] && timer >= StartTimeTopLeft[i])
                    {
                        if (IntervalTimeTopLeft >= IntervalTriggerTopLeft[i])
                        {
                            GameObject obj = Instantiate(SpeedMissile, SpawnTopLeft.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                            Destroy(obj, DestroyTime);
                            IntervalTimeTopLeft = 0.0f;
                        }
                        if (StartTimeTopLeft[i] + DurationTopLeft[i] <= timer)
                        {
                            UseFlagTopLeft[i] = true;
                        }
                    }
                    if (!UseFlagLeft[i] && timer >= StartTimeLeft[i])
                    {
                        if (IntervalTimeLeft >= IntervalTriggerLeft[i])
                        {
                            GameObject obj = Instantiate(SpeedMissile, SpawnLeft.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                            Destroy(obj, DestroyTime);
                            IntervalTimeLeft = 0.0f;
                        }
                        if (StartTimeLeft[i] + DurationLeft[i] <= timer)
                        {
                            UseFlagLeft[i] = true;
                        }
                    }
                    if (!UseFlagBotLeft[i] && timer >= StartTimeBotLeft[i])
                    {
                        if (IntervalTimeBotLeft >= IntervalTriggerBotLeft[i])
                        {
                            GameObject obj = Instantiate(SpeedMissile, SpawnBotLeft.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                            Destroy(obj, DestroyTime);
                            IntervalTimeBotLeft = 0.0f;
                        }
                        if (StartTimeBotLeft[i] + DurationBotLeft[i] <= timer)
                        {
                            UseFlagBotLeft[i] = true;
                        }
                    }
                    if (!UseFlagBot[i] && timer >= StartTimeBot[i])
                    {
                        if (IntervalTimeBot >= IntervalTriggerBot[i])
                        {
                            GameObject obj = Instantiate(SpeedMissile, SpawnBot.transform.position, Quaternion.identity);
                            obj.GetComponent<MissileStraight>().Speed = SpeedStraight;
                            Destroy(obj, DestroyTime);
                            IntervalTimeBot = 0.0f;
                        }
                        if (StartTimeBot[i] + DurationBot[i] <= timer)
                        {
                            UseFlagBot[i] = true;
                        }
                    }
                }
            }
            else // �n�C�X�s�[�h�t�F�C�Y
            {
                Reset_flg = true;
            }
        }

    }
}
