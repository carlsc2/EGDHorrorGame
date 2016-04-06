Shader "Custom/WorldSpaceTiling" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Metallic("Metallic", float) = 1.0
		_ScaleX("Scale X", float) = 1.0
		_ScaleY("Scale Y", float) = 1.0
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#pragma surface surf Standard
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal; INTERNAL_DATA
		};
		sampler2D _MainTex;

		half _ScaleX;
		half _ScaleY;
		half _Metallic;

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float3 correctWorldNormal = WorldNormalVector(IN, float3(0, 0, 1));
			float2 uv = IN.worldPos.xz;

			if (abs(correctWorldNormal.x) > 0.5) uv = IN.worldPos.yz;
			if (abs(correctWorldNormal.z) > 0.5) uv = IN.worldPos.xz;

			uv.x /= _ScaleX;
			uv.y /= _ScaleY;
			o.Albedo = tex2D(_MainTex, uv).rgb;
			o.Metallic = _Metallic;
		}
		ENDCG
	}
	Fallback "Diffuse"
}