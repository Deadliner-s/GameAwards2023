using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PhaseManager : MonoBehaviour
{  
    // �t�F�C�Y�񋓑�
    public enum Phase
    {
        Normal_Phase,
        Speed_Phase,
        Attack_Phase,
        MAX_Phase
    }

    [Header("�t�F�C�Y�̎���")]
    public float NormalTime = 10.0f;
    public float SpeedTime = 10.0f;
    public float AttackTime = 10.0f;

    [Header("���݂̃t�F�C�Y(�����t�F�C�Y)")]
    public Phase currentPhase = Phase.Normal_Phase;  // ���݂̃t�F�C�Y
    private Phase nextPhase;                         // ���̃t�F�C�Y

    private float time = 0.0f;                       // �b���J�E���g�p

    [Header("�t�F�C�Y���ɊǗ�����I�u�W�F�N�g")]
    [Tooltip("�Ə�")]
    public GameObject Reticle;

    [Tooltip("�W����")]
    public GameObject Line;
    private Animator LineAnime;

    [Tooltip("�u���[")]
    [SerializeField]
    private Volume volume;
    private RadialBlurVolume Blur;
    public float BlurStrength;
    private float BlurCount;
    private float CountPoint = 0.001f;
    private int i;

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
        if (Line)
        {
            LineAnime = Line.GetComponent<Animator>();
            Line.SetActive(false);
            LineAnime.SetBool("isMove", false);
        }

        // �u���[�̏���������
        volume.profile.TryGet(out Blur);
        BlurCount = BlurStrength * 1000;
        Blur.strength.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
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
                    currentPhase = Phase.Speed_Phase;
                    time = 0.0f;
                }
            }
            // �n�C�X�s�[�h�t�F�C�Y
            else if (currentPhase == Phase.Speed_Phase)
            {
                if (time >= SpeedTime)
                {
                    currentPhase = Phase.Attack_Phase;
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


        // �t�F�C�Y���ς�����ꍇ
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            if (currentPhase == Phase.Normal_Phase)
            {
                Reticle.SetActive(true);
                if (Line)
                {
                    Line.SetActive(false);
                    LineAnime.SetBool("isMove", false);
                }
                Blur.strength.value = 0.0f;
            }

            if (currentPhase == Phase.Speed_Phase)
            {
                SoundManager.instance.PlaySE("HighSpeed");
                Reticle.SetActive(false);
                if (Line)
                {
                    Line.SetActive(true);
                    LineAnime.SetBool("isMove", true);
                }
                vibrationManager.GetComponent<VibrationManager>().StartCoroutine("PlayVibration", "HighSpeed");

                i = 0;
            }

            if (currentPhase == Phase.Attack_Phase)
            {
                Reticle.SetActive(true);
                if (Line)
                {
                    Line.SetActive(false);
                    LineAnime.SetBool("isMove", false);
                }

                i = 0;
            }
        }

        // �t�F�[�Y�ύX��̏���
        if (i < BlurCount)
        {
            if (currentPhase == Phase.Speed_Phase)
            {
                // �u���[�����񂾂�Ƃ����Ă�������
                Blur.strength.value += CountPoint;
                i++;
            }
            if (currentPhase == Phase.Attack_Phase)
            {
                // �u���[�����񂾂��0�ɂ��Ă�������
                Blur.strength.value -= CountPoint;
                i++;
            }
        }
    }

    public Phase GetPhase()
    {
        return currentPhase;
    }
}