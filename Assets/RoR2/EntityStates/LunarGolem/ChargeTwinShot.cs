using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarGolem
{
	// Token: 0x020002B5 RID: 693
	public class ChargeTwinShot : BaseState
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x00033A34 File Offset: 0x00031C34
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeTwinShot.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			this.chargePlayID = Util.PlayAttackSpeedSound(ChargeTwinShot.chargeSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Gesture, Additive", "ChargeTwinShot", "TwinShot.playbackRate", this.duration, 0.1f);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					List<Transform> list = new List<Transform>();
					list.Add(component.FindChild("MuzzleLT"));
					list.Add(component.FindChild("MuzzleLB"));
					list.Add(component.FindChild("MuzzleRT"));
					list.Add(component.FindChild("MuzzleRB"));
					if (ChargeTwinShot.effectPrefab)
					{
						for (int i = 0; i < list.Count; i++)
						{
							if (list[i])
							{
								GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeTwinShot.effectPrefab, list[i].position, list[i].rotation);
								gameObject.transform.parent = list[i];
								ScaleParticleSystemDuration component2 = gameObject.GetComponent<ScaleParticleSystemDuration>();
								if (component2)
								{
									component2.newDuration = this.duration;
								}
								this.chargeEffects.Add(gameObject);
							}
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00033BB4 File Offset: 0x00031DB4
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.chargePlayID);
			base.OnExit();
			for (int i = 0; i < this.chargeEffects.Count; i++)
			{
				if (this.chargeEffects[i])
				{
					EntityState.Destroy(this.chargeEffects[i]);
				}
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00033C0C File Offset: 0x00031E0C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireTwinShots nextState = new FireTwinShots();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000EEC RID: 3820
		public static float baseDuration = 3f;

		// Token: 0x04000EED RID: 3821
		public static float laserMaxWidth = 0.2f;

		// Token: 0x04000EEE RID: 3822
		public static GameObject effectPrefab;

		// Token: 0x04000EEF RID: 3823
		public static string chargeSoundString;

		// Token: 0x04000EF0 RID: 3824
		private float duration;

		// Token: 0x04000EF1 RID: 3825
		private uint chargePlayID;

		// Token: 0x04000EF2 RID: 3826
		private List<GameObject> chargeEffects = new List<GameObject>();
	}
}
