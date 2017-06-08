Shader "Hidden/OutlineReplacement"
{
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
			"Glowable" = "True"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 _glowColor;

			fixed4 frag (v2f i) : SV_Target
			{
				return _glowColor; //Renders the object with a solid color
			}
			ENDCG
		}
	}
}
