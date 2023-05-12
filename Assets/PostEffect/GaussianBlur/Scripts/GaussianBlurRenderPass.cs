using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum PostproTiming
{
    BeforePostprocess,
    AfterPostprocess
}

public class GaussianBlurRenderPass : ScriptableRenderPass
{
    private const string RenderPassName = nameof(GaussianBlurRenderPass);
    private const string ProfilingSamplerName = "SrcToDest";

    private readonly bool _applyToSceneView;
    private readonly int _mainTexPropertyId = Shader.PropertyToID("_MainTex");
    private readonly Material _material;
    private readonly ProfilingSampler _profilingSampler;
    //private readonly int _sampleCountPropertyId = Shader.PropertyToID("_SampleCount");
    //private readonly int _strengthPropertyId = Shader.PropertyToID("_Strength");
    private readonly int _samplingDistancePropertyId = Shader.PropertyToID("_SamplingDistance");

    private RenderTargetHandle _afterPostProcessTexture;
    private RenderTargetIdentifier _cameraColorTarget;
    private RenderTargetHandle _tempRenderTargetHandle;
    private GaussianBlurVolume _volume;

    public GaussianBlurRenderPass(bool applyToSceneView, Shader shader)
    {
        if (shader == null)
        {
            return;
        }

        _applyToSceneView = applyToSceneView;
        _profilingSampler = new ProfilingSampler(ProfilingSamplerName);
        _tempRenderTargetHandle.Init("_TempRT");
        _material = CoreUtils.CreateEngineMaterial(shader);
        _afterPostProcessTexture.Init("_AfterPostProcessTexture");
    }

    public void Setup(RenderTargetIdentifier cameraColorTarget, PostproTiming timing)
    {
        _cameraColorTarget = cameraColorTarget;
        renderPassEvent = GetRenderPassEvent(timing);
        var volumeStack = VolumeManager.instance.stack;
        _volume = volumeStack.GetComponent<GaussianBlurVolume>();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_material == null)
        {
            return;
        }

        if (!renderingData.cameraData.postProcessEnabled)
        {
            return;
        }

        if (!_applyToSceneView && renderingData.cameraData.cameraType == CameraType.SceneView)
        {
            return;
        }

        //if (!SamplingDistance.IsActive())
        //{
        //    return;
        //}

        var source = renderPassEvent == RenderPassEvent.AfterRendering && renderingData.cameraData.resolveFinalTarget
            ? _afterPostProcessTexture.Identifier()
            : _cameraColorTarget;

        var cmd = CommandBufferPool.Get(RenderPassName);
        cmd.Clear();
        var tempTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        tempTargetDescriptor.depthBufferBits = 0;
        cmd.GetTemporaryRT(_tempRenderTargetHandle.id, tempTargetDescriptor);

        using (new ProfilingScope(cmd, _profilingSampler))
        {
            //_material.SetInt(_sampleCountPropertyId, _volume.sampleCount.value);
            //_material.SetFloat(_strengthPropertyId, _volume.strength.value);
            _material.SetFloat(_samplingDistancePropertyId, _volume.SamplingDistance.value);
            cmd.SetGlobalTexture(_mainTexPropertyId, source);
            Blit(cmd, source, _tempRenderTargetHandle.Identifier(), _material);
        }

        Blit(cmd, _tempRenderTargetHandle.Identifier(), source);

        cmd.ReleaseTemporaryRT(_tempRenderTargetHandle.id);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    private static RenderPassEvent GetRenderPassEvent(PostproTiming PostproTiming)
    {
        switch (PostproTiming)
        {
            case PostproTiming.BeforePostprocess:
                return RenderPassEvent.BeforeRenderingPostProcessing;
            case PostproTiming.AfterPostprocess:
                return RenderPassEvent.AfterRendering;
            default:
                throw new ArgumentOutOfRangeException(nameof(PostproTiming), PostproTiming, null);
        }
    }
}