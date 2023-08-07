Shader "Effects/Flash"
{
	Properties
	{
		_MainTex("Texture", 2D) = "White" {}
		_Texture("Texture", 2D) = "white" {}

		_Color("Flash Color", COLOR) = (1, 1, 1, 1)
		_Fade("Fade", Range(0, 1)) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off

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
			
			sampler2D _MainTex;
			sampler2D _Texture;

			float4 _Color;
			float _Fade;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				float4 tex = tex2D(_Texture, i.uv);

				//if(tex.b < _Fade)
					//return _Color;

				//return col;

				return col = lerp(col, _Color, (tex.r * _Fade) + _Fade);
			}
			ENDCG
		}
	}
}
