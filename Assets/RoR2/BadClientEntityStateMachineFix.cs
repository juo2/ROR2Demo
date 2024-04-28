using System;
using System.Collections.Generic;
using EntityStates;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004D5 RID: 1237
	public static class BadClientEntityStateMachineFix
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x00062EFE File Offset: 0x000610FE
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			LocalUserManager.onUserSignIn += BadClientEntityStateMachineFix.OnUserSignIn;
			LocalUserManager.onUserSignOut += BadClientEntityStateMachineFix.OnUserSignOut;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00062F24 File Offset: 0x00061124
		private static void OnUserSignIn(LocalUser localUser)
		{
			BadClientEntityStateMachineFix.Watcher watcher = new BadClientEntityStateMachineFix.Watcher
			{
				localUser = localUser
			};
			BadClientEntityStateMachineFix.watchers[localUser] = watcher;
			watcher.OnInstall();
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00062F50 File Offset: 0x00061150
		private static void OnUserSignOut(LocalUser localUser)
		{
			BadClientEntityStateMachineFix.Watcher watcher = BadClientEntityStateMachineFix.watchers[localUser];
			watcher.OnUninstall();
			watcher.localUser = null;
			BadClientEntityStateMachineFix.watchers.Remove(localUser);
		}

		// Token: 0x04001C2B RID: 7211
		private static readonly Dictionary<LocalUser, BadClientEntityStateMachineFix.Watcher> watchers = new Dictionary<LocalUser, BadClientEntityStateMachineFix.Watcher>();

		// Token: 0x020004D6 RID: 1238
		private class Watcher
		{
			// Token: 0x06001678 RID: 5752 RVA: 0x00062F81 File Offset: 0x00061181
			public void OnInstall()
			{
				this.localUser.onBodyChanged += this.OnBodyChanged;
			}

			// Token: 0x06001679 RID: 5753 RVA: 0x00062F9C File Offset: 0x0006119C
			public void OnUninstall()
			{
				this.localUser.onBodyChanged -= this.OnBodyChanged;
				if (this.currentTimerHandle != null)
				{
					RoR2Application.fixedTimeTimers.RemoveTimer(this.currentTimerHandle.Value);
					this.currentTimerHandle = null;
				}
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x00062FF0 File Offset: 0x000611F0
			private void OnBodyChanged()
			{
				if (this.currentTimerHandle != null)
				{
					RoR2Application.fixedTimeTimers.RemoveTimer(this.currentTimerHandle.Value);
					this.currentTimerHandle = null;
				}
				if (this.localUser.cachedBody)
				{
					this.currentTimerHandle = new TimerQueue.TimerHandle?(RoR2Application.fixedTimeTimers.CreateTimer(1f, new Action(this.OnTimer)));
				}
			}

			// Token: 0x0600167B RID: 5755 RVA: 0x00063063 File Offset: 0x00061263
			private void OnTimer()
			{
				this.currentTimerHandle = null;
				BadClientEntityStateMachineFix.Watcher.ForceInitializeEntityStateMachines(this.localUser.cachedBody);
			}

			// Token: 0x0600167C RID: 5756 RVA: 0x00063084 File Offset: 0x00061284
			private static void ForceInitializeEntityStateMachines(CharacterBody characterBody)
			{
				if (!characterBody)
				{
					return;
				}
				EntityStateMachine[] components = characterBody.GetComponents<EntityStateMachine>();
				for (int i = 0; i < components.Length; i++)
				{
					EntityStateMachine entityStateMachine = components[i];
					if (!entityStateMachine.HasPendingState() && entityStateMachine.state is Uninitialized)
					{
						EntityState entityState = EntityStateCatalog.InstantiateState(entityStateMachine.mainStateType);
						if (entityState != null)
						{
							Debug.LogFormat("Setting {0} uninitialized state machine [{1}] next state to {2}", new object[]
							{
								characterBody.name,
								i,
								entityState.GetType().Name
							});
							entityStateMachine.SetInterruptState(entityState, InterruptPriority.Any);
						}
					}
				}
			}

			// Token: 0x04001C2C RID: 7212
			public LocalUser localUser;

			// Token: 0x04001C2D RID: 7213
			private TimerQueue.TimerHandle? currentTimerHandle;
		}
	}
}
