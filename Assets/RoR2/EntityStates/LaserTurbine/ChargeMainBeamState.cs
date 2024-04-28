using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002DD RID: 733
	public class ChargeMainBeamState : LaserTurbineBaseState
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00036DC0 File Offset: 0x00034FC0
		public override void OnEnter()
		{
			base.OnEnter();
			this.beamIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMainBeamState.beamIndicatorPrefab, base.GetMuzzleTransform(), false);
			this.beamIndicatorChildLocator = this.beamIndicatorInstance.GetComponent<ChildLocator>();
			if (this.beamIndicatorChildLocator)
			{
				this.beamIndicatorEndTransform = this.beamIndicatorChildLocator.FindChild("End");
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00036E1E File Offset: 0x0003501E
		public override void OnExit()
		{
			EntityState.Destroy(this.beamIndicatorInstance);
			base.OnExit();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00036E31 File Offset: 0x00035031
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= ChargeMainBeamState.baseDuration)
			{
				this.outer.SetNextState(new FireMainBeamState());
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00036E60 File Offset: 0x00035060
		public override void Update()
		{
			base.Update();
			if (this.beamIndicatorInstance && this.beamIndicatorEndTransform)
			{
				float num = 1000f;
				Ray aimRay = base.GetAimRay();
				Vector3 position = this.beamIndicatorInstance.transform.parent.position;
				Vector3 point = aimRay.GetPoint(num);
				RaycastHit raycastHit;
				if (Util.CharacterRaycast(base.ownerBody.gameObject, aimRay, out raycastHit, num, LayerIndex.entityPrecise.mask | LayerIndex.world.mask, QueryTriggerInteraction.UseGlobal))
				{
					point = raycastHit.point;
				}
				this.beamIndicatorEndTransform.transform.position = point;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldFollow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000FEA RID: 4074
		public static float baseDuration;

		// Token: 0x04000FEB RID: 4075
		public static GameObject beamIndicatorPrefab;

		// Token: 0x04000FEC RID: 4076
		private GameObject beamIndicatorInstance;

		// Token: 0x04000FED RID: 4077
		private ChildLocator beamIndicatorChildLocator;

		// Token: 0x04000FEE RID: 4078
		private Transform beamIndicatorEndTransform;
	}
}
