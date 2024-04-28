using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.CharacterSpeech
{
	// Token: 0x02000C27 RID: 3111
	[RequireComponent(typeof(SimpleCombatSpeechDriver))]
	[RequireComponent(typeof(CharacterSpeechController))]
	public class BrotherSpeechDriver : BaseCharacterSpeechDriver
	{
		// Token: 0x06004643 RID: 17987 RVA: 0x00122CC7 File Offset: 0x00120EC7
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			BrotherSpeechDriver.hereticBodyIndex = BodyCatalog.FindBodyIndex("HereticBody");
			BrotherSpeechDriver.titanGoldBodyIndex = BodyCatalog.FindBodyIndex("TitanGoldBody");
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x00122CE8 File Offset: 0x00120EE8
		protected new void Awake()
		{
			base.Awake();
			if (!NetworkServer.active)
			{
				return;
			}
			this.simpleCombatSpeechDriver = base.GetComponent<SimpleCombatSpeechDriver>();
			this.simpleCombatSpeechDriver.onBodyKill.AddListener(new UnityAction<DamageReport>(this.OnBodyKill));
			CharacterBody.onBodyStartGlobal += this.OnCharacterBodyStartGlobal;
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x00122D3C File Offset: 0x00120F3C
		protected new void OnDestroy()
		{
			CharacterBody.onBodyStartGlobal -= this.OnCharacterBodyStartGlobal;
			base.OnDestroy();
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x00122D55 File Offset: 0x00120F55
		private void OnCharacterBodyStartGlobal(CharacterBody characterBody)
		{
			if (characterBody.bodyIndex == BrotherSpeechDriver.titanGoldBodyIndex)
			{
				UnityEvent unityEvent = this.onTitanGoldSighted;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00122D74 File Offset: 0x00120F74
		public void DoInitialSightResponse()
		{
			bool flag = false;
			bool flag2 = false;
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				BodyIndex bodyIndex = readOnlyInstancesList[i].bodyIndex;
				flag |= (bodyIndex == BrotherSpeechDriver.hereticBodyIndex);
				flag2 |= (bodyIndex == BrotherSpeechDriver.titanGoldBodyIndex);
			}
			BrotherSpeechDriver.<>c__DisplayClass14_0 CS$<>8__locals1;
			CS$<>8__locals1.responsePool = Array.Empty<CharacterSpeechController.SpeechInfo>();
			if (flag && flag2)
			{
				BrotherSpeechDriver.<DoInitialSightResponse>g__TrySetResponsePool|14_0(this.seeHereticAndTitanGoldResponses, ref CS$<>8__locals1);
			}
			if (flag)
			{
				BrotherSpeechDriver.<DoInitialSightResponse>g__TrySetResponsePool|14_0(this.seeHereticResponses, ref CS$<>8__locals1);
			}
			if (flag2)
			{
				BrotherSpeechDriver.<DoInitialSightResponse>g__TrySetResponsePool|14_0(this.seeTitanGoldResponses, ref CS$<>8__locals1);
			}
			this.SendReponseFromPool(CS$<>8__locals1.responsePool);
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x00122E14 File Offset: 0x00121014
		private void OnBodyKill(DamageReport damageReport)
		{
			Debug.Log("BrotherSpeechDriver.OnBodyKill()");
			if (damageReport.victimBody)
			{
				BrotherSpeechDriver.<>c__DisplayClass15_0 CS$<>8__locals1;
				CS$<>8__locals1.responsePool = Array.Empty<CharacterSpeechController.SpeechInfo>();
				if (damageReport.victimBodyIndex == BrotherSpeechDriver.hereticBodyIndex)
				{
					BrotherSpeechDriver.<OnBodyKill>g__TrySetResponsePool|15_0(this.killHereticResponses, ref CS$<>8__locals1);
				}
				else if (damageReport.victimBodyIndex == BrotherSpeechDriver.titanGoldBodyIndex)
				{
					BrotherSpeechDriver.<OnBodyKill>g__TrySetResponsePool|15_0(this.killTitanGoldReponses, ref CS$<>8__locals1);
				}
				else if ((damageReport.victimBody.bodyFlags &= CharacterBody.BodyFlags.Mechanical) == CharacterBody.BodyFlags.Mechanical && this.killMechanicalResponses.Length != 0)
				{
					BrotherSpeechDriver.<OnBodyKill>g__TrySetResponsePool|15_0(this.killMechanicalResponses, ref CS$<>8__locals1);
				}
				this.SendReponseFromPool(CS$<>8__locals1.responsePool);
			}
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x00122EB7 File Offset: 0x001210B7
		private void SendReponseFromPool(CharacterSpeechController.SpeechInfo[] responsePool)
		{
			if (responsePool.Length != 0)
			{
				base.characterSpeechController.EnqueueSpeech(responsePool[UnityEngine.Random.Range(0, responsePool.Length - 1)]);
			}
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00122EEF File Offset: 0x001210EF
		[CompilerGenerated]
		internal static void <DoInitialSightResponse>g__TrySetResponsePool|14_0(CharacterSpeechController.SpeechInfo[] newResponsePool, ref BrotherSpeechDriver.<>c__DisplayClass14_0 A_1)
		{
			if (A_1.responsePool.Length == 0)
			{
				A_1.responsePool = newResponsePool;
			}
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x00122F01 File Offset: 0x00121101
		[CompilerGenerated]
		internal static void <OnBodyKill>g__TrySetResponsePool|15_0(CharacterSpeechController.SpeechInfo[] newResponsePool, ref BrotherSpeechDriver.<>c__DisplayClass15_0 A_1)
		{
			if (A_1.responsePool.Length == 0)
			{
				A_1.responsePool = newResponsePool;
			}
		}

		// Token: 0x04004428 RID: 17448
		public CharacterSpeechController.SpeechInfo[] seeHereticResponses;

		// Token: 0x04004429 RID: 17449
		public CharacterSpeechController.SpeechInfo[] seeTitanGoldResponses;

		// Token: 0x0400442A RID: 17450
		public CharacterSpeechController.SpeechInfo[] seeHereticAndTitanGoldResponses;

		// Token: 0x0400442B RID: 17451
		public CharacterSpeechController.SpeechInfo[] killMechanicalResponses;

		// Token: 0x0400442C RID: 17452
		public CharacterSpeechController.SpeechInfo[] killHereticResponses;

		// Token: 0x0400442D RID: 17453
		public CharacterSpeechController.SpeechInfo[] killTitanGoldReponses;

		// Token: 0x0400442E RID: 17454
		public UnityEvent onTitanGoldSighted;

		// Token: 0x0400442F RID: 17455
		private SimpleCombatSpeechDriver simpleCombatSpeechDriver;

		// Token: 0x04004430 RID: 17456
		private static BodyIndex hereticBodyIndex = BodyIndex.None;

		// Token: 0x04004431 RID: 17457
		private static BodyIndex titanGoldBodyIndex = BodyIndex.None;
	}
}
