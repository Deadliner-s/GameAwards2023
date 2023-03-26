using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    // ����
    private Myproject InputActions;
    private Vector2 inputMove;

    // ���W
    private Vector3 pos;            // ���݂̈ʒu
    private Vector3 nextPosition;   // �ړ���̈ʒu
    private float viewX;            // �r���[�|�[�g���W��x�̒l
    private float viewY;            // �r���[�|�[�g���W��y�̒l

    [Header("�ړ����x")]
    [Tooltip("�ړ����x")]
    public float speed = 0.1f;

    [Header("�A�^�b�N�t�F�C�Y���̏㉺�ړ����x")]
    [Tooltip("�㉺�ړ����x")]
    public float AtkSpeed = 0.05f;

    // �N�C�b�N�}�j���[�o�p�ϐ�
    [Header("�}�j���[�o")]
    [Tooltip("�}�j���[�o�̎�������")]
    public float ManerverTime = 0.5f;   // ��������
    [Tooltip("�N�[���^�C��")]
    public float coolTime = 1.0f;       // �Ďg�p�ł���悤�ɂȂ鎞��
    [Tooltip("���x")]
    public float ManeverSpeed = 0.5f;   // �}�j���[�o���̑��x

    // �}�j���[�o�J�E���g
    private float elapsedTime;          // �}�j���[�o�o�ߎ���
    private float coolTimeCnt;          // �N�[���^�C���̌o�ߎ���
    private bool maneverFlg = false;

    // �t�F�C�Y�؂�ւ��p
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;


    [Header("SE�֌W")]
    public AudioClip MoveSE;
    public AudioClip ManeverSE;
    private AudioSource audioSource;
    private bool MoveSeFlg = false;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Manever.performed += context => OnManever();
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������
        pos = transform.position;
        nextPosition = pos;

        // �}�j���[�o�ϐ�������
        elapsedTime = ManerverTime + coolTime;
        coolTimeCnt = 0;

        // �t�F�C�Y�擾
        AtkPhaseFlg = PhaseObj.activeSelf;

        // SE
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MoveSE;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�C�Y�m�F
        AtkPhaseFlg = PhaseObj.activeSelf;

        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ���W�v�Z
        if (maneverFlg == false)
        {
            if (AtkPhaseFlg == false)
            {
                // �n�C�X�s�[�h�t�F�C�Y
                // �c�����̈ړ�
                nextPosition.y = pos.y + inputMove.y * speed;
                // �������̈ړ�
                nextPosition.x = pos.x + inputMove.x * speed;
            }
            else
            {
                // �A�^�b�N�t�F�C�Y
                // �c�����̈ړ�
                nextPosition.y = pos.y + inputMove.y * AtkSpeed;
                // �������̈ړ�
                nextPosition.x = pos.x + inputMove.x * speed;
            }
        }
        else
        {
            // �N�C�b�N�}�j���[�o
            // �c�����̈ړ�
            nextPosition.y = pos.y + inputMove.y * ManeverSpeed;
            // �������̈ړ�
            nextPosition.x = pos.x + inputMove.x * ManeverSpeed;

            // �}�j���[�o�o�ߎ���
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > ManerverTime)
        {
            // �}�j���[�o�ł��鎞�Ԃ��߂�����
            maneverFlg = false;
            // �N�[���^�C���̃J�E���g���n�߂�
            coolTimeCnt += Time.deltaTime;
        }

        // �ړ���̃r���[�|�[�g���W��x�̒l���擾
        viewX = Camera.main.WorldToViewportPoint(nextPosition).x;
        viewY = Camera.main.WorldToViewportPoint(nextPosition).y;

        // �ړ���̃r���[�|�[�g���W���O����P�͈̔͂Ȃ��
        if (0.0f <= viewX && viewX <= 1.0f && 0.0f <= viewY && viewY <= 1.0f)
        {
            // �ړ��X�V
            transform.position = nextPosition;

            pos = nextPosition;
        }

        // �����Ă鎞�ɖ�SE
        if (0.0f < inputMove.x)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f > inputMove.x)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f < inputMove.y)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f > inputMove.y)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f == inputMove.x && 0.0f == inputMove.y && MoveSeFlg == true)
        {
            audioSource.Stop();
            MoveSeFlg = false;
        }
    }

    private void OnManever()
    {
        // �N�[���^�C�����I����Ă����� ���A�^�b�N�t�F�C�Y�ł͂Ȃ�������
        if (coolTimeCnt > coolTime)
        {
            maneverFlg = true;
            elapsedTime = 0;
            coolTimeCnt = 0;
            audioSource.PlayOneShot(ManeverSE);
        }
    }
}
