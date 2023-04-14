using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour
{
    [SerializeField]

    private Transform targetTfm;

    private RectTransform myRectTfm;
    private Image image;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.08f, 0);

    [SerializeField, Header("各HP割合毎の色")]
    private Color[] _color = new Color[5];


    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (targetTfm != null)
        {
            myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
        }


    }

    /// <summary>
    ///  HP割合に応じたHPゲージの色に変化させる
    /// </summary>
    /// <param name="currenthelth"> 現在HP</param>
    /// <param name="maxhelth">最大HP</param>
    public void ChangeHpGaugeColor(float currenthelth, float maxhelth)
    {
        float fhpratio = currenthelth / maxhelth * 100;
        int hpratio = (int)fhpratio;

        // Hp割合毎の色に変える
        if (hpratio >= 100)
        {
            this.image.color = _color[0];

        }
        if (hpratio < 100 && hpratio >= 50)
        {
            this.image.color = _color[1];
        }
        if (hpratio < 50 && hpratio >= 20)
        {
            this.image.color = _color[2];
        }
        if (hpratio < 20 && hpratio >= 1)
        {
            this.image.color = _color[3];
        }
        if (hpratio <= 0)
        {
            this.image.color = _color[4];
        }
    }
}
