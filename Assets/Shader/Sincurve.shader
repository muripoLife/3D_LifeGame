Shader "Sincurve" {
    SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        struct Input {
            float4 color : COLOR;
        };
        void vert (inout appdata_full v) {
        	v.vertex.y += 5.0 * v.normal.x * -sin(v.vertex.x * 3.14);
        }
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = half3(1.0, 1.0, 0.5);
        }
        ENDCG
    }
    Fallback "Diffuse"
}