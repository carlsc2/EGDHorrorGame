Shader "Custom/WorldSpaceTiling" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Metallic("Metallic", float) = 1.0
		_Smoothness("Smoothness", float) = 1.0
		_ScaleX("Scale X", float) = 1.0
		_ScaleY("Scale Y", float) = 1.0
		_Emission("Emission", float) = 0.5
	}
	SubShader{
		Tags{"RenderType" = "Opaque" }
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#pragma surface surf Standard fullforwardshadows
		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 worldPos;
			float3 worldNormal; INTERNAL_DATA
		};
		sampler2D _MainTex;
		sampler2D _NormalMap;

		half _ScaleX;
		half _ScaleY;
		half _Metallic;
		half _Smoothness;
		float _Emission;
		float4 _Color;

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float3 correctWorldNormal = WorldNormalVector(IN, float3(0, 0, 1));
			float2 uv = IN.worldPos.xz;

			if (abs(correctWorldNormal.x) > 0.5) uv = IN.worldPos.yz;
			if (abs(correctWorldNormal.z) > 0.5) uv = IN.worldPos.xz;

			uv.x /= _ScaleX;
			uv.y /= _ScaleY;

			float4 c = tex2D(_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Normal = UnpackNormal(tex2D(_NormalMap, uv));
			o.Smoothness = _Smoothness;
			o.Emission = o.Albedo * _Emission;
		}
		ENDCG

	}

	Fallback "Diffuse"
}