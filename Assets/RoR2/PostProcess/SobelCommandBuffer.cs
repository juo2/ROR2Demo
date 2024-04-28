using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace RoR2.PostProcess
{
	// Token: 0x02000BBD RID: 3005
	[RequireComponent(typeof(Camera))]
	[ExecuteAlways]
	public class SobelCommandBuffer : MonoBehaviour
	{
		// Token: 0x06004473 RID: 17523 RVA: 0x0011D021 File Offset: 0x0011B221
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x0011D029 File Offset: 0x0011B229
		private void OnDestroy()
		{
			this.DestroyTemporaryAsset(this.sobelBufferMaterial);
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x0011D037 File Offset: 0x0011B237
		private void OnEnable()
		{
			if (!Application.isPlaying)
			{
				this.Initialize();
			}
			this.camera.AddCommandBuffer(this.cameraEvent, this.sobelCommandBuffer);
			this.sobelCommandBufferSubscribedEvent = this.cameraEvent;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x0011D069 File Offset: 0x0011B269
		private void OnDisable()
		{
			this.DestroyTemporaryAsset(this.sobelInfoTex);
			this.camera.RemoveCommandBuffer(this.sobelCommandBufferSubscribedEvent, this.sobelCommandBuffer);
			this.sobelCommandBuffer.Clear();
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x0011D099 File Offset: 0x0011B299
		private void OnPreRender()
		{
			this.Rebuild();
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x0011D0A4 File Offset: 0x0011B2A4
		private void Initialize()
		{
			if (this.sobelCommandBuffer != null)
			{
				return;
			}
			this.camera = base.GetComponent<Camera>();
			this.sobelBufferMaterial = new Material(LegacyShaderAPI.Find("Hopoo Games/Internal/SobelBuffer"));
			this.sobelCommandBuffer = new CommandBuffer();
			this.sobelCommandBuffer.name = "Sobel Command Buffer";
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x0011D0F8 File Offset: 0x0011B2F8
		private void Rebuild()
		{
			Vector2Int rhs = new Vector2Int(this.camera.pixelWidth, this.camera.pixelHeight);
			UnityEngine.Object x = this.sobelInfoTex;
			if (this.sobelInfoTex && new Vector2Int(this.sobelInfoTex.width, this.sobelInfoTex.height) != rhs)
			{
				this.DestroyTemporaryAsset(this.sobelInfoTex);
				this.sobelInfoTex = null;
			}
			if (!this.sobelInfoTex && rhs.x > 0 && rhs.y > 0)
			{
				this.sobelInfoTex = new RenderTexture(rhs.x, rhs.y, 0, GraphicsFormat.R8_UNorm, 0);
				this.sobelInfoTex.name = "Sobel Outline Information";
			}
			if (x != this.sobelInfoTex)
			{
				int nameID = Shader.PropertyToID("_SobelTex");
				RenderTargetIdentifier renderTargetIdentifier = new RenderTargetIdentifier(this.sobelInfoTex);
				RenderTargetIdentifier source = new RenderTargetIdentifier(BuiltinRenderTextureType.ResolvedDepth);
				RenderTargetIdentifier renderTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget);
				this.sobelCommandBuffer.Clear();
				this.sobelCommandBuffer.Blit(source, renderTargetIdentifier, this.sobelBufferMaterial);
				this.sobelCommandBuffer.SetGlobalTexture(nameID, renderTargetIdentifier);
				this.sobelCommandBuffer.SetRenderTarget(renderTarget);
			}
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0011D225 File Offset: 0x0011B425
		private void DestroyTemporaryAsset(UnityEngine.Object temporaryAsset)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(temporaryAsset);
				return;
			}
			UnityEngine.Object.DestroyImmediate(temporaryAsset);
		}

		// Token: 0x040042EB RID: 17131
		public CameraEvent cameraEvent = CameraEvent.BeforeLighting;

		// Token: 0x040042EC RID: 17132
		private Camera camera;

		// Token: 0x040042ED RID: 17133
		private RenderTexture sobelInfoTex;

		// Token: 0x040042EE RID: 17134
		private CommandBuffer sobelCommandBuffer;

		// Token: 0x040042EF RID: 17135
		private CameraEvent sobelCommandBufferSubscribedEvent;

		// Token: 0x040042F0 RID: 17136
		private Material sobelBufferMaterial;
	}
}
