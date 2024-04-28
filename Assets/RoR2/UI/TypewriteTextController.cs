using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000DA0 RID: 3488
	[ExecuteAlways]
	public class TypewriteTextController : MonoBehaviour
	{
		// Token: 0x06004FE9 RID: 20457 RVA: 0x0014A9CD File Offset: 0x00148BCD
		private void Start()
		{
			this.isEnginePlaying = Application.IsPlaying(this);
			if (this.isEnginePlaying && this.playOnStart)
			{
				this.StartTyping();
			}
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0014A9F4 File Offset: 0x00148BF4
		private void Update()
		{
			float num = Time.deltaTime;
			if (!this.isEnginePlaying)
			{
				num = Time.unscaledDeltaTime;
			}
			if (this.isPlayingAnimation)
			{
				this.stopwatch += num;
			}
			this.SetTime(this.stopwatch);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0014AA38 File Offset: 0x00148C38
		private static bool IsEndOfSentence(TMP_TextInfo textInfo, in TMP_CharacterInfo characterInfo)
		{
			char character = characterInfo.character;
			if (Array.IndexOf<char>(TypewriteTextController.sentenceTerminators, character) == -1)
			{
				return false;
			}
			if (character == '.' && characterInfo.index + 1 < textInfo.characterCount)
			{
				char character2 = textInfo.characterInfo[characterInfo.index + 1].character;
				if (!char.IsLetter(character2))
				{
					char.IsWhiteSpace(character2);
				}
				return true;
			}
			return true;
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0014AA9C File Offset: 0x00148C9C
		private void GenerateTimingInfo()
		{
			this.UpdateAllLabelTextInfo();
			TypewriteTextController.<>c__DisplayClass34_0 CS$<>8__locals1;
			CS$<>8__locals1.currentTimedTextChunk = default(TypewriteTextController.TimedTextChunk);
			bool flag = this.delayBetweenSentences > 0f;
			bool flag2 = this.delayBetweenTexts > 0f;
			bool flag3 = this.delayBetweenNewLines > 0f;
			this.totalCharacterCount = 0;
			for (int i = 0; i < this.labels.Length; i++)
			{
				TextMeshProUGUI textMeshProUGUI = this.labels[i];
				if (textMeshProUGUI)
				{
					TMP_TextInfo textInfo = textMeshProUGUI.textInfo;
					TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenKeys, ref CS$<>8__locals1);
					this.totalCharacterCount += textInfo.characterCount;
					if (flag2 && i > 0)
					{
						CS$<>8__locals1.currentTimedTextChunk.duration = this.delayBetweenTexts;
						TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenKeys, ref CS$<>8__locals1);
					}
					else if (i == 0)
					{
						CS$<>8__locals1.currentTimedTextChunk.duration = this.initialDelay;
						TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenKeys, ref CS$<>8__locals1);
					}
					int num = Math.Min(textInfo.characterCount, textInfo.characterInfo.Length);
					while (CS$<>8__locals1.currentTimedTextChunk.endCharIndex < num)
					{
						ref TMP_CharacterInfo ptr = ref textInfo.characterInfo[CS$<>8__locals1.currentTimedTextChunk.endCharIndex];
						if (flag && TypewriteTextController.IsEndOfSentence(textInfo, ptr))
						{
							TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenSentences, ref CS$<>8__locals1);
							CS$<>8__locals1.currentTimedTextChunk.endCharIndex = CS$<>8__locals1.currentTimedTextChunk.endCharIndex + 1;
							TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenKeys, ref CS$<>8__locals1);
						}
						else if (flag3 && ptr.character == '\n')
						{
							TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenNewLines, ref CS$<>8__locals1);
							CS$<>8__locals1.currentTimedTextChunk.endCharIndex = CS$<>8__locals1.currentTimedTextChunk.endCharIndex + 1;
							TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(textMeshProUGUI, this.delayBetweenKeys, ref CS$<>8__locals1);
						}
						CS$<>8__locals1.currentTimedTextChunk.endCharIndex = CS$<>8__locals1.currentTimedTextChunk.endCharIndex + 1;
					}
				}
			}
			TypewriteTextController.<GenerateTimingInfo>g__StartNewChunk|34_0(null, 0f, ref CS$<>8__locals1);
			this.textChunks = TypewriteTextController.sharedChunkBuilder.ToArray();
			TypewriteTextController.sharedChunkBuilder.Clear();
			this.totalTypingDuration = 0f;
			if (this.textChunks.Length != 0)
			{
				ref TypewriteTextController.TimedTextChunk ptr2 = ref this.textChunks[this.textChunks.Length - 1];
				this.totalTypingDuration = ptr2.startTime + ptr2.duration;
			}
			this.totalFadingDuration = this.fadeOutDelay + this.fadeOutDuration;
			this.typingTimeScale = 1f;
			this.fadingTimeScale = 1f;
			if (this.timeToFit > 0f)
			{
				if (this.includeFadeoutInTimeToFit)
				{
					float num2 = (this.totalTypingDuration + this.fadeOutDelay + this.fadeOutDuration) / this.timeToFit;
					this.typingTimeScale = num2;
					this.fadingTimeScale = num2;
					return;
				}
				this.typingTimeScale = this.totalTypingDuration / this.timeToFit;
			}
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014AD57 File Offset: 0x00148F57
		[ContextMenu("Start preview")]
		public void StartTyping()
		{
			this.GenerateTimingInfo();
			this.isPlayingAnimation = true;
			this.stopwatch = 0f;
			this.SetTime(this.stopwatch);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0014AD80 File Offset: 0x00148F80
		private void UpdateAllLabelTextInfo()
		{
			for (int i = 0; i < this.labels.Length; i++)
			{
				TextMeshProUGUI textMeshProUGUI = this.labels[i];
				if (textMeshProUGUI)
				{
					textMeshProUGUI.ForceMeshUpdate(true, false);
				}
			}
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0014ADBC File Offset: 0x00148FBC
		public void SetTime(float t)
		{
			float num = this.totalTypingDuration / this.typingTimeScale;
			float typingTime = Mathf.Clamp(t * this.typingTimeScale, 0f, this.totalTypingDuration);
			float fadingTime = Mathf.Clamp((t - num) * this.fadingTimeScale, 0f, this.totalFadingDuration);
			this.SetTypingTime(typingTime);
			this.SetFadingTime(fadingTime);
			if (this.isDoneTyping && this.isDoneFading)
			{
				this.isPlayingAnimation = false;
			}
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014AE30 File Offset: 0x00149030
		private void SetTypingTime(float typingTime)
		{
			bool flag = this.isDoneTyping;
			this.isDoneTyping = (typingTime >= this.totalTypingDuration);
			TypewriteTextController.<>c__DisplayClass38_0 CS$<>8__locals1;
			CS$<>8__locals1.newTotalRevealedCharacterCount = 0;
			CS$<>8__locals1.currentLabel = null;
			CS$<>8__locals1.currentLabelRevealedCharacterCount = 0;
			CS$<>8__locals1.isPlaying = this.isEnginePlaying;
			int i;
			for (i = 0; i < this.textChunks.Length; i++)
			{
				ref TypewriteTextController.TimedTextChunk ptr = ref this.textChunks[i];
				if (ptr.startTime > typingTime)
				{
					break;
				}
				float num = ptr.startTime + ptr.duration;
				if (typingTime < num)
				{
					float t = Mathf.InverseLerp(ptr.startTime, num, typingTime);
					int newRevealedCharacterCount = Mathf.CeilToInt(Mathf.Lerp((float)ptr.startCharIndex, (float)ptr.endCharIndex, t));
					TypewriteTextController.<SetTypingTime>g__UpdateCurrentLabel|38_0(ptr.label, newRevealedCharacterCount, ref CS$<>8__locals1);
					break;
				}
				TypewriteTextController.<SetTypingTime>g__UpdateCurrentLabel|38_0(ptr.label, ptr.endCharIndex, ref CS$<>8__locals1);
			}
			for (i++; i < this.textChunks.Length; i++)
			{
				ref TypewriteTextController.TimedTextChunk ptr2 = ref this.textChunks[i];
				if (CS$<>8__locals1.currentLabel != ptr2.label)
				{
					TypewriteTextController.<SetTypingTime>g__UpdateCurrentLabel|38_0(ptr2.label, 0, ref CS$<>8__locals1);
				}
			}
			TypewriteTextController.<SetTypingTime>g__UpdateCurrentLabel|38_0(null, 0, ref CS$<>8__locals1);
			if (CS$<>8__locals1.newTotalRevealedCharacterCount > this.totalRevealedCharacterCount && this.soundString.Length > 0 && this.isEnginePlaying)
			{
				Util.PlaySound(this.soundString, RoR2Application.instance.gameObject);
			}
			this.totalRevealedCharacterCount = CS$<>8__locals1.newTotalRevealedCharacterCount;
			this.isDoneTyping = (this.totalRevealedCharacterCount >= this.totalCharacterCount);
			if (!flag && this.isDoneTyping && this.isEnginePlaying)
			{
				UnityEvent unityEvent = this.onFinishTyping;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014AFD8 File Offset: 0x001491D8
		private void SetFadingTime(float fadingTime)
		{
			bool flag = this.isDoneFading;
			this.isDoneFading = (fadingTime >= this.totalFadingDuration);
			if (this.fadeOutAfterCompletion)
			{
				float num = Mathf.Clamp01((fadingTime - this.fadeOutDelay) / this.fadeOutDuration);
				float labelAlpha = 1f - num;
				this.SetLabelAlpha(labelAlpha);
				if (this.isDoneFading && !flag && this.isEnginePlaying)
				{
					UnityEvent unityEvent = this.onFinishFade;
					if (unityEvent != null)
					{
						unityEvent.Invoke();
					}
					if (this.disableObjectOnFadeEnd)
					{
						base.gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014B068 File Offset: 0x00149268
		private void SetLabelAlpha(float alpha)
		{
			for (int i = 0; i < this.labels.Length; i++)
			{
				TextMeshProUGUI textMeshProUGUI = this.labels[i];
				if (textMeshProUGUI)
				{
					Color color = textMeshProUGUI.color;
					if (!color.a.Equals(alpha))
					{
						color.a = alpha;
						textMeshProUGUI.color = color;
					}
				}
			}
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private bool IsBeingEdited()
		{
			return false;
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0014B0BE File Offset: 0x001492BE
		private void OnValidate()
		{
			if (this.IsBeingEdited() && !this.isPlayingAnimation)
			{
				this.GenerateTimingInfo();
			}
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0014B130 File Offset: 0x00149330
		[CompilerGenerated]
		internal static void <GenerateTimingInfo>g__StartNewChunk|34_0(TextMeshProUGUI label, float interval, ref TypewriteTextController.<>c__DisplayClass34_0 A_2)
		{
			if (A_2.currentTimedTextChunk.endCharIndex != A_2.currentTimedTextChunk.startCharIndex)
			{
				A_2.currentTimedTextChunk.duration = (float)(A_2.currentTimedTextChunk.endCharIndex - A_2.currentTimedTextChunk.startCharIndex) * A_2.currentTimedTextChunk.interval;
			}
			if (A_2.currentTimedTextChunk.duration > 0f)
			{
				TypewriteTextController.sharedChunkBuilder.Add(A_2.currentTimedTextChunk);
			}
			float startTime = A_2.currentTimedTextChunk.startTime + A_2.currentTimedTextChunk.duration;
			int num = A_2.currentTimedTextChunk.endCharIndex;
			if (label != A_2.currentTimedTextChunk.label)
			{
				num = 0;
			}
			A_2.currentTimedTextChunk = new TypewriteTextController.TimedTextChunk
			{
				label = label,
				startTime = startTime,
				duration = 0f,
				startCharIndex = num,
				endCharIndex = num,
				interval = interval
			};
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014B220 File Offset: 0x00149420
		[CompilerGenerated]
		internal static void <SetTypingTime>g__UpdateCurrentLabel|38_0(TextMeshProUGUI newCurrentLabel, int newRevealedCharacterCount, ref TypewriteTextController.<>c__DisplayClass38_0 A_2)
		{
			if (A_2.currentLabel != newCurrentLabel && A_2.currentLabel != null)
			{
				A_2.currentLabel.maxVisibleCharacters = A_2.currentLabelRevealedCharacterCount;
				if (A_2.currentLabelRevealedCharacterCount == 0)
				{
					if (A_2.currentLabel.enabled & A_2.isPlaying)
					{
						A_2.currentLabel.enabled = false;
					}
				}
				else
				{
					if (!A_2.currentLabel.enabled)
					{
						A_2.currentLabel.enabled = true;
					}
					A_2.newTotalRevealedCharacterCount += A_2.currentLabelRevealedCharacterCount;
				}
			}
			A_2.currentLabel = newCurrentLabel;
			A_2.currentLabelRevealedCharacterCount = newRevealedCharacterCount;
		}

		// Token: 0x04004C81 RID: 19585
		[Min(0f)]
		[Tooltip("How long to delay before beginning to reveal characters. 0 is no delay.")]
		public float initialDelay;

		// Token: 0x04004C82 RID: 19586
		[Min(0f)]
		[Tooltip("How long to delay between individual characters. 0 is no delay.")]
		public float delayBetweenKeys = 0.1f;

		// Token: 0x04004C83 RID: 19587
		[Min(0f)]
		[Tooltip("How long to delay between sentences. 0 is no delay.")]
		public float delayBetweenSentences;

		// Token: 0x04004C84 RID: 19588
		[Min(0f)]
		[Tooltip("How long to delay between line breaks.")]
		public float delayBetweenNewLines;

		// Token: 0x04004C85 RID: 19589
		[Tooltip("How long to delay between labels. 0 is no delay.")]
		[Min(0f)]
		public float delayBetweenTexts = 1f;

		// Token: 0x04004C86 RID: 19590
		[Tooltip("The labels to control.")]
		[FormerlySerializedAs("textMeshProUGui")]
		public TextMeshProUGUI[] labels;

		// Token: 0x04004C87 RID: 19591
		[Tooltip("The sound to play for each character typed.")]
		public string soundString;

		// Token: 0x04004C88 RID: 19592
		[Tooltip("Whether or not to fade out the label once typing is finished.")]
		public bool fadeOutAfterCompletion;

		// Token: 0x04004C89 RID: 19593
		[Min(0f)]
		[Tooltip("How long to wait after typing is finished to begin fading out.")]
		public float fadeOutDelay;

		// Token: 0x04004C8A RID: 19594
		[Tooltip("How long it takes to fade out once fading begins.")]
		[Min(0f)]
		public float fadeOutDuration;

		// Token: 0x04004C8B RID: 19595
		[Tooltip("The current simulation time of this object. You'll probably want to make sure this is set to zero when saving if you preview the animation in-editor.")]
		[Min(0f)]
		public float stopwatch;

		// Token: 0x04004C8C RID: 19596
		[Tooltip("Whether or not to disable the attached GameObject when fading is complete.")]
		public bool disableObjectOnFadeEnd = true;

		// Token: 0x04004C8D RID: 19597
		[Tooltip("Whether or not to start playing the animation immediately.")]
		public bool playOnStart = true;

		// Token: 0x04004C8E RID: 19598
		[Tooltip("How long (in seconds) it should take to complete this animation if nonzero. Mainly useful for achieving consistent timings with different languages.")]
		public float timeToFit;

		// Token: 0x04004C8F RID: 19599
		[Tooltip("Whether or not timeToFit includes fade values.")]
		public bool includeFadeoutInTimeToFit;

		// Token: 0x04004C90 RID: 19600
		[Tooltip("Event called when typing has finished.")]
		public UnityEvent onFinishTyping;

		// Token: 0x04004C91 RID: 19601
		[Tooltip("Event called when fadeout has finished.")]
		public UnityEvent onFinishFade;

		// Token: 0x04004C92 RID: 19602
		private TypewriteTextController.TimedTextChunk[] textChunks = Array.Empty<TypewriteTextController.TimedTextChunk>();

		// Token: 0x04004C93 RID: 19603
		private float totalTypingDuration;

		// Token: 0x04004C94 RID: 19604
		private float totalFadingDuration;

		// Token: 0x04004C95 RID: 19605
		private float typingTimeScale;

		// Token: 0x04004C96 RID: 19606
		private float fadingTimeScale;

		// Token: 0x04004C97 RID: 19607
		private int totalCharacterCount;

		// Token: 0x04004C98 RID: 19608
		private int totalRevealedCharacterCount;

		// Token: 0x04004C99 RID: 19609
		private bool isPlayingAnimation;

		// Token: 0x04004C9A RID: 19610
		private bool isDoneTyping;

		// Token: 0x04004C9B RID: 19611
		private bool isDoneFading;

		// Token: 0x04004C9C RID: 19612
		private bool isEnginePlaying;

		// Token: 0x04004C9D RID: 19613
		private static readonly char[] sentenceTerminators = new char[]
		{
			'.',
			'?',
			'!',
			'。'
		};

		// Token: 0x04004C9E RID: 19614
		private static readonly List<TypewriteTextController.TimedTextChunk> sharedChunkBuilder = new List<TypewriteTextController.TimedTextChunk>();

		// Token: 0x02000DA1 RID: 3489
		[Serializable]
		private struct TimedTextChunk
		{
			// Token: 0x04004C9F RID: 19615
			public TextMeshProUGUI label;

			// Token: 0x04004CA0 RID: 19616
			public int startCharIndex;

			// Token: 0x04004CA1 RID: 19617
			public int endCharIndex;

			// Token: 0x04004CA2 RID: 19618
			public float startTime;

			// Token: 0x04004CA3 RID: 19619
			public float duration;

			// Token: 0x04004CA4 RID: 19620
			public float interval;
		}
	}
}
