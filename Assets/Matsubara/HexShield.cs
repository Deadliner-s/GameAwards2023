using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexShield : MonoBehaviour
{
    [SerializeField]
    GameObject Hex_Shield;

    private Material _mat;

    [SerializeField, ColorUsage(true, true), Header("Color1�̊eHP�������̐F")]
    private Color[] _color1 = new Color[5];

    [SerializeField, ColorUsage(true, true), Header("_PolygonEdgeColor�̊eHP�������̐F")]
    private Color[] _PolygonEdgeColor = new Color[5];

    [SerializeField, ColorUsage(true, true), Header("FrensnelColor�̊eHP�������̐F")]
    private Color[] _FresnelColor = new Color[5];

    // Start is called before the first frame update
    void Start()
    {
        _mat = Hex_Shield.gameObject.GetComponent<Renderer>().material; 
    }

    // Update is called once per frame

    /// <summary>
    ///  HP�����ɉ������V�[���h�̐F�ɕω�������
    /// </summary>
    /// <param name="currenthelth"> ����HP</param>
    /// <param name="maxhelth">�ő�HP</param>
    public void ChangeShieldColor(float currenthelth, float maxhelth)
    {
        float fhpratio = currenthelth / maxhelth * 100;
        int hpratio = (int)fhpratio;
       
        // Hp�������̐F�ɕς���
        if (hpratio >= 100)
        {
            _mat.SetColor("_Color01", _color1[0]);
            _mat.SetColor("_PolygonEdgeColor", _PolygonEdgeColor[0]); 
            _mat.SetColor("_FresnelColor", _FresnelColor[0]);
        }
        if (hpratio < 100 && hpratio >= 50)
        {
            _mat.SetColor("_Color01", _color1[1]);
            _mat.SetColor("_PolygonEdgeColor", _PolygonEdgeColor[1]);
            _mat.SetColor("_FresnelColor", _FresnelColor[1]);
        }
        if (hpratio < 50 && hpratio >= 20)
        {
            _mat.SetColor("_Color01", _color1[2]);
            _mat.SetColor("_PolygonEdgeColor", _PolygonEdgeColor[2]);
            _mat.SetColor("_FresnelColor", _FresnelColor[2]);
        }
        if (hpratio < 20 && hpratio >= 1)
        {
            _mat.SetColor("_Color01", _color1[3]);
            _mat.SetColor("_PolygonEdgeColor", _PolygonEdgeColor[3]);
            _mat.SetColor("_FresnelColor", _FresnelColor[3]);
        }
        if(hpratio <= 0)
        {
            _mat.SetColor("_Color01", _color1[4]);
            _mat.SetColor("_PolygonEdgeColor", _PolygonEdgeColor[4]);
            _mat.SetColor("_FresnelColor", _FresnelColor[4]);
        }
    }
}
