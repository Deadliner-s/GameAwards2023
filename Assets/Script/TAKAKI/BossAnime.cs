using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    //Animator weakanime;

    //public GameObject weakobj;//下
    public GameObject wpobj;//翼
 
    public WeakPoint weakpointtop;

    public AudioClip BossOpenSE;
    public AudioClip BossCloseSE;
    private AudioSource audioSource;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;
    public GameObject Child;

    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        //weakanime = weakobj.GetComponent<Animator>();

        currntPhase = PhaseManager.instance.GetPhase();
        nextPhase = currntPhase;

        audioSource = GetComponent<AudioSource>();


        Child = transform.GetChild(2).gameObject;

        weakpointtop = Child.GetComponent<WeakPoint>();
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

                weakpointtop.enabled = false;

                audioSource.PlayOneShot(BossCloseSE);
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ
                anime.SetBool("isWing", true);

                audioSource.PlayOneShot(BossOpenSE);
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                anime.SetBool("isBinder", true);

                weakpointtop.enabled = true;

                obj = weakpointtop.Setobj();
            }
        }

        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if(obj == null)
            {
                anime.SetBool("isBinder", false);

                weakpointtop.enabled = false;
            }
        }
    }
}
