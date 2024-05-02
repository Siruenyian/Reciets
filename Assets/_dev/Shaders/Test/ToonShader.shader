Shader "Stylized/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(0,1))=0.3
        _Strength ("Strength", Range(0,1))=0.5
        _Color ("Color", COLOR) = (0,1,1,1)
        _shadowDetail ("shadowDetail", Range(0,1)) = 0.3

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //LOD 100

        Pass
        {
            HLSLPROGRAM
            #define CUSTOM_LIGHTING_INCLUDED
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
			#include "AutoLight.cginc"
            //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };
            
            struct Varyings
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1);
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };
            float Toon(Varyings v, float3 light, float shadowDet)
            {
                //float shadow = SHADOW_ATTENUATION(i);
                float3 normal=v.worldNormal;
                float NdotL = dot(normalize(normal), normalize(light));
                float maxVal = max(0.0, NdotL) / shadowDet;

                float final=floor(maxVal);
                return final;   
            }
            
            float ToonAdvanced(Varyings v, float3 light, float shadowDet)
            {
                float3 normal=v.worldNormal;
                float NdotL = dot(normalize(normal), normalize(light));
                float maxVal = max(0.0, NdotL) / shadowDet;
    
                float final=floor(maxVal);
                
                

           
        

                return final;   
            }
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;
            float _Strength;
            float4 _Color;
            float _shadowDetail;
            
            
            Varyings vert (Attributes v)
            {
                Varyings o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag (Varyings i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //col*=Toon(i, _WorldSpaceLightPos0.xyz, _shadowDetail)*_Strength*_Color+_Brightness;
                col*=ToonAdvanced(i, _WorldSpaceLightPos0.xyz, _shadowDetail)*_Strength*_Color+_Brightness;

                return col;
            }
            ENDHLSL
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
