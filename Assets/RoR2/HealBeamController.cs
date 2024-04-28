using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000717 RID: 1815
	[RequireComponent(typeof(GenericOwnership))]
	public class HealBeamController : NetworkBehaviour
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x000A21F1 File Offset: 0x000A03F1
		// (set) Token: 0x06002574 RID: 9588 RVA: 0x000A21F9 File Offset: 0x000A03F9
		public GenericOwnership ownership { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x000A2202 File Offset: 0x000A0402
		// (set) Token: 0x06002576 RID: 9590 RVA: 0x000A220A File Offset: 0x000A040A
		public HurtBox target
		{
			get
			{
				return this.cachedHurtBox;
			}
			[Server]
			set
			{
				if (!NetworkServer.active)
				{
					Debug.LogWarning("[Server] function 'System.Void RoR2.HealBeamController::set_target(RoR2.HurtBox)' called on client");
					return;
				}
				this.NetworknetTarget = HurtBoxReference.FromHurtBox(value);
				this.UpdateCachedHurtBox();
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000A2233 File Offset: 0x000A0433
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x000A223B File Offset: 0x000A043B
		public float healRate { get; set; }

		// Token: 0x06002579 RID: 9593 RVA: 0x000A2244 File Offset: 0x000A0444
		private void Awake()
		{
			this.ownership = base.GetComponent<GenericOwnership>();
			this.startPointTransform.SetParent(null, true);
			this.endPointTransform.SetParent(null, true);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000A226C File Offset: 0x000A046C
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.UpdateCachedHurtBox();
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000A227A File Offset: 0x000A047A
		private void OnDestroy()
		{
			if (this.startPointTransform)
			{
				UnityEngine.Object.Destroy(this.startPointTransform.gameObject);
			}
			if (this.endPointTransform)
			{
				UnityEngine.Object.Destroy(this.endPointTransform.gameObject);
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000A22B6 File Offset: 0x000A04B6
		private void OnEnable()
		{
			InstanceTracker.Add<HealBeamController>(this);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000A22BE File Offset: 0x000A04BE
		private void OnDisable()
		{
			InstanceTracker.Remove<HealBeamController>(this);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000A22C6 File Offset: 0x000A04C6
		private void LateUpdate()
		{
			this.UpdateHealBeamVisuals();
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000A22CE File Offset: 0x000A04CE
		private void OnSyncTarget(HurtBoxReference newValue)
		{
			this.NetworknetTarget = newValue;
			this.UpdateCachedHurtBox();
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000A22DD File Offset: 0x000A04DD
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000A22EC File Offset: 0x000A04EC
		private void FixedUpdateServer()
		{
			if (!this.cachedHurtBox)
			{
				this.BreakServer();
				return;
			}
			if (this.tickInterval > 0f)
			{
				this.stopwatchServer += Time.fixedDeltaTime;
				while (this.stopwatchServer >= this.tickInterval)
				{
					this.stopwatchServer -= this.tickInterval;
					this.OnTickServer();
				}
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000A2358 File Offset: 0x000A0558
		private void OnTickServer()
		{
			if (!this.cachedHurtBox || !this.cachedHurtBox.healthComponent)
			{
				this.BreakServer();
				return;
			}
			this.cachedHurtBox.healthComponent.Heal(this.healRate * this.tickInterval, default(ProcChainMask), true);
			if (this.breakOnTargetFullyHealed && this.cachedHurtBox.healthComponent.health >= this.cachedHurtBox.healthComponent.fullHealth)
			{
				this.BreakServer();
				return;
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000A23E4 File Offset: 0x000A05E4
		private void UpdateCachedHurtBox()
		{
			if (!this.previousHurtBoxReference.Equals(this.netTarget))
			{
				this.cachedHurtBox = this.netTarget.ResolveHurtBox();
				this.previousHurtBoxReference = this.netTarget;
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000A2416 File Offset: 0x000A0616
		public static bool HealBeamAlreadyExists(GameObject owner, HurtBox target)
		{
			return HealBeamController.HealBeamAlreadyExists(owner, target.healthComponent);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000A2424 File Offset: 0x000A0624
		public static bool HealBeamAlreadyExists(GameObject owner, HealthComponent targetHealthComponent)
		{
			List<HealBeamController> instancesList = InstanceTracker.GetInstancesList<HealBeamController>();
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				HealBeamController healBeamController = instancesList[i];
				HurtBox target = healBeamController.target;
				if (((target != null) ? target.healthComponent : null) == targetHealthComponent && healBeamController.ownership.ownerObject == owner)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000A2478 File Offset: 0x000A0678
		public static int GetHealBeamCountForOwner(GameObject owner)
		{
			int num = 0;
			List<HealBeamController> instancesList = InstanceTracker.GetInstancesList<HealBeamController>();
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				if (instancesList[i].ownership.ownerObject == owner)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000A24BC File Offset: 0x000A06BC
		private void UpdateHealBeamVisuals()
		{
			float target = this.target ? 1f : 0f;
			this.scaleFactor = Mathf.SmoothDamp(this.scaleFactor, target, ref this.scaleFactorVelocity, this.smoothTime);
			Vector3 localScale = new Vector3(this.scaleFactor, this.scaleFactor, this.scaleFactor);
			this.startPointTransform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
			this.startPointTransform.localScale = localScale;
			if (this.cachedHurtBox)
			{
				this.endPointTransform.position = this.cachedHurtBox.transform.position;
			}
			this.endPointTransform.localScale = localScale;
			this.lineRenderer.widthMultiplier = this.scaleFactor * this.maxLineWidth;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000A2594 File Offset: 0x000A0794
		[Server]
		public void BreakServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealBeamController::BreakServer()' called on client");
				return;
			}
			if (this.broken)
			{
				return;
			}
			this.broken = true;
			this.target = null;
			base.transform.SetParent(null);
			this.ownership.ownerObject = null;
			UnityEngine.Object.Destroy(base.gameObject, this.lingerAfterBrokenDuration);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000A2620 File Offset: 0x000A0820
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x000A2633 File Offset: 0x000A0833
		public HurtBoxReference NetworknetTarget
		{
			get
			{
				return this.netTarget;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncTarget(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<HurtBoxReference>(value, ref this.netTarget, 1U);
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000A2674 File Offset: 0x000A0874
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteHurtBoxReference_None(writer, this.netTarget);
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
				GeneratedNetworkCode._WriteHurtBoxReference_None(writer, this.netTarget);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000A26E0 File Offset: 0x000A08E0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.netTarget = GeneratedNetworkCode._ReadHurtBoxReference_None(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncTarget(GeneratedNetworkCode._ReadHurtBoxReference_None(reader));
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002952 RID: 10578
		public Transform startPointTransform;

		// Token: 0x04002953 RID: 10579
		public Transform endPointTransform;

		// Token: 0x04002954 RID: 10580
		public float tickInterval = 1f;

		// Token: 0x04002955 RID: 10581
		public bool breakOnTargetFullyHealed;

		// Token: 0x04002956 RID: 10582
		public LineRenderer lineRenderer;

		// Token: 0x04002957 RID: 10583
		public float lingerAfterBrokenDuration;

		// Token: 0x04002959 RID: 10585
		[SyncVar(hook = "OnSyncTarget")]
		private HurtBoxReference netTarget;

		// Token: 0x0400295B RID: 10587
		private float stopwatchServer;

		// Token: 0x0400295C RID: 10588
		private bool broken;

		// Token: 0x0400295D RID: 10589
		private HurtBoxReference previousHurtBoxReference;

		// Token: 0x0400295E RID: 10590
		private HurtBox cachedHurtBox;

		// Token: 0x0400295F RID: 10591
		private float scaleFactorVelocity;

		// Token: 0x04002960 RID: 10592
		private float maxLineWidth = 0.3f;

		// Token: 0x04002961 RID: 10593
		private float smoothTime = 0.1f;

		// Token: 0x04002962 RID: 10594
		private float scaleFactor;
	}
}
