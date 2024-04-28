using System;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CCF RID: 3279
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class ChatBox : MonoBehaviour
	{
		// Token: 0x06004AB4 RID: 19124 RVA: 0x00132F88 File Offset: 0x00131188
		private void UpdateFade(float deltaTime)
		{
			this.fadeTimer -= deltaTime;
			if (!this.fadeGroup)
			{
				return;
			}
			float alpha;
			if (this.showInput)
			{
				alpha = 1f;
				this.ResetFadeTimer();
			}
			else if (this.fadeTimer < 0f)
			{
				alpha = 0f;
			}
			else if (this.fadeTimer < this.fadeDuration)
			{
				alpha = Mathf.Sqrt(Util.Remap(this.fadeTimer, this.fadeDuration, 0f, 1f, 0f));
			}
			else
			{
				alpha = 1f;
			}
			this.fadeGroup.alpha = alpha;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00133024 File Offset: 0x00131224
		private void ResetFadeTimer()
		{
			this.fadeTimer = this.fadeDuration + this.fadeWait;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x00133039 File Offset: 0x00131239
		private bool showKeybindTipOnStart
		{
			get
			{
				return Chat.readOnlyLog.Count == 0;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x00133048 File Offset: 0x00131248
		// (set) Token: 0x06004AB8 RID: 19128 RVA: 0x00133050 File Offset: 0x00131250
		private bool showInput
		{
			get
			{
				return this._showInput;
			}
			set
			{
				if (this._showInput != value)
				{
					this._showInput = value;
					this.RebuildChatRects();
					if (this.inputField && this.deactivateInputFieldIfInactive)
					{
						this.inputField.gameObject.SetActive(this._showInput);
					}
					if (this.sendButton)
					{
						this.sendButton.gameObject.SetActive(this._showInput);
					}
					for (int i = 0; i < this.gameplayHiddenGraphics.Length; i++)
					{
						this.gameplayHiddenGraphics[i].enabled = this._showInput;
					}
					if (this._showInput)
					{
						this.FocusInputField();
						return;
					}
					this.UnfocusInputField();
				}
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001330FF File Offset: 0x001312FF
		public void SetShowInput(bool value)
		{
			this.showInput = value;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00133108 File Offset: 0x00131308
		public void SubmitChat()
		{
			string text = this.inputField.text;
			if (text != "")
			{
				this.inputField.text = "";
				ReadOnlyCollection<NetworkUser> readOnlyLocalPlayersList = NetworkUser.readOnlyLocalPlayersList;
				if (readOnlyLocalPlayersList.Count > 0)
				{
					string text2 = text;
					text2 = text2.Replace("\\", "\\\\");
					text2 = text2.Replace("\"", "\\\"");
					Console.instance.SubmitCmd(readOnlyLocalPlayersList[0], "say \"" + text2 + "\"", false);
					Debug.Log("Submitting say cmd.");
				}
			}
			Debug.LogFormat("SubmitChat() submittedText={0}", new object[]
			{
				text
			});
			if (this.deselectAfterSubmitChat)
			{
				this.showInput = false;
				return;
			}
			this.FocusInputField();
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnInputFieldEndEdit()
		{
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x001331C7 File Offset: 0x001313C7
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.showInput = true;
			this.showInput = false;
			Chat.onChatChanged += this.OnChatChangedHandler;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x001331F4 File Offset: 0x001313F4
		private void OnDestroy()
		{
			Chat.onChatChanged -= this.OnChatChangedHandler;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00133207 File Offset: 0x00131407
		private void Start()
		{
			if (this.showKeybindTipOnStart && !RoR2Application.isInSinglePlayer)
			{
				Chat.AddMessage(Language.GetString("CHAT_KEYBIND_TIP"));
			}
			this.BuildChat();
			this.ScrollToBottom();
			this.inputField.resetOnDeActivation = true;
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x0013323F File Offset: 0x0013143F
		private void OnEnable()
		{
			this.BuildChat();
			this.ScrollToBottom();
			base.Invoke("ScrollToBottom", 0f);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnDisable()
		{
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x0013325D File Offset: 0x0013145D
		private void OnChatChangedHandler()
		{
			this.ResetFadeTimer();
			if (base.enabled)
			{
				this.BuildChat();
				this.ScrollToBottom();
			}
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00133279 File Offset: 0x00131479
		public void ScrollToBottom()
		{
			this.messagesText.verticalScrollbar.value = 0f;
			this.messagesText.verticalScrollbar.value = 1f;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x001332A8 File Offset: 0x001314A8
		private void BuildChat()
		{
			ReadOnlyCollection<string> readOnlyLog = Chat.readOnlyLog;
			string[] array = new string[readOnlyLog.Count];
			readOnlyLog.CopyTo(array, 0);
			this.messagesText.text = string.Join("\n", array);
			this.RebuildChatRects();
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x001332EC File Offset: 0x001314EC
		private void Update()
		{
			this.UpdateFade(Time.deltaTime);
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			GameObject gameObject = eventSystem ? eventSystem.currentSelectedGameObject : null;
			bool flag = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
			if (!this.showInput && flag && !(ConsoleWindow.instance != null))
			{
				this.showInput = true;
				return;
			}
			if (gameObject == this.inputField.gameObject)
			{
				if (flag)
				{
					if (this.showInput)
					{
						this.SubmitChat();
					}
					else if (!gameObject)
					{
						this.showInput = true;
					}
				}
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					this.showInput = false;
					return;
				}
			}
			else
			{
				this.showInput = false;
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x001333AC File Offset: 0x001315AC
		private void RebuildChatRects()
		{
			RectTransform component = this.scrollRect.GetComponent<RectTransform>();
			component.SetParent((this.showInput && this.allowExpandedChatbox) ? this.expandedChatboxRect : this.standardChatboxRect);
			component.offsetMin = Vector2.zero;
			component.offsetMax = Vector2.zero;
			this.ScrollToBottom();
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00133404 File Offset: 0x00131604
		private void FocusInputField()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			if (eventSystem)
			{
				eventSystem.SetSelectedGameObject(this.inputField.gameObject);
			}
			this.inputField.ActivateInputField();
			this.inputField.text = "";
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00133454 File Offset: 0x00131654
		private void UnfocusInputField()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			if (eventSystem && eventSystem.currentSelectedGameObject == this.inputField.gameObject)
			{
				eventSystem.SetSelectedGameObject(null);
			}
			this.inputField.DeactivateInputField(false);
		}

		// Token: 0x04004782 RID: 18306
		[Header("Cached Components")]
		public TMP_InputField messagesText;

		// Token: 0x04004783 RID: 18307
		public TMP_InputField inputField;

		// Token: 0x04004784 RID: 18308
		public Button sendButton;

		// Token: 0x04004785 RID: 18309
		public Graphic[] gameplayHiddenGraphics;

		// Token: 0x04004786 RID: 18310
		public RectTransform standardChatboxRect;

		// Token: 0x04004787 RID: 18311
		public RectTransform expandedChatboxRect;

		// Token: 0x04004788 RID: 18312
		public ScrollRect scrollRect;

		// Token: 0x04004789 RID: 18313
		[Tooltip("The canvas group to use to fade this chat box. Leave empty for no fading behavior.")]
		public CanvasGroup fadeGroup;

		// Token: 0x0400478A RID: 18314
		[Header("Parameters")]
		public bool allowExpandedChatbox;

		// Token: 0x0400478B RID: 18315
		public bool deselectAfterSubmitChat;

		// Token: 0x0400478C RID: 18316
		public bool deactivateInputFieldIfInactive;

		// Token: 0x0400478D RID: 18317
		public float fadeWait = 5f;

		// Token: 0x0400478E RID: 18318
		public float fadeDuration = 5f;

		// Token: 0x0400478F RID: 18319
		private float fadeTimer;

		// Token: 0x04004790 RID: 18320
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004791 RID: 18321
		private bool _showInput;
	}
}
