Shader "Hidden/GaussianBlur2"
{
	SubShader
	{
		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);
			float _SamplingDistance;
			//half _SampleCount;
			//half _Strength;
			static const int samplingCount = 7;
			static const half weights[samplingCount] = { 0.036, 0.113, 0.216, 0.269, 0.216, 0.113, 0.036 };

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float2 uv : TEXCOORD0;
				half2 coordV : TEXCOORD1;
				half2 coordH : TEXCOORD2;
				half2 offsetV: TEXCOORD3;
				half2 offsetH: TEXCOORD4;
			};

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.uv = IN.uv;

				OUT.offsetV = OUT.uv.xy * half2(0.0, 1.0) * (_SamplingDistance * 1 / 100);
				OUT.offsetH = OUT.uv.xy * half2(1.0, 0.0) * (_SamplingDistance * 1 / 100);

				OUT.coordV = OUT.uv - OUT.offsetV * ((samplingCount - 1) * 0.5);
				OUT.coordH = OUT.uv - OUT.offsetH * ((samplingCount - 1) * 0.5);

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 color = 0;

				//// 垂直方向
				//for (int i = 0; i < samplingCount; i++) {
				//	// サンプリングして重みを掛ける。後で水平方向も合成するため0.5をかける
				//	color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.coordV) * weights[i] * 0.5;
				//	// offset分だけサンプリングポイントをずらす
				//	IN.coordV += IN.offsetV;
				//}

				// 水平方向
				for (int j = 0; j < samplingCount; j++) {
					color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.coordH) * weights[j] * 1.0f;
					IN.coordH += IN.offsetH;
				}

				return color;
			}
			ENDHLSL
		}
	}
}
