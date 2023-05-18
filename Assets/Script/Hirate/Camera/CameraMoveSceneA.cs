using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSceneA : MonoBehaviour
{
    private GameObject player;
    private GameObject playerManager;

    public GameObject next;

    private float playerDistance = 0.0f; // �v���C���[�܂ł̋���
    private Vector3 centerPoint;

    // ��������
    [SerializeField] float moveTime = 5.0f;
    [SerializeField] float stopTime = 5.0f;
    // �����ʒu�ƏI���ʒu
    private Vector3 start;
    private Vector3 end;
    // �J�����A�j���[�V�����p
    private bool bMove = false;
    private float elapsedTime; // �o�ߎ���
    private float rate; // ����
    private GameObject childObj;
    //private PlayerMove playerMove;           // �v���C���[�̈ړ���؂�p
    //private PlayerMoveAngle playerMoveAngle; // �v���C���[�̉�]��؂�p

    public bool bInput { get; private set; } = false;
    public bool bSceneMove { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        //�J�����ƃv���C���[�̋����𒲂ׂ�
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // ���̒���

        // ��̈ʒu��ݒ� (x,y,z)���W
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);

        childObj = transform.GetChild(0).gameObject;
        //playerMove = player.GetComponent<PlayerMove>();           // �v���C���[�̈ړ��X�N���v�g������p
        //playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // �v���C���[�̉�]�X�N���v�g������p
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            elapsedTime = 0;
            start = this.transform.position;
            bInput = true;
            next.SetActive(true);
        }
        // �}�j���[�o�g�p��
        if (bInput)
        {
            // �v���C���[�̈ړ��̃X�N���v�g���~
            playerManager.GetComponent<PlayerMove>().enabled = false;
            // �v���C���[�̉�]�̃X�N���v�g���~
            playerManager.GetComponent<PlayerMoveAngle>().enabled = false;

            // �}�j���[�o�I����
            if (bMove && elapsedTime >= moveTime)
            {
                // �v���C���[�̈ړ��̃X�N���v�g���~
                playerManager.GetComponent<PlayerMove>().enabled = true;
                // �v���C���[�̉�]�̃X�N���v�g���~
                playerManager.GetComponent<PlayerMoveAngle>().enabled = true;
                bInput = false;
                bMove = false;
                rate = 0;
                // �V�[���J�ڃt���O�����Ă�
                bSceneMove = true;
                return;
            }
            // �J�����ړ��J�n
            if (bMove)
            {
                Vector3 pos = player.transform.position;
                pos.x += 0.02f;
                pos.z += 0.001f;
                player.transform.position = pos;
                // �X�^�[�g�n�_�����݂̍��W�ɂ���
                transform.position = Vector3.Lerp(start, end, rate);
                this.transform.LookAt(player.transform.position);
                rate = elapsedTime / moveTime;
                //Debug.Log(rate);
            }
            // �}�j���[�o�J�n���̃J�����ړ���~
            if (!bMove && elapsedTime <= stopTime)
            {
                this.transform.LookAt(player.transform.position);
                //Debug.Log("stop");
            }
            // �J�����ړ��J�n�̑O����
            if (!bMove && elapsedTime >= stopTime)
            {
                bMove = true;
                elapsedTime = 0;
                end = childObj.transform.position;
            }
            // ���Ԍo��
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
        }
    }
}
