using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ObjectMotionBlurPass : ScriptableRenderPass
{
    private readonly ProfilingSampler _objectMotionBlurSampler = new ProfilingSampler("Object Motion Blur");
    private readonly PostProcessingMotionBlur _postProcessingMotionBlur;
    private readonly RenderTargetHandle _tmpColorBuffer;
    private Material _material;

    public ObjectMotionBlurPass(Shader shader)
    {
        // MotionVector�v������
        ConfigureInput(ScriptableRenderPassInput.Motion);

        _postProcessingMotionBlur = new PostProcessingMotionBlur();
        _tmpColorBuffer.Init("_TempColorBuffer");
        _material = CoreUtils.CreateEngineMaterial(shader);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        ref var cameraData = ref renderingData.cameraData;
        // SceneView�ł̓u���[�������Ȃ�
        if (cameraData.cameraType == CameraType.SceneView) return;

        CommandBuffer cmd = CommandBufferPool.Get();

        using (new ProfilingScope(cmd, _objectMotionBlurSampler))
        {
            var descriptor = cameraData.cameraTargetDescriptor;
            var colorTarget = cameraData.renderer.cameraColorTarget;

            // �J�����̉摜��_TempColorBuffer�ɃR�s�[����
            cmd.GetTemporaryRT(_tmpColorBuffer.id, descriptor);
            Blit(cmd, colorTarget, _tmpColorBuffer.id);

            // �I�u�W�F�N�g���[�V�����u���[
            _postProcessingMotionBlur.ObjectMotionBlur(cmd, _material, _tmpColorBuffer.id, colorTarget, descriptor);

            cmd.ReleaseTemporaryRT(_tmpColorBuffer.id);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}