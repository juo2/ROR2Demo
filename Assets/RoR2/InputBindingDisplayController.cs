using System;
using System.Collections.Generic;
using System.Text;
using Rewired;
using RoR2.UI;
using TMPro;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008D4 RID: 2260
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class InputBindingDisplayController : MonoBehaviour
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x000D5983 File Offset: 0x000D3B83
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.guiLabel = base.GetComponent<TextMeshProUGUI>();
			this.label = base.GetComponent<TextMeshPro>();
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000D59A9 File Offset: 0x000D3BA9
		private void OnEnable()
		{
			InputBindingDisplayController.instances.Add(this);
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000D59B6 File Offset: 0x000D3BB6
		private void OnDisable()
		{
			InputBindingDisplayController.instances.Remove(this);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000D59C4 File Offset: 0x000D3BC4
		public void Refresh(bool forceRefresh = false)
		{
			MPEventSystemLocator mpeventSystemLocator = this.eventSystemLocator;
			MPEventSystem mpeventSystem = (mpeventSystemLocator != null) ? mpeventSystemLocator.eventSystem : null;
			if (!mpeventSystem)
			{
				Debug.LogError("MPEventSystem is invalid.");
				return;
			}
			if (!forceRefresh && mpeventSystem == this.lastEventSystem && mpeventSystem.currentInputSource == this.lastInputSource)
			{
				return;
			}
			if (this.useExplicitInputSource)
			{
				InputBindingDisplayController.sharedStringBuilder.Clear();
				InputBindingDisplayController.sharedStringBuilder.Append(Glyphs.GetGlyphString(this.eventSystemLocator.eventSystem, this.actionName, this.axisRange, this.explicitInputSource));
			}
			else
			{
				InputBindingDisplayController.sharedStringBuilder.Clear();
				InputBindingDisplayController.sharedStringBuilder.Append(Glyphs.GetGlyphString(this.eventSystemLocator.eventSystem, this.actionName, AxisRange.Full));
			}
			if (this.guiLabel)
			{
				this.guiLabel.SetText(InputBindingDisplayController.sharedStringBuilder);
			}
			else if (this.label)
			{
				this.label.SetText(InputBindingDisplayController.sharedStringBuilder);
			}
			this.lastEventSystem = mpeventSystem;
			this.lastInputSource = mpeventSystem.currentInputSource;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000D5AD5 File Offset: 0x000D3CD5
		private void Update()
		{
			this.Refresh(false);
		}

		// Token: 0x040033BD RID: 13245
		public string actionName;

		// Token: 0x040033BE RID: 13246
		public AxisRange axisRange;

		// Token: 0x040033BF RID: 13247
		public bool useExplicitInputSource;

		// Token: 0x040033C0 RID: 13248
		public MPEventSystem.InputSource explicitInputSource;

		// Token: 0x040033C1 RID: 13249
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040033C2 RID: 13250
		private TextMeshProUGUI guiLabel;

		// Token: 0x040033C3 RID: 13251
		private TextMeshPro label;

		// Token: 0x040033C4 RID: 13252
		private MPEventSystem lastEventSystem;

		// Token: 0x040033C5 RID: 13253
		private MPEventSystem.InputSource lastInputSource;

		// Token: 0x040033C6 RID: 13254
		public static readonly List<InputBindingDisplayController> instances = new List<InputBindingDisplayController>();

		// Token: 0x040033C7 RID: 13255
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
