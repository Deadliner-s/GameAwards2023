using UnityEngine;

public class PhaseManager : MonoBehaviour
{  
    // �t�F�C�Y�񋓑�
    public enum Phase
    {
        Normal_Phase,
        Speed_Phase, 
        Attack_Phase,
        First_Normal,
        MAX_Phase
    }

    [Header("�t�F�C�Y�̎���")]
    public float NormalTime = 10.0f;
    public float SpeedTime = 10.0f;
    public float AttackTime = 10.0f;

    [Header("���݂̃t�F�C�Y(�����t�F�C�Y)")]
    public Phase currentPhase = Phase.Normal_Phase;  // ���݂̃t�F�C�Y
    [System.NonSerialized]
    public Phase nextPhase;                          // ���̃t�F�C�Y
    [Header("�ʏ�t�F�C�Y���玟�̃t�F�C�Y")]
    public Phase NormaltoNext = Phase.First_Normal;  // �ʏ�t�F�C�Y���玟�̃t�F�C�Y

    private float time = 0.0f;                       // �b���J�E���g�p

    [Header("�t�F�C�Y���ɊǗ�����I�u�W�F�N�g")]
    [Tooltip("�Ə�")]
    public GameObject Reticle;

    [Header("�f�o�b�O�p �t�F�C�Y���Œ肷��")]
    public bool Debug_FixPhaseFlg = false;


    private GameObject vibrationManager;            // �o�C�u���[�V�����}�l�[�W���[

    // �C���X�^���X
    public static PhaseManager instance;

    void Awake()
    {
        // �C���X�^���X�����݂��Ȃ��ꍇ
        if(instance == null)
        {
            // �C���X�^���X����
            instance = this;
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        //currentPhase = Phase.Normal_Phase;          // �����t�F�C�Y

        nextPhase = currentPhase;

        vibrationManager = GameObject.Find("VibrationManagerObj");

        NormaltoNext = Phase.First_Normal;

    }
    // Update is called once per frame
    void Update()
    {
        // �X�e�[�W1,2
        //if (SceneManager.GetActiveScene().name == "Stage1" || SceneManager.GetActiveScene().name == "Stage2")
        if (SceneNow.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
            SceneNow.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2) 
        {
            // �t�F�C�Y���Œ肳��ĂȂ��ꍇ
            if (Debug_FixPhaseFlg != true)
            {
                // ���ԍX�V
                time += Time.deltaTime;

                // �ʏ�t�F�C�Y
                if (currentPhase == Phase.Normal_Phase)
                {
                    if (time >= NormalTime)
                    {
                        // �X�e�[�W1,2�̓A�^�b�N�t�F�C�Y����
                        if (NormaltoNext == Phase.First_Normal)
                        {
                            NormaltoNext = Phase.Attack_Phase;
                        }
                        if (NormaltoNext == Phase.Attack_Phase)
                        {
                            currentPhase = Phase.Attack_Phase;
                            NormaltoNext = Phase.Speed_Phase;
                        }
                        else if (NormaltoNext == Phase.Speed_Phase)
                        {
                            currentPhase = Phase.Speed_Phase;
                            NormaltoNext = Phase.Attack_Phase;
                        }

                        time = 0.0f;
                    }
                }
                // �n�C�X�s�[�h�t�F�C�Y
                else if (currentPhase == Phase.Speed_Phase)
                {
                    if (time >= SpeedTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
                // �A�^�b�N�t�F�C�Y
                else if (currentPhase == Phase.Attack_Phase)
                {
                    if (time >= AttackTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
            }
        }
        // �X�e�[�W3
        else
        {
            // �t�F�C�Y���Œ肳��ĂȂ��ꍇ
            if (Debug_FixPhaseFlg != true)
            {
                // ���ԍX�V
                time += Time.deltaTime;

                // �ʏ�t�F�C�Y
                if (currentPhase == Phase.Normal_Phase)
                {
                    if (time >= NormalTime)
                    {
                        // �X�e�[�W3�̓X�s�[�h�t�F�C�Y����
                        if (NormaltoNext == Phase.First_Normal)
                        {
                            NormaltoNext = Phase.Speed_Phase;
                        }
                        if (NormaltoNext == Phase.Speed_Phase)
                        {
                            currentPhase = Phase.Speed_Phase;
                            NormaltoNext = Phase.Attack_Phase;
                        }
                        else if (NormaltoNext == Phase.Attack_Phase)
                        {
                            currentPhase = Phase.Attack_Phase;
                            NormaltoNext = Phase.Speed_Phase;
                        }


                        time = 0.0f;
                    }
                }
                // �n�C�X�s�[�h�t�F�C�Y
                else if (currentPhase == Phase.Speed_Phase)
                {
                    if (time >= SpeedTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
                // �A�^�b�N�t�F�C�Y
                else if (currentPhase == Phase.Attack_Phase)
                {
                    if (time >= AttackTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
            }

        }
    


        // �t�F�C�Y���ς�����ꍇ
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            if (currentPhase == Phase.Normal_Phase)
            {
                Reticle.SetActive(true);
            }

            if (currentPhase == Phase.Speed_Phase)
            {
                SoundManager.instance.PlaySE("HighSpeed");
                Reticle.SetActive(false);
                
                vibrationManager.GetComponent<VibrationManager>().StartCoroutine("PlayVibration", "HighSpeed");
            }

            if (currentPhase == Phase.Attack_Phase)
            {
                Reticle.SetActive(true);
            }
        }
    }

    public Phase GetPhase()
    {
        return currentPhase;
    }
    public Phase GetNextPhase()
    {
        return nextPhase;
    }
    public Phase GetNormaltoNext()
    {
        return NormaltoNext;
    }
}