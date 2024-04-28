using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000D9C RID: 3484
	[RequireComponent(typeof(RectTransform))]
	public class TimerText : MonoBehaviour
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x0014A15E File Offset: 0x0014835E
		// (set) Token: 0x06004FC6 RID: 20422 RVA: 0x0014A166 File Offset: 0x00148366
		public TimerStringFormatter format
		{
			get
			{
				return this._format;
			}
			set
			{
				if (this._format == value)
				{
					return;
				}
				this._format = value;
				this.Rebuild();
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x0014A184 File Offset: 0x00148384
		// (set) Token: 0x06004FC8 RID: 20424 RVA: 0x0014A18C File Offset: 0x0014838C
		public double seconds
		{
			get
			{
				return this._seconds;
			}
			set
			{
				if (this._seconds == value)
				{
					return;
				}
				this._seconds = value;
				this.Rebuild();
			}
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0014A1A5 File Offset: 0x001483A5
		private void Awake()
		{
			this.Rebuild();
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0014A1B0 File Offset: 0x001483B0
		private void Rebuild()
		{
			if (this.targetLabel)
			{
				TimerText.sharedStringBuilder.Clear();
				if (this.format)
				{
					this.format.AppendToStringBuilder(this.seconds, TimerText.sharedStringBuilder);
				}
				this.targetLabel.SetText(TimerText.sharedStringBuilder);
			}
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0014A208 File Offset: 0x00148408
		private void OnValidate()
		{
			this.Rebuild();
			if (!this.targetLabel)
			{
				Debug.LogErrorFormat(this, "TimerText does not specify a target label.", Array.Empty<object>());
			}
		}

		// Token: 0x04004C63 RID: 19555
		[FormerlySerializedAs("targetText")]
		public TextMeshProUGUI targetLabel;

		// Token: 0x04004C64 RID: 19556
		[SerializeField]
		private TimerStringFormatter _format;

		// Token: 0x04004C65 RID: 19557
		[SerializeField]
		private double _seconds;

		// Token: 0x04004C66 RID: 19558
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
