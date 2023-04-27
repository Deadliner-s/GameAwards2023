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
    public GameObject Shotpos;
    [Tooltip("���ˊԊu")]
    public float interval;

    // �o�ߎ���
    private float currenttime;
    // �C���^�[�o���p����
    private float intervalTime;
    // �ˌ��t���O
    public bool Shotflg;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;
    private PhaseManager.Phase nextPhase;

    // ���b�N�I�����ꂽ�I�u�W�F�N�g�̃��X�g
    public List<GameObject> targets;
    // �ޔ�p
    public List<GameObject> sub = new List<GameObject>();

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
        InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
    }

    // Start is called before the first frame update
    private void Start()
    {
        currenttime = 0.0f;    // �o�ߎ��ԏ�����
        intervalTime = 0.0f;
        Shotflg = false;    // �t���O���I�t��

        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();
        nextPhase = currentPhase;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Shotflg == true)
        {
            // ���Ԃ̍X�V
            currenttime += Time.deltaTime;

            // �e�̔���
            if (targets.Count != 0)                
            {
                intervalTime += Time.deltaTime;

                if (interval <= intervalTime)
                {
                    // ���b�N�I�����ꂽ�^�[�Q�b�g�Ƀ~�T�C����ł�
                    // �����ꏊ�擾
                    Vector3 PlayerPos = Shotpos.transform.position;

                    // 
                    while (targets[0] == null)
                    {
                        if (targets[1] == null)
                        {
                            break;
                        }
                        targets.RemoveAt(0);
                    }

                    //
                    if (targets.Count != 0)
                    {

                        // �V�����U���~�T�C���v���n�u���C���X�^���X������
                        GameObject missile = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);

                        // �~�T�C���̃^�[�Q�b�g��ݒ肷��
                        missile.GetComponent<TrackingMissile_2>().SetTarget(targets[0]);

                        //SE�Đ�
                        int num = targets.Count;
                        switch (num)
                        {
                            case (5):
                                SoundManager.instance.Play("Shot5");
                                break;
                            case (4):
                                SoundManager.instance.Play("Shot4");
                                break;
                            case (3):
                                SoundManager.instance.Play("Shot3");
                                break;
                            case (2):
                                SoundManager.instance.Play("Shot2");
                                break;
                            case (1):
                                SoundManager.instance.Play("Shot1");
                                break;
                        }

                        // �^�[�Q�b�g���X�g�X�V
                        targets.RemoveAt(0);

                        intervalTime = 0.0f;
                    }
                }   
            }

            // �t���O�Ǘ�
            if (targets.Count == 0 && currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }


        // �t�F�C�Y�m�F

        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();
        if (currentPhase != nextPhase)
        {
            nextPhase = currentPhase;
            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                sub = new List<GameObject>();
            }
        }
    }

    public void OnShot()
    {
        // �t�F�C�Y�̊m�F
        if (currentPhase == PhaseManager.Phase.Attack_Phase)
        {
            // �A�^�b�N�t�F�C�Y    
            // �t���O�̊m�F
            if (Shotflg == false)
            {
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

                // �o�ߎ��ԏ�����
                currenttime = 0.0f;
                // �ˌ��t���O�𗧂Ă�
                Shotflg = true;
                // �����˗p
                intervalTime = 10.0f;
            }
        }
    }
}
