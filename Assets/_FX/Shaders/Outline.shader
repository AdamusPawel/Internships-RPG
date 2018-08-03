Shader "Custom/Outline"
{
	Properties // variables
	{
		_MainTex("Main Texture (RGB)", 2D) = "white" {} // allows for a texture property
		_Color("Color", Color) = (1,1,1,1) // allows for a color property

		_OutlineTex("Outline Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
		_OutlineWidth("Outline Width", Range(1.0, 10.0)) = 1.1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		
		Pass
		{
			Name "OUTLINE"

			ZWrite Off

			CGPROGRAM // allows talk between two languages: ShaderLab and nVidia C for graphics

			/* FUNCTION DEFINES - names for the vertes and fragment functions */

			#pragma vertex vert // define for the building function
			#pragma fragment frag // define for colouring function

			/* INCLUDES */

			#include "UnityCG.cginc" // library of built in shader functions

			/* STRUCTURES - can get data like: vertices, normal, colorm uv etc. */

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv: TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv: TEXCOORD0;
			};

			/* IMPORTS - reimport property from ShaderLab to nVidia CG  */

			float _OutlineWidth;
			float4 _OutlineColor;
			sampler2D _OutlineTex;

			/* VERTEX FUNCTIONS - builds the objects */

			v2f vert(appdata IN)
			{
				IN.vertex.xyz *= _OutlineWidth;
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			/* FRAGMENT FUNCTION - color it in*/

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_OutlineTex, IN.uv);
				return texColor * _OutlineColor;
			}

		ENDCG
	}

		Pass
		{
			Name "OBJECT"
			CGPROGRAM // allows talk between two languages: ShaderLab and nVidia C for graphics

		    /* FUNCTION DEFINES - names for the vertes and fragment functions */

			#pragma vertex vert // define for the building function
			#pragma fragment frag // define for colouring function

			/* INCLUDES */

			#include "UnityCG.cginc" // library of built in shader functions

			/* STRUCTURES - can get data like: vertices, normal, colorm uv etc. */

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv: TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv: TEXCOORD0;
			};

			/* IMPORTS - reimport property from ShaderLab to nVidia CG  */

			float4 _Color;
			sampler2D _MainTex;

			/* VERTEX FUNCTIONS - builds the objects */

			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			/* FRAGMENT FUNCTION - color it in*/

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_MainTex, IN.uv);
				return texColor * _Color;
			}

		ENDCG
		}
	}
}