Shader "MyShaders/BrushPaint_V3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _CenterRadius ("Center Radius", Vector) = (0,0,0,0) 
        _Strength ("Strength", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img 
            #pragma fragment frag
            #include "UnityCG.cginc" 

            sampler2D _MainTex;
            float4 _CenterRadius;
            float _Strength;

            fixed4 frag (v2f_img i) : SV_Target
            {
                
                fixed oldPixel = tex2D(_MainTex, i.uv).r;

                float2 center = _CenterRadius.xy;
                float radius = _CenterRadius.z;
                float hardness = _CenterRadius.w;
                
                float dist = distance(i.uv, center);
                float brush = 1.0 - smoothstep(radius * hardness, radius, dist);
                brush *= _Strength;

                float result = max(oldPixel, brush);

                return fixed4(result, result, result, 1.0);
            }
            ENDCG
        }
    }
}