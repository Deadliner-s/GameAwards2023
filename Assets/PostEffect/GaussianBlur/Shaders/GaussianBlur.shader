Shader "Hidden/GaussianBlur"
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
			float4 _MainTex_ST;
			half4 _MainTex_TexelSize;
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
				half2 coordH : TEXCOORD0;
				half2 offsetH: TEXCOORD1;
			};

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				half2 uv = TRANSFORM_TEX(IN.uv, _MainTex);

				OUT.offsetH = _MainTex_TexelSize.xy * half2(1.0, 0.0) * _SamplingDistance;
				OUT.coordH = uv - OUT.offsetH * ((samplingCount - 1) * 0.5);

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 color = 0;

				// êÖïΩï˚å¸
				for (int j = 0; j < samplingCount; j++) {
					//color += tex2D(_MainTex, IN.coordH) * weights[j] * 0.5;
					IN.coordH += IN.offsetH;
				}

				return color;
			}
			ENDHLSL
		}
	}
}
