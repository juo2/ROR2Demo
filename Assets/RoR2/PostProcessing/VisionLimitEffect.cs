using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoR2.PostProcessing
{
	// Token: 0x02000BC0 RID: 3008
	[RequireComponent(typeof(Camera))]
	public class VisionLimitEffect : MonoBehaviour
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x0011D424 File Offset: 0x0011B624
		private void Awake()
		{
			this.camera = base.GetComponent<Camera>();
			this.commandBuffer = new CommandBuffer();
			this.commandBuffer.name = "VisionLimitEffect";
			Shader shader = LegacyShaderAPI.Find("Hopoo Games/Internal/VisionLimit");
			this.material = new Material(shader);
			this.material.name = "VisionLimitEffectMaterial";
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x0011D47F File Offset: 0x0011B67F
		private void OnDestroy()
		{
			this.DestroyTemporaryAsset(this.material);
			this.commandBuffer = null;
			this.camera = null;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x0011D49B File Offset: 0x0011B69B
		private void OnEnable()
		{
			this.camera.AddCommandBuffer(CameraEvent.AfterImageEffectsOpaque, this.commandBuffer);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x0011D4B0 File Offset: 0x0011B6B0
		private void OnDisable()
		{
			this.camera.RemoveCommandBuffer(CameraEvent.AfterImageEffectsOpaque, this.commandBuffer);
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0011D4C5 File Offset: 0x0011B6C5
		private void OnPreCull()
		{
			this.UpdateCommandBuffer();
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0011D4D0 File Offset: 0x0011B6D0
		private void LateUpdate()
		{
			Transform transform = this.cameraRigController.target ? this.cameraRigController.target.transform : null;
			CharacterBody targetBody = this.cameraRigController.targetBody;
			if (transform)
			{
				this.lastKnownTargetPosition = transform.position;
			}
			this.desiredVisionDistance = (targetBody ? targetBody.visionDistance : float.PositiveInfinity);
			float target = 0f;
			float target2 = 4000f;
			if (this.desiredVisionDistance != float.PositiveInfinity)
			{
				target = 1f;
				target2 = this.desiredVisionDistance;
			}
			this.currentAlpha = Mathf.SmoothDamp(this.currentAlpha, target, ref this.alphaVelocity, 0.2f, float.PositiveInfinity, Time.deltaTime);
			this.currentVisionDistance = Mathf.SmoothDamp(this.currentVisionDistance, target2, ref this.currentVisionDistanceVelocity, 0.2f, float.PositiveInfinity, Time.deltaTime);
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0011D5B4 File Offset: 0x0011B7B4
		private void UpdateCommandBuffer()
		{
			this.commandBuffer.Clear();
			if (this.currentAlpha <= 0f)
			{
				return;
			}
			float num = Mathf.Max(0f, this.currentVisionDistance * 0.5f);
			float value = Mathf.Max(num + 0.01f, this.currentVisionDistance);
			this.material.SetVector(VisionLimitEffect.ShaderParamsIDs.origin, this.lastKnownTargetPosition);
			this.material.SetFloat(VisionLimitEffect.ShaderParamsIDs.rangeStart, num);
			this.material.SetFloat(VisionLimitEffect.ShaderParamsIDs.rangeEnd, value);
			this.material.SetColor(VisionLimitEffect.ShaderParamsIDs.color, new Color(0f, 0f, 0f, this.currentAlpha));
			this.commandBuffer.Blit(null, BuiltinRenderTextureType.CurrentActive, this.material);
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x0011D225 File Offset: 0x0011B425
		private void DestroyTemporaryAsset(UnityEngine.Object temporaryAsset)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(temporaryAsset);
				return;
			}
			UnityEngine.Object.DestroyImmediate(temporaryAsset);
		}

		// Token: 0x040042FE RID: 17150
		public CameraRigController cameraRigController;

		// Token: 0x040042FF RID: 17151
		private Camera camera;

		// Token: 0x04004300 RID: 17152
		private float desiredVisionDistance = float.PositiveInfinity;

		// Token: 0x04004301 RID: 17153
		private CommandBuffer commandBuffer;

		// Token: 0x04004302 RID: 17154
		private Material material;

		// Token: 0x04004303 RID: 17155
		private Vector3 lastKnownTargetPosition;

		// Token: 0x04004304 RID: 17156
		private float currentAlpha;

		// Token: 0x04004305 RID: 17157
		private float alphaVelocity;

		// Token: 0x04004306 RID: 17158
		private float currentVisionDistance;

		// Token: 0x04004307 RID: 17159
		private float currentVisionDistanceVelocity;

		// Token: 0x02000BC1 RID: 3009
		private static class ShaderParamsIDs
		{
			// Token: 0x04004308 RID: 17160
			public static int origin = Shader.PropertyToID("_Origin");

			// Token: 0x04004309 RID: 17161
			public static int rangeStart = Shader.PropertyToID("_RangeStart");

			// Token: 0x0400430A RID: 17162
			public static int rangeEnd = Shader.PropertyToID("_RangeEnd");

			// Token: 0x0400430B RID: 17163
			public static int color = Shader.PropertyToID("_Color");
		}
	}
}
