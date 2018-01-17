Shader "MyOwn/HeatEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HeatColors("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off
		ZWrite Off
		ZTest Off

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
			sampler2D _HeatColors;
			fixed4 _Enemies[16];
			int _EnemiesCount;

			fixed2 CalculateDistanceToClosest(float2 pos)
			{
				fixed2 result = fixed2(999.0f, 999.0f);
				for (int i = 0; i < _EnemiesCount; i++)
				{
					if (_Enemies[i].z > 0) // if is not behind the camera (positive UV but negative distance)
					{
						float dist = distance(_Enemies[i], pos);
						result.x = dist < result ? dist : result;
						result.y = _Enemies[i].z;
					}
				}
				return result;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed distHeat = 0.0f;
				fixed4 col = tex2D(_MainTex, i.uv);
				if (_EnemiesCount > 0)
				{
					fixed2 dist = CalculateDistanceToClosest(i.uv);
					fixed maxSize = lerp(0.2f, 0.05f, smoothstep(150, 500, dist.y));
					fixed linearDist = smoothstep(maxSize, 0.0f, dist.x);

					fixed distrgb = pow(linearDist, 2);
					distHeat = pow(linearDist, 10);
					
					col = lerp(col, fixed4(1, 1, 1, 1), distrgb);
				}
				fixed avg = (col.r + col.g + col.b) / 3.0f;
				fixed4 colHeat = tex2D(_HeatColors, float2(avg, 0.5f));
				return lerp(colHeat, fixed4(1,1,1,1), distHeat);
			}
			ENDCG
		}
	}
}
