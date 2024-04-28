using System;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.TitanMonster
{
	// Token: 0x0200035D RID: 861
	public class ChargeMegaLaser : BaseState
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x00043C60 File Offset: 0x00041E60
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeMegaLaser.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ChargeMegaLaser.chargeAttackSoundString, base.gameObject, 2.1f / this.duration);
			Ray aimRay = base.GetAimRay();
			this.enemyFinder = new BullseyeSearch();
			this.enemyFinder.maxDistanceFilter = 2000f;
			this.enemyFinder.maxAngleFilter = ChargeMegaLaser.lockOnAngle;
			this.enemyFinder.searchOrigin = aimRay.origin;
			this.enemyFinder.searchDirection = aimRay.direction;
			this.enemyFinder.filterByLoS = false;
			this.enemyFinder.sortMode = BullseyeSearch.SortMode.Angle;
			this.enemyFinder.teamMaskFilter = TeamMask.allButNeutral;
			if (base.teamComponent)
			{
				this.enemyFinder.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
			}
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleLaser");
					if (transform)
					{
						if (this.effectPrefab)
						{
							this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(this.effectPrefab, transform.position, transform.rotation);
							this.chargeEffect.transform.parent = transform;
							ScaleParticleSystemDuration component2 = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
							if (component2)
							{
								component2.newDuration = this.duration;
							}
						}
						if (this.laserPrefab)
						{
							this.laserEffect = UnityEngine.Object.Instantiate<GameObject>(this.laserPrefab, transform.position, transform.rotation);
							this.laserEffect.transform.parent = transform;
							this.laserLineComponent = this.laserEffect.GetComponent<LineRenderer>();
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
			this.flashTimer = 0f;
			this.laserOn = true;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00043E5D File Offset: 0x0004205D
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			if (this.laserEffect)
			{
				EntityState.Destroy(this.laserEffect);
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00043E98 File Offset: 0x00042098
		public override void Update()
		{
			base.Update();
			if (this.laserEffect && this.laserLineComponent)
			{
				float num = 1000f;
				Ray aimRay = base.GetAimRay();
				this.enemyFinder.RefreshCandidates();
				this.lockedOnHurtBox = this.enemyFinder.GetResults().FirstOrDefault<HurtBox>();
				if (this.lockedOnHurtBox)
				{
					aimRay.direction = this.lockedOnHurtBox.transform.position - aimRay.origin;
				}
				Vector3 position = this.laserEffect.transform.parent.position;
				Vector3 point = aimRay.GetPoint(num);
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, num, LayerIndex.world.mask | LayerIndex.defaultLayer.mask))
				{
					point = raycastHit.point;
				}
				this.laserLineComponent.SetPosition(0, position);
				this.laserLineComponent.SetPosition(1, point);
				float num2;
				if (this.duration - base.age > 0.5f)
				{
					num2 = base.age / this.duration;
				}
				else
				{
					this.flashTimer -= Time.deltaTime;
					if (this.flashTimer <= 0f)
					{
						this.laserOn = !this.laserOn;
						this.flashTimer = 0.033333335f;
					}
					num2 = (this.laserOn ? 1f : 0f);
				}
				num2 *= ChargeMegaLaser.laserMaxWidth;
				this.laserLineComponent.startWidth = num2;
				this.laserLineComponent.endWidth = num2;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00044038 File Offset: 0x00042238
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireMegaLaser nextState = new FireMegaLaser();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400139A RID: 5018
		public static float baseDuration = 3f;

		// Token: 0x0400139B RID: 5019
		public static float laserMaxWidth = 0.2f;

		// Token: 0x0400139C RID: 5020
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x0400139D RID: 5021
		[SerializeField]
		public GameObject laserPrefab;

		// Token: 0x0400139E RID: 5022
		public static string chargeAttackSoundString;

		// Token: 0x0400139F RID: 5023
		public static float lockOnAngle;

		// Token: 0x040013A0 RID: 5024
		private HurtBox lockedOnHurtBox;

		// Token: 0x040013A1 RID: 5025
		public float duration;

		// Token: 0x040013A2 RID: 5026
		private GameObject chargeEffect;

		// Token: 0x040013A3 RID: 5027
		private GameObject laserEffect;

		// Token: 0x040013A4 RID: 5028
		private LineRenderer laserLineComponent;

		// Token: 0x040013A5 RID: 5029
		private Vector3 visualEndPosition;

		// Token: 0x040013A6 RID: 5030
		private float flashTimer;

		// Token: 0x040013A7 RID: 5031
		private bool laserOn;

		// Token: 0x040013A8 RID: 5032
		private BullseyeSearch enemyFinder;

		// Token: 0x040013A9 RID: 5033
		private const float originalSoundDuration = 2.1f;
	}
}
