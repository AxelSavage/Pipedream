Shader "Custom/GlassShader" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 0.2)
        _Specular ("Specular", Range(0, 1)) = 0.5
        _Smoothness ("Smoothness", Range(0, 1)) = 0.9
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert transparent

        sampler2D _MainTex;
        float _Specular;
        float _Smoothness;
        float4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = _Color.rgb;
            o.Specular = _Specular;
            o.Smoothness = _Smoothness;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}