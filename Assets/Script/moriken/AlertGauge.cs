using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertGauge : MonoBehaviour
{
    
    Image AlertObject;
    [Tooltip("増えるゲージのオブジェクト")]
    public GameObject AlertGaugeObject;
    [Tooltip("1フレームでのゲージの進み具合(MAX 1)")]
    public float flamecount;

    // Start is called before the first frame update
    void Start()
    {
        AlertObject = AlertGaugeObject.GetComponent<Image>();
        AlertObject.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲージを徐々に表示させていく処理
        AlertObject.fillAmount += flamecount;
    }
}
