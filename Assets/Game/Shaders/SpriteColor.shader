Shader "Custom/SpriteColor" {
	Properties{
		_Key("Key Color", Color) = (1,1,1,1)		
		_MainTex("Texture", 2D) = "white" {}
	} SubShader{
		Tags {
			"Queue" = "Transparent"
			//"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		Cull Off
		//Lighting Off
		LOD 100
		ZWrite Off
		//ZTest Always
		//Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}

			sampler2D _MainTex;
			float4 _Key;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);

                if(all(col==_Key)){
                    col = i.color;
                }
                
				return col;
			}
		ENDCG
		}
	}
}