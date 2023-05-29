using UnityEngine;

public class MoveToInitialPosition : MonoBehaviour
{
    private GameObject player;          // �v���C���[

    public Transform initialTransform;  // �����ʒu
    public float duration = 2.0f;       // �ړ��ɂ����鎞��

    private Vector3 startPosition;      // �ړ��J�n���̈ʒu
    private Quaternion startRotation;   // �ړ��J�n���̉�]
    private float startTime;            // ����

    private void Start()
    {
        // �v���C���[�̎擾
        player = GameObject.Find("Player");

        // �v���C���[�̈ړ����~
        this.gameObject.GetComponent<PlayerMove>().enabled = false;
        this.gameObject.GetComponent<PlayerMoveAngle>().enabled = false;

        // �����ʒu��ݒ�
        startPosition = player.transform.position;
        startRotation = player.transform.rotation;

        // ���Ԃ�������
        startTime = Time.time;

        // ���͂𖳌���
        InputManager.instance.InputActions.Player.Disable();
    }

    private void Update()
    {
        // �o�ߎ��Ԃ��擾
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // �ړ�
        player.transform.position = Vector3.Lerp(startPosition, initialTransform.position, t);
        // ��]
        player.transform.rotation = Quaternion.Lerp(startRotation, initialTransform.rotation, t);

        // �w�肵���b���o�ߌ�ɏ����ʒu�ɓ��B�����珈�����I��
        if (t >= 1f)
        {
            // ���B��̏����������ɒǉ�

            enabled = false; // �X�N���v�g�𖳌������Ē�~
        }
    }
}
