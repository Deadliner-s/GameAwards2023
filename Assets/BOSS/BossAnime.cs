using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    public GameObject wpobj;
    private WeakPoint wp;

    public AudioClip BossAnimeSE;
    private AudioSource audioSource;
    private bool SeFlg = false;

    private PhaseManager.Phase currntPhase;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        wp = gameObject.GetComponent<WeakPoint>();

        currntPhase = PhaseManager.instance.GetPhase();

        audioSource = GetComponent<AudioSource>();
        SeFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();

        if (currntPhase == PhaseManager.Phase.Normal_Phase)
        {
            anime.SetBool("isMove", false);
            wp.enabled = false;
            SeFlg = false;
        }
        else if(currntPhase == PhaseManager.Phase.Speed_Phase) 
        {
            // �n�C�X�s�[�h�t�F�C�Y
            anime.SetBool("isMove", true);

            if (SeFlg != true)
            {
                audioSource.PlayOneShot(BossAnimeSE);
                SeFlg = true;
            }
        }
        else if(currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            // �A�^�b�N�t�F�C�Y

            wp.enabled = true;
        }
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    anime.SetBool("isMove", true);

        //    wp.enabled = true;
        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    anime.SetBool("isMove", false);

        //    wp.enabled = false;
        //}
    }
}
