using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class CameraEventMove2 : MonoBehaviour
{
    // �ړ������̐ݒ�
    //[Header("�ړ������̐ݒ�")]
    //[Tooltip("�ړ�����")]
    //[SerializeField] float move = 5.0f;
    // �ړ����Ԃ̐ݒ�
    //[Header("�ړ����Ԃ̐ݒ�")]
    // �{�X�̐ݒ�
    //[Tooltip("�{�X�̈ړ�����")]
    //[SerializeField] float moveTimeBoss = 0.5f;
    // �J�����̐ݒ�
    //[Tooltip("�J�����̈ړ�����")]
    //[SerializeField] float moveTimeCamera = 1.0f;

    private GameObject player; // �v���C���[�I�u�W�F�N�g
    private GameObject boss; // �{�X�I�u�W�F�N�g
    private PlayerMove playerMove;           // �v���C���[�̈ړ���؂�p
    private PlayerMoveAngle playerMoveAngle; // �v���C���[�̉�]��؂�p

    private Vector3 initPos;
    //private bool bInput = false; // ���͔���p

    //private AsyncOperation async;

    private GameObject fade; // �t�F�[�h�I�u�W�F�N�g

    // �������Ԃ̐ݒ�
    [Header("��������")]
    [Tooltip("��������")]
    [SerializeField] float TimeLimit = 60; //��������
    private float Counttime; //���Ԃ𑪂�

    private bool bSceneStart = false;
    public bool bAniEnd { get; set; } = false; // �A�j���[�V�����̏I���擾�p

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̎擾
        player = GameObject.FindGameObjectWithTag("Player");
        // �{�X�̎擾
        boss = GameObject.FindGameObjectWithTag("BossParent");
        // �t�F�[�h�̎擾
        fade = GameObject.FindGameObjectWithTag("Fade");

        playerMove = player.GetComponent<PlayerMove>();           // �v���C���[�̈ړ��X�N���v�g������p
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // �v���C���[�̉�]�X�N���v�g������p

        // �J�����̏����ʒu���擾
        initPos = gameObject.transform.position;

        //async = SceneManager.LoadSceneAsync("Stage2");
        //async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
    }

    // �V�[���J�ڏ���
    private void SceneMove()
    {
        //async.allowSceneActivation = true;
        //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2");

        // SceneMoveManager���^�O����
        GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
        // �{�X�̃��[�e�[�V������������
        GameObject boss = GameObject.FindGameObjectWithTag("BossParent");
        BossRotationCatch.instance.fRotation = boss.transform.rotation;
        // �V�[���̊J�n
        obj.GetComponent<SceneMoveManager>().SceneStartUnload();
    }

    // �J�E���g�_�E��
    private void CountTime()
    {
        Counttime += Time.deltaTime;//���Ԃ𑫂�

        if (Counttime > TimeLimit)
        {
            // �V�[�����X�ȑJ��
            //Seamless();

            // �A�j���[�V�����I���������̔���
            if (bAniEnd)
            {
                // �V�[���J��
                SceneMove();
            }
        }

        if (Input.GetKeyDown(KeyCode.H) ||
            Input.GetKeyDown(KeyCode.P))
        {
            if (bSceneStart) { return; }

            // �V�[���J��
            SceneMove();

            bSceneStart = true;
        }
    }

    // �V�[�����X�ȑJ��
    private void Seamless()
    {
        //if (bInput) { return; }
        //// �v���C���[�̈ړ��̃X�N���v�g���~
        //if (playerMove != null)
        //{
        //    playerMove.enabled = false;
        //}
        //// �v���C���[�̉�]�̃X�N���v�g���~
        //if (playerMoveAngle != null)
        //{
        //    playerMoveAngle.enabled = false;
        //}

        ////---- ���[�ނꂷ�Ȉړ� ----
        //// �A���œ��삳���邽�߂̑O����
        //var DOTMove = DOTween.Sequence();
        ////�{�X�̈ړ��̒ǉ�
        //float pos = boss.transform.position.z;
        //DOTMove.Append(boss.transform.DOMoveZ(pos - move, moveTimeBoss));
        //// �J�����̈ړ��̒ǉ�
        //pos = gameObject.transform.position.z;
        //DOTMove.Join(gameObject.transform.DOMoveZ(
        //    pos - move, moveTimeBoss));
        //// ��������������A�J�����̈ړ����s��
        //// ���̌�A�V�[���J�ڏ���
        //DOTMove.Append(gameObject.transform.DOMoveZ(
        //    initPos.z, moveTimeCamera));
        //// �ړ����s
        //DOTMove.Play().OnComplete(SceneMove);

        //// ������x�����Ă����s����Ȃ��悤�ɂ���
        //bInput = true;
    }
}
