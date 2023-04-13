using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDown : MonoBehaviour
{
    // ���Ď��̐ݒ�
    [Header("���Ď��̐ݒ�")]
    [Tooltip("���Ă܂ł̎���")]
    [SerializeField] float destroyTime = 3.0f;
    [Tooltip("���Ď��̈ړ�")]
    [SerializeField] Vector3 movePos;
    // �G�t�F�N�g
    [Header("�G�t�F�N�g�̐ݒ�")]
    [Tooltip("�����G�t�F�N�g�̃I�u�W�F�N�g")]
    [SerializeField] GameObject effect_1;
    [Tooltip("�����G�t�F�N�g�̃I�u�W�F�N�g")]
    [SerializeField] GameObject effect_2;

    private GameObject obj;                  // �v���C���[
    private PlayerMove playerMove;           // �v���C���[�̈ړ���؂�p
    private PlayerMoveAngle playerMoveAngle; // �v���C���[�̉�]��؂�p
    private PlayerHp playerHp;               // �o���A�j���̊��S���Ď��̃t���O�擾�p
    private float cnt = 2.0f;                // �����܂ł̎���
    private Vector3 pos;                     // ���W
    private Quaternion q;                    // �e�̉�]�̑���p
    private bool GameOverStartFlg = true;    // �����J�n�p

    public bool EffectFlag { get; protected set; } // �G�t�F�N�g�̉��o�p

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject; // �v���C���[�̃I�u�W�F�N�g����
        playerMove = gameObject.GetComponent<PlayerMove>();           // �v���C���[�̈ړ��X�N���v�g������p
        playerMoveAngle = gameObject.GetComponent<PlayerMoveAngle>(); // �v���C���[�̉�]�X�N���v�g������p
        playerHp = gameObject.GetComponent<PlayerHp>();               // �v���C���[��HP�X�N���v�g������p

        // �G�t�F�N�g�̉��o�p
        EffectFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // playerHP�ɓ����p
        //public bool BreakFlag { get; private set; }

        //BreakFlag = false;

        //if (BreakShieldFlag)
        //{
        //    // ���S�Ɍ��Ă��ꂽ����ɂ���
        //    BreakFlag = true;
        //}

        // ���S�Ɍ��Ă��ꂽ��
        if (playerHp.BreakFlag)
        {
            // ���Ă̎��Ԃ��߂�����
            if (cnt > destroyTime)
            {
                // �G�t�F�N�g�𐶐�
                Instantiate(
                    effect_2, // �G�t�F�N�g���������I�u�W�F�N�g
                    obj.transform.position, // ���W
                    Quaternion.identity);   // ��]

                // �G�t�F�N�g�̉��o�p�t���O�����Ă�
                EffectFlag = true;

                // �v���C���[�̍폜
                Destroy(this.gameObject);

                return;
            }

            // �v���C���[�̍��W����
            pos = obj.transform.position;
            // ���Ď��̈ړ�
            pos += movePos;
            movePos.y *= 1.1f;
            // �v���C���[�̍��W�ɑ��
            obj.transform.position = pos;

            // �����J�n���̂ݒʂ�
            if (GameOverStartFlg)
            {
                // �v���C���[�̈ړ��̃X�N���v�g���~
                playerMove.enabled = false;
                // �v���C���[�̉�]�̃X�N���v�g���~
                playerMoveAngle.enabled = false;

                // �e�̉�]����
                q = gameObject.transform.rotation;
                q *= Quaternion.Euler(-180, 0, 0);
                // �G�t�F�N�g�𐶐�
                effect_1 = Instantiate(
                    effect_1, // �G�t�F�N�g���������I�u�W�F�N�g
                    transform.position, // ���W
                    q); // ��]
                // �e�Ɉ����t����
                effect_1.transform.parent = gameObject.transform;
                // �����J�n�������I���ɂ���
                GameOverStartFlg = false;
            }

            // ���Ă���̎��Ԍo��
            cnt += Time.deltaTime;
        }
    }
}
