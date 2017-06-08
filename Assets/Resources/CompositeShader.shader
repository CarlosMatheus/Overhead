Shader "Hidden/CompositeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
			
			sampler2D _MainTex;   //Camera's Texture
			sampler2D _PrePassTex; //Solid Color Texture
			sampler2D _BlurTex; //Blurred Texture version of the solid color one
			float _Intensity; //Intensity of the glow effect

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);  //Gets what the camera would render normally
				fixed4 glow = max(0, tex2D(_BlurTex, i.uv) - tex2D(_PrePassTex, i.uv)); //Subtracts the blurred image from de solid one, leaving a glow aura only
				return col + glow*_Intensity; //Adds the aura to what the color would render
			}
			ENDCG
		}
	}
}
