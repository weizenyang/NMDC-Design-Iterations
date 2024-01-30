Shader "Custom/ClippingVolume"
{
    Properties
    {
        _ClipBoxMin ("Clip Box Min", Vector) = (-1, -1, -1, 1)
        _ClipBoxMax ("Clip Box Max", Vector) = (1, 1, 1, 1)
    }

    struct appdata
    {
        float4 vertex : POSITION;
        // Additional vertex attributes (normals, UVs, etc.) go here.
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float3 worldPos : TEXCOORD0; // Use TEXCOORD0 to pass world position.
    };

    v2f vert(appdata v)
    {
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex); // Transform vertex position to clip space.
        o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // Transform vertex position to world space.
        return o;
    }

    fixed4 frag(v2f i) : SV_TARGET
    {
        // Clipping logic
        if (i.worldPos.x < _ClipBoxMin.x || i.worldPos.x > _ClipBoxMax.x ||
            i.worldPos.y < _ClipBoxMin.y || i.worldPos.y > _ClipBoxMax.y ||
            i.worldPos.z < _ClipBoxMin.z || i.worldPos.z > _ClipBoxMax.z)
        {
            discard; // Discard fragment outside the box.
        }

        // Render remaining fragments.
        return fixed4(1, 1, 1, 1); // Placeholder color. Adjust as needed.
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _ClipBoxMin;
            float4 _ClipBoxMax;

            // vert and frag functions defined earlier.

            ENDCG
        }
    }
}