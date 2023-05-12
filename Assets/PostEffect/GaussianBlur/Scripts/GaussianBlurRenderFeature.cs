using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class GaussianBlurRenderFeature : ScriptableRendererFeature
{
    [SerializeField] private Shader _shader;
    [SerializeField] private PostproTiming _timing = PostproTiming.AfterPostprocess;
    [SerializeField] private bool _applyToSceneView = true;

    private GaussianBlurRenderPass _postProcessPass;

    public override void Create()
    {
        _postProcessPass = new GaussianBlurRenderPass(_applyToSceneView, _shader);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _postProcessPass.Setup(renderer.cameraColorTarget, _timing);
        renderer.EnqueuePass(_postProcessPass);
    }
}