using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour
{
    [SerializeField]

    private Transform targetTfm;

    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0.08f, 0);

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetTfm != null)
        {
            myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
        }
    }
}
