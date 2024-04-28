using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

namespace EntityStates.Missions.LunarScavengerEncounter
{
	// Token: 0x0200024A RID: 586
	public class FadeOut : BaseState
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0002AE48 File Offset: 0x00029048
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.startTime = Run.TimeStamp.now + FadeOut.delay;
			}
			this.light = base.GetComponent<ChildLocator>().FindChild("PrimaryLight").GetComponent<Light>();
			this.initialIntensity = this.light.intensity;
			this.initialAmbientIntensity = RenderSettings.ambientIntensity;
			this.initialAmbientColor = RenderSettings.ambientLight;
			this.initialFogColor = RenderSettings.fogColor;
			this.light.GetComponent<FlickerLight>().enabled = false;
			this.postProcessVolume = base.GetComponent<PostProcessVolume>();
			this.postProcessVolume.enabled = true;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002AEED File Offset: 0x000290ED
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.startTime);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002AF02 File Offset: 0x00029102
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.startTime = reader.ReadTimeStamp();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002AF18 File Offset: 0x00029118
		public override void Update()
		{
			base.Update();
			float num = Mathf.Clamp01(this.startTime.timeSince / FadeOut.duration);
			num *= num;
			this.light.intensity = Mathf.Lerp(this.initialIntensity, 0f, num);
			RenderSettings.ambientIntensity = Mathf.Lerp(this.initialAmbientIntensity, 0f, num);
			RenderSettings.ambientLight = Color.Lerp(this.initialAmbientColor, Color.black, num);
			RenderSettings.fogColor = Color.Lerp(this.initialFogColor, Color.black, num);
			this.postProcessVolume.weight = num;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002AFB0 File Offset: 0x000291B0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
			if (this.startTime.timeSince > FadeOut.duration)
			{
				foreach (CharacterBody characterBody in CharacterBody.readOnlyInstancesList)
				{
					if (characterBody.hasEffectiveAuthority)
					{
						EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(characterBody.gameObject, "Body");
						if (entityStateMachine && !(entityStateMachine.state is Idle))
						{
							entityStateMachine.SetInterruptState(new Idle(), InterruptPriority.Frozen);
						}
					}
				}
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B058 File Offset: 0x00029258
		private void FixedUpdateServer()
		{
			if ((this.startTime + FadeOut.duration).hasPassed && !this.finished)
			{
				this.finished = true;
				Run.instance.BeginGameOver(RoR2Content.GameEndings.LimboEnding);
			}
		}

		// Token: 0x04000C13 RID: 3091
		public static float delay;

		// Token: 0x04000C14 RID: 3092
		public static float duration;

		// Token: 0x04000C15 RID: 3093
		private Run.TimeStamp startTime;

		// Token: 0x04000C16 RID: 3094
		private Light light;

		// Token: 0x04000C17 RID: 3095
		private float initialIntensity;

		// Token: 0x04000C18 RID: 3096
		private float initialAmbientIntensity;

		// Token: 0x04000C19 RID: 3097
		private Color initialAmbientColor;

		// Token: 0x04000C1A RID: 3098
		private Color initialFogColor;

		// Token: 0x04000C1B RID: 3099
		private PostProcessVolume postProcessVolume;

		// Token: 0x04000C1C RID: 3100
		private bool finished;
	}
}
