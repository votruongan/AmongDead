Shader "PostProcess/CameraMainPostProcess"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}   
        _Tint("Tint", Color) = (1,1,1,1)
        _Contrast ("Contrast", float) = 1
        _Brightness ("Brightness", float) = 1
        _VignetteIntensity ("Vignette Intensity", Range(0,1)) = 0.5            
    }
    SubShader
    {
        Cull Off 
        ZWrite Off 
        ZTest Always
    
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
            
            sampler2D _MainTex;
            


            half4 _Tint;
            
            half _Contrast;
            half _Brightness;


            half _VignetteIntensity;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);    
                o.uv = v.uv;                      
                return o;
            }


            //CONTRAST METHOD
            half3 AdjustContrast(half3 color, half contrast) 
            {
                color = saturate(lerp(half3(0.5, 0.5, 0.5), color, contrast));
                return color;
            }  


            fixed4 frag (v2f i) : SV_Target
            {   
               //MAIN CAMERA FRAME TEXTURE
               float4 colMain = tex2D(_MainTex, i.uv);
               
               //CONTRAST
               colMain.rgb = AdjustContrast(colMain, _Contrast);                 
                
               //BRIGHTNESS
               colMain.rgb = pow(colMain, 1.0 / _Brightness); 
               
               //VIGNETTE
               half2 uvCoords = i.uv;          
               uvCoords = (uvCoords - 0.5) * 2.0;    
               half uvCoordsDot = dot (uvCoords,uvCoords);          
               half vignetteMask = 0.5 - uvCoordsDot * _VignetteIntensity;
               colMain *= vignetteMask; 
         
               //APPLY CUSTOM TINT
               colMain *= _Tint;  
                        
               return colMain;
            }
            ENDCG
        }
    }
}
