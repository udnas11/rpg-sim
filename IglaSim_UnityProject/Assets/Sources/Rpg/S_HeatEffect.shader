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

			fixed rand()
			{
				return frac(sin(_Time.x * 13434.0f) * cos(pow(_Time.y, 2.0f) * 9583.0f));
			}

			float rand(float2 seed)
			{
				return frac(sin(dot(seed.xy * _Time.x, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
			}

			fixed CalculateHeat(float2 pos)
			{
				fixed result = 0.0f;
				for (int i = 0; i < _EnemiesCount; i++)
				{
					if (_Enemies[i].z > 0) // if is not behind the camera (positive UV but negative distance)
					{
						float2 enemyScreenspace = float2(_Enemies[i].x, _Enemies[i].y);
						float dist = distance(enemyScreenspace, pos); //screenspace distance to point
						// heat signature should be proportional to distance from point, considering distance away from camera
						float depthFactor = smoothstep(50.0f, 500.0f, _Enemies[i].z);
						float depthEffect = lerp(15.0f, 30.0f, depthFactor);

						//randomizing aura
						dist = lerp(dist * 0.7f, dist * 1.2f, rand());
						dist = lerp(dist * 0.4f, dist * 2.5f, rand(pos));

						//inverting
						dist = 1 - (dist * depthEffect);
						result = dist > result ? dist : result;
					}
				}
				return result;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed distHeat = 0.0f;
				fixed4 col = tex2D(_MainTex, i.uv);
				//fixed4 col = pow(tex2D(_MainTex, i.uv), 0.5f);
				if (_EnemiesCount > 0)
				{
					fixed heat = CalculateHeat(i.uv);
					col = lerp(col, fixed4(1, 1, 1, 1), heat);
				}
				fixed avg = (col.r + col.g + col.b) / 3.0f;
				fixed4 colHeat = tex2D(_HeatColors, float2(avg, 0.5f));
				return lerp(colHeat, fixed4(1,1,1,1), distHeat);
			}
			ENDCG
		}
	}
}
