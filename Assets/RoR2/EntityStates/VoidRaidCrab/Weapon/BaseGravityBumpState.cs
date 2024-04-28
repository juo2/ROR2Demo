using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x0200012F RID: 303
	public abstract class BaseGravityBumpState : BaseState
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00016E60 File Offset: 0x00015060
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.isLeft);
			writer.Write(this.groundedForce);
			writer.Write(this.airborneForce);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00016E8D File Offset: 0x0001508D
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.isLeft = reader.ReadBoolean();
			this.groundedForce = reader.ReadVector3();
			this.airborneForce = reader.ReadVector3();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016EBC File Offset: 0x000150BC
		public override void ModifyNextState(EntityState nextState)
		{
			base.ModifyNextState(nextState);
			BaseGravityBumpState baseGravityBumpState = nextState as BaseGravityBumpState;
			if (baseGravityBumpState != null)
			{
				baseGravityBumpState.groundedForce = this.groundedForce;
				baseGravityBumpState.airborneForce = this.airborneForce;
				baseGravityBumpState.isLeft = this.isLeft;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00016F00 File Offset: 0x00015100
		protected IEnumerable<HurtBox> GetTargets()
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.viewer = base.characterBody;
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.characterBody.teamComponent.teamIndex);
			bullseyeSearch.minDistanceFilter = 0f;
			bullseyeSearch.maxDistanceFilter = this.maxDistance;
			bullseyeSearch.searchOrigin = base.inputBank.aimOrigin;
			bullseyeSearch.searchDirection = base.inputBank.aimDirection;
			bullseyeSearch.maxAngleFilter = 360f;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.filterByDistinctEntity = true;
			bullseyeSearch.RefreshCandidates();
			return bullseyeSearch.GetResults();
		}

		// Token: 0x04000645 RID: 1605
		[SerializeField]
		public float maxDistance;

		// Token: 0x04000646 RID: 1606
		protected Vector3 airborneForce;

		// Token: 0x04000647 RID: 1607
		protected Vector3 groundedForce;

		// Token: 0x04000648 RID: 1608
		protected bool isLeft;
	}
}
