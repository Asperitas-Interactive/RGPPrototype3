// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace AmplifyShaderEditor
{
	[Serializable]
	public class RenderPlatformInfo
	{
		public string Label;
		public RenderPlatforms Value;
	}

	[Serializable]
	public class RenderingPlatformOpHelper
	{
		private const string RenderingPlatformsStr = " Rendering Platforms";
		private readonly RenderPlatformInfo[] RenderingPlatformsInfo =
		{
			new RenderPlatformInfo(){Label = " Direct3D 9", Value = RenderPlatforms.d3d9},
			new RenderPlatformInfo(){Label = " Direct3D 11 9.x", Value = RenderPlatforms.d3d11_9x},
			new RenderPlatformInfo(){Label = " Direct3D 11/12", Value = RenderPlatforms.d3d11},
			new RenderPlatformInfo(){Label = " OpenGL 3.x/4.x", Value = RenderPlatforms.glcore},
			new RenderPlatformInfo(){Label = " OpenGL ES 2.0", Value = RenderPlatforms.gles},
			new RenderPlatformInfo(){Label = " OpenGL ES 3.x", Value = RenderPlatforms.gles3},
			new RenderPlatformInfo(){Label = " iOS/Mac Metal", Value = RenderPlatforms.metal},
			new RenderPlatformInfo(){Label = " Vulkan", Value = RenderPlatforms.vulkan},
			new RenderPlatformInfo(){Label = " Xbox 360", Value = RenderPlatforms.xbox360},
			new RenderPlatformInfo(){Label = " Xbox One", Value = RenderPlatforms.xboxone},
			new RenderPlatformInfo(){Label = " Xbox Series X", Value = RenderPlatforms.xboxseries},
			new RenderPlatformInfo(){Label = " PlayStation 4 (Legacy)", Value = RenderPlatforms.ps4},
			new RenderPlatformInfo(){Label = " PlayStation", Value = RenderPlatforms.playstation},
			new RenderPlatformInfo(){Label = " PlayStation Vita", Value = RenderPlatforms.psp2},
			new RenderPlatformInfo(){Label = " Nintendo 3DS", Value = RenderPlatforms.n3ds},
			new RenderPlatformInfo(){Label = " Nintendo Wii U", Value = RenderPlatforms.wiiu}
		};

		// Values from this dictionary must be the indices corresponding from the list above
		private readonly Dictionary<RenderPlatforms, int> PlatformToIndex = new Dictionary<RenderPlatforms, int>()
		{
			{RenderPlatforms.d3d9,			0},
			{RenderPlatforms.d3d11_9x,		1},
			{RenderPlatforms.d3d11,			2},
			{RenderPlatforms.glcore,		3},
			{RenderPlatforms.gles,			4},
			{RenderPlatforms.gles3,			5},
			{RenderPlatforms.metal,			6},
			{RenderPlatforms.vulkan,		7},
			{RenderPlatforms.xbox360,		8},
			{RenderPlatforms.xboxone,		9},
			{RenderPlatforms.xboxseries,	10},
			{RenderPlatforms.ps4,			11},
			{RenderPlatforms.playstation,	12},
			{RenderPlatforms.psp2,			13},
			{RenderPlatforms.n3ds,			14},
			{RenderPlatforms.wiiu,			15}
		};

		
		private readonly List<RenderPlatforms> LegacyIndexToPlatform = new List<RenderPlatforms>()
		{
			RenderPlatforms.d3d9,
			RenderPlatforms.d3d11,
			RenderPlatforms.glcore,
			RenderPlatforms.gles,
			RenderPlatforms.gles3,
			RenderPlatforms.metal,
			RenderPlatforms.d3d11_9x,
			RenderPlatforms.xbox360,
			RenderPlatforms.xboxone,
			RenderPlatforms.ps4,
			RenderPlatforms.psp2,
			RenderPlatforms.n3ds,
			RenderPlatforms.wiiu
		};

		[SerializeField]
		private bool[] m_renderingPlatformValues;

		public RenderingPlatformOpHelper()
		{
			m_renderingPlatformValues = new bool[ RenderingPlatformsInfo.Length ];
			for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
			{
				m_renderingPlatformValues[ i ] = true;
			}
		}


		public void Draw( ParentNode owner )
		{
			bool value = owner.ContainerGraph.ParentWindow.InnerWindowVariables.ExpandedRenderingPlatforms;
			NodeUtils.DrawPropertyGroup( ref value, RenderingPlatformsStr, () =>
			{
				for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
				{
					m_renderingPlatformValues[ i ] = owner.EditorGUILayoutToggleLeft( RenderingPlatformsInfo[ i ].Label, m_renderingPlatformValues[ i ] );
				}
			} );
			owner.ContainerGraph.ParentWindow.InnerWindowVariables.ExpandedRenderingPlatforms = value;
		}

		public void SetRenderingPlatforms( ref string ShaderBody )
		{
			int checkedPlatforms = 0;
			int uncheckedPlatforms = 0;

			for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
			{
				if( m_renderingPlatformValues[ i ] )
				{
					checkedPlatforms += 1;
				}
				else
				{
					uncheckedPlatforms += 1;
				}
			}

			if( checkedPlatforms > 0 && checkedPlatforms < m_renderingPlatformValues.Length )
			{
				string result = string.Empty;
				if( checkedPlatforms < uncheckedPlatforms )
				{
					result = "only_renderers ";
					for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
					{
						if( m_renderingPlatformValues[ i ] )
						{
							result += (RenderPlatforms)RenderingPlatformsInfo[i].Value + " ";
						}
					}
				}
				else
				{
					result = "exclude_renderers ";
					for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
					{
						if( !m_renderingPlatformValues[ i ] )
						{
							result += (RenderPlatforms)RenderingPlatformsInfo[ i ].Value + " ";
						}
					}
				}
				MasterNode.AddShaderPragma( ref ShaderBody, result );
			}
		}

		public void ReadFromString( ref uint index, ref string[] nodeParams )
		{
			if( UIUtils.CurrentShaderVersion() < 17006 )
			{
				for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
				{
					m_renderingPlatformValues[ i ] = false;
				}

				int count = LegacyIndexToPlatform.Count;
				int activeCount = 0;
				for( int i = 0; i < count; i++ )
				{
					RenderPlatforms platform = LegacyIndexToPlatform[ i ];
					int newIndex = PlatformToIndex[ platform ];
					bool value = Convert.ToBoolean( nodeParams[ index++ ] );
					if( value )
					{
						m_renderingPlatformValues[ newIndex ] = true;
						activeCount += 1;
					}
					else
					{
						m_renderingPlatformValues[ newIndex ] = false;
					}
				}

				if( activeCount == count )
				{
					m_renderingPlatformValues[ PlatformToIndex[ RenderPlatforms.vulkan ] ] = true;
				}
			}
			else
			{
				int count = Convert.ToInt32( nodeParams[ index++ ] );
				if( count > 0 )
				{
					RenderPlatforms firstPlatform = (RenderPlatforms)Enum.Parse( typeof(RenderPlatforms), nodeParams[ index++ ] );
					if( firstPlatform == RenderPlatforms.all )
					{
						for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
						{
							m_renderingPlatformValues[ i ] = true;
						}
					}
					else
					{
						for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
						{
							m_renderingPlatformValues[ i ] = false;
						}

						m_renderingPlatformValues[ PlatformToIndex[ firstPlatform ]] = true;
						for( int i = 1; i < count; i++ )
						{
							RenderPlatforms currPlatform = (RenderPlatforms)Enum.Parse( typeof( RenderPlatforms ), nodeParams[ index++ ] );
							m_renderingPlatformValues[ PlatformToIndex[ currPlatform ] ] = true;
						}
					}
				}
			}
		}

		public void WriteToString( ref string nodeInfo )
		{
			int active = 0;
			for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
			{
				if( m_renderingPlatformValues[ i ] )
					active += 1;
			}
			IOUtils.AddFieldValueToString( ref nodeInfo, active );
			if( active == m_renderingPlatformValues.Length )
			{
				IOUtils.AddFieldValueToString( ref nodeInfo, RenderPlatforms.all );
			}
			else
			{
				for( int i = 0; i < m_renderingPlatformValues.Length; i++ )
				{
					if( m_renderingPlatformValues[ i ] )
					{
						IOUtils.AddFieldValueToString( ref nodeInfo, RenderingPlatformsInfo[i].Value );
					}
				}
			}

		}
	}
}
