using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x0200041A RID: 1050
	public class HackingInProgressState : BaseMainState
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldShowEnergy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00054354 File Offset: 0x00052554
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				int difficultyScaledCost = Run.instance.GetDifficultyScaledCost(HackingInProgressState.baseGoldForBaseDuration, Stage.instance.entryDifficultyCoefficient);
				float b = (float)((double)this.target.cost / (double)difficultyScaledCost * (double)HackingInProgressState.baseDuration);
				this.startTime = Run.FixedTimeStamp.now;
				this.endTime = this.startTime + b;
			}
			this.energyComponent.normalizedChargeRate = 1f / (this.endTime - this.startTime);
			if (NetworkServer.active)
			{
				this.energyComponent.energy = 0f;
			}
			if (HackingInProgressState.targetIndicatorVfxPrefab && this.target)
			{
				this.targetIndicatorVfxInstance = UnityEngine.Object.Instantiate<GameObject>(HackingInProgressState.targetIndicatorVfxPrefab, this.target.transform.position, Quaternion.identity);
				ChildLocator component = this.targetIndicatorVfxInstance.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("LineEnd");
					if (transform)
					{
						transform.position = base.FindModelChild("ShaftTip").position;
					}
				}
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00054473 File Offset: 0x00052673
		public override void OnExit()
		{
			if (this.targetIndicatorVfxInstance)
			{
				EntityState.Destroy(this.targetIndicatorVfxInstance);
				this.targetIndicatorVfxInstance = null;
			}
			base.OnExit();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0005449C File Offset: 0x0005269C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (this.energyComponent.normalizedEnergy >= 1f)
				{
					this.outer.SetNextState(new UnlockTargetState
					{
						target = this.target
					});
					return;
				}
				if (!HackingMainState.PurchaseInteractionIsValidTarget(this.target))
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00054500 File Offset: 0x00052700
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.target ? this.target.gameObject : null);
			writer.Write(this.startTime);
			writer.Write(this.endTime);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00054550 File Offset: 0x00052750
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			GameObject gameObject = reader.ReadGameObject();
			this.target = (gameObject ? gameObject.GetComponent<PurchaseInteraction>() : null);
			this.startTime = reader.ReadFixedTimeStamp();
			this.endTime = reader.ReadFixedTimeStamp();
		}

		// Token: 0x04001845 RID: 6213
		public static int baseGoldForBaseDuration = 25;

		// Token: 0x04001846 RID: 6214
		public static float baseDuration = 15f;

		// Token: 0x04001847 RID: 6215
		public static GameObject targetIndicatorVfxPrefab;

		// Token: 0x04001848 RID: 6216
		public PurchaseInteraction target;

		// Token: 0x04001849 RID: 6217
		private GameObject targetIndicatorVfxInstance;

		// Token: 0x0400184A RID: 6218
		private Run.FixedTimeStamp startTime;

		// Token: 0x0400184B RID: 6219
		private Run.FixedTimeStamp endTime;
	}
}
