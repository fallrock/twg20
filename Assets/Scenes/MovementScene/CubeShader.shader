// vim: ft=glsl
Shader "Unlit/CubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Transforms normal from object to view space
            inline float3 myObjectToViewNormal( in float3 norm )
            {
                return normalize(mul((float3x3)UNITY_MATRIX_MV, norm));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
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
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                fixed2 st = i.uv;
                st = frac(st * 48);
                st = st * 2 - 1;

                fixed dst = length(i.viewPos) / 2;

                fixed normCos = dot(normalize(i.viewNormal), normalize(-i.viewPos));
                fixed c = 0.025;
                fixed d = 0.025;
                fixed md = pow(dst, 0.75) / pow(normCos, 0.75);
                d *= md;
                // c *= pow(md, 0.5);

                fixed t = 0.0;
                t += drawLine(st.x, c, d);
                t += drawLine(st.y, c, d);

                fixed m = max(abs(st.x), abs(st.y));
                // t *= step(m, 0.25);

                t = clamp(t, 0, 1);
                // t /= dst;
                // t *= 1 - (length(i.viewPos) / 30);

                col = fixed4(t, t, t, 1.0);
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
