using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserLightning : MonoBehaviour
{
    private GameObject[] lightning = new GameObject[4];
    //public LineRenderer lineRenderer;

    private PhaseManager.Phase currentPhase;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        lightning[0] = GameObject.Find("Lightning1");
        lightning[1] = GameObject.Find("Lightning2");
        lightning[2] = GameObject.Find("Lightning3");
        lightning[3] = GameObject.Find("Lightning4");

        currentPhase = PhaseManager.instance.GetPhase();

        for(int i = 0; i < 4; i++)
        {
            lightning[i].GetComponent<LineRenderer>().enabled = false;
        }

        //lineRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        currentPhase = PhaseManager.instance.GetPhase();        

        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            timer += Time.deltaTime;

            //if (timer >= 4.0f)
            //{
                for (int i = 0; i < 4; i++)
                {
                    lightning[i].GetComponent<LineRenderer>().enabled = true;
                }
            //}
        }
        if (currentPhase != PhaseManager.Phase.Speed_Phase)
        {
            for (int i = 0; i < 4; i++)
            {
                lightning[i].GetComponent<LineRenderer>().enabled = false;
            }
            //timer = 0.0f;
        }
    }
}