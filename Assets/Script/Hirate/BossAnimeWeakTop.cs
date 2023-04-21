using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeWeakTop : MonoBehaviour
{
    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;

    public WeakPoint weakpointtop;

    private GameObject Child;
    public GameObject obj { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        currntPhase = PhaseManager.instance.GetPhase();
        nextPhase = currntPhase;

        Child = transform.GetChild(0).gameObject;

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
                weakpointtop.enabled = false;
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                weakpointtop.enabled = true;

                obj = weakpointtop.Setobj();
            }

        }
        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if (obj == null)
            {
                weakpointtop.enabled = false;
            }
        }
    }
}
