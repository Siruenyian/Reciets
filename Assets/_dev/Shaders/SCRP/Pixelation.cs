
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Pixelation : ScriptableRendererFeature
{
    [SerializeField] private CustomPassSettings passSettings;
    private PixelationPass customPass;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        #if UNITY_EDITOR
            if (renderingData.cameraData.isSceneViewCamera)
            {
                return;
            }
        #endif
        renderer.EnqueuePass(customPass);
    }

    public override void Create()
    {
        customPass = new PixelationPass(passSettings);
    }
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight = 144;
    }

    public class PixelationPass : ScriptableRenderPass
    {


        private Pixelation.CustomPassSettings settings;

        private RenderTargetIdentifier colorBuffer, pixelBuffer;
        //property untuk shader
        private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");


        private Material material;
        private int pixelScreenHeight, pixelScreenWidth;

        public PixelationPass(CustomPassSettings settings)
        {
            this.settings = settings;
            this.renderPassEvent = settings.passEvent;
            if (material == null) material = CoreUtils.CreateEngineMaterial("Stylized/PixelShader");
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

            pixelScreenHeight = settings.screenHeight;
            pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

            //set property shadernya
            material.SetVector("_blockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
            material.SetVector("_blockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
            material.SetVector("_halfblockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

            descriptor.height = pixelScreenHeight;
            descriptor.width = pixelScreenWidth;

            //ambil rendertexturenya
            cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
            pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (cmd == null) throw new System.ArgumentNullException("cmd");
            cmd.ReleaseTemporaryRT(pixelBufferID);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            //get commandbuffer
            CommandBuffer cmd = CommandBufferPool.Get();

            using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
            {
                //Sets the active render texture to the dest texture
                Blit(cmd, colorBuffer, pixelBuffer, material);
                Blit(cmd, pixelBuffer, colorBuffer);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
        
    }

}

