using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public static Shot instance;
    Myproject InputActions;

    [Header("�~�T�C�����˗p")]
    [Tooltip("�v���C���[�~�T�C���̃v���n�u")]
    public GameObject MissilePrefab;
    [Tooltip("���U����")]
    public int ReloadTime;
    [Tooltip("�~�T�C�������ꏊ")]
    public GameObject Shotpos1;
    public GameObject Shotpos2;
    public GameObject Shotpos3;
    public GameObject Shotpos4;
    public GameObject Shotpos5;

    // �o�ߎ���
    private float currenttime;
    // �ˌ��t���O
    private bool Shotflg;

    //�t�F�C�Y�؂�ւ��p
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // ���b�N�I�����ꂽ�I�u�W�F�N�g�̃��X�g
    public List<GameObject> targets;
    // �ޔ�p
    public List<GameObject> sub = new List<GameObject>();

    private int num;
    private Vector3 PlayerPos;

    public AudioClip ShotSE;
    private AudioSource audioSource;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currenttime = 0.0f;    // �o�ߎ��ԏ�����
        Shotflg = false;    // �t���O���I�t��

        // �t�F�C�Y�擾
        AtkPhaseFlg = PhaseObj.activeSelf;

        audioSource = GetComponent<AudioSource>();

        num = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Shotflg == true)
        {
            currenttime += Time.deltaTime;

            if (currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }

        // �t�F�C�Y�m�F
        AtkPhaseFlg = PhaseObj.activeSelf;
        if (AtkPhaseFlg == false)
        {
            sub = new List<GameObject>();
        }

    }

    public void OnShot()
    {
        // �t�F�C�Y�̊m�F
        if (AtkPhaseFlg == true)
        {
            // �A�^�b�N�t�F�C�Y    
            // �t���O�̊m�F
            if (Shotflg == false)
            {
                // �^�[�Q�b�g���X�g���T�u�ɑޔ�
                foreach (GameObject k in targets)
                {
                    // �^�[�Q�b�g�����X�g�Ɋ܂܂�Ă��Ȃ���Βǉ�����
                    if (!sub.Contains(k))
                    {
                        sub.Add(k);
                    }
                }

                // �����ꏊ�擾
                //Vector3 PlayerPos = Shotpos1.transform.position;
                // ���X�g�Đ���
                targets = new List<GameObject>();

                // �z��Ƀ^�O��Target�̃I�u�W�F�N�g������
                GameObject[] targetsObj = GameObject.FindGameObjectsWithTag("Target");
                foreach (GameObject target in targetsObj)
                {
                    // �^�[�Q�b�g�����X�g�Ɋ܂܂�Ă��Ȃ���Βǉ�����
                    if (!targets.Contains(target))
                    {
                        targets.Add(target);
                    }
                }

                // �e���b�N�I�����ꂽ�^�[�Q�b�g�Ƀ~�T�C����ł�
                foreach (GameObject target in targets)
                {
                    switch(num)
                    {
                        case (0):
                            // �����ꏊ�擾
                            PlayerPos = Shotpos1.transform.position;
                            break;
                        case (1):
                            // �����ꏊ�擾
                            PlayerPos = Shotpos2.transform.position;
                            break;
                        case (2):
                            // �����ꏊ�擾
                            PlayerPos = Shotpos3.transform.position;
                            break;
                        case (3):
                            // �����ꏊ�擾
                            PlayerPos = Shotpos4.transform.position;
                            break;
                        case (4):
                            // �����ꏊ�擾
                            PlayerPos = Shotpos5.transform.position;
                            break;
                    }
                    // �V�����U���~�T�C���v���n�u���C���X�^���X������
                    GameObject missile = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);

                    // �U���~�T�C���̃^�[�Q�b�g��ݒ肷��
                    missile.GetComponent<TrackingBullet>().SetTarget(target);

                    audioSource.PlayOneShot(ShotSE);

                    num++;
                }

                // �o�ߎ��ԏ�����
                currenttime = 0;
                // �ˌ��t���O�𗧂Ă�
                Shotflg = true;
                // �����ꏊ������
                num = 0;
            }
        }
    }
}
