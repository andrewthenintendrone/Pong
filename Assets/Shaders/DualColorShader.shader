Shader "Custom/DualColorShader"
{
    Properties
    {
        _Map ("Map", 2D) = "white" { }
        _Color1("Color 1", Color) = (1,1,1,1)
        _Color2("Color 2", Color) = (0,0,0,0)
        _Specular("Specular", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags{ "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _Map;
        fixed4 _Color1;
        fixed4 _Color2;
        float _Specular;

        struct Input
        {
            fixed2 uv_Map;
            fixed4 color : COLOR;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = (_Color1 * tex2D(_Map, IN.uv_Map).rgb);
            o.Albedo += (_Color2 * (1 - tex2D(_Map, IN.uv_Map).rgb));
            //o.Specular = _Specular;
            o.Smoothness = _Specular;
        }
        ENDCG
    }
    FallBack "Specular"
}