using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000765 RID: 1893
	public class IcicleAuraController : NetworkBehaviour
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x000A9D30 File Offset: 0x000A7F30
		private int maxIcicleCount
		{
			get
			{
				int num = 1;
				if (this.cachedOwnerInfo.characterBody.inventory)
				{
					num = this.cachedOwnerInfo.characterBody.inventory.GetItemCount(RoR2Content.Items.Icicle);
				}
				return this.baseIcicleMax + (num - 1) * this.icicleMaxPerStack;
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000A9D82 File Offset: 0x000A7F82
		private void Awake()
		{
			this.transform = base.transform;
			if (this.buffWard)
			{
				this.buffWard.interval = this.baseIcicleAttackInterval;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000A9DB0 File Offset: 0x000A7FB0
		private void FixedUpdate()
		{
			if (this.cachedOwnerInfo.gameObject != this.owner)
			{
				this.cachedOwnerInfo = new IcicleAuraController.OwnerInfo(this.owner);
			}
			this.UpdateRadius();
			if (NetworkServer.active)
			{
				if (!this.owner)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				for (int i = this.icicleLifetimes.Count - 1; i >= 0; i--)
				{
					List<float> list = this.icicleLifetimes;
					int index = i;
					list[index] -= Time.fixedDeltaTime;
					if (this.icicleLifetimes[i] <= 0f)
					{
						this.icicleLifetimes.RemoveAt(i);
					}
				}
				this.NetworkfinalIcicleCount = Mathf.Min((this.icicleLifetimes.Count > 0) ? (this.icicleCountOnFirstKill + this.icicleLifetimes.Count) : 0, this.maxIcicleCount);
				this.attackStopwatch += Time.fixedDeltaTime;
			}
			if (this.cachedOwnerInfo.characterBody)
			{
				if (this.finalIcicleCount > 0)
				{
					if (this.lastIcicleCount == 0)
					{
						this.OnIciclesActivated();
					}
					if (this.lastIcicleCount < this.finalIcicleCount)
					{
						this.OnIcicleGained();
					}
				}
				else if (this.lastIcicleCount > 0)
				{
					this.OnIciclesDeactivated();
				}
				this.lastIcicleCount = this.finalIcicleCount;
			}
			if (NetworkServer.active && this.cachedOwnerInfo.characterBody && this.finalIcicleCount > 0 && this.attackStopwatch >= this.baseIcicleAttackInterval)
			{
				this.attackStopwatch = 0f;
				new BlastAttack
				{
					attacker = this.owner,
					inflictor = base.gameObject,
					teamIndex = this.cachedOwnerInfo.characterBody.teamComponent.teamIndex,
					position = this.transform.position,
					procCoefficient = this.icicleProcCoefficientPerTick,
					radius = this.actualRadius,
					baseForce = 0f,
					baseDamage = this.cachedOwnerInfo.characterBody.damage * (this.icicleDamageCoefficientPerTick + this.icicleDamageCoefficientPerTickPerIcicle * (float)this.finalIcicleCount),
					bonusForce = Vector3.zero,
					crit = false,
					damageType = DamageType.Generic,
					falloffModel = BlastAttack.FalloffModel.None,
					damageColorIndex = DamageColorIndex.Item,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
			}
			if (this.buffWard)
			{
				this.buffWard.Networkradius = this.actualRadius;
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000AA034 File Offset: 0x000A8234
		private void UpdateRadius()
		{
			if (this.owner)
			{
				if (this.finalIcicleCount > 0)
				{
					this.actualRadius = (this.cachedOwnerInfo.characterBody ? (this.cachedOwnerInfo.characterBody.radius + this.icicleBaseRadius + this.icicleRadiusPerIcicle * (float)this.finalIcicleCount) : 0f);
					return;
				}
				this.actualRadius = 0f;
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000AA0A8 File Offset: 0x000A82A8
		private void UpdateVisuals()
		{
			if (this.cachedOwnerInfo.gameObject)
			{
				this.transform.position = (this.cachedOwnerInfo.characterBody ? this.cachedOwnerInfo.characterBody.corePosition : this.cachedOwnerInfo.transform.position);
			}
			float num = Mathf.SmoothDamp(this.transform.localScale.x, this.actualRadius, ref this.scaleVelocity, 0.5f);
			this.transform.localScale = new Vector3(num, num, num);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000AA140 File Offset: 0x000A8340
		private void OnIciclesDeactivated()
		{
			Util.PlaySound("Stop_item_proc_icicle", base.gameObject);
			ParticleSystem[] array = this.auraParticles;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].main.loop = false;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest == null)
			{
				return;
			}
			aimRequest.Dispose();
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000AA194 File Offset: 0x000A8394
		private void OnIciclesActivated()
		{
			Util.PlaySound("Play_item_proc_icicle", base.gameObject);
			if (this.cachedOwnerInfo.cameraTargetParams)
			{
				this.aimRequest = this.cachedOwnerInfo.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			foreach (ParticleSystem particleSystem in this.auraParticles)
			{
				particleSystem.main.loop = true;
				particleSystem.Play();
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000AA208 File Offset: 0x000A8408
		private void OnIcicleGained()
		{
			ParticleSystem[] array = this.procParticles;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Play();
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000AA232 File Offset: 0x000A8432
		private void LateUpdate()
		{
			this.UpdateVisuals();
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000AA23A File Offset: 0x000A843A
		public void OnOwnerKillOther()
		{
			this.icicleLifetimes.Add(this.icicleDuration);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000AA24D File Offset: 0x000A844D
		public void OnDestroy()
		{
			this.OnIciclesDeactivated();
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000AA2D8 File Offset: 0x000A84D8
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000AA2EB File Offset: 0x000A84EB
		public int NetworkfinalIcicleCount
		{
			get
			{
				return this.finalIcicleCount;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.finalIcicleCount, 1U);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000AA300 File Offset: 0x000A8500
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x000AA313 File Offset: 0x000A8513
		public GameObject Networkowner
		{
			get
			{
				return this.owner;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.owner, 2U, ref this.___ownerNetId);
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000AA330 File Offset: 0x000A8530
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.finalIcicleCount);
				writer.Write(this.owner);
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
				writer.WritePackedUInt32((uint)this.finalIcicleCount);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.owner);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000AA3DC File Offset: 0x000A85DC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.finalIcicleCount = (int)reader.ReadPackedUInt32();
				this.___ownerNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.finalIcicleCount = (int)reader.ReadPackedUInt32();
			}
			if ((num & 2) != 0)
			{
				this.owner = reader.ReadGameObject();
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000AA442 File Offset: 0x000A8642
		public override void PreStartClient()
		{
			if (!this.___ownerNetId.IsEmpty())
			{
				this.Networkowner = ClientScene.FindLocalObject(this.___ownerNetId);
			}
		}

		// Token: 0x04002B0A RID: 11018
		public float baseIcicleAttackInterval = 0.25f;

		// Token: 0x04002B0B RID: 11019
		public float icicleBaseRadius = 10f;

		// Token: 0x04002B0C RID: 11020
		public float icicleRadiusPerIcicle = 2f;

		// Token: 0x04002B0D RID: 11021
		public float icicleDamageCoefficientPerTick = 2f;

		// Token: 0x04002B0E RID: 11022
		public float icicleDamageCoefficientPerTickPerIcicle = 1f;

		// Token: 0x04002B0F RID: 11023
		public float icicleDuration = 5f;

		// Token: 0x04002B10 RID: 11024
		public float icicleProcCoefficientPerTick = 0.2f;

		// Token: 0x04002B11 RID: 11025
		public int icicleCountOnFirstKill = 2;

		// Token: 0x04002B12 RID: 11026
		public int baseIcicleMax = 6;

		// Token: 0x04002B13 RID: 11027
		public int icicleMaxPerStack = 3;

		// Token: 0x04002B14 RID: 11028
		public BuffWard buffWard;

		// Token: 0x04002B15 RID: 11029
		private CameraTargetParams.AimRequest aimRequest;

		// Token: 0x04002B16 RID: 11030
		private List<float> icicleLifetimes = new List<float>();

		// Token: 0x04002B17 RID: 11031
		private float attackStopwatch;

		// Token: 0x04002B18 RID: 11032
		private int lastIcicleCount;

		// Token: 0x04002B19 RID: 11033
		[SyncVar]
		private int finalIcicleCount;

		// Token: 0x04002B1A RID: 11034
		[SyncVar]
		public GameObject owner;

		// Token: 0x04002B1B RID: 11035
		private IcicleAuraController.OwnerInfo cachedOwnerInfo;

		// Token: 0x04002B1C RID: 11036
		public ParticleSystem[] auraParticles;

		// Token: 0x04002B1D RID: 11037
		public ParticleSystem[] procParticles;

		// Token: 0x04002B1E RID: 11038
		private new Transform transform;

		// Token: 0x04002B1F RID: 11039
		private float actualRadius;

		// Token: 0x04002B20 RID: 11040
		private float scaleVelocity;

		// Token: 0x04002B21 RID: 11041
		private NetworkInstanceId ___ownerNetId;

		// Token: 0x02000766 RID: 1894
		private struct OwnerInfo
		{
			// Token: 0x06002728 RID: 10024 RVA: 0x000AA468 File Offset: 0x000A8668
			public OwnerInfo(GameObject gameObject)
			{
				this.gameObject = gameObject;
				if (gameObject)
				{
					this.transform = gameObject.transform;
					this.characterBody = gameObject.GetComponent<CharacterBody>();
					this.cameraTargetParams = gameObject.GetComponent<CameraTargetParams>();
					return;
				}
				this.transform = null;
				this.characterBody = null;
				this.cameraTargetParams = null;
			}

			// Token: 0x04002B22 RID: 11042
			public readonly GameObject gameObject;

			// Token: 0x04002B23 RID: 11043
			public readonly Transform transform;

			// Token: 0x04002B24 RID: 11044
			public readonly CharacterBody characterBody;

			// Token: 0x04002B25 RID: 11045
			public readonly CameraTargetParams cameraTargetParams;
		}
	}
}
