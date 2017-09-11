// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Ground"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Map("Detail Map", 2D) = "white" {}

		_Color("Main Color", Color) = (1, 1, 1, 1)
		_Cutoff("Cutoff", Range(0, 1)) = 0
	}
		SubShader
		{
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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				sampler2D _Map;

				float4 _Color;

				float _Cutoff;

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv);		// Cor do mapa de Alpha no texel atual
					float4 mapCol = tex2D(_Map, i.uv);		// Cor da textura de grama no texel atual

					mapCol = clamp(mapCol + _Cutoff, 0.0, 1.0);

					return mapCol * _Color;
				}
				ENDCG
			}
		}
}
