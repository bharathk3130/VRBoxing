Shader "Custom/UniformGlowShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Pass
        {
            // Set the render queue for transparency
            Tags { "Queue"="Overlay" }
            Blend SrcAlpha OneMinusSrcAlpha // Enable blending for transparency
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GlowColor;
            float _GlowIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Only apply the glow to non-transparent areas
                if (col.a < 0.1)
                    discard; // Fully transparent, no rendering

                // Calculate a glow based on the alpha
                float glow = col.a * _GlowIntensity; 
                
                // Apply glow color to non-transparent areas, preserving alpha
                return fixed4(_GlowColor.rgb * glow, col.a);
            }
            ENDCG
        }
    }
}
