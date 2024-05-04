// Code from here:
// https://forum.unity.com/threads/how-to-turn-off-back-face-culling.329744/
Shader "Custom/DoubleSidePlane"
{
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
            _MainTex ("Base (RGB)", 2D) = "white"{}
            _Glossiness ("Smoothness", Range(0,1)) = 0.0
            _Metallic ("Metallic", Range(0,1)) = 0.0
            }
                SubShader {
                Pass {
                Cull Off
                Material {
                Diffuse [_Color]
                Ambient [_Color]
            }
            Lighting On
            SetTexture [_MainTex] {
            Combine texture * primary DOUBLE, texture * primary
            }
        }
    }
}
