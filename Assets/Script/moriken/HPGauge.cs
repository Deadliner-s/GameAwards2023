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
    private Vector3 offset;

    [SerializeField, Header("�eHP�������̐F")]
    private Color[] _color = new Color[5];

    private GameObject obj;
    private GaugeOffset gaugeOffset;

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        // �I�t�Z�b�g�l�̎擾
        obj = transform.parent.gameObject;
        gaugeOffset = obj.GetComponent<GaugeOffset>();
        offset = gaugeOffset.offset;
    }

    void Update()
    {
        if (targetTfm != null)
        {
            myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
        }

#if DEBUG
        //offset = gaugeOffset.offset;
#endif
    }

    /// <summary>
    ///  HP�����ɉ�����HP�Q�[�W�̐F�ɕω�������
    /// </summary>
    /// <param name="currenthelth"> ����HP</param>
    /// <param name="maxhelth">�ő�HP</param>
    public void ChangeHpGaugeColor(float currenthelth, float maxhelth)
    {
        float fhpratio = currenthelth / maxhelth * 100;
        float hpratio =  fhpratio;

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
        if (hpratio < 20 && hpratio > 0)
        {
            this.image.color = _color[3];
        }
        if (hpratio <= 0)
        {
            this.image.color = _color[4];
        }
    }
}
