Shader "Custom/ApplyTexture"
{
	Properties//Variables
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {}//Allows for a texture property.
		_Color("Color", Color) = (1,1,1,1)//Allows for a color property.
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM//Allows talk between two languages: shader lab and nvidia C for graphics.

			//\===========================================================================================
			//\ Function Defines - defines the name for the vertex and fragment functions
			//\===========================================================================================

			#pragma vertex vert//Define for the building function.

			#pragma fragment frag//Define for coloring function.

			//\===========================================================================================
			//\ Includes
			//\===========================================================================================

			#include "UnityCG.cginc"//Built in shader functions.

			//\===========================================================================================
			//\ Structures - Can get data like - vertices's, normal, color, uv.
			//\===========================================================================================
			
			struct appdata//How the vertex function receives info.
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f//How the fragment function receives info.
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//\===========================================================================================
			//\ Imports - Re-import property from shader lab to nvidia cg
			//\===========================================================================================

			float4 _Color;
			sampler2D _MainTex;

			//\===========================================================================================
			//\ Vertex Function - Builds the object
			//\===========================================================================================
			
			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);//Puts the object into the camera view.
				OUT.uv = IN.uv;

				return OUT;
			}

			//\===========================================================================================
			//\ Fragment Function - Color it in
			//\===========================================================================================

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_MainTex, IN.uv);//Wraps the texture around the uv's.
				return texColor * _Color;//Tints the texture.
			}

			ENDCG
		}


	}
}
