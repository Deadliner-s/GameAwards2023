using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseManager;
using Effekseer;
using UnityEngine.InputSystem;

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

    [Header("�ʏ�t�F�C�Y���̈ړ����x")]
    [Tooltip("�c�ړ�")]
    public float Normal_VerticalSpeed = 0.01f;
    [Tooltip("���ړ�")]
    public float Normal_HorizontalSpeed = 0.01f;

    [Header("�n�C�X�s�[�h�t�F�C�Y���̈ړ����x")]
    [Tooltip("�c�ړ�")]
    public float Spd_VerticalSpeed = 0.005f;
    [Tooltip("���ړ�")]
    public float Spd_HorizontalSpeed = 0.01f;

    [Header("�A�^�b�N�t�F�C�Y���̈ړ����x")]
    [Tooltip("�c�ړ�")]
    public float Atk_VerticalSpeed = 0.01f;
    [Tooltip("���ړ�")]
    public float Atk_HorizontalSpeed = 0.01f;

    // �N�C�b�N�}�j���[�o�p�ϐ�
    [Header("�}�j���[�o����")]
    [Tooltip("�}�j���[�o�̎�������")]
    public float ManerverTime = 0.5f;   // ��������
    [Tooltip("�N�[���^�C��")]
    public float coolTime = 1.0f;       // �Ďg�p�ł���悤�ɂȂ鎞��
    [Header("�}�j���[�o���̈ړ����x")]
    [Tooltip("�c�ړ�")]
    public float Manever_VerticalSpeed = 0.03f;   // �}�j���[�o���̑��x
    [Tooltip("���ړ�")]
    public float Manever_HorizontalSpeed = 0.03f;

    // �}�j���[�o�J�E���g
    private float elapsedTime;          // �}�j���[�o�o�ߎ���
    private float coolTimeCnt;          // �N�[���^�C���̌o�ߎ���
    //private bool maneverFlg = false;
    public bool maneverFlg { get; private set; } = false; // �}�j���[�o�̃t���O

    [Header("SE�֌W")]
    public AudioClip MoveSE;
    public AudioClip ManeverSE;
    private AudioSource audioSource;
    private bool MoveSeFlg = false;
    [Header("�G�t�F�N�g")]
    public EffekseerEffectAsset effect;     // �Đ�����G�t�F�N�g
    private EffekseerHandle handle;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;
    private PhaseManager.Phase nextPhase;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Manever.performed += context => OnManever();

        InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
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
        currentPhase = PhaseManager.instance.GetPhase();

        // SE
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MoveSE;

        // �G�t�F�N�g���擾����B
        effect = Resources.Load<EffekseerEffectAsset>(effect.name);

        nextPhase = currentPhase;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ���W�v�Z
        if (maneverFlg == false)
        {
            if(currentPhase == PhaseManager.Phase.Normal_Phase)
            {
                // �ʏ�t�F�C�Y
                // �c�����̈ړ�
                nextPosition.y = pos.y + inputMove.y * Normal_VerticalSpeed;
                // �������̈ړ�
                nextPosition.x = pos.x + inputMove.x * Normal_HorizontalSpeed;
            }
            else if (currentPhase == PhaseManager.Phase.Speed_Phase) 
            {
                // �n�C�X�s�[�h�t�F�C�Y
                // �c�����̈ړ�
                nextPosition.y = pos.y + inputMove.y * Spd_VerticalSpeed;
                // �������̈ړ�
                nextPosition.x = pos.x + inputMove.x * Spd_HorizontalSpeed;
            }
            else if(currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                // �A�^�b�N�t�F�C�Y
                // �c�����̈ړ�
                nextPosition.y = pos.y + inputMove.y * Atk_VerticalSpeed;
                // �������̈ړ�
                nextPosition.x = pos.x + inputMove.x * Atk_HorizontalSpeed;
            }
        }
        else
        {
            // �N�C�b�N�}�j���[�o
            // �c�����̈ړ�
            nextPosition.y = pos.y + inputMove.y * Manever_VerticalSpeed;
            // �������̈ړ�
            nextPosition.x = pos.x + inputMove.x * Manever_HorizontalSpeed;

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

        // �t�F�C�Y���ς�����ꍇ
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;
            // �ʏ�t�F�C�Y
            if (currentPhase == Phase.Normal_Phase)
            {
                
            }
            // �n�C�X�s�[�h�t�F�C�Y
            if (currentPhase == Phase.Speed_Phase)
            {
                // transform�̈ʒu�ŃG�t�F�N�g���Đ�����
                handle = EffekseerSystem.PlayEffect(effect, transform.position);
                // transform�̉�]��ݒ肷��B
                // �v���C���[�̌X���ɉe������Ȃ�
                Vector3 eulerAngles = this.transform.eulerAngles;
                Vector3 rot = new Vector3(0.0f, eulerAngles.y, eulerAngles.z);
                Quaternion quaternion = Quaternion.Euler(rot);
                handle.SetLocation(transform.position);
                handle.SetRotation(quaternion);
            }
            // �A�^�b�N�t�F�C�Y
            if (currentPhase == Phase.Attack_Phase)
            {
                
            }
        }
        // �G�t�F�N�g���v���C���[�ɒǏ]������
        handle.SetLocation(transform.position);
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
