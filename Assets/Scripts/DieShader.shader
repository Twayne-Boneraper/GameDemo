Shader "Custom/DieShader"
{
    Properties
    {
        _MainTex("主纹理",2D) = "white"{}
        _Value("灰度",Float)= 0
    }
    SubShader
    {
        Pass{
        
        CGPROGRAM
#pragma vertex vert//顶点函数

#pragma fragment frag//片元函数

#include "UnityCG.cginc"//引用Unity里面的shader

        //这个结构体应用于顶点函数
        struct a2v {

            float4 position : POSITION;
            float2 uv : TEXCOORD0;

        };

        sampler2D _MainTex;
        float _Value;

        struct v2f {

            float4 sv_position : SV_POSITION;
            float2 sv_uv : TEXCPOORD0;

        };

        v2f vert(a2v i) {

            v2f o;
            o.sv_position = UnityObjectToClipPos(i.position);
            o.sv_uv = i.uv;
            return o;

        }

        float4 frag(v2f u) : SV_TARGET{

            float4 mainTex = tex2D(_MainTex,u.sv_uv);
            float4 tempColor = 0.299 * mainTex.r + 0.587 * mainTex.g + 0.114 * mainTex.b;
            float4 finalColor = lerp(mainTex, tempColor, _Value);

            return finalColor;
        }
        ENDCG

        }
        
    }
    FallBack "Diffuse"
}
