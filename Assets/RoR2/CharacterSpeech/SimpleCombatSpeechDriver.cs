using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.CharacterSpeech
{
	// Token: 0x02000C2D RID: 3117
	[RequireComponent(typeof(CharacterSpeechController))]
	public class SimpleCombatSpeechDriver : BaseCharacterSpeechDriver
	{
		// Token: 0x0600466D RID: 18029 RVA: 0x001235EC File Offset: 0x001217EC
		protected new void Awake()
		{
			base.Awake();
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x001235F4 File Offset: 0x001217F4
		protected void Start()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.Initialize();
			UnityEvent unityEvent = this.onStart;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x00123614 File Offset: 0x00121814
		protected new void OnDestroy()
		{
			base.OnDestroy();
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x0012361C File Offset: 0x0012181C
		protected void FixedUpdate()
		{
			this.lastSpeechStopwatch += Time.fixedDeltaTime;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00123630 File Offset: 0x00121830
		public void TriggerSpeech(string refName)
		{
			if (this.lastSpeechStopwatch < this.minimumDurationBetweenSpeech)
			{
				return;
			}
			int num = this.FindSpeechCategoryIndexByName(refName);
			if (num == -1)
			{
				Debug.LogWarningFormat("Speech category \"{0}\" could not be found.", new object[]
				{
					refName
				});
				return;
			}
			ref SimpleCombatSpeechDriver.SpeechCategory ptr = ref this.speechCategories[num];
			if (UnityEngine.Random.value < ptr.chanceToTrigger)
			{
				this.SubmitSpeechRequestFromCategory(ref ptr);
			}
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x00123690 File Offset: 0x00121890
		private void SubmitSpeechRequestFromCategory(ref SimpleCombatSpeechDriver.SpeechCategory category)
		{
			if (category.speechSelection == null || category.speechSelection.Count <= 0)
			{
				if (category.speechInfos.Length == 0)
				{
					return;
				}
				this.InitializeCategory(ref category);
			}
			int num = category.speechSelection.EvaluateToChoiceIndex(UnityEngine.Random.value);
			SimpleCombatSpeechDriver.SpeechInfo value = category.speechSelection.GetChoice(num).value;
			if (this.removeRepeats)
			{
				category.speechSelection.RemoveChoice(num);
			}
			this.lastSpeechStopwatch = 0f;
			CharacterSpeechController characterSpeechController = base.characterSpeechController;
			CharacterSpeechController.SpeechInfo speechInfo = default(CharacterSpeechController.SpeechInfo);
			speechInfo.token = value.nameToken;
			speechInfo.duration = this.minimumDurationBetweenSpeech;
			speechInfo.maxWait = this.minimumDurationBetweenSpeech;
			speechInfo.mustPlay = false;
			characterSpeechController.EnqueueSpeech(speechInfo);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0012374C File Offset: 0x0012194C
		private int FindSpeechCategoryIndexByName(string refName)
		{
			for (int i = 0; i < this.speechCategories.Length; i++)
			{
				if (string.Equals(this.speechCategories[i].refName, refName, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x0012378C File Offset: 0x0012198C
		private void Initialize()
		{
			for (int i = 0; i < this.speechCategories.Length; i++)
			{
				this.InitializeCategory(ref this.speechCategories[i]);
			}
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x001237C0 File Offset: 0x001219C0
		private void InitializeCategory(ref SimpleCombatSpeechDriver.SpeechCategory speechCategory)
		{
			WeightedSelection<SimpleCombatSpeechDriver.SpeechInfo> weightedSelection = new WeightedSelection<SimpleCombatSpeechDriver.SpeechInfo>(8);
			for (int i = 0; i < speechCategory.speechInfos.Length; i++)
			{
				SimpleCombatSpeechDriver.SpeechInfo speechInfo = speechCategory.speechInfos[i];
				weightedSelection.AddChoice(speechInfo, speechInfo.weight);
			}
			speechCategory.speechSelection = weightedSelection;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00123808 File Offset: 0x00121A08
		protected override void OnCharacterBodyDiscovered(CharacterBody characterBody)
		{
			base.OnCharacterBodyDiscovered(characterBody);
			GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00123833 File Offset: 0x00121A33
		protected override void OnCharacterBodyLost(CharacterBody characterBody)
		{
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
			GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
			base.OnCharacterBodyLost(characterBody);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0012385E File Offset: 0x00121A5E
		private void OnServerDamageDealt(DamageReport damageReport)
		{
			if (damageReport.attackerBody == base.currentCharacterBody)
			{
				this.OnBodyDamageDealt(damageReport);
			}
			if (damageReport.victimBody == base.currentCharacterBody)
			{
				this.OnBodyDamageTaken(damageReport);
			}
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x0012388A File Offset: 0x00121A8A
		private void OnCharacterDeathGlobal(DamageReport damageReport)
		{
			if (damageReport.attackerBody == base.currentCharacterBody)
			{
				this.OnBodyKill(damageReport);
			}
			if (damageReport.victimBody == base.currentCharacterBody)
			{
				this.OnBodyDeath(damageReport);
			}
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x001238B8 File Offset: 0x00121AB8
		private void OnBodyDamageDealt(DamageReport damageReport)
		{
			if (this.scaleChanceOnDamageDealtByHealthFractionDealt && damageReport.victim && !string.IsNullOrEmpty(this.damageDealtRefName))
			{
				int num = this.FindSpeechCategoryIndexByName(this.damageDealtRefName);
				if (num == -1)
				{
					Debug.LogWarningFormat("Speech category \"{0}\" could not be found.", new object[]
					{
						this.damageDealtRefName
					});
				}
				else
				{
					ref SimpleCombatSpeechDriver.SpeechCategory ptr = ref this.speechCategories[num];
					float num2 = ptr.chanceToTrigger;
					if (this.scaleChanceOnDamageDealtByHealthFractionDealt)
					{
						float fullCombinedHealth = damageReport.victim.fullCombinedHealth;
						float num3 = damageReport.damageDealt / fullCombinedHealth;
						num2 *= num3;
					}
					num2 *= this.scaleChanceOnDamageDealtMultiplier;
					if (UnityEngine.Random.value < num2)
					{
						this.SubmitSpeechRequestFromCategory(ref ptr);
					}
				}
			}
			DamageReportUnityEvent damageReportUnityEvent = this.onBodyDamageDealt;
			if (damageReportUnityEvent == null)
			{
				return;
			}
			damageReportUnityEvent.Invoke(damageReport);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnBodyDamageTaken(DamageReport damageReport)
		{
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00123979 File Offset: 0x00121B79
		private void OnBodyKill(DamageReport damageReport)
		{
			DamageReportUnityEvent damageReportUnityEvent = this.onBodyKill;
			if (damageReportUnityEvent == null)
			{
				return;
			}
			damageReportUnityEvent.Invoke(damageReport);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x0012398C File Offset: 0x00121B8C
		private void OnBodyDeath(DamageReport damageReport)
		{
			DamageReportUnityEvent damageReportUnityEvent = this.onBodyDeath;
			if (damageReportUnityEvent == null)
			{
				return;
			}
			damageReportUnityEvent.Invoke(damageReport);
		}

		// Token: 0x04004447 RID: 17479
		[Header("Speech Info")]
		public float minimumDurationBetweenSpeech;

		// Token: 0x04004448 RID: 17480
		public bool removeRepeats;

		// Token: 0x04004449 RID: 17481
		[SerializeField]
		public SimpleCombatSpeechDriver.SpeechCategory[] speechCategories;

		// Token: 0x0400444A RID: 17482
		[Tooltip("The refname to automatically submit speech for when damage is dealt.")]
		public string damageDealtRefName;

		// Token: 0x0400444B RID: 17483
		[Header("Combat Parameters")]
		public bool scaleChanceOnDamageDealtByHealthFractionDealt = true;

		// Token: 0x0400444C RID: 17484
		public float scaleChanceOnDamageDealtMultiplier = 1f;

		// Token: 0x0400444D RID: 17485
		[Header("Events")]
		public DamageReportUnityEvent onBodyDamageDealt;

		// Token: 0x0400444E RID: 17486
		public UnityEvent onStart;

		// Token: 0x0400444F RID: 17487
		public DamageReportUnityEvent onBodyKill;

		// Token: 0x04004450 RID: 17488
		public DamageReportUnityEvent onBodyDeath;

		// Token: 0x04004451 RID: 17489
		private float lastSpeechStopwatch = float.PositiveInfinity;

		// Token: 0x02000C2E RID: 3118
		[Serializable]
		public struct SpeechCategory
		{
			// Token: 0x04004452 RID: 17490
			public string refName;

			// Token: 0x04004453 RID: 17491
			public float chanceToTrigger;

			// Token: 0x04004454 RID: 17492
			public SimpleCombatSpeechDriver.SpeechInfo[] speechInfos;

			// Token: 0x04004455 RID: 17493
			public WeightedSelection<SimpleCombatSpeechDriver.SpeechInfo> speechSelection;
		}

		// Token: 0x02000C2F RID: 3119
		[Serializable]
		public struct SpeechInfo
		{
			// Token: 0x04004456 RID: 17494
			public string nameToken;

			// Token: 0x04004457 RID: 17495
			public float weight;
		}
	}
}
