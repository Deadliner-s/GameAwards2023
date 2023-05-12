using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraEventMove : MonoBehaviour
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

    private PlayerMove playerMove;           // �v���C���[�̈ړ���؂�p
    private PlayerMoveAngle playerMoveAngle; // �v���C���[�̉�]��؂�p
    private GManager GManager; // �V�[���؂�ւ��p

    private float elapsedTime = 0.0f; // �o�ߎ���
    private bool bInput = false; // ���͔���p

    private AsyncOperation async; // �V�[���J�ڗp

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMove>();           // �v���C���[�̈ړ��X�N���v�g������p
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // �v���C���[�̉�]�X�N���v�g������p

        // �V�[���ǂݍ���
        SceneLoad("Stage2");
        // �V�[���J�ڃt���O��false�ɂ���
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
    }

    // �V�[���J�ڏ���
    private void SceneMove()
    {
        async.allowSceneActivation = true;
    }
    // �V�[���ǂݍ��ݏ���
    private void SceneLoad(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
    }
}
