using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    Animator weakanime;

    public GameObject weakobj;//下
    public GameObject wpobj;//翼
 
    public WeakPoint weakpointtop;

    public AudioClip BossAnimeSE;
    private AudioSource audioSource;
    private bool SeFlg = false;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;
    public GameObject Child;

    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        weakanime = weakobj.GetComponent<Animator>();

        currntPhase = PhaseManager.instance.GetPhase();
        nextPhase = currntPhase;

        audioSource = GetComponent<AudioSource>();
        SeFlg = false;

        //for (int i = 0; i < 4; i++)
        //{
            Child = transform.GetChild(0).gameObject;
        //}

        //for (int i = 0; i < 4; i++)
        //{
            weakpointtop = Child.GetComponent<WeakPoint>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();
        //Debug.Log(gameObject.transform.position);
        if (nextPhase != currntPhase)
        {
            nextPhase = currntPhase;
            if (currntPhase == PhaseManager.Phase.Normal_Phase)
            {
                anime.SetBool("isWing", false);
                anime.SetBool("isBinder", false);
                weakanime.SetBool("isOpen", false);
                weakanime.SetBool("isClose", true);

                //for (int i = 0; i < 4; i++)
                //{
                weakpointtop.enabled = false;
                //}

                SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ
                anime.SetBool("isWing", true);
                weakanime.SetBool("isClose", false);

                if (SeFlg != true)
                {
                    audioSource.PlayOneShot(BossAnimeSE);
                    SeFlg = true;
                }
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                anime.SetBool("isBinder", true);
                weakanime.SetBool("isOpen", true);

                //for (int i = 0; i < 4; i++)
                //{
                weakpointtop.enabled = true;
                //}
                obj = weakpointtop.Setobj();
            }
        }

        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if(obj == null)
            {
                anime.SetBool("isBinder", false);
                weakanime.SetBool("isClose", true);
                weakpointtop.enabled = false;
            }
        }
    }

    //public void WingAnime()
    //{
    //    anime.SetBool("isWing", false);
    //}

    //public void CloseAnime()
    //{
    //    weakanime.SetBool("isOpen", false);
    //    weakanime.SetBool("isClose", true);
    //}

}
