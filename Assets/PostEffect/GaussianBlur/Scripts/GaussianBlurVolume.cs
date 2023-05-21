using System;
using UnityEngine.Rendering;

[Serializable]
[VolumeComponentMenu("Gaussian Blur")]
public class GaussianBlurVolume : VolumeComponent
{
    public FloatParameter SamplingDistance = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);
}