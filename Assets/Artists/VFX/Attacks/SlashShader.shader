// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SlashShader"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Main("Main", 2D) = "white" {}
		_Opacity("Opacity", Float) = 20
		_NoiseSpeed("Noise Speed", Vector) = (0,0,0,0)
		_Emission("Emission", 2D) = "white" {}
		_EmissionIntensity("EmissionIntensity", Float) = 2
		[ASEEnd]_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord1 : TEXCOORD1;
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _Color0;
				uniform sampler2D _Emission;
				uniform float4 _Emission_ST;
				uniform float _EmissionIntensity;
				uniform sampler2D _Main;
				uniform float4 _Main_ST;
				uniform float _Opacity;
				uniform float4 _NoiseSpeed;
						float2 voronoihash18( float2 p )
						{
							
							p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
							return frac( sin( p ) *43758.5453);
						}
				
						float voronoi18( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
						{
							float2 n = floor( v );
							float2 f = frac( v );
							float F1 = 8.0;
							float F2 = 8.0; float2 mg = 0;
							for ( int j = -1; j <= 1; j++ )
							{
								for ( int i = -1; i <= 1; i++ )
							 	{
							 		float2 g = float2( i, j );
							 		float2 o = voronoihash18( n + g );
									o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
									float d = 0.5 * dot( r, r );
							 		if( d<F1 ) {
							 			F2 = F1;
							 			F1 = d; mg = g; mr = r; id = o;
							 		} else if( d<F2 ) {
							 			F2 = d;
							
							 		}
							 	}
							}
							return F1;
						}
				


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord1;

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 uv_Emission = i.texcoord.xy * _Emission_ST.xy + _Emission_ST.zw;
					float4 _Vector1 = float4(-0.5,1,-10,1);
					float4 temp_cast_0 = (_Vector1.x).xxxx;
					float4 temp_cast_1 = (_Vector1.y).xxxx;
					float4 temp_cast_2 = (_Vector1.z).xxxx;
					float4 temp_cast_3 = (_Vector1.w).xxxx;
					float4 clampResult54 = clamp( (temp_cast_2 + (tex2D( _Emission, uv_Emission ) - temp_cast_0) * (temp_cast_3 - temp_cast_2) / (temp_cast_1 - temp_cast_0)) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
					float2 uv_Main = i.texcoord.xy * _Main_ST.xy + _Main_ST.zw;
					float4 clampResult4 = clamp( ( tex2D( _Main, uv_Main ) * _Opacity ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
					float time18 = 5.0;
					float2 voronoiSmoothId0 = 0;
					float2 appendResult35 = (float2(_NoiseSpeed.z , _NoiseSpeed.w));
					float4 texCoord28 = i.texcoord;
					texCoord28.xy = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float2 panner37 = ( 1.0 * _Time.y * appendResult35 + texCoord28.xy);
					float2 break46 = panner37;
					float4 texCoord51 = i.ase_texcoord3;
					texCoord51.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float2 appendResult47 = (float2(break46.x , ( texCoord51.z + break46.y )));
					float2 coords18 = appendResult47 * 8.0;
					float2 id18 = 0;
					float2 uv18 = 0;
					float voroi18 = voronoi18( coords18, time18, id18, uv18, 0, voronoiSmoothId0 );
					float temp_output_23_0 = ( voroi18 * 2.0 );
					float T30 = texCoord28.w;
					float W29 = texCoord28.z;
					float3 _Vector0 = float3(0.3,0,1);
					float ifLocalVar11 = 0;
					if( ( temp_output_23_0 * T30 ) >= W29 )
					ifLocalVar11 = _Vector0.y;
					else
					ifLocalVar11 = _Vector0.z;
					float4 appendResult5 = (float4(( ( _Color0 * i.color ) + ( clampResult54 * _EmissionIntensity * i.color ) ).rgb , ( i.color.a * clampResult4 * ifLocalVar11 ).r));
					

					fixed4 col = appendResult5;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	
	
	
}
/*ASEBEGIN
Version=18908
1109;73;533;665;1292.947;1181.186;2.547693;False;False
Node;AmplifyShaderEditor.Vector4Node;33;-2446.539,-104.409;Inherit;False;Property;_NoiseSpeed;Noise Speed;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-2130.83,-88.3565;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;35;-2090.828,108.5039;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;37;-1890.105,126.7149;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-1956.333,377.3895;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;46;-1662.276,291.3846;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-1543.22,399.8895;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1549.736,793.5225;Inherit;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;0;False;0;False;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;47;-1406.085,295.9055;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1549.736,729.5226;Inherit;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;0;False;0;False;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;53;-908.0292,-597.1416;Inherit;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;0;False;0;False;-0.5,1,-10,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;38;-1002.583,-793.3212;Inherit;True;Property;_Emission;Emission;3;0;Create;True;0;0;0;False;0;False;-1;01c44783f56b46840ad6307e73ca40c3;01c44783f56b46840ad6307e73ca40c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-1817.992,7.847648;Inherit;False;T;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1293.736,937.5223;Inherit;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;18;-1293.736,649.5228;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SamplerNode;1;-521.6498,-44.5;Inherit;True;Property;_Main;Main;0;0;Create;True;0;0;0;False;0;False;-1;22cf2a05ff7a35c4eaf7d73536b505c2;22cf2a05ff7a35c4eaf7d73536b505c2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-1824.392,-92.55872;Inherit;False;W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-349.5,188.5;Inherit;False;Property;_Opacity;Opacity;1;0;Create;True;0;0;0;False;0;False;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1101.735,745.5225;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;52;-632.8645,-699.9367;Inherit;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;-829.7331,1065.523;Inherit;False;30;T;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;57;-143.0367,-917.6281;Inherit;False;Property;_Color0;Color 0;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;56;-423.8204,-454.686;Inherit;False;Property;_EmissionIntensity;EmissionIntensity;4;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;7;-264.0801,-244.11;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-493.7333,793.5225;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-184.5,79.5;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-573.7333,937.5223;Inherit;False;29;W;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;12;-557.7333,1033.522;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;0.3,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClampOpNode;54;-357.8098,-673.7943;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;4;-47.5,79.5;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;11;-253.7334,777.5225;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-101.7234,-562.498;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;139.9727,-649.2104;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;180.8425,-1.568478;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;310.5159,-524.4656;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;402.1075,-68.5169;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1597.736,585.5229;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;34;-2120.244,-224.7924;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;22;-870.6167,732.5475;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;557.6407,-67.16409;Float;False;True;-1;2;;0;8;SlashShader;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;False;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;35;0;33;3
WireConnection;35;1;33;4
WireConnection;37;0;28;0
WireConnection;37;2;35;0
WireConnection;46;0;37;0
WireConnection;48;0;51;3
WireConnection;48;1;46;1
WireConnection;47;0;46;0
WireConnection;47;1;48;0
WireConnection;30;0;28;4
WireConnection;18;0;47;0
WireConnection;18;1;20;0
WireConnection;18;2;21;0
WireConnection;29;0;28;3
WireConnection;23;0;18;0
WireConnection;23;1;24;0
WireConnection;52;0;38;0
WireConnection;52;1;53;1
WireConnection;52;2;53;2
WireConnection;52;3;53;3
WireConnection;52;4;53;4
WireConnection;10;0;23;0
WireConnection;10;1;32;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;54;0;52;0
WireConnection;4;0;2;0
WireConnection;11;0;10;0
WireConnection;11;1;31;0
WireConnection;11;2;12;2
WireConnection;11;3;12;2
WireConnection;11;4;12;3
WireConnection;55;0;54;0
WireConnection;55;1;56;0
WireConnection;55;2;7;0
WireConnection;59;0;57;0
WireConnection;59;1;7;0
WireConnection;6;0;7;4
WireConnection;6;1;4;0
WireConnection;6;2;11;0
WireConnection;58;0;59;0
WireConnection;58;1;55;0
WireConnection;5;0;58;0
WireConnection;5;3;6;0
WireConnection;34;0;33;1
WireConnection;34;1;33;2
WireConnection;22;0;23;0
WireConnection;0;0;5;0
ASEEND*/
//CHKSM=00CE05082BE8EA472038006088D01C261696E8C8