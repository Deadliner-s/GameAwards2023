using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleMove : MonoBehaviour
{
    public float speed = 5.0f;      // �ړ��X�s�[�h

    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // ���݂̈ʒu
    private Vector3 nextPosition;   // �ړ���̈ʒu
    private float viewX;            // �r���[�|�[�g���W��x�̒l
    private float viewY;            // �r���[�|�[�g���W��y�̒l
    private GameObject player;     // �v���C���[


    private GameObject[] RockOnCnt = new GameObject[5];     // ���b�N�I���ł��鐔�I�u�W�F�N�g
    private GameObject RockOnCntPrefab;                     // ����Prefab
    private float offsetY = 40.0f;

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

        // ���b�N�I�������^�[�Q�b�g�^�O���t�����I�u�W�F�N�g���J�E���g
        GameObject[] tagObj = GameObject.FindGameObjectsWithTag("Target");

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
}
