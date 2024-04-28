using System;
using System.Text;
using EntityStates.TimedChest;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008C8 RID: 2248
	public sealed class TimedChestController : NetworkBehaviour, IInteractable
	{
		// Token: 0x06003254 RID: 12884 RVA: 0x000D4803 File Offset: 0x000D2A03
		public string GetContextString(Interactor activator)
		{
			return Language.GetString(this.contextString);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000D4810 File Offset: 0x000D2A10
		public Interactability GetInteractability(Interactor activator)
		{
			if (this.purchased)
			{
				return Interactability.Disabled;
			}
			if (!this.locked)
			{
				return Interactability.Available;
			}
			return Interactability.ConditionsNotMet;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000D4827 File Offset: 0x000D2A27
		public void OnInteractionBegin(Interactor activator)
		{
			base.GetComponent<EntityStateMachine>().SetNextState(new Opening());
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000D483C File Offset: 0x000D2A3C
		private int remainingTime
		{
			get
			{
				float num = 0f;
				if (Run.instance)
				{
					num = Run.instance.GetRunStopwatch();
				}
				return (int)(this.lockTime - num);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000D486F File Offset: 0x000D2A6F
		private bool locked
		{
			get
			{
				return this.remainingTime <= 0;
			}
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000D4880 File Offset: 0x000D2A80
		public void FixedUpdate()
		{
			if (NetworkClient.active)
			{
				if (!this.purchased)
				{
					int num = this.remainingTime;
					bool flag = num >= 0;
					bool flag2 = true;
					if (num < -599)
					{
						flag2 = ((num & 1) != 0);
						num = -599;
					}
					int num2 = flag ? num : (-num);
					uint num3 = (uint)(num2 / 60);
					uint value = (uint)(num2 - (int)(num3 * 60U));
					TimedChestController.sharedStringBuilder.Clear();
					TimedChestController.sharedStringBuilder.Append("<mspace=0.75em>");
					if (flag2)
					{
						uint num4 = 2U;
						if (!flag)
						{
							TimedChestController.sharedStringBuilder.Append("-");
							num4 = 1U;
						}
						TimedChestController.sharedStringBuilder.AppendUint(num3, num4, num4);
						TimedChestController.sharedStringBuilder.Append(":");
						TimedChestController.sharedStringBuilder.AppendUint(value, 2U, 2U);
					}
					else
					{
						TimedChestController.sharedStringBuilder.Append("--:--");
					}
					TimedChestController.sharedStringBuilder.Append("</mspace>");
					this.displayTimer.SetText(TimedChestController.sharedStringBuilder);
					this.displayTimer.color = (this.locked ? this.displayIsLockedColor : this.displayIsAvailableColor);
					this.displayTimer.SetText(TimedChestController.sharedStringBuilder);
					this.displayScaleCurve.enabled = false;
					return;
				}
				this.displayScaleCurve.enabled = true;
			}
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000D49BE File Offset: 0x000D2BBE
		private void OnEnable()
		{
			InstanceTracker.Add<TimedChestController>(this);
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000D49C6 File Offset: 0x000D2BC6
		private void OnDisable()
		{
			InstanceTracker.Remove<TimedChestController>(this);
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000D49CE File Offset: 0x000D2BCE
		public bool ShouldShowOnScanner()
		{
			return !this.purchased;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000D49F8 File Offset: 0x000D2BF8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003376 RID: 13174
		public float lockTime = 600f;

		// Token: 0x04003377 RID: 13175
		public TextMeshPro displayTimer;

		// Token: 0x04003378 RID: 13176
		public ObjectScaleCurve displayScaleCurve;

		// Token: 0x04003379 RID: 13177
		public string contextString;

		// Token: 0x0400337A RID: 13178
		public Color displayIsAvailableColor;

		// Token: 0x0400337B RID: 13179
		public Color displayIsLockedColor;

		// Token: 0x0400337C RID: 13180
		public bool purchased;

		// Token: 0x0400337D RID: 13181
		private const int minTime = -599;

		// Token: 0x0400337E RID: 13182
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
