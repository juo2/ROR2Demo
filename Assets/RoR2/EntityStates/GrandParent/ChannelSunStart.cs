using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GrandParent
{
	// Token: 0x0200035B RID: 859
	public class ChannelSunStart : ChannelSunBase
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x000439B4 File Offset: 0x00041BB4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChannelSunStart.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(ChannelSunStart.animLayerName, ChannelSunStart.animStateName, ChannelSunStart.animPlaybackRateParam, this.duration);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				AimAnimator component = modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
			if (base.isAuthority)
			{
				Vector3? vector = this.sunSpawnPosition;
				this.sunSpawnPosition = ((vector != null) ? vector : ChannelSun.FindSunSpawnPosition(base.transform.position));
			}
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator && this.sunSpawnPosition != null)
			{
				this.CreateBeamEffect(modelChildLocator, ChannelSunBase.leftHandVfxTargetNameInChildLocator, this.sunSpawnPosition.Value, ref this.leftHandBeamParticleSystem);
				this.CreateBeamEffect(modelChildLocator, ChannelSunBase.rightHandVfxTargetNameInChildLocator, this.sunSpawnPosition.Value, ref this.rightHandBeamParticleSystem);
			}
			if (ChannelSunStart.beginSoundName != null)
			{
				Util.PlaySound(ChannelSunStart.beginSoundName, base.gameObject);
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00043AB8 File Offset: 0x00041CB8
		public override void OnExit()
		{
			this.EndBeamEffect(ref this.leftHandBeamParticleSystem);
			this.EndBeamEffect(ref this.rightHandBeamParticleSystem);
			base.OnExit();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00043AD8 File Offset: 0x00041CD8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new ChannelSun
				{
					activatorSkillSlot = base.activatorSkillSlot,
					sunSpawnPosition = this.sunSpawnPosition
				});
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00043B29 File Offset: 0x00041D29
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.sunSpawnPosition != null);
			if (this.sunSpawnPosition != null)
			{
				writer.Write(this.sunSpawnPosition.Value);
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00043B61 File Offset: 0x00041D61
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.sunSpawnPosition = null;
			if (reader.ReadBoolean())
			{
				this.sunSpawnPosition = new Vector3?(reader.ReadVector3());
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00043B90 File Offset: 0x00041D90
		private void CreateBeamEffect(ChildLocator childLocator, string nameInChildLocator, Vector3 targetPosition, ref ParticleSystem dest)
		{
			Transform transform = childLocator.FindChild(nameInChildLocator);
			if (transform)
			{
				ChildLocator component = UnityEngine.Object.Instantiate<GameObject>(ChannelSunStart.beamVfxPrefab, transform).GetComponent<ChildLocator>();
				component.FindChild("EndPoint").SetPositionAndRotation(targetPosition, Quaternion.identity);
				Transform transform2 = component.FindChild("BeamParticles");
				dest = transform2.GetComponent<ParticleSystem>();
				return;
			}
			dest = null;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00043BEC File Offset: 0x00041DEC
		private void EndBeamEffect(ref ParticleSystem particleSystem)
		{
			if (particleSystem)
			{
				particleSystem.main.loop = false;
				particleSystem.Stop();
			}
			particleSystem = null;
		}

		// Token: 0x04001390 RID: 5008
		public static string animLayerName;

		// Token: 0x04001391 RID: 5009
		public static string animStateName;

		// Token: 0x04001392 RID: 5010
		public static string animPlaybackRateParam;

		// Token: 0x04001393 RID: 5011
		public static string beginSoundName;

		// Token: 0x04001394 RID: 5012
		public static float baseDuration;

		// Token: 0x04001395 RID: 5013
		public static GameObject beamVfxPrefab;

		// Token: 0x04001396 RID: 5014
		private float duration;

		// Token: 0x04001397 RID: 5015
		private Vector3? sunSpawnPosition;

		// Token: 0x04001398 RID: 5016
		private ParticleSystem leftHandBeamParticleSystem;

		// Token: 0x04001399 RID: 5017
		private ParticleSystem rightHandBeamParticleSystem;
	}
}
