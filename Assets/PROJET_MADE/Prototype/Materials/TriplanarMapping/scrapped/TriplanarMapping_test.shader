Shader "Custom/TriplanarMapping_test"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Scale ("Texture Scale", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Scale;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Normalisation de la normale monde
            float3 blend = abs(normalize(IN.worldNormal));

            // Adoucit le blending
            blend = pow(blend, 4);
            blend /= (blend.x + blend.y + blend.z);

            // Coordonnées de projection
            float2 xUV = IN.worldPos.yz * _Scale;
            float2 yUV = IN.worldPos.xz * _Scale;
            float2 zUV = IN.worldPos.xy * _Scale;

            // Textures projetées
            fixed4 xTex = tex2D(_MainTex, xUV);
            fixed4 yTex = tex2D(_MainTex, yUV);
            fixed4 zTex = tex2D(_MainTex, zUV);

            // Blend triplanaire
            fixed4 c =
                xTex * blend.x +
                yTex * blend.y +
                zTex * blend.z;

            c *= _Color;

            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        ENDCG
    }

    FallBack "Diffuse"
}