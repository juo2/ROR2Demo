using System;
using EntityStates.InfiniteTowerSafeWard;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000768 RID: 1896
	public class InfiniteTowerSafeWardController : MonoBehaviour
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000AA542 File Offset: 0x000A8742
		public IZone safeZone
		{
			get
			{
				return this._safeZone;
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x0600272C RID: 10028 RVA: 0x000AA54C File Offset: 0x000A874C
		// (remove) Token: 0x0600272D RID: 10029 RVA: 0x000AA584 File Offset: 0x000A8784
		public event Action<InfiniteTowerSafeWardController> onActivated;

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000AA5B9 File Offset: 0x000A87B9
		public bool isAwaitingInteraction
		{
			get
			{
				return this.wardStateMachine && this.wardStateMachine.state is AwaitingActivation;
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000AA5E0 File Offset: 0x000A87E0
		private void Awake()
		{
			if (this.positionIndicatorPrefab)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.positionIndicatorPrefab, base.transform.position, Quaternion.identity);
				if (gameObject)
				{
					this.positionIndicator = gameObject.GetComponent<PositionIndicator>();
					if (this.positionIndicator)
					{
						this.positionIndicator.targetTransform = base.transform;
					}
					this.chargeIndicatorController = gameObject.GetComponent<ChargeIndicatorController>();
					if (this.chargeIndicatorController)
					{
						this.chargeIndicatorController.holdoutZoneController = this.holdoutZoneController;
					}
					gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000AA67C File Offset: 0x000A887C
		public void Activate()
		{
			if (this.wardStateMachine)
			{
				AwaitingActivation awaitingActivation = this.wardStateMachine.state as AwaitingActivation;
				if (awaitingActivation != null)
				{
					awaitingActivation.Activate();
					Action<InfiniteTowerSafeWardController> action = this.onActivated;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000AA6C4 File Offset: 0x000A88C4
		public void SelfDestruct()
		{
			if (this.wardStateMachine)
			{
				Active active = this.wardStateMachine.state as Active;
				if (active != null)
				{
					active.SelfDestruct();
				}
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000AA6F8 File Offset: 0x000A88F8
		public void RandomizeLocation(Xoroshiro128Plus rng)
		{
			if (this.wardStateMachine)
			{
				this.wardStateMachine.SetNextState(new Unburrow(rng));
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000AA718 File Offset: 0x000A8918
		public void WaitForPortal()
		{
			if (this.wardStateMachine)
			{
				this.wardStateMachine.SetNextState(new AwaitingPortalUse());
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000AA737 File Offset: 0x000A8937
		public void SetIndicatorEnabled(bool enabled)
		{
			if (this.positionIndicator)
			{
				this.positionIndicator.gameObject.SetActive(enabled);
			}
		}

		// Token: 0x04002B27 RID: 11047
		[SerializeField]
		private EntityStateMachine wardStateMachine;

		// Token: 0x04002B28 RID: 11048
		[SerializeField]
		private VerticalTubeZone _safeZone;

		// Token: 0x04002B29 RID: 11049
		[SerializeField]
		private GameObject positionIndicatorPrefab;

		// Token: 0x04002B2A RID: 11050
		[SerializeField]
		private HoldoutZoneController holdoutZoneController;

		// Token: 0x04002B2B RID: 11051
		private PositionIndicator positionIndicator;

		// Token: 0x04002B2C RID: 11052
		private ChargeIndicatorController chargeIndicatorController;
	}
}
