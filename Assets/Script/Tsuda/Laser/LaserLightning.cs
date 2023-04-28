using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightning : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private PhaseManager.Phase currentPhase;    

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = PhaseManager.instance.GetPhase();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        currentPhase = PhaseManager.instance.GetPhase();

        if(currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            lineRenderer.enabled = true;
        }
        if (currentPhase != PhaseManager.Phase.Speed_Phase)
        {
            lineRenderer.enabled = false;
        }
    }
}