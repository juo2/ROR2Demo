using System;
using RoR2;

namespace EntityStates.LunarTeleporter
{
	// Token: 0x020002B3 RID: 691
	public class IdleToActive : LunarTeleporterBaseState
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0003393C File Offset: 0x00031B3C
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Base", "IdleToActive", "playbackRate", IdleToActive.duration);
			this.preferredInteractability = Interactability.Disabled;
			Util.PlaySound("Play_boss_spawn_rumble", base.gameObject);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00033976 File Offset: 0x00031B76
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > IdleToActive.duration)
			{
				this.outer.SetNextState(new Active());
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003399B File Offset: 0x00031B9B
		public override void OnExit()
		{
			Chat.SendBroadcastChat(new Chat.SimpleChatMessage
			{
				baseToken = "LUNAR_TELEPORTER_ACTIVE"
			});
			base.OnExit();
		}

		// Token: 0x04000EEA RID: 3818
		public static float duration;
	}
}
