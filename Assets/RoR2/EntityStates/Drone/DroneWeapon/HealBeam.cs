using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003CB RID: 971
	public class HealBeam : BaseState
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Gesture", "Heal", 0.2f);
			this.duration = HealBeam.baseDuration / this.attackSpeedStat;
			float healRate = HealBeam.healCoefficient * this.damageStat / this.duration;
			Ray aimRay = base.GetAimRay();
			Transform transform = base.FindModelChild("Muzzle");
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.none;
				if (base.teamComponent)
				{
					bullseyeSearch.teamMaskFilter.AddTeam(base.teamComponent.teamIndex);
				}
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.maxDistanceFilter = 50f;
				bullseyeSearch.maxAngleFilter = 180f;
				bullseyeSearch.searchOrigin = aimRay.origin;
				bullseyeSearch.searchDirection = aimRay.direction;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
				bullseyeSearch.RefreshCandidates();
				bullseyeSearch.FilterOutGameObject(base.gameObject);
				this.target = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				if (transform && this.target)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(HealBeam.healBeamPrefab, transform);
					this.healBeamController = gameObject.GetComponent<HealBeamController>();
					this.healBeamController.healRate = healRate;
					this.healBeamController.target = this.target;
					this.healBeamController.ownership.ownerObject = base.gameObject;
					NetworkServer.Spawn(gameObject);
				}
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0004C93D File Offset: 0x0004AB3D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if ((base.fixedAge >= this.duration || !this.target) && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0004C974 File Offset: 0x0004AB74
		public override void OnExit()
		{
			base.PlayCrossfade("Gesture", "Empty", 0.2f);
			if (this.healBeamController)
			{
				this.healBeamController.BreakServer();
			}
			base.OnExit();
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004C9AC File Offset: 0x0004ABAC
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			HurtBoxReference.FromHurtBox(this.target).Write(writer);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0004C9D4 File Offset: 0x0004ABD4
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			HurtBoxReference hurtBoxReference = default(HurtBoxReference);
			hurtBoxReference.Read(reader);
			GameObject gameObject = hurtBoxReference.ResolveGameObject();
			this.target = ((gameObject != null) ? gameObject.GetComponent<HurtBox>() : null);
		}

		// Token: 0x04001608 RID: 5640
		public static float baseDuration;

		// Token: 0x04001609 RID: 5641
		public static float healCoefficient = 5f;

		// Token: 0x0400160A RID: 5642
		public static GameObject healBeamPrefab;

		// Token: 0x0400160B RID: 5643
		public HurtBox target;

		// Token: 0x0400160C RID: 5644
		private HealBeamController healBeamController;

		// Token: 0x0400160D RID: 5645
		private float duration;

		// Token: 0x0400160E RID: 5646
		private float lineWidthRefVelocity;
	}
}
