using System;
using RoR2;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200005D RID: 93
public class SpeechBubbleController : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x000081FD File Offset: 0x000063FD
	private void Start()
	{
		this.Initialize();
		this.lastSpeechStopwatch = this.minimumDurationBetweenSpeech;
		UnityEvent unityEvent = this.onStart;
		if (unityEvent == null)
		{
			return;
		}
		unityEvent.Invoke();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00008221 File Offset: 0x00006421
	private void OnEnable()
	{
		GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
		GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00008245 File Offset: 0x00006445
	private void OnDisable()
	{
		GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
		GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00008269 File Offset: 0x00006469
	private void FixedUpdate()
	{
		this.lastSpeechStopwatch += Time.fixedDeltaTime;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000827D File Offset: 0x0000647D
	private void OnCharacterDeathGlobal(DamageReport damageReport)
	{
		if (damageReport.victimBody != this.characterBody)
		{
			if (damageReport.attackerBody == this.characterBody)
			{
				UnityEvent unityEvent = this.onBodyKill;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
			return;
		}
		UnityEvent unityEvent2 = this.onBodyDeath;
		if (unityEvent2 == null)
		{
			return;
		}
		unityEvent2.Invoke();
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000082BC File Offset: 0x000064BC
	private void OnServerDamageDealt(DamageReport damageReport)
	{
		if (damageReport.attackerBody == this.characterBody)
		{
			UnityEvent unityEvent = this.onBodyDamageDealt;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x000082DC File Offset: 0x000064DC
	private void Initialize()
	{
		for (int i = 0; i < this.speechCategories.Length; i++)
		{
			WeightedSelection<SpeechBubbleController.SpeechInfo> weightedSelection = new WeightedSelection<SpeechBubbleController.SpeechInfo>(8);
			for (int j = 0; j < this.speechCategories[i].speechInfos.Length; j++)
			{
				SpeechBubbleController.SpeechInfo speechInfo = this.speechCategories[i].speechInfos[j];
				weightedSelection.AddChoice(speechInfo, speechInfo.weight);
			}
			this.speechCategories[i].speechSelection = weightedSelection;
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00008358 File Offset: 0x00006558
	private void SubmitSpeech(string token)
	{
		this.lastSpeechStopwatch = 0f;
		Chat.SendBroadcastChat(new Chat.NpcChatMessage
		{
			baseToken = token,
			formatStringToken = this.formatStringToken,
			sender = base.gameObject,
			sound = this.sfxLocator.barkSound
		});
	}

	// Token: 0x06000192 RID: 402 RVA: 0x000083AC File Offset: 0x000065AC
	public void TriggerSpeech(string refName)
	{
		if (this.lastSpeechStopwatch < this.minimumDurationBetweenSpeech)
		{
			return;
		}
		for (int i = 0; i < this.speechCategories.Length; i++)
		{
			if (this.speechCategories[i].refName == refName)
			{
				if (this.speechCategories[i].speechSelection.Count <= 0)
				{
					Debug.Log("Ran out of speech options - rebuilding.");
					this.Initialize();
				}
				int num = this.speechCategories[i].speechSelection.EvaluateToChoiceIndex(UnityEngine.Random.value);
				SpeechBubbleController.SpeechInfo value = this.speechCategories[i].speechSelection.GetChoice(num).value;
				if (this.removeRepeats)
				{
					SpeechBubbleController.SpeechCategory speechCategory = this.speechCategories[i];
					speechCategory.speechSelection.RemoveChoice(num);
					this.speechCategories[i] = speechCategory;
				}
				if (UnityEngine.Random.value < this.speechCategories[i].chanceToTrigger)
				{
					this.SubmitSpeech(value.nameToken);
				}
			}
		}
	}

	// Token: 0x04000198 RID: 408
	[Header("Cached Components")]
	public Transform idealBubbleTransform;

	// Token: 0x04000199 RID: 409
	public CharacterBody characterBody;

	// Token: 0x0400019A RID: 410
	public SfxLocator sfxLocator;

	// Token: 0x0400019B RID: 411
	[Header("Speech Info")]
	public string formatStringToken;

	// Token: 0x0400019C RID: 412
	public float minimumDurationBetweenSpeech;

	// Token: 0x0400019D RID: 413
	public bool removeRepeats;

	// Token: 0x0400019E RID: 414
	[SerializeField]
	public SpeechBubbleController.SpeechCategory[] speechCategories;

	// Token: 0x0400019F RID: 415
	[Header("Events")]
	public UnityEvent onBodyDamageDealt;

	// Token: 0x040001A0 RID: 416
	public UnityEvent onStart;

	// Token: 0x040001A1 RID: 417
	public UnityEvent onBodyKill;

	// Token: 0x040001A2 RID: 418
	public UnityEvent onBodyDeath;

	// Token: 0x040001A3 RID: 419
	private GameObject speechBubbleInstance;

	// Token: 0x040001A4 RID: 420
	private float lastSpeechStopwatch;

	// Token: 0x0200005E RID: 94
	[Serializable]
	public struct SpeechCategory
	{
		// Token: 0x040001A5 RID: 421
		public string refName;

		// Token: 0x040001A6 RID: 422
		public float chanceToTrigger;

		// Token: 0x040001A7 RID: 423
		public SpeechBubbleController.SpeechInfo[] speechInfos;

		// Token: 0x040001A8 RID: 424
		public WeightedSelection<SpeechBubbleController.SpeechInfo> speechSelection;
	}

	// Token: 0x0200005F RID: 95
	[Serializable]
	public struct SpeechInfo
	{
		// Token: 0x040001A9 RID: 425
		public string nameToken;

		// Token: 0x040001AA RID: 426
		public float weight;
	}
}
