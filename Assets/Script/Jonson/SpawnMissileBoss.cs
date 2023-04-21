using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissileBoss : MonoBehaviour
{
    static int FlagMax = 3;
    public GameObject HomingMissile;
    public float HomingSpeed;
    public GameObject ContenaMissile;
    public float ContenaUpSpeed;
    public float ContenaToSpeed;
    public int ContenaNumber;
    public float DestroyTimeHoming;
    public float DestroyTimeContena;
    public string KeyHoming;
    public string KeyContena;

    GameObject player;

    // �i�s���ԗp
    private float timer;

    // ���ˊԊu�p
    private float IntervalTimeHoming;
    private float IntervalTimeContena;

    // ���Z�b�g�t���O
    private bool Reset_flg = false;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    [Header("AttackPhase���牽�b��������o�����i�U���j")]
    public float[] StartTimeHoming = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i�U���j")]
    public float[] DurationHoming = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i�U���j")]
    public float[] IntervalTriggerHoming = new float[FlagMax];   //�o���Ԋu

    [Header("AttackPhase���牽�b��������o�����i�R���e�i�j")]
    public float[] StartTimeContena = new float[FlagMax];           //�o���^�C�~���O

    [Header("���b�o�����i�R���e�i�j")]
    public float[] DurationContena = new float[FlagMax];           //�o���g�[�^������

    [Header("�~�T�C����o���Ԋu�i�R���e�i�j")]
    public float[] IntervalTriggerContena = new float[FlagMax];   //�o���Ԋu

    // �m�F�p�t���O
    private bool[] UseFlagHoming = new bool[FlagMax];
    private bool[] UseFlagContena = new bool[FlagMax];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // ���Ԃ̏�����
        timer = 0.0f;
        IntervalTimeHoming = 0.0f;
        IntervalTimeContena = 0.0f;
        for (int i = 0; i < FlagMax; i++)
        {
            UseFlagHoming[i] = false;
            UseFlagContena[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)//�v���C���[�͐����Ă���i���݂���j
        {
            // �f�o�b�O�p
            if (Input.GetKeyDown(KeyHoming))
            {
                GameObject obj = Instantiate(HomingMissile, transform.position, Quaternion.identity);
                obj.GetComponent<MissileBossHoming>().Speed = HomingSpeed;
                Destroy(obj, DestroyTimeHoming);
            }
            if (Input.GetKeyDown(KeyContena))
            {
                GameObject obj = Instantiate(ContenaMissile, transform.position, Quaternion.identity);
                obj.GetComponent<MissileBossContena>().UpSpeed = ContenaUpSpeed;
                obj.GetComponent<MissileBossContena>().ToSpeed = ContenaToSpeed;
                obj.GetComponent<MissileBossContena>().ContenaNumber = ContenaNumber;
                Destroy(obj, DestroyTimeContena);
            }
            // �t�F�C�Y�m�F
            currentPhase = PhaseManager.instance.GetPhase();

            if (currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                // �A�^�b�N�t�F�C�Y
                // �e�t���O�A�ϐ��Ȃǃ��Z�b�g
                if (Reset_flg)
                {
                    timer = 0.0f;
                    IntervalTimeHoming = 0.0f;
                    IntervalTimeContena = 0.0f;
                    for (int i = 0; i < FlagMax; i++)
                    {
                        UseFlagHoming[i] = false;
                        UseFlagContena[i] = false;
                    }
                    Reset_flg = false;
                }
                // ���ԍX�V
                timer += Time.deltaTime;
                IntervalTimeHoming += Time.deltaTime;
                IntervalTimeContena += Time.deltaTime;
                // i��ڃC���^�[�o��
                for (int i = 0; i < FlagMax; i++)
                {
                    if (!UseFlagHoming[i] && timer >= StartTimeHoming[i])
                    {
                        if (IntervalTimeHoming >= IntervalTriggerHoming[i])
                        {
                            GameObject obj = Instantiate(HomingMissile, transform.position, Quaternion.identity);
                            obj.GetComponent<MissileBossHoming>().Speed = HomingSpeed;
                            Destroy(obj, DestroyTimeHoming);
                            IntervalTimeHoming = 0.0f;
                        }
                        if (StartTimeHoming[i] + DurationHoming[i] <= timer)
                        {
                            UseFlagHoming[i] = true;
                        }
                    }
                    if (!UseFlagContena[i] && timer >= StartTimeContena[i])
                    {
                        if (IntervalTimeContena >= IntervalTriggerContena[i])
                        {
                            GameObject obj = Instantiate(ContenaMissile, transform.position, Quaternion.identity);
                            obj.GetComponent<MissileBossContena>().UpSpeed = ContenaUpSpeed;
                            obj.GetComponent<MissileBossContena>().ToSpeed = ContenaToSpeed;
                            obj.GetComponent<MissileBossContena>().ContenaNumber = ContenaNumber;
                            Destroy(obj, DestroyTimeContena);
                            IntervalTimeContena = 0.0f;
                        }
                        if (StartTimeContena[i] + DurationContena[i] <= timer)
                        {
                            UseFlagContena[i] = true;
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