Shader "Spine/Lit Skeleton" {
Properties {
   _Color ("Tint", Color) = (1,1,1,1)
   _MainTex ("Texture Atlas", 2D) = "white" {}
   _Cutoff ("Alpha cutoff", Range(0,1)) = 1
   _Brightness ("Brightness Boost", Range(0,3)) = 2
}

SubShader {
   Tags {"Queue"="AlphaTest"}
   Cull Off
   ZWrite Off
   Blend One OneMinusSrcAlpha
   
   CGPROGRAM
      #pragma surface surf Lambert alphatest:_Cutoff

      sampler2D _MainTex;
      fixed4 _Color;
      half _Brightness;

      struct Input {
         float2 uv_MainTex;
      };

      void surf (Input IN, inout SurfaceOutput o) {
         fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
         o.Albedo = c.rgb * _Brightness;
         o.Alpha = c.a;
      }
   ENDCG   
}

Fallback "Transparent/Cutout/VertexLit"
}