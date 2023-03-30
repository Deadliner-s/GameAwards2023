using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeWeak : MonoBehaviour
{

    private WeakPointBottom[] weakpointbottom = new WeakPointBottom[4];

    private PhaseManager.Phase currntPhase;
    private GameObject[] Child = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Child[i] = transform.GetChild(0).gameObject;
        }

        for (int i = 0; i < 4; i++)
        {
            weakpointbottom[i] = Child[i].GetComponent<WeakPointBottom>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();
        //Debug.Log(gameObject.transform.position);

        if (currntPhase == PhaseManager.Phase.Normal_Phase)
        {
            for (int i = 0; i < 4; i++)
            {
                weakpointbottom[i].enabled = false;
            }

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

            for (int i = 0; i < 4; i++)
            {
                weakpointbottom[i].enabled = true;
            }
        }
    }
}
