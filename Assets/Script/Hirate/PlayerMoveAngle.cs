using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAngle : MonoBehaviour
{
    //---- ƒCƒ“ƒXƒyƒNƒ^[‚ÉŒ©‚¹‚é•Ï” ----
    // ‰ñ“]
    [Header("‰ñ“]")]
    [Tooltip("‰ñ“]‚ÌÅ‘å’l")]
    public float angleMax = 45; // ‰ñ“]‚ÌÅ‘å’l
    [Tooltip("‰ñ“]—Ê")]
    public Vector3 angleAdd;  // ‰ñ“]—Ê
    // …•½‰»
    [Header("…•½")]
    [Tooltip("…•½‚É–ß‚é‚Ì•â³—Ê")]
    public float horizon; // …•½‚É–ß‚é‚Ì—Ê
    //[Tooltip("‰ñ“]—Ê‚Ì•â³\n(ãŒü‚«‚É‚È‚é‚Ì•â³—p)")]
    //public float angleCorrection; // ‰ñ“]—Ê‚Ì•â³

    //---- •Ï”éŒ¾ ----
    private Vector3 angle;        // ‰ñ“]
    private Myproject InputActions; // “ü—Í
    private Vector2 inputMove;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Œ»İ‚Ì’l‚ğ‘ã“ü
        // ‰ñ“]
        angle = transform.eulerAngles;
        // ‚»‚Ì‚Ü‚Ü‚¾‚ÆŒvZ‚µ‚É‚­‚¢‚½‚ßA…•½‚ğ0‚Æ‚µ‚½”’l‚É‚·‚é
        if (angle.x >= 315) { angle.x = angle.x - 360; }

        // ƒL[“ü—Í
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ‰ñ“]
        if (inputMove.y < 0.0f) {
            angle += angleAdd;
        }
        if (inputMove.y > 0.0f) {
            angle -= angleAdd;// * angleCorrection;
        }

        // …•½‚É‚·‚é
        Horizon();

        // ‰ñ“]‚ÌC³
        // ‰º•ûŒü‚ÌC³
        if (angle.x >= angleMax) { angle.x = angleMax; }
        // ã•ûŒü‚ÌC³
        if (angle.x <= -angleMax) {
            angle.x = -angleMax;
            angle.x = 360 + angle.x;
        }

        // ‰ñ“]‚Ì‘ã“ü
        transform.eulerAngles = angle;
    }

    //void FixedUpdate()
    //{

    //}

    //```` …•½‚É‚·‚éŠÖ” ````
    private void Horizon()
    {
        // ‰º
        if (angle.x >= 0) {
            angle.x = angle.x - horizon;
            if (angle.x <= 0) { angle.x = 0; }
        }
        // ã
        if (angle.x < 0) {
            angle.x = angle.x + horizon;
            if (angle.x >= 0) { angle.x = 0; }
        }
    }
}
