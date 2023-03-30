using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    Animator weakanime;

    public GameObject weakobj;//��
    public GameObject wpobj;//��
 
    private WeakPoint[] weakpointtop = new WeakPoint[4];

    public AudioClip BossAnimeSE;
    private AudioSource audioSource;
    private bool SeFlg = false;

    private PhaseManager.Phase currntPhase;
    private GameObject[] Child = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        weakanime = weakobj.GetComponent<Animator>();

        currntPhase = PhaseManager.instance.GetPhase();

        audioSource = GetComponent<AudioSource>();
        SeFlg = false;

        for (int i = 0; i < 4; i++)
        {
            Child[i] = transform.GetChild(0).gameObject;
        }

        for (int i = 0; i < 4; i++)
        {
            weakpointtop[i] = Child[i].GetComponent<WeakPoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();
        //Debug.Log(gameObject.transform.position);

        if (currntPhase == PhaseManager.Phase.Normal_Phase)
        {
            anime.SetBool("isWing", false);
            anime.SetBool("isBinder", false);
            weakanime.SetBool("isOpen", false);
            weakanime.SetBool("isClose", true);

            for (int i = 0; i < 4; i++)
            {
                weakpointtop[i].enabled = false;
            }

            SeFlg = false;
        }
        else if(currntPhase == PhaseManager.Phase.Speed_Phase) 
        {
            // �n�C�X�s�[�h�t�F�C�Y
            anime.SetBool("isWing", true);
            weakanime.SetBool("isClose", false);

            if (SeFlg != true)
            {
                audioSource.PlayOneShot(BossAnimeSE);
                SeFlg = true;
            }
        }
        else if(currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            // �A�^�b�N�t�F�C�Y
            anime.SetBool("isBinder", true);
            weakanime.SetBool("isOpen", true);

            for (int i = 0; i < 4; i++)
            {
                weakpointtop[i].enabled = true;
            }
        }
    }
}
