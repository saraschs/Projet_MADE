Shader "Custom/TriplanarTextureBombing"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}

        _Sharpness ("Blend Sharpness", Range(1,64)) = 4

        _TextureScale ("Texture Scale", Float) = 1
        _BombingTiling ("Bombing Cell Size", Float) = 4
        _BombingStrength ("Bombing Offset Strength", Range(0,1)) = 0.35
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;

            float _Sharpness;
            float _TextureScale;

            float _BombingTiling;
            float _BombingStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            // ----------------------------------------------------
            // Random
            // ----------------------------------------------------

            float2 hash22(float2 p)
            {
                p = float2(
                    dot(p, float2(127.1,311.7)),
                    dot(p, float2(269.5,183.3))
                );

                return frac(sin(p) * 43758.5453);
            }

            // ----------------------------------------------------
            // Texture bombing sample
            // ----------------------------------------------------

            fixed4 TextureBombing(float2 uv)
{
    float2 baseUV = uv * _BombingTiling;

    // hash basé uniquement sur position globale (pas de grid visible)
    float2 rnd = hash22(floor(baseUV));

    // jitter continu (pas de blend de cellules)
    float2 offset = (rnd - 0.5) * _BombingStrength;

    float2 uvJittered = baseUV + offset;

    // IMPORTANT: wrap pour éviter seams UV
    uvJittered = frac(uvJittered);

    return tex2D(_MainTex, uvJittered);
}
            // ----------------------------------------------------

            v2f vert(appdata v)
            {
                v2f o;

                o.position = UnityObjectToClipPos(v.vertex);

                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.worldPos = worldPos.xyz;

                o.normal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldPos = i.worldPos * _TextureScale;

                // Triplanar UVs
                float2 uvX = worldPos.zy;
                float2 uvY = worldPos.xz;
                float2 uvZ = worldPos.xy;

                // Texture bombing samples
                fixed4 colX = TextureBombing(uvX);
                fixed4 colY = TextureBombing(uvY);
                fixed4 colZ = TextureBombing(uvZ);

                // Blend weights
                float3 weights = abs(normalize(i.normal));

                weights = pow(weights, _Sharpness);

                weights /= (weights.x + weights.y + weights.z);

                fixed4 col =
                    colX * weights.x +
                    colY * weights.y +
                    colZ * weights.z;

                col *= _Color;

                return col;
            }

            ENDCG
        }
    }

    FallBack "Standard"
}