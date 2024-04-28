using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A4 RID: 932
	public class EngiOtherShield : BaseState
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x00048F54 File Offset: 0x00047154
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.target)
			{
				this.indicator = new Indicator(base.gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiShieldRetractIndicator"));
				this.indicator.active = true;
				this.indicator.targetTransform = Util.GetCoreTransform(this.target.gameObject);
				this.target.AddBuff(RoR2Content.Buffs.EngiShield);
				this.target.RecalculateStats();
				HealthComponent component = this.target.GetComponent<HealthComponent>();
				if (component)
				{
					component.RechargeShieldFull();
				}
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00048FEB File Offset: 0x000471EB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.target || !base.characterBody.healthComponent.alive)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00049020 File Offset: 0x00047220
		public override void OnExit()
		{
			base.skillLocator.utility = base.skillLocator.FindSkill("GiveShield");
			if (NetworkServer.active && this.target)
			{
				this.target.RemoveBuff(RoR2Content.Buffs.EngiShield);
			}
			if (base.isAuthority)
			{
				base.skillLocator.utility.RemoveAllStocks();
			}
			if (this.indicator != null)
			{
				this.indicator.active = false;
			}
			base.OnExit();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004909E File Offset: 0x0004729E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge < this.minimumDuration)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000490B1 File Offset: 0x000472B1
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.target.gameObject);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000490CC File Offset: 0x000472CC
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			GameObject gameObject = reader.ReadGameObject();
			if (gameObject)
			{
				this.target = gameObject.GetComponent<CharacterBody>();
			}
		}

		// Token: 0x04001507 RID: 5383
		public CharacterBody target;

		// Token: 0x04001508 RID: 5384
		public float minimumDuration;

		// Token: 0x04001509 RID: 5385
		private Indicator indicator;
	}
}
