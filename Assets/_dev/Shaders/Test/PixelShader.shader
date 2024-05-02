Shader "Stylized/PixelShader"
{
 Properties
    {
        _MainTex ("Texture", 2D) = "white"
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" 
            "RenderPipeline" = "UniversalPipeline"
        }

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        TEXTURE2D(_MainTex);
        float4 _MainTex_TexelSize;
        float4 _MainTex_ST;

        SamplerState sampler_point_clamp;
        uniform float2 _blockCount;
        uniform float2 _blockSize;
        uniform float2 _halfblockSize;


        Varyings vert(Attributes a)
        {
            Varyings v;
            v.vertex = TransformObjectToHClip(a.vertex.xyz);
            v.uv = TRANSFORM_TEX(a.uv, _MainTex);
            return v;
        }

        ENDHLSL

        Pass
        {
            Name "Pixelation"

            HLSLPROGRAM
            half4 frag(Varyings v) : SV_TARGET
            {
                float2 blockPos = floor(v.uv * _blockCount);
                float2 blockCenter = blockPos * 2 + _halfblockSize;
                float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_point_clamp, blockCenter);

                return tex;
            }
            ENDHLSL
        }

        
    }
}
