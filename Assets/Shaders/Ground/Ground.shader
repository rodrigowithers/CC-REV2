Shader "Unlit/Ground"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_Map("Detail Map", 2D) = "white" {}
		_Aplha("Aplha Map", 2D) = "white" {}

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
				sampler2D _Alpha;

				float4 _Color;

				float _Cutoff;

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					//fixed4 col = tex2D(_MainTex, i.uv);

					float4 alphaMap = tex2D(_Alpha, i.uv); // Cor do mapa de Alpha no texel atual
					float4 mapCol = tex2D(_Map, i.uv);

					//if (mapCol.r < _Cutoff)
					//	return _Color;//float4(0, 0, 1, 1);

					return _Color * mapCol;

					/*if (alphaMap.r > _Cutoff)
						return mapCol;
					else
						return _Color;*/
				}
				ENDCG
			}
		}
}
