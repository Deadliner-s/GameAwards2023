using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertGauge : MonoBehaviour
{
    Image AlertObject;
    public float flamecount;

    // Start is called before the first frame update
    void Start()
    {
        AlertObject = this.gameObject.GetComponent<Image>();
        AlertObject.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AlertObject.fillAmount += flamecount;
    }
}
