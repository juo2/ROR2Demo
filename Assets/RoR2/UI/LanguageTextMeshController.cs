using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D29 RID: 3369
	public class LanguageTextMeshController : MonoBehaviour
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x0013D301 File Offset: 0x0013B501
		// (set) Token: 0x06004CD3 RID: 19667 RVA: 0x0013D309 File Offset: 0x0013B509
		public string token
		{
			get
			{
				return this._token;
			}
			set
			{
				if (value != this.previousToken)
				{
					this._token = value;
					this.ResolveString();
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x0013D32C File Offset: 0x0013B52C
		// (set) Token: 0x06004CD5 RID: 19669 RVA: 0x0013D334 File Offset: 0x0013B534
		public object[] formatArgs
		{
			get
			{
				return this._formatArgs;
			}
			set
			{
				this._formatArgs = value;
				this.ResolveString();
				this.UpdateLabel();
			}
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0013D349 File Offset: 0x0013B549
		private void OnEnable()
		{
			this.ResolveString();
			this.UpdateLabel();
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0013D357 File Offset: 0x0013B557
		public void ResolveString()
		{
			this.previousToken = this._token;
			if (this.formatArgs.Length == 0)
			{
				this.resolvedString = Language.GetString(this._token);
				return;
			}
			this.resolvedString = Language.GetStringFormatted(this._token, this.formatArgs);
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0013D397 File Offset: 0x0013B597
		private void CacheComponents()
		{
			this.textMeshPro = base.GetComponent<TMP_Text>();
			if (!this.textMeshPro)
			{
				this.textMeshPro = base.GetComponentInChildren<TMP_Text>();
			}
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0013D3BE File Offset: 0x0013B5BE
		private void Awake()
		{
			this.CacheComponents();
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0013D3BE File Offset: 0x0013B5BE
		private void OnValidate()
		{
			this.CacheComponents();
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0013D349 File Offset: 0x0013B549
		private void Start()
		{
			this.ResolveString();
			this.UpdateLabel();
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0013D3C6 File Offset: 0x0013B5C6
		private void UpdateLabel()
		{
			if (this.textMeshPro)
			{
				this.textMeshPro.text = this.resolvedString;
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0013D3E6 File Offset: 0x0013B5E6
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			Language.onCurrentLanguageChanged += LanguageTextMeshController.OnCurrentLanguageChanged;
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0013D3FC File Offset: 0x0013B5FC
		private static void OnCurrentLanguageChanged()
		{
			foreach (LanguageTextMeshController languageTextMeshController in UnityEngine.Object.FindObjectsOfType<LanguageTextMeshController>())
			{
				languageTextMeshController.ResolveString();
				languageTextMeshController.UpdateLabel();
			}
		}

		// Token: 0x040049D4 RID: 18900
		[SerializeField]
		private string _token;

		// Token: 0x040049D5 RID: 18901
		private string previousToken;

		// Token: 0x040049D6 RID: 18902
		private string resolvedString;

		// Token: 0x040049D7 RID: 18903
		private TMP_Text textMeshPro;

		// Token: 0x040049D8 RID: 18904
		private object[] _formatArgs = Array.Empty<object>();
	}
}
