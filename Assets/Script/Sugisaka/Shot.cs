using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    Myproject InputActions;
    public GameObject PlayerMissile;    // �~�T�C���Ƃ��Ďg�p����I�u�W�F�N�g�̎w��
    private Vector3 PlayerPos;                  // �v���C���[�̍��W
    public Vector3 ShiftPos;                   // �����ꏊ(�v���C���[�̍��W����ǂ̂��炢���炷��

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShot()
    {
        var TargetObj = GameObject.Find("RockOn");
        if (TargetObj == null)
        {
            PlayerPos = transform.position;
            PlayerPos.x += ShiftPos.x;
            PlayerPos.y += ShiftPos.y;
            PlayerPos.z += ShiftPos.z;
            // �e�𔭎˂��鏈��
            GameObject obj = Instantiate(PlayerMissile, PlayerPos, Quaternion.identity);
        }        
    }
}
