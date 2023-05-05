using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraEventMove3 : MonoBehaviour
{
    // �I�u�W�F�N�g�̐ݒ�
    [Header("�I�u�W�F�N�g�ݒ�")]
    [Tooltip("�v���C���[�̃I�u�W�F�N�g")]
    public GameObject player;

    // �ړ������̐ݒ�
    [Header("�ړ������̐ݒ�")]
    [Tooltip("�ړ�����")]
    [SerializeField] float move = 2.0f;
    // �ړ����Ԃ̐ݒ�
    [Header("�ړ����Ԃ̐ݒ�")]
    // �v���C���[�̐ݒ�
    [Tooltip("�v���C���[�̈ړ�����")]
    [SerializeField] float moveTimePlayer = 0.5f;
    // �J�����̐ݒ�
    [Tooltip("�J�����̈ړ�����")]
    [SerializeField] float moveTimeCamera = 1.0f;

    // �V�[���J�ڗp
    private enum Scene
    {
        Scene1 = 1,
        Scene2,
        Scene3,

        SceneMax = 99,
    }

    private PlayerMove playerMove;           // �v���C���[�̈ړ���؂�p
    private PlayerMoveAngle playerMoveAngle; // �v���C���[�̉�]��؂�p
    private GManager GManager; // �V�[���؂�ւ��p

    private float elapsedTime = 0.0f; // �o�ߎ���
    private bool bInput = false; // ���͔���p
    private bool bMoveCamera = false; // �J�����̈ړ����n�܂������̔���p

    private AsyncOperation async;

    //public bool bInput { get; private set; } = false;
    //public bool bSceneMove { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMove>();           // �v���C���[�̈ړ��X�N���v�g������p
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // �v���C���[�̉�]�X�N���v�g������p

        async = SceneManager.LoadSceneAsync("merge_2");
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !bInput)
        {
            // �v���C���[�̈ړ��̃X�N���v�g���~
            if (playerMove != null)
            {
                playerMove.enabled = false;
            }
            // �v���C���[�̉�]�̃X�N���v�g���~
            if (playerMoveAngle != null)
            {
                playerMoveAngle.enabled = false;
            }

            //---- ���[�ނꂷ�Ȉړ� ----
            // �A���œ��삳���邽�߂̑O����
            var DOTMove = DOTween.Sequence();
            // �v���C���[�̈ړ��̒ǉ�
            float pos = player.transform.position.z;
            DOTMove.Append(player.transform.DOMoveZ(pos + move, moveTimePlayer));
            // �J�����̈ړ��̒ǉ�
            pos = gameObject.transform.position.z;
            DOTMove.Append(gameObject.transform.DOMoveZ(pos + move, moveTimeCamera));

            // �ړ����s
            DOTMove.Play().OnComplete(SceneMove);

            // ������x�����Ă����s����Ȃ��悤�ɂ���
            bInput = true;
        }

        //if (elapsedTime >= moveTimePlayer && !bMoveCamera && bInput)
        //{
        //    // �J�����̈ړ�
        //    //gameObject.transform.DOMoveZ(move, moveTimeCamera);
        //    // ������x���s����Ȃ��悤�ɂ���
        //    bMoveCamera = true;
        //}

        //// �V�[���J�ڏ���
        //if (elapsedTime >= (moveTimePlayer + moveTimeCamera))
        //{
        //    SceneManager.LoadScene("Stage1");
        //    //GManager.instance.SetClearFlg((int)Scene.Scene1);
        //}

        //if (bInput)
        //{
        //    // ���Ԍo��
        //    elapsedTime += Time.deltaTime;
        //    Debug.Log(gameObject.transform.position);
        //}
    }

    // �V�[���J�ڏ���
    private void SceneMove()
    {
        async.allowSceneActivation = true;
    }

    //void LateUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        elapsedTime = 0;
    //        start = this.transform.position;
    //        bInput = true;
    //    }
    //    // �}�j���[�o�g�p��
    //    if (bInput)
    //    {
    //        // �v���C���[�̈ړ��̃X�N���v�g���~
    //        playerMove.enabled = false;
    //        // �v���C���[�̉�]�̃X�N���v�g���~
    //        playerMoveAngle.enabled = false;

    //        // �}�j���[�o�I����
    //        if (bMove && elapsedTime >= moveTime)
    //        {
    //            // �v���C���[�̈ړ��̃X�N���v�g���~
    //            playerMove.enabled = true;
    //            // �v���C���[�̉�]�̃X�N���v�g���~
    //            playerMoveAngle.enabled = true;
    //            bInput = false;
    //            bMove = false;
    //            rate = 0;
    //            // �V�[���J�ڃt���O�����Ă�
    //            bSceneMove = true;
    //            return;
    //        }
    //        // �J�����ړ��J�n
    //        if (bMove)
    //        {
    //            Vector3 pos = player.transform.position;
    //            pos.x += 0.02f;
    //            pos.z += 0.001f;
    //            player.transform.position = pos;
    //            // �X�^�[�g�n�_�����݂̍��W�ɂ���
    //            transform.position = Vector3.Lerp(start, end, rate);
    //            this.transform.LookAt(player.transform.position);
    //            rate = elapsedTime / moveTime;
    //            //Debug.Log(rate);
    //        }
    //        // �}�j���[�o�J�n���̃J�����ړ���~
    //        if (!bMove && elapsedTime <= stopTime)
    //        {
    //            this.transform.LookAt(player.transform.position);
    //            //Debug.Log("stop");
    //        }
    //        // �J�����ړ��J�n�̑O����
    //        if (!bMove && elapsedTime >= stopTime)
    //        {
    //            bMove = true;
    //            elapsedTime = 0;
    //            end = childObj.transform.position;
    //        }
    //        // ���Ԍo��
    //        elapsedTime += Time.deltaTime;
    //        //Debug.Log(elapsedTime);

    //        //// �J�������v���C���[�ɒǏ]������
    //        //// ��x�v���C���[�̍��W�Ɠ����ɂ�����
    //        //Vector3 pos = player.transform.position;
    //        //pos += centerPoint;
    //        //// �J�����������Ă�������Ƃ͋t�����Ƀv���C���[���痣��
    //        //pos -= transform.forward * playerDistance;
    //        //// �V�����J�����̈ʒu
    //        //transform.position = pos;
    //    }
    //}
}
