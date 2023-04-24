using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeOffset : MonoBehaviour
{
    // HPゲージの位置調整の値取得用
    [field: SerializeField] public Vector3 offset { get; private set; } = new Vector3(0, 0.11f, 0);
}
