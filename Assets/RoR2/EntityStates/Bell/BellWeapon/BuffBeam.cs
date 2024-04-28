using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Bell.BellWeapon
{
	// Token: 0x0200045B RID: 1115
	public class BuffBeam : BaseState
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x00058B24 File Offset: 0x00056D24
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BuffBeam.playBeamSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
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
			Debug.LogFormat("Buffing target {0}", new object[]
			{
				this.target
			});
			if (this.target)
			{
				this.targetBody = this.target.healthComponent.body;
				this.targetBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex);
			}
			string childName = "Muzzle";
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.muzzleTransform = component.FindChild(childName);
					this.buffBeamInstance = UnityEngine.Object.Instantiate<GameObject>(BuffBeam.buffBeamPrefab);
					ChildLocator component2 = this.buffBeamInstance.GetComponent<ChildLocator>();
					if (component2)
					{
						this.beamTipTransform = component2.FindChild("BeamTip");
					}
					this.healBeamCurve = this.buffBeamInstance.GetComponentInChildren<BezierCurveLine>();
				}
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00058CB4 File Offset: 0x00056EB4
		private void UpdateHealBeamVisuals()
		{
			float widthMultiplier = BuffBeam.beamWidthCurve.Evaluate(base.age / BuffBeam.duration);
			this.healBeamCurve.lineRenderer.widthMultiplier = widthMultiplier;
			this.healBeamCurve.v0 = this.muzzleTransform.forward * 3f;
			this.healBeamCurve.transform.position = this.muzzleTransform.position;
			if (this.target)
			{
				this.beamTipTransform.position = this.targetBody.mainHurtBox.transform.position;
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00058D51 File Offset: 0x00056F51
		public override void Update()
		{
			base.Update();
			this.UpdateHealBeamVisuals();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00058D5F File Offset: 0x00056F5F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if ((base.fixedAge >= BuffBeam.duration || this.target == null) && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00058D98 File Offset: 0x00056F98
		public override void OnExit()
		{
			Util.PlaySound(BuffBeam.stopBeamSoundString, base.gameObject);
			EntityState.Destroy(this.buffBeamInstance);
			if (this.targetBody)
			{
				this.targetBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex);
			}
			base.OnExit();
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00058DEC File Offset: 0x00056FEC
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			HurtBoxReference.FromHurtBox(this.target).Write(writer);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00058E14 File Offset: 0x00057014
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			HurtBoxReference hurtBoxReference = default(HurtBoxReference);
			hurtBoxReference.Read(reader);
			GameObject gameObject = hurtBoxReference.ResolveGameObject();
			this.target = ((gameObject != null) ? gameObject.GetComponent<HurtBox>() : null);
		}

		// Token: 0x04001985 RID: 6533
		public static float duration;

		// Token: 0x04001986 RID: 6534
		public static GameObject buffBeamPrefab;

		// Token: 0x04001987 RID: 6535
		public static AnimationCurve beamWidthCurve;

		// Token: 0x04001988 RID: 6536
		public static string playBeamSoundString;

		// Token: 0x04001989 RID: 6537
		public static string stopBeamSoundString;

		// Token: 0x0400198A RID: 6538
		public HurtBox target;

		// Token: 0x0400198B RID: 6539
		private float healTimer;

		// Token: 0x0400198C RID: 6540
		private float healInterval;

		// Token: 0x0400198D RID: 6541
		private float healChunk;

		// Token: 0x0400198E RID: 6542
		private CharacterBody targetBody;

		// Token: 0x0400198F RID: 6543
		private GameObject buffBeamInstance;

		// Token: 0x04001990 RID: 6544
		private BezierCurveLine healBeamCurve;

		// Token: 0x04001991 RID: 6545
		private Transform muzzleTransform;

		// Token: 0x04001992 RID: 6546
		private Transform beamTipTransform;
	}
}
