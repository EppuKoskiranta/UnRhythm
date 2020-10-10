Shader "Custom/fadeawaylikemyhairline"
{
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    // The blending texture blend amount
     _Blend("Blend Textures Amount", Range(0.0,1.0)) = 0.0
         // The Blend texture
         _BlendColor("Blend Texture Color", Color) = (1,1,1,1)
         _BlendTex("Blend Texture Albedo (RGB)", 2D) = "white" {}
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }

            CGPROGRAM
        // Physically based Standard lighting model
        #pragma surface surf Standard alpha  
        struct Input {
            float2 uv_MainTex;
            float2 uv_BlendTex;
        };

        fixed4 _Color;
        fixed4 _BlendColor;
        sampler2D _MainTex;
        sampler2D _BlendTex;
        half _Blend;
        void surf(Input IN, inout SurfaceOutputStandard o) {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 b = tex2D(_BlendTex, IN.uv_BlendTex) * _BlendColor;
            o.Albedo = (c.rgb * (1 - _Blend)) + (b.rgb * (_Blend));
            o.Alpha = (c.a * (1 - _Blend)) + (b.a * _Blend);
        }
        ENDCG
    }
    FallBack "Diffuse"
}