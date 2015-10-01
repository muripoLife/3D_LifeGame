Shader "Sincurve" {
	Properties{
		_DiffuseColor("Diffuse Color",Color)=(1.0, 1.0, 0.5)
	}

    SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        struct Input {
            float4 color : COLOR;
        };
        float4 _DiffuseColor;
        void vert (inout appdata_full v) {
        	v.vertex.y += 10.0 * v.normal.x * abs(sin(_Time.x*100)) * -sin(v.vertex.x * 3.14);
        	v.vertex.z += 10.0 * v.normal.x * abs(sin(_Time.x*100)) * -sin(v.vertex.x * 3.14);
        }
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = _DiffuseColor;
        }
        ENDCG
    }
    Fallback "Diffuse"
}