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

    [SerializeField, Header("�eHP�������̐F")]
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
    ///  HP�����ɉ�����HP�Q�[�W�̐F�ɕω�������
    /// </summary>
    /// <param name="currenthelth"> ����HP</param>
    /// <param name="maxhelth">�ő�HP</param>
    public void ChangeHpGaugeColor(float currenthelth, float maxhelth)
    {
        float fhpratio = currenthelth / maxhelth * 100;
        int hpratio = (int)fhpratio;

        // Hp�������̐F�ɕς���
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
