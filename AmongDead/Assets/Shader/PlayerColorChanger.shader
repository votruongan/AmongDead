// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PlayerShader/ColorSwap"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _ColorToChange("Color You Want To Change", Color) = (0,0,1,1)
        _DesiredColor("Desired Color ", Color) = (1,0,0,1)
        _ColorToChange2("Color 2 You Want To Change", Color) = (0,0,1,1)
        _DesiredColor2("Desired Color 2", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Transparent+1"
        }

        Pass
        {
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile DUMMY PIXELSNAP_ON

        sampler2D _MainTex;
        float4 _ColorToChange;
        float4 _DesiredColor;
        float4 _ColorToChange2;
        float4 _DesiredColor2;

        struct Vertex
        {
            float4 vertex : POSITION;
            float2 uv_MainTex : TEXCOORD0;
            float2 uv2 : TEXCOORD1;
        };

        struct Fragment
        {
            float4 vertex : POSITION;
            float2 uv_MainTex : TEXCOORD0;
            float2 uv2 : TEXCOORD1;
        };

        Fragment vert(Vertex v)
        {
            Fragment o;

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv_MainTex = v.uv_MainTex;
            o.uv2 = v.uv2;

            return o;
        }

        float4 frag(Fragment IN) : COLOR
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);

                if (c.r >= _ColorToChange.r - 0.6 && c.r <= _ColorToChange.r + 0.6
                && c.g >= _ColorToChange.g - 0.6 && c.g <= _ColorToChange.g + 0.6
                    && c.b >= _ColorToChange.b - 0.6 && c.b <= _ColorToChange.b + 0.6)
            {
                return _DesiredColor;
            }
                if (c.r >= _ColorToChange2.r - 0.6 && c.r <= _ColorToChange2.r + 0.6
                && c.g >= _ColorToChange2.g - 0.6 && c.g <= _ColorToChange2.g + 0.6
                    && c.b >= _ColorToChange2.b - 0.6 && c.b <= _ColorToChange2.b + 0.6)
            {
                return _DesiredColor2;
            }
            return c;
        }
            ENDCG
        }
    }
}
