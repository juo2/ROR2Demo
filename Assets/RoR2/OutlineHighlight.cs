using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoR2
{
	// Token: 0x0200081C RID: 2076
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(SceneCamera))]
	public class OutlineHighlight : MonoBehaviour
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000BFF42 File Offset: 0x000BE142
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x000BFF4A File Offset: 0x000BE14A
		public SceneCamera sceneCamera { get; private set; }

		// Token: 0x06002D01 RID: 11521 RVA: 0x000BFF54 File Offset: 0x000BE154
		private void Awake()
		{
			this.sceneCamera = base.GetComponent<SceneCamera>();
			this.CreateBuffers();
			this.CreateMaterials();
			this.m_RTWidth = (int)((float)Screen.width / (float)this.m_resolution);
			this.m_RTHeight = (int)((float)Screen.height / (float)this.m_resolution);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000BFFA3 File Offset: 0x000BE1A3
		private void CreateBuffers()
		{
			this.commandBuffer = new CommandBuffer();
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000BFFB0 File Offset: 0x000BE1B0
		private void ClearCommandBuffers()
		{
			this.commandBuffer.Clear();
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000BFFBD File Offset: 0x000BE1BD
		private void CreateMaterials()
		{
			this.highlightMaterial = new Material(LegacyShaderAPI.Find("Hopoo Games/Internal/Outline Highlight"));
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000BFFD4 File Offset: 0x000BE1D4
		private void RenderHighlights(RenderTexture rt)
		{
			RenderTargetIdentifier renderTarget = new RenderTargetIdentifier(rt);
			this.commandBuffer.SetRenderTarget(renderTarget);
			foreach (Highlight highlight in Highlight.readonlyHighlightList)
			{
				if (highlight.isOn && highlight.targetRenderer)
				{
					this.highlightQueue.Enqueue(new OutlineHighlight.HighlightInfo
					{
						color = highlight.GetColor() * highlight.strength,
						renderer = highlight.targetRenderer
					});
				}
			}
			Action<OutlineHighlight> action = OutlineHighlight.onPreRenderOutlineHighlight;
			if (action != null)
			{
				action(this);
			}
			while (this.highlightQueue.Count > 0)
			{
				OutlineHighlight.HighlightInfo highlightInfo = this.highlightQueue.Dequeue();
				if (highlightInfo.renderer)
				{
					this.highlightMaterial.SetColor("_Color", highlightInfo.color);
					this.commandBuffer.DrawRenderer(highlightInfo.renderer, this.highlightMaterial, 0, 0);
					RenderTexture.active = rt;
					Graphics.ExecuteCommandBuffer(this.commandBuffer);
					RenderTexture.active = null;
					this.ClearCommandBuffers();
				}
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000C0108 File Offset: 0x000BE308
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			int width = (int)((float)destination.width / (float)this.m_resolution);
			int height = (int)((float)destination.height / (float)this.m_resolution);
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
			RenderTexture.active = temporary;
			GL.Clear(true, true, Color.clear);
			RenderTexture.active = null;
			this.ClearCommandBuffers();
			this.RenderHighlights(temporary);
			this.highlightMaterial.SetTexture("_OutlineMap", temporary);
			this.highlightMaterial.SetColor("_Color", Color.white);
			Graphics.Blit(source, destination, this.highlightMaterial, 1);
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x04002F28 RID: 12072
		public OutlineHighlight.RTResolution m_resolution = OutlineHighlight.RTResolution.Full;

		// Token: 0x04002F29 RID: 12073
		public readonly Queue<OutlineHighlight.HighlightInfo> highlightQueue = new Queue<OutlineHighlight.HighlightInfo>();

		// Token: 0x04002F2B RID: 12075
		private Material highlightMaterial;

		// Token: 0x04002F2C RID: 12076
		private CommandBuffer commandBuffer;

		// Token: 0x04002F2D RID: 12077
		private int m_RTWidth = 512;

		// Token: 0x04002F2E RID: 12078
		private int m_RTHeight = 512;

		// Token: 0x04002F2F RID: 12079
		public static Action<OutlineHighlight> onPreRenderOutlineHighlight;

		// Token: 0x0200081D RID: 2077
		private enum Passes
		{
			// Token: 0x04002F31 RID: 12081
			FillPass,
			// Token: 0x04002F32 RID: 12082
			Blit
		}

		// Token: 0x0200081E RID: 2078
		public enum RTResolution
		{
			// Token: 0x04002F34 RID: 12084
			Quarter = 4,
			// Token: 0x04002F35 RID: 12085
			Half = 2,
			// Token: 0x04002F36 RID: 12086
			Full = 1
		}

		// Token: 0x0200081F RID: 2079
		public struct HighlightInfo
		{
			// Token: 0x04002F37 RID: 12087
			public Color color;

			// Token: 0x04002F38 RID: 12088
			public Renderer renderer;
		}
	}
}
