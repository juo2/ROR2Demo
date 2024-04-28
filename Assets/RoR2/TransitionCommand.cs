using System;

namespace RoR2
{
	// Token: 0x02000978 RID: 2424
	public static class TransitionCommand
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000E7E0E File Offset: 0x000E600E
		// (set) Token: 0x0600370B RID: 14091 RVA: 0x000E7E15 File Offset: 0x000E6015
		public static bool requestPending { get; private set; }

		// Token: 0x0600370C RID: 14092 RVA: 0x000E7E20 File Offset: 0x000E6020
		private static void Update()
		{
			if (FadeToBlackManager.fullyFaded)
			{
				RoR2Application.onUpdate -= TransitionCommand.Update;
				TransitionCommand.requestPending = false;
				FadeToBlackManager.fadeCount--;
				string cmd = TransitionCommand.commandString;
				TransitionCommand.commandString = null;
				Console.instance.SubmitCmd(null, cmd, false);
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000E7E70 File Offset: 0x000E6070
		[ConCommand(commandName = "transition_command", flags = ConVarFlags.None, helpText = "Fade out and execute a command at the end of the fadeout.")]
		private static void CCTransitionCommand(ConCommandArgs args)
		{
			args.CheckArgumentCount(1);
			if (TransitionCommand.requestPending)
			{
				return;
			}
			TransitionCommand.requestPending = true;
			TransitionCommand.commandString = args[0];
			FadeToBlackManager.fadeCount++;
			RoR2Application.onUpdate += TransitionCommand.Update;
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000E7EBD File Offset: 0x000E60BD
		public static void ForceClearFadeToBlack()
		{
			RoR2Application.onUpdate -= TransitionCommand.Update;
			TransitionCommand.requestPending = false;
			TransitionCommand.commandString = null;
		}

		// Token: 0x04003754 RID: 14164
		private static float timer;

		// Token: 0x04003755 RID: 14165
		private static string commandString;
	}
}
