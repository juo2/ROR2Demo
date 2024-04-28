using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D85 RID: 3461
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class SimpleDialogBox : UIBehaviour
	{
		// Token: 0x06004F4F RID: 20303 RVA: 0x001480F9 File Offset: 0x001462F9
		protected override void OnEnable()
		{
			base.OnEnable();
			SimpleDialogBox.instancesList.Add(this);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0014810C File Offset: 0x0014630C
		protected override void OnDisable()
		{
			SimpleDialogBox.instancesList.Remove(this);
			base.OnDisable();
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00148120 File Offset: 0x00146320
		private static string GetString(SimpleDialogBox.TokenParamsPair pair)
		{
			string text = Language.GetString(pair.token);
			if (pair.formatParams != null && pair.formatParams.Length != 0)
			{
				text = string.Format(text, pair.formatParams);
			}
			return text;
		}

		// Token: 0x1700073A RID: 1850
		// (set) Token: 0x06004F52 RID: 20306 RVA: 0x00148158 File Offset: 0x00146358
		public SimpleDialogBox.TokenParamsPair headerToken
		{
			set
			{
				this.headerLabel.text = SimpleDialogBox.GetString(value);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (set) Token: 0x06004F53 RID: 20307 RVA: 0x0014816B File Offset: 0x0014636B
		public SimpleDialogBox.TokenParamsPair descriptionToken
		{
			set
			{
				this.descriptionLabel.text = SimpleDialogBox.GetString(value);
			}
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x00148180 File Offset: 0x00146380
		private MPButton AddButton(UnityAction callback, string displayToken, params object[] formatParams)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.buttonContainer);
			MPButton component = gameObject.GetComponent<MPButton>();
			string text = Language.GetString(displayToken);
			if (formatParams != null && formatParams.Length != 0)
			{
				text = string.Format(text, formatParams);
			}
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
			component.onClick.AddListener(callback);
			gameObject.SetActive(true);
			if (!this.defaultChoice)
			{
				this.defaultChoice = component;
			}
			if (this.buttonContainer.childCount > 1)
			{
				int siblingIndex = gameObject.transform.GetSiblingIndex();
				int num = siblingIndex - 1;
				int num2 = siblingIndex + 1;
				MPButton mpbutton = null;
				MPButton mpbutton2 = null;
				if (num > 0)
				{
					mpbutton = this.buttonContainer.GetChild(num).GetComponent<MPButton>();
				}
				if (num2 < this.buttonContainer.childCount)
				{
					mpbutton2 = this.buttonContainer.GetChild(num2).GetComponent<MPButton>();
				}
				if (mpbutton)
				{
					Navigation navigation = mpbutton.navigation;
					navigation.mode = Navigation.Mode.Explicit;
					navigation.selectOnRight = component;
					mpbutton.navigation = navigation;
					Navigation navigation2 = component.navigation;
					navigation2.mode = Navigation.Mode.Explicit;
					navigation2.selectOnLeft = mpbutton;
					component.navigation = navigation2;
				}
				if (mpbutton2)
				{
					Navigation navigation3 = mpbutton2.navigation;
					navigation3.mode = Navigation.Mode.Explicit;
					navigation3.selectOnLeft = mpbutton;
					mpbutton2.navigation = navigation3;
					Navigation navigation4 = component.navigation;
					navigation4.mode = Navigation.Mode.Explicit;
					navigation4.selectOnRight = mpbutton2;
					component.navigation = navigation4;
				}
			}
			return component;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x001482F0 File Offset: 0x001464F0
		public MPButton AddCommandButton(string consoleString, string displayToken, params object[] formatParams)
		{
			return this.AddButton(delegate
			{
				if (!string.IsNullOrEmpty(consoleString))
				{
					Console.instance.SubmitCmd(null, consoleString, true);
				}
				UnityEngine.Object.Destroy(this.rootObject);
			}, displayToken, formatParams);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x00148325 File Offset: 0x00146525
		public MPButton AddCancelButton(string displayToken, params object[] formatParams)
		{
			return this.AddButton(delegate
			{
				UnityEngine.Object.Destroy(this.rootObject);
			}, displayToken, formatParams);
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x0014833C File Offset: 0x0014653C
		public MPButton AddActionButton(UnityAction action, string displayToken, bool destroyDialog = true, params object[] formatParams)
		{
			return this.AddButton(delegate
			{
				action();
				if (destroyDialog)
				{
					UnityEngine.Object.Destroy(this.rootObject);
				}
			}, displayToken, formatParams);
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x00148379 File Offset: 0x00146579
		protected override void Start()
		{
			base.Start();
			if (this.defaultChoice)
			{
				this.defaultChoice.defaultFallbackButton = true;
			}
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0014839A File Offset: 0x0014659A
		private void Update()
		{
			this.buttonContainer.gameObject.SetActive(this.buttonContainer.childCount > 0);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x001483BC File Offset: 0x001465BC
		public static SimpleDialogBox Create(MPEventSystem owner = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/SimpleDialogRoot"));
			if (owner)
			{
				MPEventSystemProvider component = gameObject.GetComponent<MPEventSystemProvider>();
				component.eventSystem = owner;
				component.fallBackToMainEventSystem = false;
				component.eventSystem.SetSelectedGameObject(null);
			}
			return gameObject.transform.GetComponentInChildren<SimpleDialogBox>();
		}

		// Token: 0x04004BF8 RID: 19448
		public static readonly List<SimpleDialogBox> instancesList = new List<SimpleDialogBox>();

		// Token: 0x04004BF9 RID: 19449
		public GameObject rootObject;

		// Token: 0x04004BFA RID: 19450
		public GameObject buttonPrefab;

		// Token: 0x04004BFB RID: 19451
		public RectTransform buttonContainer;

		// Token: 0x04004BFC RID: 19452
		public TextMeshProUGUI headerLabel;

		// Token: 0x04004BFD RID: 19453
		public TextMeshProUGUI descriptionLabel;

		// Token: 0x04004BFE RID: 19454
		private MPButton defaultChoice;

		// Token: 0x04004BFF RID: 19455
		public object[] descriptionFormatParameters;

		// Token: 0x02000D86 RID: 3462
		public struct TokenParamsPair
		{
			// Token: 0x06004F5E RID: 20318 RVA: 0x00148424 File Offset: 0x00146624
			public TokenParamsPair(string token, params object[] formatParams)
			{
				this.token = token;
				this.formatParams = formatParams;
			}

			// Token: 0x04004C00 RID: 19456
			public string token;

			// Token: 0x04004C01 RID: 19457
			public object[] formatParams;
		}
	}
}
