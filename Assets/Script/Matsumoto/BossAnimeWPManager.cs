using UnityEngine;

public class BossAnimeWPManager : MonoBehaviour
{
    private GameObject boss;                                        // �{�X�̃I�u�W�F�N�g
    private GameObject playerManager;                               // �v���C���[�}�l�[�W���[
    private GameObject bossManager;                                 // �{�X�}�l�[�W���[

    private GameObject[] BossWing = new GameObject[4];              // �{�X�̉H�̎�_
    private Animator[] BossWing_Anime = new Animator[4];            // �{�X�̉H�̃A�j���[�V����
    private GameObject[] WP_Bottom = new GameObject[4];             // �{�X�̉��̎�_
    private Animator[] WP_Bottom_Anime = new Animator[4];           // �{�X�̉��̃A�j���[�V����

    public GameObject WP_Top_Prefab;                                // �{�X�̏�̎�_�̃R���C�_�[�t���v���n�u
    public GameObject WP_Bottom_Prefab;                             // �{�X�̉��̎�_�̃R���C�_�[�t���v���n�u
    private GameObject[] WP_Top_Collision = new GameObject[4];      // �{�X�̏�̎�_�̃R���C�_�[�I�u�W�F�N�g
    private GameObject[] WP_Bottom_Collision = new GameObject[4];   // �{�X�̉��̎�_�̃R���C�_�[�I�u�W�F�N�g

    private GameObject[] WingWP_PosObj = new GameObject[4];         // �{�X�̉H�̎�_�̈ʒu�I�u�W�F�N�g

    private PhaseManager.Phase currntPhase;                         // ���݂̃t�F�C�Y
    private PhaseManager.Phase nextPhase;                           // ���̃t�F�C�Y
    private PhaseManager.Phase NormaltoNext;                        // ���̃t�F�C�Y


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        playerManager = GameObject.Find("PlayerManager");
        bossManager = GameObject.Find("BossManager");

        // �{�X�̉H�̃I�u�W�F�N�g�̎擾
        BossWing[0] = boss.transform.Find("wing1").gameObject;
        BossWing[1] = boss.transform.Find("wing2").gameObject;
        BossWing[2] = boss.transform.Find("wing3").gameObject;
        BossWing[3] = boss.transform.Find("wing4").gameObject;

        // �{�X�̉H�̎�_�̈ʒu�I�u�W�F�N�g�̎擾
        WingWP_PosObj[0] = GameObject.Find("Wing_wp1").gameObject;
        WingWP_PosObj[1] = GameObject.Find("Wing_wp2").gameObject;
        WingWP_PosObj[2] = GameObject.Find("Wing_wp3").gameObject;
        WingWP_PosObj[3] = GameObject.Find("Wing_wp4").gameObject;

        // �{�X�̉��̎�_�̃I�u�W�F�N�g�̎擾
        WP_Bottom[0] = boss.transform.Find("weak1").gameObject;
        WP_Bottom[1] = boss.transform.Find("weak2").gameObject;
        WP_Bottom[2] = boss.transform.Find("weak3").gameObject;
        WP_Bottom[3] = boss.transform.Find("weak4").gameObject;

        // �A�j���[�V�����̎擾
        for (int i = 0; i < 4; i++)
        {
            BossWing_Anime[i] = BossWing[i].GetComponent<Animator>();
        }
        for (int i = 0; i < 4; i++)
        {
            WP_Bottom_Anime[i] = WP_Bottom[i].GetComponent<Animator>();
        }

        // �t�F�C�Y�̏�����
        currntPhase = PhaseManager.instance.GetPhase();
        NormaltoNext = PhaseManager.instance.GetNormaltoNext();

        //nextPhase = currntPhase;
        nextPhase = NormaltoNext;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�C�Y�̎擾
        currntPhase = PhaseManager.instance.GetPhase();
        NormaltoNext = PhaseManager.instance.GetNormaltoNext();

        // �t�F�C�Y���ς������
        if (nextPhase != currntPhase)
        {
            // �t�F�C�Y�̍X�V
            nextPhase = currntPhase;


            // �m�[�}���t�F�C�Y
            if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.First_Normal)
            {
                // �A�j���[�V������
                for(int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isWing", true);
                }
                // �A�j���[�V������
                //for(int i = 0; i < 4; i++)
                //{
                //}
            }
            // �m�[�}���t�F�C�Y�̎����A�^�b�N�t�F�[�Y
            else if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.Attack_Phase)
            {
                // �A�j���[�V������
                for (int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isBinder", true);
                }
                // �A�j���[�V������
                for (int i = 0; i < 4; i++)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", true);
                    WP_Bottom_Anime[i].SetBool("isClose", false);
                    WP_Bottom_Anime[i].SetBool("isMove", false);
                }
            }
            //�@�m�[�}���t�F�C�Y�̎����X�s�[�h�t�F�C�Y
            else if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.Speed_Phase)
            {
                // �A�j���[�V������
                for (int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isBinder", false);
                }
                // �A�j���[�V������
                for (int i = 0; i < 4; i++)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", false);
                    WP_Bottom_Anime[i].SetBool("isClose", true);
                    WP_Bottom_Anime[i].SetBool("isMove", true);
                }

                // ��������Ă���ꍇ�A�{�X�̎�_�̃R���C�_�[���폜
                for (int i = 0; i < 4; i++)
                {
                    if (WP_Top_Collision[i] != null)
                    {
                        Destroy(WP_Top_Collision[i]);
                    }
                    if (WP_Bottom_Collision[i] != null)
                    {
                        Destroy(WP_Bottom_Collision[i]);
                    }
                }
            }
            if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // �{�X�̎�_�̃R���C�_�[�I�u�W�F�N�g�𐶐�
                for (int i = 0; i < 4; i++)
                {
                    if (WP_Top_Collision[i] == null)
                    {
                        WP_Top_Collision[i] = Instantiate(WP_Top_Prefab, BossWing[i].transform.position, Quaternion.identity);
                    }
                    if (WP_Bottom_Collision[i] == null)
                    {
                        WP_Bottom_Collision[i] = Instantiate(WP_Bottom_Prefab, WingWP_PosObj[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }

        // �A�^�b�N�t�F�C�Y��
        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            // �v���C���[�ɂ���Ď�_���j�󂳂ꂽ��
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] == null)
                {
                    BossWing_Anime[i].SetBool("isBinder", false);
                }
                if (WP_Bottom_Collision[i] == null)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", false);
                    WP_Bottom_Anime[i].SetBool("isClose", true);
                    WP_Bottom_Anime[i].SetBool("isMove", true);
                }
            }
        }

        // �v���C���[��HP��0�ɂȂ������A�{�X�̎�_�̃R���C�_�[���폜
        if (playerManager.GetComponent<PlayerHp>().BreakFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] != null)
                {
                    Destroy(WP_Top_Collision[i]);
                }
                if (WP_Bottom_Collision[i] != null)
                {
                    Destroy(WP_Bottom_Collision[i]);
                }
            }
        }

        // �{�X�̎�_�̃R���C�_�[�̈ʒu���X�V
        for (int i = 0; i < 4; i++)
        {
            if (WP_Top_Collision[i] != null)
            {
                WP_Top_Collision[i].transform.position = WingWP_PosObj[i].transform.position;
            }
            if (WP_Bottom_Collision[i] != null)
            {
                WP_Bottom_Collision[i].transform.position = WP_Bottom[i].transform.position;
            }
        }

        // �v���C���[���{�X�����Ă��ꂽ�ꍇ�ɏ���
        if (playerManager.GetComponent<PlayerHp>().BreakFlag ||
            bossManager.GetComponent<MainBossHp>().BreakFlag)
        {
            // ��������Ă���ꍇ�A�{�X�̎�_�̃R���C�_�[���폜
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] != null)
                {
                    Destroy(WP_Top_Collision[i]);
                }
                if (WP_Bottom_Collision[i] != null)
                {
                    Destroy(WP_Bottom_Collision[i]);
                }
            }
        }
    }
}
