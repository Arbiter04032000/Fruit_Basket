Shader "Custom/VertexColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert vertex:vert
			#pragma target 3.0

			struct Input {
				float4 vertColor;
			};

			void vert(inout appdata_full v, out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.vertColor = v.color;
			}

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = IN.vertColor.rgb;
			}
			ENDCG
		}
    FallBack "Diffuse"
}
