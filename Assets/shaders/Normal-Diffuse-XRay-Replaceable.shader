Shader "XRay Shaders/Diffuse-XRay-Replaceable"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_EdgeColor("XRay Edge Color", Color) = (0,0,0,0)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			//////////////////////////////////////////////////////////////////////////////////////////////////////
			// In some cases, it's necessary to force XRay objects to render before the rest of the geometry	//
			// This is so their depth info is already in the ZBuffer, and Occluding objects won't mistakenly	//
			// write to the Stencil buffer when they shouldn't.													//
			//																									//
			// This is what "Queue" = "Geometry-1" is for.														//
			// I didn't bring this up in the video because I'm an idiot.										//
			//																									//
			// Cheers,																							//
			// Dan																								//
			//////////////////////////////////////////////////////////////////////////////////////////////////////
			"Queue" = "Geometry-1"
			"RenderType" = "Opaque"
			"XRay" = "ColoredOutline"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	
	Fallback "Legacy Shaders/VertexLit"
}
