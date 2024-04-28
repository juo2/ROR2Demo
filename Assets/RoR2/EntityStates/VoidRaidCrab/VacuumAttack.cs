using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using RoR2;
using RoR2.Audio;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200012A RID: 298
	public class VacuumAttack : BaseVacuumAttackState
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00016738 File Offset: 0x00014938
		public override void OnEnter()
		{
			base.OnEnter();
			this.losTracker = new CharacterLosTracker();
			this.losTracker.enabled = true;
			this.killSphereVfxHelper = VFXHelper.Rent();
			this.killSphereVfxHelper.vfxPrefabReference = VacuumAttack.killSphereVfxPrefab;
			this.killSphereVfxHelper.followedTransform = base.vacuumOrigin;
			this.killSphereVfxHelper.useFollowedTransformScale = false;
			this.killSphereVfxHelper.enabled = true;
			this.UpdateKillSphereVfx();
			this.environmentVfxHelper = VFXHelper.Rent();
			this.environmentVfxHelper.vfxPrefabReference = VacuumAttack.environmentVfxPrefab;
			this.environmentVfxHelper.followedTransform = base.vacuumOrigin;
			this.environmentVfxHelper.useFollowedTransformScale = false;
			this.environmentVfxHelper.enabled = true;
			this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, VacuumAttack.loopSound);
			if (NetworkServer.active)
			{
				this.killSearch = new SphereSearch();
			}
			this.jointBodyIndex = BodyCatalog.FindBodyIndex("VoidRaidCrabJointBody");
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016828 File Offset: 0x00014A28
		public override void OnExit()
		{
			this.killSphereVfxHelper = VFXHelper.Return(this.killSphereVfxHelper);
			this.environmentVfxHelper = VFXHelper.Return(this.environmentVfxHelper);
			this.losTracker.enabled = false;
			this.losTracker.Dispose();
			this.losTracker = null;
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			base.OnExit();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016888 File Offset: 0x00014A88
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float time = Mathf.Clamp01(base.fixedAge / base.duration);
			this.killRadius = VacuumAttack.killRadiusCurve.Evaluate(time);
			this.UpdateKillSphereVfx();
			Vector3 position = base.vacuumOrigin.position;
			this.losTracker.origin = position;
			this.losTracker.Step();
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			float num = VacuumAttack.pullMagnitudeCurve.Evaluate(time);
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CharacterBody characterBody = readOnlyInstancesList[i];
				if (characterBody != base.characterBody)
				{
					bool flag = this.losTracker.CheckBodyHasLos(characterBody);
					if (characterBody.hasEffectiveAuthority)
					{
						IDisplacementReceiver component = characterBody.GetComponent<IDisplacementReceiver>();
						if (component != null)
						{
							float num2 = flag ? 1f : VacuumAttack.losObstructionFactor;
							component.AddDisplacement((position - characterBody.transform.position).normalized * (num * num2 * Time.fixedDeltaTime));
						}
					}
				}
			}
			if (NetworkServer.active)
			{
				List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
				List<HealthComponent> list2 = CollectionPool<HealthComponent, List<HealthComponent>>.RentCollection();
				try
				{
					this.killSearch.radius = this.killRadius;
					this.killSearch.origin = position;
					this.killSearch.mask = LayerIndex.entityPrecise.mask;
					this.killSearch.RefreshCandidates();
					this.killSearch.OrderCandidatesByDistance();
					this.killSearch.FilterCandidatesByDistinctHurtBoxEntities();
					this.killSearch.GetHurtBoxes(list);
					for (int j = 0; j < list.Count; j++)
					{
						HurtBox hurtBox = list[j];
						if (hurtBox.healthComponent)
						{
							list2.Add(hurtBox.healthComponent);
						}
					}
					for (int k = 0; k < list2.Count; k++)
					{
						HealthComponent healthComponent = list2[k];
						if (base.healthComponent != healthComponent && healthComponent.body.bodyIndex != this.jointBodyIndex)
						{
							Debug.Log(string.Format("Destroying HealthComponent. healthComponent={0}({1}) victim={2}{3} bodyIndex={4} jointBodyIndex={5}", new object[]
							{
								base.healthComponent,
								base.healthComponent.GetInstanceID(),
								healthComponent,
								healthComponent.GetInstanceID(),
								base.healthComponent.body.bodyIndex,
								this.jointBodyIndex
							}));
							healthComponent.Suicide(base.gameObject, base.gameObject, DamageType.VoidDeath);
						}
					}
				}
				finally
				{
					list2 = CollectionPool<HealthComponent, List<HealthComponent>>.ReturnCollection(list2);
					list = CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016B4C File Offset: 0x00014D4C
		private void UpdateKillSphereVfx()
		{
			if (this.killSphereVfxHelper.vfxInstanceTransform)
			{
				this.killSphereVfxHelper.vfxInstanceTransform.localScale = Vector3.one * this.killRadius;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00016B80 File Offset: 0x00014D80
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new VacuumWindDown());
		}

		// Token: 0x04000627 RID: 1575
		public static AnimationCurve killRadiusCurve;

		// Token: 0x04000628 RID: 1576
		public static AnimationCurve pullMagnitudeCurve;

		// Token: 0x04000629 RID: 1577
		public static float losObstructionFactor = 0.5f;

		// Token: 0x0400062A RID: 1578
		public static GameObject killSphereVfxPrefab;

		// Token: 0x0400062B RID: 1579
		public static GameObject environmentVfxPrefab;

		// Token: 0x0400062C RID: 1580
		public static LoopSoundDef loopSound;

		// Token: 0x0400062D RID: 1581
		private CharacterLosTracker losTracker;

		// Token: 0x0400062E RID: 1582
		private VFXHelper killSphereVfxHelper;

		// Token: 0x0400062F RID: 1583
		private VFXHelper environmentVfxHelper;

		// Token: 0x04000630 RID: 1584
		private SphereSearch killSearch;

		// Token: 0x04000631 RID: 1585
		private float killRadius = 1f;

		// Token: 0x04000632 RID: 1586
		private BodyIndex jointBodyIndex = BodyIndex.None;

		// Token: 0x04000633 RID: 1587
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
