Shader "Unlit/UnlitColor"
{
	Properties {
    _Color ("Color", Color) = (1,1,1,1)
}
 
Category {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
 
    SubShader {Pass {
        GLSLPROGRAM
        varying mediump vec2 uv;
       
        #ifdef VERTEX
        void main() {
            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
        }
        #endif
       
        #ifdef FRAGMENT
        uniform lowp vec4 _Color;
        void main() {
            gl_FragColor = _Color;
        }
        #endif     
        ENDGLSL
    }}
}

}
