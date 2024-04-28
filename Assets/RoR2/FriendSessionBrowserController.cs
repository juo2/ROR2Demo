using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class FriendSessionBrowserController : MonoBehaviour
{
	// Token: 0x040001B3 RID: 435
	public SessionButtonController SessionButtonPrefab;

	// Token: 0x040001B4 RID: 436
	public Transform SessionButtonContainer;

	// Token: 0x040001B5 RID: 437
	public RectTransform InProgressSpinner;

	// Token: 0x040001B6 RID: 438
	private List<SessionButtonController> sessionButtons;
}
