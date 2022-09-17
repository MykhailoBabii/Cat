Shader "BO/Road1" 
{
	Properties 
	{
		_DiffTex("Diffuse (RGBA)", 2D) = "white" {}
		_NormalTex("Normal (S_Nx_G_Ny)", 2D) = "white" {}
		
		_Diffuse("Diffuse color", Color) = (1,1,1,1)
		
		_Cutoff("Cutoff", Range(0,1)) = 0.01
        _SpecularC("Specular color", Color) = (0,0,0,1)
        _Specular("Specular intencity", Range(0,1)) = 0
        _Shininess("Shininess", Range(0,1)) = 0.0
	}
	SubShader 
	{
		Tags 
		{ 
			"Queue"="Geometry-72"
			"RenderType"="Opaque"
			"Shadow"="On"
		}
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		Cull Off
		ZWrite Off
		
		CGPROGRAM
		#pragma debug
		#pragma surface surf Custom vertex:vert nolightmap alphatest:_Cutoff
		#include "BO-WarFog.cginc"

		sampler2D _DiffTex;
		sampler2D _NormalTex;
		
		float4 _Diffuse;
        float4 _SpecularC;
        float  _Specular;
        float  _Shininess;
		
		struct Input 
		{
			float2 uv_DiffTex;
			float  alpha;
			float3 worldPos;
		};

		struct SurfaceOutputCustom {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Reflection;			
			half  Specular;
			half  Gloss;
			half  Alpha;
			half  Reflectivity;
			half  WarFog;
		};
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.alpha = v.texcoord1.y;
		}
		
		void surf(Input IN, inout SurfaceOutputCustom o) 
		{
			half4 diffTex = tex2D(_DiffTex, IN.uv_DiffTex);
			half4 normTex = tex2D(_NormalTex, IN.uv_DiffTex);
			
			// Decode normal
			o.Normal.xy = normTex.ag * 2 - 1;
			o.Normal.z  = sqrt( 1 - dot(o.Normal.xy, o.Normal.xy) );
			
			// WarFog value
			o.WarFog = tex2D(_WarFog, IN.worldPos.xz * _TerrainScale.xy).x;

			// Make albedo
			o.Albedo = diffTex.rgb * _Diffuse.rgb;
			
			// Write scalar values
            o.Specular = _Shininess;
            o.Gloss = normTex.r * _Specular;
			o.Alpha = diffTex.a * IN.alpha;
		}
		
		half4 LightingCustom(SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize(lightDir + viewDir);
			
			half diff = max(0, dot(s.Normal, lightDir));

            float nh = max(0, dot(s.Normal, h));
            float spec = pow(nh, s.Specular * 128.0f) * s.Gloss;
			
			half4 c;
			c.rgb = _LightColor0.rgb * (s.Albedo.rgb * diff + _SpecularC.rgb * spec) * (atten * 2);
			c.a = s.Alpha;
			
			c.rgb = lerp(c.rgb, _WarFogColor.rgb, (1 - s.WarFog) * _WarFogColor.a);
			
			return c;
		}
		
		ENDCG
	} 
	FallBack off
}
