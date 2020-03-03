Shader "Custom/Test"
{
    Properties
    {
       _MainTex ("Texture Image", 2D) = "white" {}
       _RotationSpeed("RotationSpeed", Float) = 8.0
       _MaxSpeed("MaxSpeed", Float) = .1
      _ScaleX ("Scale X", Float) = 1.0
      _ScaleY ("Scale Y", Float) = 1.0
    }
SubShader {
            Tags { "Queue" = "Transparent" "RenderType"="Transparent"  "ForceNoShadowCasting" = "True" }
            Blend SrcAlpha OneMinusSrcAlpha
            Lighting Off
            ZWrite off
            LOD 200
           
            CGPROGRAM
            #pragma surface surf Standard alpha vertex:vert
            #pragma target 3.0
     
            uniform sampler2D _MainTex;     
            uniform float _RotationSpeed;   
            uniform float _MaxSpeed;
            uniform float _ScaleX;
            uniform float _ScaleY;
            uniform float4x4 _OldWorldMatrix;
            uniform float4x4 _WorldMatrix;
     
            struct Input {
                float2 uv_MainTex;
               // float2 texcoord1;
               // float blend;
                float4 color;
            };
     
            
            void vert (inout appdata_full v,  out Input o) {

                //half maxSpeed = 100;
                half3 oldPos = mul(_OldWorldMatrix, v.vertex);
                half3 newPos = mul(_WorldMatrix, v.vertex);
                half3 speedVec = newPos - oldPos;
                half viewSpeed = dot(speedVec, _WorldSpaceCameraPos - newPos);//length(speedVec);
                float RelativeSpeed = (viewSpeed/_MaxSpeed);

                //float theta = smoothstep( 0 , 2*3.1415 , viewSpeed );


                // v.texcoord.xy -=0.5;
                //  float s = sin ( viewSpeed* _Time  );
                // float c = cos ( viewSpeed * _Time );
                // // float s = sin ( _RotationSpeed*_Time  );
                // // float c = cos (  _RotationSpeed*_Time );
                // float2x2 rotationMatrix = float2x2( c, -s, s, c);
                // rotationMatrix *=0.5;
                // rotationMatrix +=0.5;
                // rotationMatrix = rotationMatrix * 2-1;
                // v.texcoord.xy = mul ( v.texcoord.xy, rotationMatrix );
                // v.texcoord.xy += 0.5;

                o.uv_MainTex = v.texcoord.xy;
                //o.blend = v.texcoordBlend;
                o.color = v.color;
                //v.vertex = mul(UNITY_MATRIX_P,  mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) + float4(v.vertex.x, v.vertex.y, 0.0, 0.0)* float4(_ScaleX, _ScaleY, 1.0, 1.0));
            }
     
            void surf (Input IN, inout SurfaceOutputStandard o) {  
                half4 c = tex2D (_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
        FallBack "Standard"
}
