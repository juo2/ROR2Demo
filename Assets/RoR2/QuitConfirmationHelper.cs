using System;
using RoR2.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020009FD RID: 2557
	public static class QuitConfirmationHelper
	{
		// Token: 0x06003B21 RID: 15137 RVA: 0x000F4D94 File Offset: 0x000F2F94
		private static bool IsQuitConfirmationRequired()
		{
			return Run.instance && !GameOverController.instance;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000F4DB1 File Offset: 0x000F2FB1
		public static void IssueQuitCommand(NetworkUser sender, string consoleCmd)
		{
			QuitConfirmationHelper.<>c__DisplayClass2_0 CS$<>8__locals1 = new QuitConfirmationHelper.<>c__DisplayClass2_0();
			CS$<>8__locals1.sender = sender;
			CS$<>8__locals1.consoleCmd = consoleCmd;
			QuitConfirmationHelper.IssueQuitCommand(new Action(CS$<>8__locals1.<IssueQuitCommand>g__RunCmd|0));
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000F4DD8 File Offset: 0x000F2FD8
		public static void IssueQuitCommand(Action action)
		{
			if (!QuitConfirmationHelper.IsQuitConfirmationRequired())
			{
				action();
				return;
			}
			QuitConfirmationHelper.NetworkStatus networkStatus;
			if (NetworkUser.readOnlyInstancesList.Count <= NetworkUser.readOnlyLocalPlayersList.Count)
			{
				networkStatus = QuitConfirmationHelper.NetworkStatus.SinglePlayer;
			}
			else if (NetworkServer.active)
			{
				networkStatus = QuitConfirmationHelper.NetworkStatus.Host;
			}
			else
			{
				networkStatus = QuitConfirmationHelper.NetworkStatus.Client;
			}
			string token;
			switch (networkStatus)
			{
			case QuitConfirmationHelper.NetworkStatus.None:
				token = "";
				break;
			case QuitConfirmationHelper.NetworkStatus.SinglePlayer:
				token = "QUIT_RUN_CONFIRM_DIALOG_BODY_SINGLEPLAYER";
				break;
			case QuitConfirmationHelper.NetworkStatus.Client:
				token = "QUIT_RUN_CONFIRM_DIALOG_BODY_CLIENT";
				break;
			case QuitConfirmationHelper.NetworkStatus.Host:
				token = "QUIT_RUN_CONFIRM_DIALOG_BODY_HOST";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
			simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair("QUIT_RUN_CONFIRM_DIALOG_TITLE", Array.Empty<object>());
			simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair(token, Array.Empty<object>());
			simpleDialogBox.AddActionButton(new UnityAction(action.Invoke), "DIALOG_OPTION_YES", true, Array.Empty<object>());
			simpleDialogBox.AddCancelButton("CANCEL", Array.Empty<object>());
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000F4EBA File Offset: 0x000F30BA
		[ConCommand(commandName = "quit_confirmed_command", flags = ConVarFlags.None, helpText = "Runs the command provided in the argument only if the user confirms they want to quit the current game via dialog UI.")]
		private static void CCQuitConfirmedCommand(ConCommandArgs args)
		{
			QuitConfirmationHelper.<>c__DisplayClass4_0 CS$<>8__locals1 = new QuitConfirmationHelper.<>c__DisplayClass4_0();
			CS$<>8__locals1.sender = args.sender;
			CS$<>8__locals1.consoleCmd = args[0];
			QuitConfirmationHelper.IssueQuitCommand(new Action(CS$<>8__locals1.<CCQuitConfirmedCommand>g__RunCmd|0));
		}

		// Token: 0x020009FE RID: 2558
		private enum NetworkStatus
		{
			// Token: 0x040039D7 RID: 14807
			None,
			// Token: 0x040039D8 RID: 14808
			SinglePlayer,
			// Token: 0x040039D9 RID: 14809
			Client,
			// Token: 0x040039DA RID: 14810
			Host
		}
	}
}
