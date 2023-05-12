using System;
using UnityEngine.Rendering;

[Serializable]
[VolumeComponentMenu("Gaussian Blur")]
public class GaussianBlurVolume : VolumeComponent
{
    public bool IsActive() => SamplingDistance.value != 0;

//    public ClampedIntParameter sampleCount = new ClampedIntParameter(8, 4, 16);
    public FloatParameter SamplingDistance = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);
}