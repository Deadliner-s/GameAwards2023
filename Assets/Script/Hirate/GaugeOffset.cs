using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeOffset : MonoBehaviour
{
    // HP�Q�[�W�̈ʒu�����̒l�擾�p
    [field: SerializeField] public Vector3 offset { get; private set; } = new Vector3(0, 0.11f, 0);
}
