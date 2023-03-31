using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeWeak : MonoBehaviour
{
    Animator weakanime;

    public GameObject weakobj;//下

    private WeakPointBottom weakpointbottom;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;
    private GameObject Child;

    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        weakanime = weakobj.GetComponent<Animator>();

        nextPhase = currntPhase;

        Child = transform.GetChild(0).gameObject;

        weakpointbottom = Child.GetComponent<WeakPointBottom>();
 
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
                weakanime.SetBool("isOpen", false);
                weakanime.SetBool("isClose", true);

                weakpointbottom.enabled = false;


                //SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ

                weakanime.SetBool("isClose", false);

                //if (SeFlg != true)
                //{
                //    audioSource.PlayOneShot(BossAnimeSE);
                //    SeFlg = true;
                //}
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                weakanime.SetBool("isOpen", true);


                weakpointbottom.enabled = true;

                obj = weakpointbottom.Setobj();
            }

        }
        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if (obj == null)
            {
                weakanime.SetBool("isOpen", false);
                weakanime.SetBool("isClose", true);

                weakpointbottom.enabled = false;
            }
        }
    }
}
