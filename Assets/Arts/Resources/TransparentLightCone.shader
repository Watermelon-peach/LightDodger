
Shader "Unlit/TransparentLightCone"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 0.95, 0.6, 0.3)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Color[_Color]
            SetTexture[_MainTex] { combine texture * primary }
        }
    }
}
