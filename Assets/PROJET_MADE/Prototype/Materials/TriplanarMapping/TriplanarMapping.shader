Shader "Custom/TriplanarMapping"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        struct InterpolatorVertex {
            #if !defined(NO_DEFAULT_UV)
                float4 uv : TEXTCOORD0;
            #endif
        };

        struct Interpolators {
            #if !defined(NO_DEFAULT_UV)
                float4 uv : TEXTCOORD0;
            #endif
        };

        float4 GetDefaultUV (Interpolators i) {
            #if defined(NO_DEFAULT_UV)
                return float4(0, 0, 0, 0);
            #else
                return i.uv;
            #endif
        }

        #if !defined(UV_FUNCTION)
            #define UV_FUNCTION GetDefaultUV
        #endif

        float GetDetailMask (Interpolators i) {
            #if defined (_DETAIL_MASK)
                return tex2D(_DetailMask, UV_FUNCTION(i).xy).a;
            #else 
                return 1;
            #endif
        }

        InterpolatorVertex MyVertexProgram (VertexData v) {
            #if !defined(NO_DEFAULT_UV)
                i.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                i.uv.zw = TRANSFORM_TEX(v.uv, _DetailTex);

                #if VERTEX_DISPLACEMENT
                    float displacement = tex2Dlod(_DisplacementMap, float4(i.uv.xy, 0, 0)).g;
                    displacement = (displacement - 0.5) * _DisplacementStrenght;
                    v.normal = normalize(v.normal);
                    v.vertex.xyz += v.normal * displacement;
                #endif
            #endif
        }

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
