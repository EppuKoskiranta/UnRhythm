Shader "Custom/wireframe_no_diag"{
    Properties
    {
        [PowerSlider(3.0)]
        _WireframeVal("Wireframe width", Range(0., 0.5)) = 0.05
        [Toggle] _RemoveDiag("Remove diagonals?", Float) = 0.
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull Mode", Int) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

        ZTest[_ZTest]

        Pass
        {
            Cull[_CullMode]
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom

        // Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
        #pragma shader_feature __ _REMOVEDIAG_ON

        #include "UnityCG.cginc"

        struct v2g {
            float4 worldPos : SV_POSITION;
            float4 color : COLOR;
        };

        struct g2f {
            float4 pos : SV_POSITION;
            float3 bary : TEXCOORD0;
            float4 color : COLOR;
        };

        v2g vert(appdata_full v) {
            v2g o;
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            o.color = v.color;
            return o;
        }

        [maxvertexcount(3)]
        void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) {
            float3 param = float3(0., 0., 0.);

            #if _REMOVEDIAG_ON
            float EdgeA = length(IN[0].worldPos - IN[1].worldPos);
            float EdgeB = length(IN[1].worldPos - IN[2].worldPos);
            float EdgeC = length(IN[2].worldPos - IN[0].worldPos);

            if (EdgeA > EdgeB && EdgeA > EdgeC)
                param.y = 1.;
            else if (EdgeB > EdgeC && EdgeB > EdgeA)
                param.x = 1.;
            else
                param.z = 1.;
            #endif

            g2f o;
            o.pos = mul(UNITY_MATRIX_VP, IN[0].worldPos);
            o.bary = float3(1., 0., 0.) + param;
            o.color = IN[0].color;
            triStream.Append(o);
            o.pos = mul(UNITY_MATRIX_VP, IN[1].worldPos);
            o.bary = float3(0., 0., 1.) + param;
            o.color = IN[1].color;
            triStream.Append(o);
            o.pos = mul(UNITY_MATRIX_VP, IN[2].worldPos);
            o.bary = float3(0., 1., 0.) + param;
            o.color = IN[2].color;
            triStream.Append(o);
        }

        float _WireframeVal;

        fixed4 frag(g2f i) : SV_Target {
        if (!any(bool3(i.bary.x < _WireframeVal, i.bary.y < _WireframeVal, i.bary.z < _WireframeVal)) || i.pos.y < 1.0f)
                discard;

            return i.color;
        }

        ENDCG
    }

    Pass
    {
        Cull [_CullMode]
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma geometry geom

        // Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
        #pragma shader_feature __ _REMOVEDIAG_ON

        #include "UnityCG.cginc"
           
        struct v2g {
            float4 worldPos : SV_POSITION;
            float4 color : COLOR;
        };

        struct g2f {
            float4 pos : SV_POSITION;
            float3 bary : TEXCOORD0;
            float4 color : COLOR;
        };

        v2g vert(appdata_full v) {
            v2g o;
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            o.color = v.color;
            return o;
        }

        [maxvertexcount(3)]
        void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) {
            float3 param = float3(0., 0., 0.);

            #if _REMOVEDIAG_ON
            float EdgeA = length(IN[0].worldPos - IN[1].worldPos);
            float EdgeB = length(IN[1].worldPos - IN[2].worldPos);
            float EdgeC = length(IN[2].worldPos - IN[0].worldPos);

            if (EdgeA > EdgeB && EdgeA > EdgeC)
                param.y = 1.;
            else if (EdgeB > EdgeC && EdgeB > EdgeA)
                param.x = 1.;
            else
                param.z = 1.;
            #endif

            g2f o;
            o.pos = mul(UNITY_MATRIX_VP, IN[0].worldPos);
            o.bary = float3(1., 0., 0.) + param;
            o.color = IN[0].color;
            triStream.Append(o);
            o.pos = mul(UNITY_MATRIX_VP, IN[1].worldPos);
            o.bary = float3(0., 0., 1.) + param;
            o.color = IN[1].color;
            triStream.Append(o);
            o.pos = mul(UNITY_MATRIX_VP, IN[2].worldPos);
            o.bary = float3(0., 1., 0.) + param;
            o.color = IN[2].color;
            triStream.Append(o);
        }

        float _WireframeVal;

        fixed4 frag(g2f i) : SV_Target {
        if (!any(bool3(i.bary.x <= _WireframeVal, i.bary.y <= _WireframeVal, i.bary.z <= _WireframeVal)))
                discard;

            return i.color;
        }

        ENDCG
        }
    }
}