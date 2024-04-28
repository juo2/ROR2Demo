using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003CC RID: 972
	public class StartHealBeam : BaseState
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x0004CA20 File Offset: 0x0004AC20
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				this.targetHurtBox = this.FindTarget(aimRay);
			}
			if (NetworkServer.active)
			{
				if (HealBeamController.GetHealBeamCountForOwner(base.gameObject) >= this.maxSimultaneousBeams)
				{
					return;
				}
				if (this.targetHurtBox)
				{
					Transform transform = base.FindModelChild(this.muzzleName);
					if (transform)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.healBeamPrefab, transform);
						HealBeamController component = gameObject.GetComponent<HealBeamController>();
						component.healRate = this.healRateCoefficient * this.damageStat * this.attackSpeedStat;
						component.target = this.targetHurtBox;
						component.ownership.ownerObject = base.gameObject;
						gameObject.AddComponent<DestroyOnTimer>().duration = this.duration;
						NetworkServer.Spawn(gameObject);
					}
				}
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004CB00 File Offset: 0x0004AD00
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004CB1C File Offset: 0x0004AD1C
		private HurtBox FindTarget(Ray aimRay)
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.teamMaskFilter = TeamMask.none;
			if (base.teamComponent)
			{
				bullseyeSearch.teamMaskFilter.AddTeam(base.teamComponent.teamIndex);
			}
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.maxDistanceFilter = this.targetSelectionRange;
			bullseyeSearch.maxAngleFilter = 180f;
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			return bullseyeSearch.GetResults().Where(new Func<HurtBox, bool>(this.NotAlreadyHealingTarget)).Where(new Func<HurtBox, bool>(StartHealBeam.IsHurt)).FirstOrDefault<HurtBox>();
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0004CBDB File Offset: 0x0004ADDB
		private bool NotAlreadyHealingTarget(HurtBox hurtBox)
		{
			return !HealBeamController.HealBeamAlreadyExists(base.gameObject, hurtBox);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004CBEC File Offset: 0x0004ADEC
		private static bool IsHurt(HurtBox hurtBox)
		{
			return hurtBox.healthComponent.alive && hurtBox.healthComponent.health < hurtBox.healthComponent.fullHealth;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004CC15 File Offset: 0x0004AE15
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(HurtBoxReference.FromHurtBox(this.targetHurtBox));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004CC30 File Offset: 0x0004AE30
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.targetHurtBox = reader.ReadHurtBoxReference().ResolveHurtBox();
		}

		// Token: 0x0400160F RID: 5647
		[SerializeField]
		public float baseDuration;

		// Token: 0x04001610 RID: 5648
		[SerializeField]
		public float targetSelectionRange;

		// Token: 0x04001611 RID: 5649
		[SerializeField]
		public float healRateCoefficient;

		// Token: 0x04001612 RID: 5650
		[SerializeField]
		public GameObject healBeamPrefab;

		// Token: 0x04001613 RID: 5651
		[SerializeField]
		public string muzzleName;

		// Token: 0x04001614 RID: 5652
		[SerializeField]
		public int maxSimultaneousBeams;

		// Token: 0x04001615 RID: 5653
		private HurtBox targetHurtBox;

		// Token: 0x04001616 RID: 5654
		private float duration;
	}
}
