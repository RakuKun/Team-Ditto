Shader "Unlit/NewUnlitShader"
{
    Properties
    {
       _MainTex ("Texture Image", 2D) = "white" {}
       _RotationSpeed("RotationSpeed", Float) = 8.0
      _ScaleX ("Scale X", Float) = 1.0
      _ScaleY ("Scale Y", Float) = 1.0
    }
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100

        Pass
        {
           CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag

         // User-specified uniforms            
         uniform sampler2D _MainTex;     
         uniform float _RotationSpeed;   
         uniform float _ScaleX;
         uniform float _ScaleY;

         struct vertexInput {
            float4 vertex : POSITION;
            float4 tex : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            float sinX = sin ( _RotationSpeed * _Time );
            float cosX = cos ( _RotationSpeed * _Time );
            float sinY = sin ( _RotationSpeed * _Time );
            float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
           // input.tex.xy = mul ((float3x3)unity_ObjectToWorld, input.tex.xyz ).xy;
            input.tex.yz = mul ( input.tex.yz, rotationMatrix);

            vertexOutput output;

            output.pos = mul(UNITY_MATRIX_P, 
              mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
              + float4(input.vertex.x, input.vertex.y, 0.0, 0.0)
              * float4(_ScaleX, _ScaleY, 1.0, 1.0));

            output.tex = input.tex;

            return output;
         }
    
         float4 frag(vertexOutput input) : COLOR
         {
            return tex2D(_MainTex, float2(input.tex.xy));   
         }
 
         ENDCG
        }
    }
}
