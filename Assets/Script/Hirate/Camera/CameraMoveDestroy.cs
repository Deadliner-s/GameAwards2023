using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMoveDestroy : MonoBehaviour
{
    // �v���C���[
    [Header("�I�u�W�F�N�g�ݒ�")]
    private GameObject player;          // �v���C���[
    private GameObject playerManager;   // �v���C���[�}�l�[�W���[
    [Tooltip("�����̃I�u�W�F�N�g")]
    [SerializeField] GameObject obj;
    // �G�t�F�N�g
    [Tooltip("�G�t�F�N�g�̉��o����")]
    [SerializeField] float effectTime = 3.0f;

    // ���݂̃V�[���̐ݒ�
    [Header("���݂̃V�[���ݒ�")]
    [Tooltip("���݂̃V�[��")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNow;

    // ���̃V�[���̐ݒ�
    [Header("���̃V�[���ݒ�")]
    [Tooltip("���̃V�[��")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNext;

    private float playerDistance = 0.0f; // �v���C���[�܂ł̋���
    private Vector3 centerPoint;
    //private PlayerHp playerHp; // �o���A�j���̊��S���Ď��̃t���O�擾�p
    //private ShotDown shotDown; // �G�t�F�N�g���o�p
    private float cnt = 0.0f;  // �o�ߎ���
    private bool breakStart;   // ���j�J�n

    [SerializeField]
    private GameObject fade; // �t�F�[�h�I�u�W�F�N�g
    private bool bFade = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        //// �v���C���[��HP�X�N���v�g������p
        //playerHp = player.GetComponent<PlayerHp>();
        //// ���Ď��̃X�N���v�g������p
        //shotDown = player.GetComponent<ShotDown>();

        //�J�����ƃv���C���[�̋����𒲂ׂ�
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // ���̒���

        // ��̈ʒu��ݒ� (x,y,z)���W
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
        // ���j�J�n�t���O
        breakStart = false;
    }

    void LateUpdate()
    {
        // ���S�Ɍ��Ă��ꂽ��
        if (player != null && playerManager.GetComponent<PlayerHp>().BreakFlag && !breakStart)
        {
            // �J�������v���C���[�ɒǏ]������
            // ��x�v���C���[�̍��W�Ɠ����ɂ�����
            Vector3 pos = player.transform.position;
            pos += centerPoint;
            // �J�����������Ă�������Ƃ͋t�����Ƀv���C���[���痣��
            pos -= transform.forward * playerDistance / 2.3f;
            // �V�����J�����̈ʒu
            transform.position = pos;
            // ���j�J�n�t���O�����Ă�
            breakStart = true;
        }
        // �ʒu�ύX��̓J�����͒ǂ������ɂ���
        if (player != null && breakStart)
        {
            // �J�����Ńv���C���[��ǂ�
            gameObject.transform.LookAt(obj.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }

        if (bFade) { return; }
        if (playerManager.GetComponent<ShotDown>().EffectFlag)
        {
            if (cnt > effectTime)
            {
                // �V�[���ړ�
                //SceneManager.LoadScene("GameOver");
                fade.GetComponent<Fade>().StartCoroutine(
                    fade.GetComponent<Fade>().Color_FadeOut_GameOver(SceneNow));
                bFade = true;
            }
            // ���Ă���̎��Ԍo��
            cnt += Time.deltaTime;
        }
    }
}
