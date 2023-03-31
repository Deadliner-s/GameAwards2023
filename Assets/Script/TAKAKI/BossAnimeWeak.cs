using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeWeak : MonoBehaviour
{

    private WeakPointBottom weakpointbottom;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;
    private GameObject Child;

    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
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
                weakpointbottom.enabled = false;


                //SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ

                //if (SeFlg != true)
                //{
                //    audioSource.PlayOneShot(BossAnimeSE);
                //    SeFlg = true;
                //}
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ

                weakpointbottom.enabled = true;

            }

        }
        //if (currntPhase == PhaseManager.Phase.Attack_Phase)
        //{
        //    if (obj == null)
        //    {
        //        weakpointbottom.enabled = false;
        //    }
        //}
    }
}
