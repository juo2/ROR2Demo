using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x0200013A RID: 314
	public abstract class BaseStompState : BaseLegState
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldUseWarningIndicator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldUpdateLegStompTargetPosition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017E54 File Offset: 0x00016054
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration;
			string stompPlaybackRateParam = base.legController.stompPlaybackRateParam;
			if (!string.IsNullOrEmpty(stompPlaybackRateParam))
			{
				EntityState.PlayAnimationOnAnimator(base.legController.animator, base.legController.primaryLayerName, this.animName, stompPlaybackRateParam, this.duration);
			}
			else
			{
				EntityState.PlayAnimationOnAnimator(base.legController.animator, base.legController.primaryLayerName, this.animName);
				int layerIndex = base.legController.animator.GetLayerIndex(base.legController.primaryLayerName);
				this.duration = base.legController.animator.GetCurrentAnimatorStateInfo(layerIndex).length;
			}
			this.SetWarningIndicatorActive(this.shouldUseWarningIndicator);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017F19 File Offset: 0x00016119
		public override void OnExit()
		{
			this.SetWarningIndicatorActive(false);
			base.OnExit();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017F28 File Offset: 0x00016128
		public override void ModifyNextState(EntityState nextState)
		{
			base.ModifyNextState(nextState);
			BaseStompState baseStompState;
			if ((baseStompState = (nextState as BaseStompState)) != null)
			{
				baseStompState.warningIndicatorInstance = this.warningIndicatorInstance;
				this.warningIndicatorInstance = null;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017F5C File Offset: 0x0001615C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.legController.mainBody.hasEffectiveAuthority && base.fixedAge >= this.duration && !this.lifetimeExpiredAuthority)
			{
				this.lifetimeExpiredAuthority = true;
				this.OnLifetimeExpiredAuthority();
			}
			if (this.shouldUpdateLegStompTargetPosition && this.target)
			{
				base.legController.SetStompTargetWorldPosition(new Vector3?(this.target.transform.position));
			}
			this.UpdateWarningIndicatorInstance();
		}

		// Token: 0x06000599 RID: 1433
		protected abstract void OnLifetimeExpiredAuthority();

		// Token: 0x0600059A RID: 1434 RVA: 0x00017FE0 File Offset: 0x000161E0
		protected void SetWarningIndicatorActive(bool newWarningIndicatorActive)
		{
			if (this.warningIndicatorInstance == newWarningIndicatorActive)
			{
				return;
			}
			if (newWarningIndicatorActive)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(BaseStompState.warningIndicatorPrefab);
				this.warningIndicatorInstance = gameObject.GetComponent<RayAttackIndicator>();
				this.UpdateWarningIndicatorInstance();
				return;
			}
			if (this.warningIndicatorInstance)
			{
				EntityState.Destroy(this.warningIndicatorInstance.gameObject);
			}
			this.warningIndicatorInstance = null;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00018044 File Offset: 0x00016244
		private void UpdateWarningIndicatorInstance()
		{
			if (this.warningIndicatorInstance)
			{
				Vector3 position = base.legController.toeTipTransform.position;
				Vector3 vector = base.legController.mainBody ? base.legController.mainBody.transform.position : position;
				this.warningIndicatorInstance.attackRay = new Ray(position, Vector3.down);
				this.warningIndicatorInstance.attackRange = position.y - vector.y;
				this.warningIndicatorInstance.attackRadius = Stomp.blastRadius;
				this.warningIndicatorInstance.layerMask = LayerIndex.world.mask;
			}
		}

		// Token: 0x040006A6 RID: 1702
		[SerializeField]
		public float baseDuration;

		// Token: 0x040006A7 RID: 1703
		[SerializeField]
		public string animName;

		// Token: 0x040006A8 RID: 1704
		public static GameObject warningIndicatorPrefab;

		// Token: 0x040006A9 RID: 1705
		public GameObject target;

		// Token: 0x040006AA RID: 1706
		protected float duration;

		// Token: 0x040006AB RID: 1707
		private bool lifetimeExpiredAuthority;

		// Token: 0x040006AC RID: 1708
		private RayAttackIndicator warningIndicatorInstance;
	}
}
