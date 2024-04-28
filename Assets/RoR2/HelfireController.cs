using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000728 RID: 1832
	[RequireComponent(typeof(NetworkedBodyAttachment))]
	public class HelfireController : NetworkBehaviour
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000A6152 File Offset: 0x000A4352
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x000A615A File Offset: 0x000A435A
		public NetworkedBodyAttachment networkedBodyAttachment { get; private set; }

		// Token: 0x06002610 RID: 9744 RVA: 0x000A6163 File Offset: 0x000A4363
		private void Awake()
		{
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			this.auraEffectTransform.SetParent(null);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000A617D File Offset: 0x000A437D
		private void OnDestroy()
		{
			if (this.auraEffectTransform)
			{
				UnityEngine.Object.Destroy(this.auraEffectTransform.gameObject);
				this.auraEffectTransform = null;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest == null)
			{
				return;
			}
			aimRequest.Dispose();
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000A61B3 File Offset: 0x000A43B3
		private void FixedUpdate()
		{
			this.radius = this.baseRadius * (1f + (float)(this.stack - 1) * 0.5f);
			if (NetworkServer.active)
			{
				this.ServerFixedUpdate();
			}
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000A61E4 File Offset: 0x000A43E4
		private void ServerFixedUpdate()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer = this.interval;
				float num = this.healthFractionPerSecond * this.dotDuration * this.networkedBodyAttachment.attachedBody.healthComponent.fullCombinedHealth;
				float num2 = 1f;
				TeamDef teamDef = TeamCatalog.GetTeamDef(this.networkedBodyAttachment.attachedBody.teamComponent.teamIndex);
				if (teamDef != null && teamDef.friendlyFireScaling > 0f)
				{
					num2 = 1f / teamDef.friendlyFireScaling;
				}
				Collider[] array = Physics.OverlapSphere(base.transform.position, this.radius, LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Collide);
				GameObject[] array2 = new GameObject[array.Length];
				int count = 0;
				for (int i = 0; i < array.Length; i++)
				{
					CharacterBody characterBody = Util.HurtBoxColliderToBody(array[i]);
					GameObject gameObject = characterBody ? characterBody.gameObject : null;
					if (gameObject && Array.IndexOf<GameObject>(array2, gameObject, 0, count) == -1)
					{
						float num3 = num;
						float num4 = 1f;
						if (this.networkedBodyAttachment.attachedBody.teamComponent.teamIndex == characterBody.teamComponent.teamIndex)
						{
							num3 *= num2;
							num4 *= num2;
							if (this.networkedBodyAttachment.attachedBody != characterBody)
							{
								num3 *= this.allyDamageScalar;
								num4 *= this.allyDamageScalar;
							}
						}
						else
						{
							num3 *= this.enemyDamageScalar;
							num4 *= this.enemyDamageScalar;
						}
						InflictDotInfo inflictDotInfo = new InflictDotInfo
						{
							attackerObject = this.networkedBodyAttachment.attachedBodyObject,
							victimObject = gameObject,
							totalDamage = new float?(num3),
							damageMultiplier = num4,
							dotIndex = DotController.DotIndex.Helfire,
							maxStacksFromAttacker = new uint?(1U)
						};
						StrengthenBurnUtils.CheckDotForUpgrade(this.networkedBodyAttachment.attachedBody.inventory, ref inflictDotInfo);
						DotController.InflictDot(ref inflictDotInfo);
						array2[count++] = gameObject;
					}
				}
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000A6404 File Offset: 0x000A4604
		private void LateUpdate()
		{
			CharacterBody attachedBody = this.networkedBodyAttachment.attachedBody;
			if (attachedBody)
			{
				this.auraEffectTransform.position = this.networkedBodyAttachment.attachedBody.corePosition;
				this.auraEffectTransform.localScale = new Vector3(this.radius, this.radius, this.radius);
				if (!this.cameraTargetParams)
				{
					this.cameraTargetParams = attachedBody.GetComponent<CameraTargetParams>();
					return;
				}
				if (this.aimRequest == null)
				{
					this.aimRequest = this.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
				}
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000A64C8 File Offset: 0x000A46C8
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x000A64DB File Offset: 0x000A46DB
		public int Networkstack
		{
			get
			{
				return this.stack;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.stack, 1U);
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000A64F0 File Offset: 0x000A46F0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.stack);
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
				writer.WritePackedUInt32((uint)this.stack);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000A655C File Offset: 0x000A475C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.stack = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.stack = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040029E8 RID: 10728
		[SyncVar]
		public int stack = 1;

		// Token: 0x040029E9 RID: 10729
		[FormerlySerializedAs("radius")]
		public float baseRadius;

		// Token: 0x040029EA RID: 10730
		public float dotDuration;

		// Token: 0x040029EB RID: 10731
		public float interval;

		// Token: 0x040029EC RID: 10732
		[Range(0f, 1f)]
		public float healthFractionPerSecond = 0.05f;

		// Token: 0x040029ED RID: 10733
		public float allyDamageScalar = 0.5f;

		// Token: 0x040029EE RID: 10734
		public float enemyDamageScalar = 24f;

		// Token: 0x040029EF RID: 10735
		public Transform auraEffectTransform;

		// Token: 0x040029F0 RID: 10736
		private float timer;

		// Token: 0x040029F1 RID: 10737
		private float radius;

		// Token: 0x040029F2 RID: 10738
		private CameraTargetParams.AimRequest aimRequest;

		// Token: 0x040029F4 RID: 10740
		private CameraTargetParams cameraTargetParams;
	}
}
