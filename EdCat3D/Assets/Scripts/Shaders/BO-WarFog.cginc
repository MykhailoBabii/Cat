float4 _TerrainScale;
float4 _WarFogColor;
sampler2D _WarFog;

float3 ApplyWarFog(float3 color, float2 coords)
{
	float wfog = tex2D(_WarFog, coords).x;
	return lerp(color, _WarFogColor.rgb, (1 - wfog) * _WarFogColor.a);
}

float3 ApplyWarFog(float3 color, float2 coords, float4 splatCoeff)
{
	float wfog = tex2D(_WarFog, coords).x;
	return lerp(color, _WarFogColor.rgb * dot(splatCoeff, float4(1,1,1,1)), (1 - wfog) * _WarFogColor.a);
	//return lerp(color, tex2D(_WarFog, coords), _WarFogColor.a);
}