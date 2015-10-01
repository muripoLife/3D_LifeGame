Shader "Custom/DeadCell" {
    Properties {
      _MainTex ("Texture", 2D) = "black" {}
      _RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1.0)
      _RimPower ("Rim Power", Range(1.0,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float3 viewDir;
      };
      sampler2D _MainTex;
      float4 _RimColor;
      float _RimPower;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    } 
   Fallback "Diffuse"
 }