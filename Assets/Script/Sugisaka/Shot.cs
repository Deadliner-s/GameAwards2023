using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public static Shot instance;

    Myproject InputActions;

    // �~�T�C���̃v���n�u
    public GameObject MissilePrefab;

    // ���U����
    public int ReloadTime;
    // �o�ߎ���
    private int currenttime;
    // �ˌ��t���O
    private bool Shotflg;
    // �~�T�C�������ꏊ
    public GameObject Shotpos;


    // ���b�N�I�����ꂽ�I�u�W�F�N�g�̃��X�g
    public List<GameObject> targets = new List<GameObject>();

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    void Start()
    {
        currenttime = 0;    // �o�ߎ��ԏ�����
        Shotflg = false;    // �t���O���I�t��

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shotflg == true)
        {
            currenttime++;

            if (currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }
    }

    public void OnShot()
    {
        if (Shotflg == false)
        {
            Vector3 PlayerPos = Shotpos.transform.position;

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
                // �V�����U���~�T�C���v���n�u���C���X�^���X������
                GameObject missile = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);

                // �U���~�T�C���̃^�[�Q�b�g��ݒ肷��
                missile.GetComponent<TrackingBullet>().SetTarget(target);
            }

            currenttime = 0;
        }
        
    }
}
