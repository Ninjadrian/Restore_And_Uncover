Shader "MyShaders/BrushPaint"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Overlay" }

        Pass
        {
            ZTest Always Zwrite off Cull off
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;     // máscara actual
            float4 _CenterRadius;    // (u, v, radius, hardness)
            float _Strength;        // 0..1 por pasada

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float current = tex2D(_MainTex, i.uv).r;

                float2 center = _CenterRadius.xy;
                float radius = _CenterRadius.z;
                float hardness = _CenterRadius.w; //0 = suave, 1 = duro

                float d = distance(i.uv, center);

                //Brush: 1 dentro, 0 fuera (con suavizado)
                float edge = max(1e-5, radius * (1.0 - hardness));
                float brush = 1.0 - smoothstep(radius - edge, radius, d);

                //Limpio (0). Blanco = polvo (1)
                float cleaned = lerp(current, 0.0, brush * _Strength);

                return fixed4(cleaned, cleaned, cleaned, 1);
            }
            ENDHLSL
        }
    }
}
