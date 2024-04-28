using System;
using System.Collections.Generic;
using System.Text;
using Rewired;
using RoR2.ConVar;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RoR2.UI
{
	// Token: 0x02000CD5 RID: 3285
	[RequireComponent(typeof(MPEventSystemProvider))]
	public class ConsoleWindow : MonoBehaviour
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x00133CCC File Offset: 0x00131ECC
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x00133CD3 File Offset: 0x00131ED3
		public static ConsoleWindow instance { get; private set; }

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00133CDC File Offset: 0x00131EDC
		public void Start()
		{
			base.GetComponent<MPEventSystemProvider>().eventSystem = MPEventSystemManager.kbmEventSystem;
			if (this.outputField.verticalScrollbar)
			{
				this.outputField.verticalScrollbar.value = 1f;
			}
			this.outputField.textComponent.gameObject.AddComponent<RectTransformDimensionsChangeEvent>().onRectTransformDimensionsChange += this.OnOutputFieldRectTransformDimensionsChange;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00133D46 File Offset: 0x00131F46
		private void OnOutputFieldRectTransformDimensionsChange()
		{
			if (this.outputField.verticalScrollbar)
			{
				this.outputField.verticalScrollbar.value = 0f;
				this.outputField.verticalScrollbar.value = 1f;
			}
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00133D84 File Offset: 0x00131F84
		public void OnEnable()
		{
			Console.onLogReceived += this.OnLogReceived;
			Console.onClear += this.OnClear;
			this.RebuildOutput();
			this.inputField.onSubmit.AddListener(new UnityAction<string>(this.Submit));
			this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldValueChanged));
			ConsoleWindow.instance = this;
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00133DF7 File Offset: 0x00131FF7
		public void SubmitInputField()
		{
			this.inputField.onSubmit.Invoke(this.inputField.text);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x00133E14 File Offset: 0x00132014
		public void Submit(string text)
		{
			if (this.inputField.text == "")
			{
				return;
			}
			if (this.autoCompleteDropdown)
			{
				this.autoCompleteDropdown.Hide();
			}
			this.inputField.text = "";
			Console.instance.SubmitCmd(LocalUserManager.GetFirstLocalUser(), text, true);
			if (this.inputField && this.inputField.IsActive())
			{
				this.inputField.ActivateInputField();
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x00133E9C File Offset: 0x0013209C
		private bool autoCompleteInUse
		{
			get
			{
				return this.autoCompleteDropdown && this.autoCompleteDropdown.IsExpanded;
			}
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00133EB8 File Offset: 0x001320B8
		private void OnInputFieldValueChanged(string text)
		{
			if (!this.preventHistoryReset)
			{
				this.historyIndex = -1;
			}
			if (!this.preventAutoCompleteUpdate)
			{
				if (text.Length > 0 != (this.autoComplete != null))
				{
					if (this.autoComplete != null)
					{
						UnityEngine.Object.Destroy(this.autoCompleteDropdown.gameObject);
						this.autoComplete = null;
					}
					else
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/ConsoleAutoCompleteDropdown"), this.inputField.transform);
						this.autoCompleteDropdown = gameObject.GetComponent<TMP_Dropdown>();
						this.autoComplete = new Console.AutoComplete(Console.instance);
						this.autoCompleteDropdown.onValueChanged.AddListener(new UnityAction<int>(this.ApplyAutoComplete));
					}
				}
				if (this.autoComplete != null && this.autoComplete.SetSearchString(text))
				{
					List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
					List<string> resultsList = this.autoComplete.resultsList;
					for (int i = 0; i < resultsList.Count; i++)
					{
						list.Add(new TMP_Dropdown.OptionData(resultsList[i]));
					}
					this.autoCompleteDropdown.options = list;
				}
			}
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00133FC0 File Offset: 0x001321C0
		public void Update()
		{
			EventSystem eventSystem = MPEventSystemManager.FindEventSystem(ReInput.players.GetPlayer(0));
			if (eventSystem && eventSystem.currentSelectedGameObject == this.inputField.gameObject)
			{
				ConsoleWindow.InputFieldState inputFieldState = ConsoleWindow.InputFieldState.Neutral;
				if (this.autoCompleteDropdown && this.autoCompleteInUse)
				{
					inputFieldState = ConsoleWindow.InputFieldState.AutoComplete;
				}
				else if (this.historyIndex != -1)
				{
					inputFieldState = ConsoleWindow.InputFieldState.History;
				}
				bool keyDown = Input.GetKeyDown(KeyCode.UpArrow);
				bool keyDown2 = Input.GetKeyDown(KeyCode.DownArrow);
				switch (inputFieldState)
				{
				case ConsoleWindow.InputFieldState.Neutral:
					if (keyDown)
					{
						if (Console.userCmdHistory.Count > 0)
						{
							this.historyIndex = Console.userCmdHistory.Count - 1;
							this.preventHistoryReset = true;
							this.inputField.text = Console.userCmdHistory[this.historyIndex];
							this.inputField.MoveToEndOfLine(false, false);
							this.preventHistoryReset = false;
						}
					}
					else if (keyDown2 && this.autoCompleteDropdown)
					{
						this.autoCompleteDropdown.Show();
						this.autoCompleteDropdown.value = 0;
						this.autoCompleteDropdown.onValueChanged.Invoke(this.autoCompleteDropdown.value);
					}
					break;
				case ConsoleWindow.InputFieldState.History:
				{
					int num = 0;
					if (keyDown)
					{
						num--;
					}
					if (keyDown2)
					{
						num++;
					}
					if (num != 0)
					{
						this.historyIndex += num;
						if (this.historyIndex < 0)
						{
							this.historyIndex = 0;
						}
						if (this.historyIndex >= Console.userCmdHistory.Count)
						{
							this.historyIndex = -1;
						}
						else
						{
							this.preventHistoryReset = true;
							this.inputField.text = Console.userCmdHistory[this.historyIndex];
							this.inputField.MoveToEndOfLine(false, false);
							this.preventHistoryReset = false;
						}
					}
					break;
				}
				case ConsoleWindow.InputFieldState.AutoComplete:
					if (keyDown2)
					{
						TMP_Dropdown tmp_Dropdown = this.autoCompleteDropdown;
						int value = tmp_Dropdown.value + 1;
						tmp_Dropdown.value = value;
					}
					if (keyDown)
					{
						if (this.autoCompleteDropdown.value > 0)
						{
							TMP_Dropdown tmp_Dropdown2 = this.autoCompleteDropdown;
							int value = tmp_Dropdown2.value - 1;
							tmp_Dropdown2.value = value;
						}
						else
						{
							this.autoCompleteDropdown.Hide();
						}
					}
					break;
				}
				eventSystem.SetSelectedGameObject(this.inputField.gameObject);
			}
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x001341F8 File Offset: 0x001323F8
		private void ApplyAutoComplete(int optionIndex)
		{
			if (this.autoCompleteDropdown && this.autoCompleteDropdown.options.Count > optionIndex)
			{
				this.preventAutoCompleteUpdate = true;
				this.inputField.text = this.autoCompleteDropdown.options[optionIndex].text;
				this.inputField.MoveToEndOfLine(false, false);
				this.preventAutoCompleteUpdate = false;
			}
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00134264 File Offset: 0x00132464
		public void OnDisable()
		{
			Console.onLogReceived -= this.OnLogReceived;
			Console.onClear -= this.OnClear;
			this.inputField.onSubmit.RemoveListener(new UnityAction<string>(this.Submit));
			this.inputField.onValueChanged.RemoveListener(new UnityAction<string>(this.OnInputFieldValueChanged));
			if (ConsoleWindow.instance == this)
			{
				ConsoleWindow.instance = null;
			}
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001342DE File Offset: 0x001324DE
		private void OnLogReceived(Console.Log log)
		{
			this.RebuildOutput();
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x001342DE File Offset: 0x001324DE
		private void OnClear()
		{
			this.RebuildOutput();
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x001342E8 File Offset: 0x001324E8
		private void RebuildOutput()
		{
			float value = 0f;
			if (this.outputField.verticalScrollbar)
			{
				value = this.outputField.verticalScrollbar.value;
			}
			string[] array = new string[Console.logs.Count];
			this.stringBuilder.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				this.stringBuilder.AppendLine(Console.logs[i].message);
			}
			this.outputField.text = this.stringBuilder.ToString();
			if (this.outputField.verticalScrollbar)
			{
				this.outputField.verticalScrollbar.value = 0f;
				this.outputField.verticalScrollbar.value = 1f;
				this.outputField.verticalScrollbar.value = value;
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x001343C8 File Offset: 0x001325C8
		private static void CheckConsoleKey()
		{
			bool keyDown = Input.GetKeyDown(KeyCode.BackQuote);
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && keyDown)
			{
				ConsoleWindow.cvConsoleEnabled.SetBool(!ConsoleWindow.cvConsoleEnabled.value);
			}
			if (ConsoleWindow.cvConsoleEnabled.value && keyDown)
			{
				if (ConsoleWindow.instance)
				{
					UnityEngine.Object.Destroy(ConsoleWindow.instance.gameObject);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/ConsoleWindow")).GetComponent<ConsoleWindow>().inputField.ActivateInputField();
				}
			}
			if (Input.GetKeyDown(KeyCode.Escape) && ConsoleWindow.instance)
			{
				UnityEngine.Object.Destroy(ConsoleWindow.instance.gameObject);
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0013447E File Offset: 0x0013267E
		[RuntimeInitializeOnLoadMethod]
		public static void Init()
		{
			RoR2Application.onUpdate += ConsoleWindow.CheckConsoleKey;
		}

		// Token: 0x040047B5 RID: 18357
		public TMP_InputField inputField;

		// Token: 0x040047B6 RID: 18358
		public TMP_InputField outputField;

		// Token: 0x040047B7 RID: 18359
		private TMP_Dropdown autoCompleteDropdown;

		// Token: 0x040047B8 RID: 18360
		private Console.AutoComplete autoComplete;

		// Token: 0x040047B9 RID: 18361
		private bool preventAutoCompleteUpdate;

		// Token: 0x040047BA RID: 18362
		private bool preventHistoryReset;

		// Token: 0x040047BB RID: 18363
		private int historyIndex = -1;

		// Token: 0x040047BC RID: 18364
		private readonly StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x040047BD RID: 18365
		private const string consoleEnabledDefaultValue = "0";

		// Token: 0x040047BE RID: 18366
		private static BoolConVar cvConsoleEnabled = new BoolConVar("console_enabled", ConVarFlags.None, "0", "Enables/Disables the console.");

		// Token: 0x02000CD6 RID: 3286
		private enum InputFieldState
		{
			// Token: 0x040047C0 RID: 18368
			Neutral,
			// Token: 0x040047C1 RID: 18369
			History,
			// Token: 0x040047C2 RID: 18370
			AutoComplete
		}
	}
}
