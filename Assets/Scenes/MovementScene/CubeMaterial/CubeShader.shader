// vim: ft=glsl
Shader "Unlit/CubeShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Scale ("Scale", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 viewNormal : TEXCOORD1;
                float3 viewPos : TEXCOORD2;
            };

            fixed4 _Color;
            float _Scale;

            // Transforms normal from object to view space
            inline float3 myObjectToViewNormal( in float3 norm )
            {
                return normalize(mul((float3x3)UNITY_MATRIX_MV, norm));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.viewNormal = myObjectToViewNormal(v.normal);
                o.viewPos = UnityObjectToViewPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed drawLine (fixed t, fixed c, fixed d)
            {
                // fixed c = 0.05 / 2;
                // fixed d = 0.01;
                return smoothstep(-c-d, -c+d, t) - smoothstep(c-d, c+d, t);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 st = i.uv;
                st = frac(st * _Scale * 3);
                // st = frac(st * 2);
                st = st * 2 - 1;

                fixed dst = length(i.viewPos);

                fixed t = 0.0;
                fixed m = max(abs(st.x), abs(st.y));
                fixed m2 = 1-min(abs(st.x), abs(st.y));

                t = m + 0.025;
                t -= pow(m2, 2.0);
                t = clamp(t, 0, 1);

                fixed normCos = dot(normalize(i.viewNormal), normalize(-i.viewPos));
                fixed dstmod = 0.02*dst*dst + 0.2*dst + 0.0;
                dstmod *= 0.01;
                fixed md = dstmod / normCos;
                t = pow(t, 1/md);

                t *= pow(1 - dstmod / (dstmod + 0.2), 1 / normCos);

                // t = pow(t, 2);

                fixed4 col = fixed4(t, t, t, 1.0);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
                // return fixed4(normCos, normCos, normCos, 1.0);
                // return fixed4(i.viewNormal*0.5+0.5, 1.0);
            }
            ENDCG
        }
    }
}
