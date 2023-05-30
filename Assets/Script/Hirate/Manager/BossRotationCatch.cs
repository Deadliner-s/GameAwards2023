using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotationCatch : MonoBehaviour
{
    // �{�X�̉�]�擾�ƃZ�b�g�p
    public Quaternion fRotation { get; set; }

    public static BossRotationCatch instance;

    private void Awake()
    {
        fRotation = new Quaternion();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
