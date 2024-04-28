using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004FB RID: 1275
	public class CharacterLosTracker : IDisposable
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0006658F File Offset: 0x0006478F
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00066597 File Offset: 0x00064797
		public bool enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled == value)
				{
					return;
				}
				this._enabled = value;
				if (this._enabled)
				{
					this.OnEnable();
					return;
				}
				this.OnDisable();
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000665C0 File Offset: 0x000647C0
		private void OnEnable()
		{
			CharacterBody.onBodyAwakeGlobal += this.OnBodyAwakeGlobal;
			CharacterBody.onBodyDestroyGlobal += this.OnBodyDestroyGlobal;
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				this.OnBodyDiscovered(readOnlyInstancesList[i]);
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00066614 File Offset: 0x00064814
		private void OnDisable()
		{
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				this.OnBodyLost(readOnlyInstancesList[i]);
			}
			CharacterBody.onBodyDestroyGlobal -= this.OnBodyDestroyGlobal;
			CharacterBody.onBodyAwakeGlobal -= this.OnBodyAwakeGlobal;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00066667 File Offset: 0x00064867
		private void OnBodyAwakeGlobal(CharacterBody characterBody)
		{
			this.OnBodyDiscovered(characterBody);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00066670 File Offset: 0x00064870
		private void OnBodyDestroyGlobal(CharacterBody characterBody)
		{
			this.OnBodyLost(characterBody);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0006667C File Offset: 0x0006487C
		public void Step()
		{
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			if (readOnlyInstancesList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.maxRaycastsPerStep; i++)
			{
				int num = this.currentCheckedBodyIndex + 1;
				this.currentCheckedBodyIndex = num;
				if (num >= readOnlyInstancesList.Count)
				{
					this.currentCheckedBodyIndex = 0;
				}
				CharacterBody characterBody = readOnlyInstancesList[this.currentCheckedBodyIndex];
				if (characterBody.mainHurtBox)
				{
					Vector3 position = characterBody.mainHurtBox.transform.position;
					CharacterLosTracker.BodyInfo value = this.bodyToBodyInfo[characterBody];
					RaycastHit raycastHit;
					bool flag = Physics.Linecast(this.origin, position, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
					value.hasLos = !flag;
					this.bodyToBodyInfo[characterBody] = value;
				}
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0006674C File Offset: 0x0006494C
		public bool CheckBodyHasLos(CharacterBody characterBody)
		{
			CharacterLosTracker.BodyInfo bodyInfo;
			return this.bodyToBodyInfo.TryGetValue(characterBody, out bodyInfo) && bodyInfo.hasLos;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00066771 File Offset: 0x00064971
		public void Dispose()
		{
			this.enabled = false;
			this.bodyToBodyInfo.Clear();
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00066788 File Offset: 0x00064988
		private void OnBodyDiscovered(CharacterBody characterBody)
		{
			this.bodyToBodyInfo.Add(characterBody, new CharacterLosTracker.BodyInfo
			{
				hasLos = false,
				lastChecked = Run.FixedTimeStamp.now
			});
			try
			{
				Action<CharacterBody> action = this.onBodyDiscovered;
				if (action != null)
				{
					action(characterBody);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000667EC File Offset: 0x000649EC
		private void OnBodyLost(CharacterBody characterBody)
		{
			this.bodyToBodyInfo.Remove(characterBody);
			try
			{
				Action<CharacterBody> action = this.onBodyLost;
				if (action != null)
				{
					action(characterBody);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600173B RID: 5947 RVA: 0x00066834 File Offset: 0x00064A34
		// (remove) Token: 0x0600173C RID: 5948 RVA: 0x0006686C File Offset: 0x00064A6C
		public event Action<CharacterBody> onBodyDiscovered;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600173D RID: 5949 RVA: 0x000668A4 File Offset: 0x00064AA4
		// (remove) Token: 0x0600173E RID: 5950 RVA: 0x000668DC File Offset: 0x00064ADC
		public event Action<CharacterBody> onBodyLost;

		// Token: 0x04001CE7 RID: 7399
		public Vector3 origin;

		// Token: 0x04001CE8 RID: 7400
		public int maxRaycastsPerStep = 4;

		// Token: 0x04001CE9 RID: 7401
		private Dictionary<CharacterBody, CharacterLosTracker.BodyInfo> bodyToBodyInfo = new Dictionary<CharacterBody, CharacterLosTracker.BodyInfo>();

		// Token: 0x04001CEA RID: 7402
		private bool _enabled;

		// Token: 0x04001CEB RID: 7403
		private int currentCheckedBodyIndex;

		// Token: 0x020004FC RID: 1276
		private struct BodyInfo
		{
			// Token: 0x04001CEE RID: 7406
			public bool hasLos;

			// Token: 0x04001CEF RID: 7407
			public Run.FixedTimeStamp lastChecked;
		}
	}
}
