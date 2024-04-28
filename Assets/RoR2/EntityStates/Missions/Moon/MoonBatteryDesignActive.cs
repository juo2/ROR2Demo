using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000245 RID: 581
	public class MoonBatteryDesignActive : MoonBatteryActive
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002AAB9 File Offset: 0x00028CB9
		public override void OnEnter()
		{
			base.OnEnter();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.pulseTimer = 0f;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002AAD8 File Offset: 0x00028CD8
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.pulseTimer -= Time.fixedDeltaTime;
				if (this.pulseTimer < 0f)
				{
					this.pulseTimer = MoonBatteryDesignActive.pulseInterval;
					this.CreatePulseServer();
				}
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002AB20 File Offset: 0x00028D20
		private void CreatePulseServer()
		{
			MoonBatteryDesignActive.<>c__DisplayClass10_0 CS$<>8__locals1 = new MoonBatteryDesignActive.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			if (!base.FindModelChild("PulseOrigin"))
			{
				Transform transform = base.transform;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MoonBatteryDesignActive.pulsePrefab, base.transform.position, base.transform.rotation);
			CS$<>8__locals1.sphereSearch = new SphereSearch();
			PulseController component = gameObject.GetComponent<PulseController>();
			component.performSearch += CS$<>8__locals1.<CreatePulseServer>g__PerformPulseSearch|0;
			component.onPulseHit += MoonBatteryDesignActive.<>c.<>9.<CreatePulseServer>g__OnPulseHit|10_1;
			component.StartPulseServer();
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x04000C06 RID: 3078
		private TeamFilter teamFilter;

		// Token: 0x04000C07 RID: 3079
		public static GameObject pulsePrefab;

		// Token: 0x04000C08 RID: 3080
		public static float pulseInterval;

		// Token: 0x04000C09 RID: 3081
		public static BuffDef buffDef;

		// Token: 0x04000C0A RID: 3082
		public static float buffDuration;

		// Token: 0x04000C0B RID: 3083
		public static float baseForce;

		// Token: 0x04000C0C RID: 3084
		private float pulseTimer;
	}
}
