using System;
using RoR2;

namespace EntityStates.LunarTeleporter
{
	// Token: 0x020002B4 RID: 692
	public class ActiveToIdle : LunarTeleporterBaseState
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x000339B8 File Offset: 0x00031BB8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Base", "ActiveToIdle", "playbackRate", ActiveToIdle.duration);
			this.preferredInteractability = Interactability.Disabled;
			Util.PlaySound("Play_boss_spawn_rumble", base.gameObject);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000339F2 File Offset: 0x00031BF2
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > ActiveToIdle.duration)
			{
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00033A17 File Offset: 0x00031C17
		public override void OnExit()
		{
			Chat.SendBroadcastChat(new Chat.SimpleChatMessage
			{
				baseToken = "LUNAR_TELEPORTER_IDLE"
			});
			base.OnExit();
		}

		// Token: 0x04000EEB RID: 3819
		public static float duration;
	}
}
