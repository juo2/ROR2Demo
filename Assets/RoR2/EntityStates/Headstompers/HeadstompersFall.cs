using System;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.Headstompers
{
	// Token: 0x02000330 RID: 816
	public class HeadstompersFall : BaseHeadstompersState
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				if (this.body)
				{
					TeamMask allButNeutral = TeamMask.allButNeutral;
					TeamIndex objectTeam = TeamComponent.GetObjectTeam(this.bodyGameObject);
					if (objectTeam != TeamIndex.None)
					{
						allButNeutral.RemoveTeam(objectTeam);
					}
					BullseyeSearch bullseyeSearch = new BullseyeSearch();
					bullseyeSearch.filterByLoS = true;
					bullseyeSearch.maxDistanceFilter = 300f;
					bullseyeSearch.maxAngleFilter = HeadstompersFall.seekCone;
					bullseyeSearch.searchOrigin = this.body.footPosition;
					bullseyeSearch.searchDirection = Vector3.down;
					bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
					bullseyeSearch.teamMaskFilter = allButNeutral;
					bullseyeSearch.viewer = this.body;
					this.initialY = this.body.footPosition.y;
					bullseyeSearch.RefreshCandidates();
					HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
					this.seekTransform = ((hurtBox != null) ? hurtBox.transform : null);
				}
				this.SetOnHitGroundProviderAuthority(this.bodyMotor);
				if (this.bodyMotor)
				{
					this.bodyMotor.velocity.y = Mathf.Min(this.bodyMotor.velocity.y, -HeadstompersFall.initialFallSpeed);
				}
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003F418 File Offset: 0x0003D618
		private void SetOnHitGroundProviderAuthority(CharacterMotor newOnHitGroundProvider)
		{
			if (this.onHitGroundProvider != null)
			{
				this.onHitGroundProvider.onHitGroundAuthority -= this.OnMotorHitGroundAuthority;
			}
			this.onHitGroundProvider = newOnHitGroundProvider;
			if (this.onHitGroundProvider != null)
			{
				this.onHitGroundProvider.onHitGroundAuthority += this.OnMotorHitGroundAuthority;
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003F46A File Offset: 0x0003D66A
		public override void OnExit()
		{
			this.SetOnHitGroundProviderAuthority(null);
			base.OnExit();
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003F479 File Offset: 0x0003D679
		private void OnMotorHitGroundAuthority(ref CharacterMotor.HitGroundInfo hitGroundInfo)
		{
			this.DoStompExplosionAuthority();
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003F481 File Offset: 0x0003D681
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003F498 File Offset: 0x0003D698
		private void FixedUpdateAuthority()
		{
			this.stopwatch += Time.deltaTime;
			if (base.isGrounded)
			{
				this.DoStompExplosionAuthority();
				return;
			}
			if (this.stopwatch >= HeadstompersFall.maxFallDuration)
			{
				this.outer.SetNextState(new HeadstompersCooldown());
				return;
			}
			if (this.bodyMotor)
			{
				Vector3 velocity = this.bodyMotor.velocity;
				if (velocity.y > -HeadstompersFall.maxFallSpeed)
				{
					velocity.y = Mathf.MoveTowards(velocity.y, -HeadstompersFall.maxFallSpeed, HeadstompersFall.accelerationY * Time.deltaTime);
				}
				if (this.seekTransform && !this.seekLost)
				{
					Vector3 normalized = (this.seekTransform.position - this.body.footPosition).normalized;
					if (Vector3.Dot(Vector3.down, normalized) >= Mathf.Cos(HeadstompersFall.seekCone * 0.017453292f))
					{
						if (velocity.y < 0f)
						{
							Vector3 vector = normalized * -velocity.y;
							vector.y = 0f;
							Vector3 vector2 = velocity;
							vector2.y = 0f;
							vector2 = vector;
							velocity.x = vector2.x;
							velocity.z = vector2.z;
						}
					}
					else
					{
						this.seekLost = true;
					}
				}
				this.bodyMotor.velocity = velocity;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003F5F8 File Offset: 0x0003D7F8
		private void DoStompExplosionAuthority()
		{
			if (this.body)
			{
				Inventory inventory = this.body.inventory;
				if ((inventory ? inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1) > 0)
				{
					this.bodyMotor.velocity = Vector3.zero;
					float num = Mathf.Max(0f, this.initialY - this.body.footPosition.y);
					if (num > 0f)
					{
						Debug.Log(string.Format("Fallboots distance: {0}", num));
						float t = Mathf.InverseLerp(0f, HeadstompersFall.maxDistance, num);
						float num2 = Mathf.Lerp(HeadstompersFall.minimumDamageCoefficient, HeadstompersFall.maximumDamageCoefficient, t);
						float num3 = Mathf.Lerp(HeadstompersFall.minimumRadius, HeadstompersFall.maximumRadius, t);
						BlastAttack blastAttack = new BlastAttack();
						blastAttack.attacker = this.body.gameObject;
						blastAttack.inflictor = this.body.gameObject;
						blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
						blastAttack.position = this.body.footPosition;
						blastAttack.procCoefficient = 1f;
						blastAttack.radius = num3;
						blastAttack.baseForce = 200f * num2;
						blastAttack.bonusForce = Vector3.up * 2000f;
						blastAttack.baseDamage = this.body.damage * num2;
						blastAttack.falloffModel = BlastAttack.FalloffModel.SweetSpot;
						blastAttack.crit = Util.CheckRoll(this.body.crit, this.body.master);
						blastAttack.damageColorIndex = DamageColorIndex.Item;
						blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
						blastAttack.Fire();
						EffectData effectData = new EffectData();
						effectData.origin = this.body.footPosition;
						effectData.scale = num3;
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/BootShockwave"), effectData, true);
					}
				}
			}
			this.SetOnHitGroundProviderAuthority(null);
			this.outer.SetNextState(new HeadstompersCooldown());
		}

		// Token: 0x0400125B RID: 4699
		private float stopwatch;

		// Token: 0x0400125C RID: 4700
		public static float maxFallDuration = 0f;

		// Token: 0x0400125D RID: 4701
		public static float maxFallSpeed = 30f;

		// Token: 0x0400125E RID: 4702
		public static float maxDistance = 30f;

		// Token: 0x0400125F RID: 4703
		public static float initialFallSpeed = 10f;

		// Token: 0x04001260 RID: 4704
		public static float accelerationY = 40f;

		// Token: 0x04001261 RID: 4705
		public static float minimumRadius = 5f;

		// Token: 0x04001262 RID: 4706
		public static float maximumRadius = 100f;

		// Token: 0x04001263 RID: 4707
		public static float minimumDamageCoefficient = 10f;

		// Token: 0x04001264 RID: 4708
		public static float maximumDamageCoefficient = 100f;

		// Token: 0x04001265 RID: 4709
		public static float seekCone = 20f;

		// Token: 0x04001266 RID: 4710
		public static float springboardSpeed = 30f;

		// Token: 0x04001267 RID: 4711
		private Transform seekTransform;

		// Token: 0x04001268 RID: 4712
		private bool seekLost;

		// Token: 0x04001269 RID: 4713
		private CharacterMotor onHitGroundProvider;

		// Token: 0x0400126A RID: 4714
		private float initialY;
	}
}
