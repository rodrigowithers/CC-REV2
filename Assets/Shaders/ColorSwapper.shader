Shader "Unlit/ColorSwapper"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("New Color", Color) = (1, 1, 1, 1)

		_Transparency("Transparency Cutout", Range(0,1)) = 0.5

		_Cutoff("Cutoff", Range(0, 1)) = 0
		_Cutin("Cutin", Range(0, 1)) = 0
		_Lerp("Lerp", Range(0, 1)) = 0

		_White("White", Color) = (1, 1, 1, 1)
		_Flash("Flash Amount", Range(0, 1)) = 0

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "RenderQueue"="Transparent"}
		LOD 100

		Pass
		{
	
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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

			float4 _Color;

			float _Transparency;

			float _Cutoff;
			float _Cutin;

			float _Lerp;

			float _Flash;
			float4 _White;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				if(col.a < _Transparency) discard;

				// Multiplica cor
				if (col.r <= _Cutoff && col.r >= _Cutin)
					col = lerp(col, _Color, _Lerp);

				// Flash
				col.rgb = lerp(col.rgb, float3(1.0, 1.0, 1.0), _Flash);
				return col;
			}
			ENDCG
		}
	}
}
