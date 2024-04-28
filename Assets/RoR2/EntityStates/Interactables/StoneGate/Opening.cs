using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Interactables.StoneGate
{
	// Token: 0x020002EF RID: 751
	public class Opening : BaseState
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x00038A90 File Offset: 0x00036C90
		public override void OnEnter()
		{
			base.OnEnter();
			this.childLocator = base.GetComponent<ChildLocator>();
			this.childLocator.FindChild(Opening.doorBeginOpenEffectChildLocatorEntry).gameObject.SetActive(true);
			Util.PlaySound(Opening.doorBeginOpenSoundString, base.gameObject);
			if (NetworkServer.active)
			{
				Chat.SendBroadcastChat(new Chat.SimpleChatMessage
				{
					baseToken = "STONEGATE_OPEN"
				});
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00038AF7 File Offset: 0x00036CF7
		public override void Update()
		{
			base.Update();
			this.UpdateGateTransform(ref this.leftGateTransform, Opening.leftGateChildLocatorEntry);
			this.UpdateGateTransform(ref this.rightGateTransform, Opening.rightGateChildLocatorEntry);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00038B24 File Offset: 0x00036D24
		private void UpdateGateTransform(ref Transform gateTransform, string childLocatorString)
		{
			if (!gateTransform)
			{
				gateTransform = this.childLocator.FindChild(childLocatorString);
				return;
			}
			Vector3 localPosition = gateTransform.localPosition;
			gateTransform.localPosition = new Vector3(localPosition.x, localPosition.y, Opening.doorPositionCurve.Evaluate(base.age / Opening.duration));
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00038B80 File Offset: 0x00036D80
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Opening.duration && !this.doorIsOpen)
			{
				this.doorIsOpen = true;
				Util.PlaySound(Opening.doorFinishedOpenSoundString, base.gameObject);
				this.childLocator.FindChild(Opening.doorBeginOpenEffectChildLocatorEntry).gameObject.SetActive(false);
				this.childLocator.FindChild(Opening.doorFinishedOpenEffectChildLocatorEntry).gameObject.SetActive(true);
			}
		}

		// Token: 0x0400106D RID: 4205
		public static string leftGateChildLocatorEntry;

		// Token: 0x0400106E RID: 4206
		public static string rightGateChildLocatorEntry;

		// Token: 0x0400106F RID: 4207
		public static AnimationCurve doorPositionCurve;

		// Token: 0x04001070 RID: 4208
		public static float duration;

		// Token: 0x04001071 RID: 4209
		public static string doorBeginOpenEffectChildLocatorEntry;

		// Token: 0x04001072 RID: 4210
		public static string doorBeginOpenSoundString;

		// Token: 0x04001073 RID: 4211
		public static string doorFinishedOpenEffectChildLocatorEntry;

		// Token: 0x04001074 RID: 4212
		public static string doorFinishedOpenSoundString;

		// Token: 0x04001075 RID: 4213
		private ChildLocator childLocator;

		// Token: 0x04001076 RID: 4214
		private bool doorIsOpen;

		// Token: 0x04001077 RID: 4215
		private Transform leftGateTransform;

		// Token: 0x04001078 RID: 4216
		private Transform rightGateTransform;
	}
}
