﻿Shader "Custom/ChowderShaderAlpha" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AlphaTex("AlphaMap (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Emission("Emission", Range(0,1)) = 0.5
		_threshold("threshold", Range(0,1)) = 0.5
	}

	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		
		LOD 200
		
		CGPROGRAM
		#include "noiseSimplex.cginc"
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float4 _MainTex_ST;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AlphaTex;
			float4 screenPos;
			float3 worldPos;
		};

		float _threshold;
		half _Glossiness;
		half _Metallic;
		float _Emission;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w * _MainTex_ST.xy + _MainTex_ST.zw ;
			fixed4 c = tex2D (_MainTex, screenUV) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = tex2D(_AlphaTex, IN.uv_AlphaTex).a;
			o.Emission = c.rgb * _Emission;
			float ns = o.Alpha * snoise(IN.worldPos);
			if (ns < _threshold) {
				discard;
			}
				
		}
		ENDCG
	} 
	FallBack "Diffuse"
}