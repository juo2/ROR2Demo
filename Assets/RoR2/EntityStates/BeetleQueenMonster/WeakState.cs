using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x0200046A RID: 1130
	public class WeakState : BaseState
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x0005A230 File Offset: 0x00058430
		public override void OnEnter()
		{
			base.OnEnter();
			this.grubStopwatch -= WeakState.grubSpawnDelay;
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			this.PlayAnimation("Body", "WeakEnter");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					Transform transform = this.childLocator.FindChild(WeakState.weakPointChildString);
					if (transform)
					{
						UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/WeakPointProcEffect"), transform.position, transform.rotation);
					}
				}
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0005A300 File Offset: 0x00058500
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.grubStopwatch += Time.fixedDeltaTime;
			if (this.grubStopwatch >= 1f / WeakState.grubSpawnFrequency && this.grubCount < WeakState.maxGrubCount)
			{
				this.grubCount++;
				this.grubStopwatch -= 1f / WeakState.grubSpawnFrequency;
				if (NetworkServer.active)
				{
					Transform transform = this.childLocator.FindChild("GrubSpawnPoint" + UnityEngine.Random.Range(1, 10));
					if (transform)
					{
						NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GrubPack"), transform.transform.position, UnityEngine.Random.rotation));
						transform.gameObject.SetActive(false);
					}
				}
			}
			if (this.stopwatch >= WeakState.weakDuration - WeakState.weakToIdleTransitionDuration && !this.beginExitTransition)
			{
				this.beginExitTransition = true;
				base.PlayCrossfade("Body", "WeakExit", "WeakExit.playbackRate", WeakState.weakToIdleTransitionDuration, 0.5f);
			}
			if (this.stopwatch >= WeakState.weakDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x040019F2 RID: 6642
		private float stopwatch;

		// Token: 0x040019F3 RID: 6643
		private float grubStopwatch;

		// Token: 0x040019F4 RID: 6644
		public static float weakDuration;

		// Token: 0x040019F5 RID: 6645
		public static float weakToIdleTransitionDuration;

		// Token: 0x040019F6 RID: 6646
		public static string weakPointChildString;

		// Token: 0x040019F7 RID: 6647
		public static int maxGrubCount;

		// Token: 0x040019F8 RID: 6648
		public static float grubSpawnFrequency;

		// Token: 0x040019F9 RID: 6649
		public static float grubSpawnDelay;

		// Token: 0x040019FA RID: 6650
		private int grubCount;

		// Token: 0x040019FB RID: 6651
		private bool beginExitTransition;

		// Token: 0x040019FC RID: 6652
		private ChildLocator childLocator;
	}
}
