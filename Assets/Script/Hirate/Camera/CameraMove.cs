using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // �v���C���[�܂ł̋���
    private Vector3 centerPoint;

    // ��������
    public float moveTime = 5.0f;
    public float stopTime = 5.0f;
    // �����ʒu�ƏI���ʒu
    private Vector3 start;
    private Vector3 end;
    // �J�����A�j���[�V�����p
    //private bool bInput = false;
    public bool bInput = false;
    private bool bMove = false;
    private float elapsedTime; // �o�ߎ���
    private float rate; // ����
    private GameObject childObj;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����ƃv���C���[�̋����𒲂ׂ�
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // ���̒���

        // ��̈ʒu��ݒ� (x,y,z)���W
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);

        childObj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            elapsedTime = 0;
            start = this.transform.position;
            bInput = true;
        }
        // �}�j���[�o�g�p��
        if (bInput)
        {
            // �}�j���[�o�I����
            if (bMove && elapsedTime >= moveTime)
            {
                bInput = false;
                bMove = false;
                rate = 0;
                Debug.Log("end");
                return;
            }
            // �J�����ړ��J�n
            if (bMove)
            {
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
            Debug.Log(elapsedTime);
        }
        // �ʏ펞
        if (!bInput)
        {
            // �J�������v���C���[�ɒǏ]������
            // ��x�v���C���[�̍��W�Ɠ����ɂ�����
            Vector3 pos = player.transform.position;
            pos += centerPoint;
            // �J�����������Ă�������Ƃ͋t�����Ƀv���C���[���痣��
            pos -= transform.forward * playerDistance;
            // �V�����J�����̈ʒu
            transform.position = pos;
            this.transform.LookAt(enemy.transform.position);
            //Debug.Log("no");
        }
    }
}
