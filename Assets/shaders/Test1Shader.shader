Shader "Custom/Test1Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// I can alter the coordinates on _MainTex by the way I do

			// Inverting coordinates and varying them by time
			//float2 offset = float2 (0,
			//tex2D (_MainTex, float2(IN.uv_MainTex.y, _Time[1]/100)).r
			//tex2D (_MainTex, float2(0, IN.uv_MainTex.x)).r
			//);

			// Or just adding a extra factor of x-coordinates and time on y-coordinates (causes a cool effect)
			float2 offset = float2 (
			0,
			IN.uv_MainTex.x + _Time[1]/10
			);

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + offset ) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
