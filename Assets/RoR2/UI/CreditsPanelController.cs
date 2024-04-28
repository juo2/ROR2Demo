using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityStates;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CD8 RID: 3288
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class CreditsPanelController : MonoBehaviour
	{
		// Token: 0x06004B05 RID: 19205 RVA: 0x00134616 File Offset: 0x00132816
		private void OnEnable()
		{
			InstanceTracker.Add<CreditsPanelController>(this);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x0013461E File Offset: 0x0013281E
		private void OnDisable()
		{
			InstanceTracker.Remove<CreditsPanelController>(this);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00134626 File Offset: 0x00132826
		private void SetScroll(float scroll)
		{
			if (this.scrollRect)
			{
				this.scrollRect.verticalNormalizedPosition = 1f - scroll;
			}
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00134647 File Offset: 0x00132847
		private void Update()
		{
			if (!Application.IsPlaying(this))
			{
				this.SetScroll(this.editorScroll);
			}
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00134660 File Offset: 0x00132860
		[ContextMenu("Generate Credits Roles JSON")]
		private void GenerateCreditsRoles()
		{
			CreditsStripGroupBuilder[] componentsInChildren = base.GetComponentsInChildren<CreditsStripGroupBuilder>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			CreditsStripGroupBuilder[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (ValueTuple<string, string> valueTuple in array[i].GetNamesAndEnglishRoles())
				{
					string text = CreditsStripGroupBuilder.EnglishRoleToToken(valueTuple.Item2);
					string text2;
					if (dictionary.TryGetValue(text, out text2))
					{
						if (!string.Equals(valueTuple.Item2, text2, StringComparison.Ordinal))
						{
							Debug.LogError(string.Concat(new string[]
							{
								"Conflict in role \"",
								text,
								"\": a=\"",
								text2,
								"\" b=\"",
								valueTuple.Item2,
								"\""
							}));
						}
					}
					else
					{
						dictionary.Add(text, valueTuple.Item2);
					}
				}
			}
			List<KeyValuePair<string, string>> list = (from kv in dictionary
			orderby kv.Key
			select kv).ToList<KeyValuePair<string, string>>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in list)
			{
				stringBuilder.Append("\"").Append(keyValuePair.Key).Append("\": \"").Append(keyValuePair.Value).Append("\",").AppendLine();
			}
			Debug.Log(stringBuilder);
			GUIUtility.systemCopyBuffer = stringBuilder.ToString();
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00134814 File Offset: 0x00132A14
		[ConCommand(commandName = "credits_start", flags = ConVarFlags.None, helpText = "Begins the credits sequence.")]
		private static void CCCreditsStart(ConCommandArgs args)
		{
			if (InstanceTracker.GetInstancesList<CreditsPanelController>().Count != 0)
			{
				throw new ConCommandException("Already in credits sequence.");
			}
			CreditsPanelController creditsPanelController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Credits/CreditsPanel"), RoR2Application.instance.mainCanvas.transform).GetComponent<CreditsPanelController>();
			creditsPanelController.skipButton.onClick.AddListener(delegate()
			{
				UnityEngine.Object.Destroy(creditsPanelController.gameObject);
			});
		}

		// Token: 0x040047C8 RID: 18376
		public RectTransform content;

		// Token: 0x040047C9 RID: 18377
		public ScrollRect scrollRect;

		// Token: 0x040047CA RID: 18378
		public float introDuration;

		// Token: 0x040047CB RID: 18379
		public AnimationCurve introFadeCurve;

		// Token: 0x040047CC RID: 18380
		public float outroDuration;

		// Token: 0x040047CD RID: 18381
		public AnimationCurve outroFadeCurve;

		// Token: 0x040047CE RID: 18382
		public float scrollDuration;

		// Token: 0x040047CF RID: 18383
		public RawImage fadePanel;

		// Token: 0x040047D0 RID: 18384
		public MPButton skipButton;

		// Token: 0x040047D1 RID: 18385
		public VoteInfoPanelController voteInfoPanel;

		// Token: 0x040047D2 RID: 18386
		[Range(0f, 1f)]
		public float editorScroll;

		// Token: 0x02000CD9 RID: 3289
		public abstract class BaseCreditsPanelState : BaseState
		{
			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x06004B0C RID: 19212 RVA: 0x00134888 File Offset: 0x00132A88
			// (set) Token: 0x06004B0D RID: 19213 RVA: 0x00134890 File Offset: 0x00132A90
			private protected CreditsPanelController creditsPanelController { protected get; private set; }

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x06004B0E RID: 19214
			protected abstract bool enableSkipButton { get; }

			// Token: 0x06004B0F RID: 19215 RVA: 0x0013489C File Offset: 0x00132A9C
			public override void OnEnter()
			{
				base.OnEnter();
				this.creditsPanelController = base.GetComponent<CreditsPanelController>();
				if (this.creditsPanelController && this.creditsPanelController.skipButton)
				{
					this.creditsPanelController.skipButton.gameObject.SetActive(this.enableSkipButton);
				}
			}

			// Token: 0x06004B10 RID: 19216 RVA: 0x001348F5 File Offset: 0x00132AF5
			protected void SetScroll(float scroll)
			{
				if (this.creditsPanelController)
				{
					this.creditsPanelController.SetScroll(scroll);
				}
			}

			// Token: 0x06004B11 RID: 19217 RVA: 0x00134910 File Offset: 0x00132B10
			protected void SetFade(float fade)
			{
				fade = Mathf.Clamp01(fade);
				if (this.creditsPanelController && this.creditsPanelController.fadePanel)
				{
					Color color = this.creditsPanelController.fadePanel.color;
					color.a = fade;
					this.creditsPanelController.fadePanel.color = color;
				}
			}
		}

		// Token: 0x02000CDA RID: 3290
		public class IntroState : CreditsPanelController.BaseCreditsPanelState
		{
			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x06004B13 RID: 19219 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected override bool enableSkipButton
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004B14 RID: 19220 RVA: 0x0013496E File Offset: 0x00132B6E
			public override void OnEnter()
			{
				base.OnEnter();
				base.SetScroll(0f);
				base.SetFade(base.creditsPanelController.introFadeCurve.Evaluate(0f));
			}

			// Token: 0x06004B15 RID: 19221 RVA: 0x0013499C File Offset: 0x00132B9C
			public override void Update()
			{
				base.Update();
				float num = Mathf.Clamp01(base.age / base.creditsPanelController.introDuration);
				base.SetFade(1f - num);
				if (num >= 1f)
				{
					this.outer.SetNextState(new CreditsPanelController.MainScrollState());
				}
			}
		}

		// Token: 0x02000CDB RID: 3291
		public class MainScrollState : CreditsPanelController.BaseCreditsPanelState
		{
			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool enableSkipButton
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06004B18 RID: 19224 RVA: 0x001349F4 File Offset: 0x00132BF4
			public override void OnEnter()
			{
				base.OnEnter();
				base.SetFade(0f);
				base.creditsPanelController.skipButton.gameObject.SetActive(true);
			}

			// Token: 0x06004B19 RID: 19225 RVA: 0x00134A20 File Offset: 0x00132C20
			public override void Update()
			{
				base.Update();
				float num = Mathf.Clamp01(base.age / base.creditsPanelController.scrollDuration);
				base.SetScroll(CreditsPanelController.MainScrollState.scrollCurve.Evaluate(num));
				if (num >= 1f)
				{
					this.outer.SetNextState(new CreditsPanelController.OutroState());
				}
			}

			// Token: 0x040047D4 RID: 18388
			public static AnimationCurve scrollCurve;
		}

		// Token: 0x02000CDC RID: 3292
		public class OutroState : CreditsPanelController.BaseCreditsPanelState
		{
			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x06004B1B RID: 19227 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool enableSkipButton
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06004B1C RID: 19228 RVA: 0x00134A74 File Offset: 0x00132C74
			public override void OnEnter()
			{
				base.OnEnter();
				base.SetScroll(1f);
				base.SetFade(base.creditsPanelController.outroFadeCurve.Evaluate(0f));
			}

			// Token: 0x06004B1D RID: 19229 RVA: 0x00134AA4 File Offset: 0x00132CA4
			public override void Update()
			{
				base.Update();
				float num = Mathf.Clamp01(base.age / base.creditsPanelController.outroDuration);
				base.SetFade(num);
				if (num >= 1f)
				{
					EntityState.Destroy(base.gameObject);
				}
			}
		}
	}
}
