using System;
using RoR2;
using RoR2.UI;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class EOSTogglePopup : MonoBehaviour
{
	// Token: 0x06000228 RID: 552 RVA: 0x00009E4C File Offset: 0x0000804C
	public void emitMessage()
	{
		SimpleDialogBox dialogBox = SimpleDialogBox.Create(null);
		Action deactiveCrossplayAndRestartFunction = delegate()
		{
			if (dialogBox)
			{
				RoR2.Console.instance.SubmitCmd(null, "quit", false);
			}
		};
		dialogBox.AddActionButton(delegate
		{
			deactiveCrossplayAndRestartFunction();
		}, this.closeNowButtonText, true, Array.Empty<object>());
		dialogBox.AddCancelButton(this.closeLaterButtonText, Array.Empty<object>());
		dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
		{
			token = this.titleText,
			formatParams = Array.Empty<object>()
		};
		dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
		{
			token = this.messageText,
			formatParams = Array.Empty<object>()
		};
	}

	// Token: 0x04000217 RID: 535
	public string titleText = "EOS_TOGGLE_POPUP_TITLE";

	// Token: 0x04000218 RID: 536
	public string messageText = "EOS_TOGGLE_POPUP_MESSAGE";

	// Token: 0x04000219 RID: 537
	public string closeNowButtonText = "EOS_TOGGLE_CLOSE_NOW";

	// Token: 0x0400021A RID: 538
	public string closeLaterButtonText = "EOS_TOGGLE_CLOSE_LATER";
}
