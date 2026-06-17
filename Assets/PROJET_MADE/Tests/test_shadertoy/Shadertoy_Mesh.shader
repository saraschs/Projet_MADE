Shader "Custom/Shadertoy_Mesh"
{
    Properties
    {
        _Mouse ("Mouse", Vector) = (0,0,0,0)
        _UseWorld ("Use World Space", Float) = 0
        _Scale ("Scale", Float) = 1.0
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

            #define R2 1.41421356237

            float4 _Mouse;
            float _UseWorld;
            float _Scale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float2x2 getMat(float t, float s)
            {
                float4 v = sin(t * 0.1 - t / s + float4(0,11,33,0));
                return float2x2(v.x, v.y, v.z, v.w);
            }

            void M(inout float2 q, float t, float s)
            {
                q = mul(q, getMat(t, s));
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 FC;

                // 🔁 Choix UV ou WORLD
                if (_UseWorld > 0.5)
                {
                    FC = i.worldPos.xy * _Scale;
                }
                else
                {
                    FC = i.uv * 2.0 - 1.0;
                    FC.x *= _ScreenParams.x / _ScreenParams.y;
                    FC *= _Scale;
                }

                float t = _Time.y * (1.0 - _Mouse.x / _ScreenParams.x);

                float4 o = 0;
                float g = 0.0;

                for (int k = 0; k < 20; k++)
                {
                    float s = 2.0;
                    float e = 2.0;

                    float4 p = float4(
                        g * FC,
                        g - 1.7,
                        1.0
                    );

                    for (int j = 0; j < 24; j++)
                    {
                        M(p.xy, t, s);
                        M(p.yz, t, s);

                        p = abs(p) * R2 - R2 + 1.0;

                        s *= R2;
                    }

                    e = length(p) / s;
                    g += e;

                    o += (1e-5 - p / s * 0.05) / e;
                }

                return o;
            }

            ENDCG
        }
    }
}