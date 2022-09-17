Shader "BO/SimpleWaterRiver"
{
	Properties 
	{
		_FoamTex("Diffuse (RGBA)", 2D) = "white" {}
		_NormalTex("Normal (S_Nx_G_Ny)", 2D) = "white" {}
		_CubemapTex("Cubemap (RGB)", Cube) = "" { TexGen CubeReflect }
		_AlphaTex("Alpha (GrayScale)", 2D) = "white" {}
		
		_WaterColor("Water Color", Color) = (1,1,1,1)
		_AlphaColor("Alpha Color", Color) = (1,1,1,1)
		
		_SpecularColor("Specular color", Color) = (0,0,0,0)
		_Specular("Specular intencity", Range(0,1)) = 0
		_Shininess("Shininess", Range(0,1)) = 0.1
		_FresnelCoeff("Fresnel", Range(1,8)) = 1
		_Bumpiness("Bumpiness", Range(0,2)) = 1
		_ScrollSpeedNormal("Wave Speed", Range(0,10)) = 4
		_ScrollSpeedFoam("Foam Speed", Range(0,10)) = 4
		_ScrollDirection("Foam Direction", Range(0,6.28)) = 0
		_FoamAlpha("Foam Alpha", Range(0,1)) = 1
		_Cutoff("Cutoff", Range(0,1)) = 0.01
	}
	SubShader 
	{
		Tags 
   		{ 
			"Queue"="Geometry-51"
			"RenderType"="Transparent"
			"Shadow"="On"
		}
		ColorMask RGB
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
	
		CGPROGRAM
		#pragma surface surf Custom vertex:vert nolightmap alphatest:_Cutoff
		#pragma target 3.0
    	#include "BO-WarFog.cginc"

		sampler2D _FoamTex;
		sampler2D _NormalTex;
		samplerCUBE _CubemapTex;
		sampler2D _AlphaTex;
		
		float4 _WaterColor;
		float4 _AlphaColor;
		float4 _SpecularColor;
		float  _FresnelCoeff;
		float  _Specular;
		float  _Shininess;
	    float  _Bumpiness;
	    float  _ScrollSpeedNormal;
	    float  _ScrollSpeedFoam;
	    float  _FoamAlpha;
	    float  _Transparency;
	    float _ScrollDirection;

		struct Input 
		{
			float2 uv_FoamTex;
			float2 uv2_AlphaTex;
			float3 worldRefl; 
      		float3 worldPos; 
			INTERNAL_DATA
		};

		struct SurfaceOutputCustom {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 viewDir;
			half  Specular;
			half  Alpha;
			half  Gloss;   
			half  WarFog;
		};
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.uv2_AlphaTex.y = v.texcoord1.y;			// use Y channel, as we always use 0.5 in surf
		}
		
		void surf(Input IN, inout SurfaceOutputCustom o) 
		{
			float2 offsDir;
			sincos(_ScrollDirection, offsDir.x, offsDir.y);
			
			float2 texCoords;
			texCoords.x = IN.uv_FoamTex.x * offsDir.y - IN.uv_FoamTex.y * offsDir.x;		// rotate
			texCoords.y = IN.uv_FoamTex.x * offsDir.x + IN.uv_FoamTex.y * offsDir.y;
			
			float2 foamUV = texCoords;
			foamUV.x +=	_ScrollSpeedFoam * _Time;			// shift
						
			half4 foamTex = tex2D(_FoamTex, foamUV);
			
			float2 normalUV = texCoords;
			normalUV.x += _ScrollSpeedNormal * _Time;		// shift
			half4 normTex = tex2D(_NormalTex, normalUV);
			
			// alpha
			half alpha = tex2D(_AlphaTex, float2(IN.uv2_AlphaTex.x, 0.5f));
			
			// Decode normal
			o.Normal.xy = _Bumpiness * (normTex.ag * 2 - 1);
			o.Normal.z  = sqrt( 1 - dot(o.Normal.xy, o.Normal.xy) );
			
			//
			o.viewDir = normalize(-IN.worldRefl); // Actually, in this case IN.worldRefl is ViewDir in WCS
			half3 worldRefl = WorldReflectionVector(IN, o.Normal);
			half3 worldNorm = normalize(-IN.worldRefl + worldRefl);
				
			// Calculate reflection
			half4 cubeTex = texCUBE(_CubemapTex, worldRefl);
			
			// Fresnel
			half fresnel = 1.0f / pow(1.0f + max(0, dot(worldNorm, o.viewDir)), _FresnelCoeff);
			
			// Foam alpha
			float foamAlpha = foamTex.a * _FoamAlpha;
			float foamLum = Luminance(foamTex.rgb) * foamTex.a;
			
			float3 tempColor = lerp(_WaterColor.xyz, _WaterColor.xyz * _AlphaColor, 1.0f - IN.uv2_AlphaTex.y); 			
			
			// Make albedo
			o.Albedo = lerp(tempColor, cubeTex.xyz, fresnel);
			o.Albedo = lerp(o.Albedo, foamTex.xyz, foamAlpha);
			
			// WarFog value
			o.WarFog = tex2D(_WarFog, IN.worldPos.xz * _TerrainScale.xy).x;
			
			// Write scalar values
			o.Specular = _Shininess;
			o.Alpha = alpha * max(IN.uv2_AlphaTex.y, foamAlpha);
			o.Gloss = _Specular * (1- foamAlpha);
		}
		
		half4 LightingCustom(SurfaceOutputCustom s, half3 lightDir, half atten)
		{
			half3 h = normalize(lightDir + s.viewDir);
			
			float nh = max(0, dot(s.Normal, h));
			float spec = pow(nh, s.Specular*256.0f) * s.Gloss;
			
			half4 c;
			c.rgb = _LightColor0.rgb * (s.Albedo + _SpecularColor.rgb * spec) * (atten * 2);
      		c.rgb = lerp(c.rgb, _WarFogColor.rgb, (1 - s.WarFog) * _WarFogColor.a);
			c.a = s.Alpha;
			//c.rgb = lightDir;
			return c;
		}
		
		ENDCG
	} 
	FallBack off
}
