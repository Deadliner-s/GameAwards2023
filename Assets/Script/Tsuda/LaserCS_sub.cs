using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCS_sub : MonoBehaviour
{
    public GameObject Laser;
    [SerializeField] private Image CS;
    public float rotate = 0.1f;

    private Camera mainCamera;  // ���C���J����
    private float timer = 0.0f;    

    // Start is called before the first frame update
    void Start()
    {
        CS.enabled = false;
        mainCamera = Camera.main;  // ���C���J�������擾����
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //        GetComponent<RectTransform>().position = Laser.GetComponent<LaserHead>().targetScreenPosition;

        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);

        if (timer >= 2.0f)
        {
            CS.enabled = true;
            GetComponent<RectTransform>().Rotate(0, 0, rotate);
        }

        if (timer >= 6.0f)
        {
            CS.enabled = false;
        }
    }
}
