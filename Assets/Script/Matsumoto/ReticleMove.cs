using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleMove : MonoBehaviour
{
    [Header("�v���C���[���猩���Ə��̏����ʒu")]
    public float initPosX = 0.0f;
    public float initPosY = 50.0f;

    [Header("�ړ����x")]
    public float speed = 5.0f;      // �ړ��X�s�[�h
    [Header("���b�N�I���ł���ő吔")]
    public int MaxRockOn = 5;       // ���b�N�I���ł���ő吔
    [Header("�Ə��̔��a")]
    public float ReticleRadius = 50.0f; // �Ə��̔��a
    [Header("�����񂹂��n�߂鋗��")]
    public float AttractDistance = 50.0f;
    [Header("�����񂹂����")]
    public float AttractPower = 10.0f;



    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // ���݂̈ʒu
    private Vector3 nextPosition;   // �ړ���̈ʒu
    private float viewX;            // �r���[�|�[�g���W��x�̒l
    private float viewY;            // �r���[�|�[�g���W��y�̒l
    private GameObject player;      // �v���C���[


    private GameObject[] RockOnCnt = new GameObject[5];     // ���b�N�I���ł��鐔�I�u�W�F�N�g
    private GameObject RockOnCntPrefab;                     // ����Prefab
    private float offsetY = 40.0f;

    private GameObject enemy;
    private Vector3 enemy2D;

    void Awake()
    {
        // �R���g���[���[
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu
        player = GameObject.Find("Player");
        Vector3 playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        playerPos.x = playerPos.x + initPosX;
        playerPos.y = playerPos.y + initPosY;
        transform.position = playerPos;

        // ������
        pos = transform.position;
        nextPosition = pos;


        // ���b�N�I���ł��鐔���A���𐶐�����
        for (int i = 0; i < 5; i++)
        {
            RockOnCntPrefab = (GameObject)Resources.Load("RockOnCnt");
            RockOnCnt[i] = Instantiate(RockOnCntPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        // �Ə��̉��ɕ\��������
        Vector3 thisPos = this.transform.position;
        RockOnCnt[4].transform.position = new Vector3(thisPos.x - 25.0f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[3].transform.position = new Vector3(thisPos.x - 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[2].transform.position = new Vector3(thisPos.x, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[1].transform.position = new Vector3(thisPos.x + 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[0].transform.position = new Vector3(thisPos.x + 25.0f, thisPos.y - offsetY, thisPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        Move();


        // ���b�N�I�������^�[�Q�b�g�^�O���t�����I�u�W�F�N�g���J�E���g
        GameObject[] tagObj = GameObject.FindGameObjectsWithTag("Target");

        // ��ԋ߂��G�̃I�u�W�F�N�g���擾
        enemy = serchTag(this.gameObject, "Enemy");

        if (enemy != null)
        {
            if(enemy.GetComponent<RockOnMarker>() != null)
            {
                // ���b�N�I���̃}�[�N���o�����Ă���ꍇ
                if (enemy.GetComponent<RockOnMarker>().GetRockOnFlg() == true)
                {
                    // ���b�N�I���̃}�[�N���B��Ă��Ȃ��ꍇ
                    if (enemy.GetComponent<RockOnMarker>().GetHideFlg() == false)
                    {
                        // ���[���h���W���X�N���[�����W�ɕϊ�
                        enemy2D = RectTransformUtility.WorldToScreenPoint(Camera.main, enemy.transform.position);

                        // �O�����̒藝
                        float a;                     // ��X
                        float b;                     // ��Y
                        float c;                     // a - b�Ԃ̋���
                        float TargetRadius = enemy.GetComponent<RockOnMarker>().TargetRadius;

                        a = enemy2D.x - transform.position.x;
                        b = enemy2D.y - transform.position.y;
                        a = a * a;                   // a�̗ݏ�
                        b = b * b;                   // b�̗ݏ�
                        c = a + b;                   // a + b �̋���
                        c = (float)Math.Sqrt(c);     // ������
                       
                        float r = ReticleRadius + TargetRadius;

                        // �G�ɋ߂Â�����Ə��������񂹂�
                        if (c <= AttractPower)
                        {
                            // �^�O�������Ă�����    ���b�N�I��������̃I�u�W�F�N�g�Ɋ��Ȃ��悤��
                            if (enemy.tag == "Enemy")
                            {
                                transform.position = Vector2.MoveTowards(transform.position, enemy2D, AttractDistance);
                            }
                        }

                        // �����蔻��
                        if (c <= r)
                        {
                            // ���b�N�I���ł��鐔�ȉ���������
                            if (tagObj.Length < MaxRockOn)
                            {
                                //// ��]
                                //Transform transform = target.transform;
                                //Vector3 angle = transform.localEulerAngles;
                                //angle.z = 45.0f;
                                //transform.localEulerAngles = angle;
                                //// �F��ԂɕύX
                                //Color color = target.GetComponent<Image>().color = Color.red; ;

                                enemy.GetComponent<RockOnMarker>().RockOnAnime();
                                enemy.gameObject.tag = "Target";
                                SoundManager.instance.PlaySE("RockOn");
                            }
                        }
                    }
                }
            }
        }

        // ���́E�̏���
        // ���b�N�I���������ɂ���ĐF��ς���
        if (tagObj.Length > 0)
        {
            for (int i = 0; i < tagObj.Length; i++)
            {
                Color color = RockOnCnt[i].GetComponent<Image>().color = Color.gray;
            }
        }
        // ���b�N�I���������ꂽ��F��߂�
        for (int i = tagObj.Length; i < 5; i++)
        {
            Color color = RockOnCnt[i].GetComponent<Image>().color = Color.white;
        }
    }

    private void Move()
    {
        // ����
        Vector3 move = InputActions.Player.Reticle.ReadValue<Vector2>();
        nextPosition = pos + move * speed;

        // �ړ���̃r���[�|�[�g���W��x�̒l���擾
        viewX = Camera.main.ScreenToViewportPoint(nextPosition).x;
        viewY = Camera.main.ScreenToViewportPoint(nextPosition).y;

        // �ړ���̃r���[�|�[�g���W���O����P�͈̔͂Ȃ��
        if (0.0f <= viewX && viewX <= 1.0f && 0.0f <= viewY && viewY <= 1.0f)
        {
            // �ړ�
            transform.position = nextPosition;

            pos = nextPosition;
        }
    }



    private GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //�����p�ꎞ�ϐ�
        float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
        //string nearObjName = "";    //�I�u�W�F�N�g����
        GameObject targetObj = null; //�I�u�W�F�N�g

        //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            Vector3 enemypos = RectTransformUtility.WorldToScreenPoint(Camera.main, obs.transform.position);

            //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
            tmpDis = Vector3.Distance(enemypos, nowObj.transform.position);

            //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
            //�ꎞ�ϐ��ɋ������i�[
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //�ł��߂������I�u�W�F�N�g��Ԃ�
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
