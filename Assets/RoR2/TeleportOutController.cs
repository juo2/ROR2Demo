using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B8 RID: 2232
	public class TeleportOutController : NetworkBehaviour
	{
		// Token: 0x060031BC RID: 12732 RVA: 0x000D2C98 File Offset: 0x000D0E98
		public static void AddTPOutEffect(CharacterModel characterModel, float beginAlpha, float endAlpha, float duration)
		{
			if (characterModel)
			{
				TemporaryOverlay temporaryOverlay = characterModel.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = duration;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, beginAlpha, 1f, endAlpha);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matTPInOut");
				temporaryOverlay.AddToCharacerModel(characterModel);
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000D2CFC File Offset: 0x000D0EFC
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (this.target)
			{
				ModelLocator component = this.target.GetComponent<ModelLocator>();
				if (component)
				{
					Transform modelTransform = component.modelTransform;
					if (modelTransform)
					{
						CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
						if (component2)
						{
							TeleportOutController.AddTPOutEffect(component2, 0f, 1f, 2f);
							if (component2.baseRendererInfos.Length != 0)
							{
								Renderer renderer = component2.baseRendererInfos[component2.baseRendererInfos.Length - 1].renderer;
								if (renderer)
								{
									ParticleSystem.ShapeModule shape = this.bodyGlowParticles.shape;
									if (renderer is MeshRenderer)
									{
										shape.shapeType = ParticleSystemShapeType.MeshRenderer;
										shape.meshRenderer = (renderer as MeshRenderer);
									}
									else if (renderer is SkinnedMeshRenderer)
									{
										shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
										shape.skinnedMeshRenderer = (renderer as SkinnedMeshRenderer);
									}
								}
							}
						}
					}
				}
			}
			this.bodyGlowParticles.Play();
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000D2DF4 File Offset: 0x000D0FF4
		public void FixedUpdate()
		{
			this.fixedAge += Time.fixedDeltaTime;
			if (this.fixedAge >= this.delayBeforePlayingSFX && !this.hasPlayedSFX)
			{
				this.hasPlayedSFX = true;
				Util.PlaySound(TeleportOutController.tpOutSoundString, this.target);
			}
			if (NetworkServer.active && this.fixedAge >= 2f && this.target)
			{
				GameObject teleportEffectPrefab = Run.instance.GetTeleportEffectPrefab(this.target);
				if (teleportEffectPrefab)
				{
					EffectManager.SpawnEffect(teleportEffectPrefab, new EffectData
					{
						origin = this.target.transform.position
					}, true);
				}
				UnityEngine.Object.Destroy(this.target);
			}
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000D2EC8 File Offset: 0x000D10C8
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000D2EDB File Offset: 0x000D10DB
		public GameObject Networktarget
		{
			get
			{
				return this.target;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.target, 1U, ref this.___targetNetId);
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000D2EF8 File Offset: 0x000D10F8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.target);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.target);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000D2F64 File Offset: 0x000D1164
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___targetNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.target = reader.ReadGameObject();
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000D2FA5 File Offset: 0x000D11A5
		public override void PreStartClient()
		{
			if (!this.___targetNetId.IsEmpty())
			{
				this.Networktarget = ClientScene.FindLocalObject(this.___targetNetId);
			}
		}

		// Token: 0x04003323 RID: 13091
		[SyncVar]
		[NonSerialized]
		public GameObject target;

		// Token: 0x04003324 RID: 13092
		public ParticleSystem bodyGlowParticles;

		// Token: 0x04003325 RID: 13093
		public static string tpOutSoundString = "Play_UI_teleport_off_map";

		// Token: 0x04003326 RID: 13094
		private float fixedAge;

		// Token: 0x04003327 RID: 13095
		private const float warmupDuration = 2f;

		// Token: 0x04003328 RID: 13096
		public float delayBeforePlayingSFX = 1f;

		// Token: 0x04003329 RID: 13097
		private bool hasPlayedSFX;

		// Token: 0x0400332A RID: 13098
		private NetworkInstanceId ___targetNetId;
	}
}
