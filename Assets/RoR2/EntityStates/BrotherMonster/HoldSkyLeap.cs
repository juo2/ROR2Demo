using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000439 RID: 1081
	public class HoldSkyLeap : BaseState
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x00056710 File Offset: 0x00054910
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.characterModel = modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = modelTransform.GetComponent<HurtBoxGroup>();
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount++;
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			Util.PlaySound("Play_moonBrother_phaseJump_land_preWhoosh", base.gameObject);
			base.gameObject.layer = LayerIndex.fakeActor.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			if (SceneInfo.instance)
			{
				ChildLocator component = SceneInfo.instance.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("CenterOfArena");
					if (transform)
					{
						base.characterMotor.Motor.SetPositionAndRotation(transform.position, Quaternion.identity, true);
					}
				}
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00056821 File Offset: 0x00054A21
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > HoldSkyLeap.duration)
			{
				this.outer.SetNextState(new ExitSkyLeap());
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00056850 File Offset: 0x00054A50
		public override void OnExit()
		{
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount--;
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			base.OnExit();
		}

		// Token: 0x040018E6 RID: 6374
		public static float duration;

		// Token: 0x040018E7 RID: 6375
		private CharacterModel characterModel;

		// Token: 0x040018E8 RID: 6376
		private HurtBoxGroup hurtboxGroup;
	}
}
