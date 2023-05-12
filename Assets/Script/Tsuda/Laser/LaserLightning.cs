using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightning : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private PhaseManager.Phase currentPhase;
    private float timer = 0.0f;

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

        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            timer += Time.deltaTime;

            if (timer >= 4.0f)
            {
                lineRenderer.enabled = true;
            }
        }
        if (currentPhase != PhaseManager.Phase.Speed_Phase)
        {
            lineRenderer.enabled = false;
            timer = 0.0f;
        }
    }
}