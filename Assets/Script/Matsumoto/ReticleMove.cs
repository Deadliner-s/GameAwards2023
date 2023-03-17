using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleMove : MonoBehaviour
{
    public float speed = 5.0f;      // �ړ��X�s�[�h

    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // ���݂̈ʒu
    private Vector3 nextPosition;   // �ړ���̈ʒu
    private float viewX;            // �r���[�|�[�g���W��x�̒l
    private float viewY;            // �r���[�|�[�g���W��y�̒l
    private GameObject player;     // �v���C���[

    void Awake()
    {
        // �R���g���[���[
        InputActions = new Myproject();
        InputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu
        player = GameObject.Find("Player");
        Vector3 playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        transform.position = playerPos;

        // ������
        pos = transform.position;
        nextPosition = pos;
    }

    // Update is called once per frame
    void Update()
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
}
