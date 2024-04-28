using System;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x0200064B RID: 1611
	public class CharacterSelectSurvivorPreviewDisplayController : MonoBehaviour
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000864F1 File Offset: 0x000846F1
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x000864F9 File Offset: 0x000846F9
		public NetworkUser networkUser { get; set; }

		// Token: 0x06001F3F RID: 7999 RVA: 0x00086502 File Offset: 0x00084702
		private void OnEnable()
		{
			this.currentLoadout = Loadout.RequestInstance();
			NetworkUser.onLoadoutChangedGlobal += this.OnLoadoutChangedGlobal;
			RoR2Application.onNextUpdate += this.Refresh;
			this.RunDefaultResponses();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00086537 File Offset: 0x00084737
		private void OnDisable()
		{
			NetworkUser.onLoadoutChangedGlobal -= this.OnLoadoutChangedGlobal;
			this.currentLoadout = Loadout.ReturnInstance(this.currentLoadout);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0008655C File Offset: 0x0008475C
		private static int FindSkillSlotIndex(BodyIndex bodyIndex, SkillFamily skillFamily)
		{
			GenericSkill[] bodyPrefabSkillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndex);
			for (int i = 0; i < bodyPrefabSkillSlots.Length; i++)
			{
				if (bodyPrefabSkillSlots[i].skillFamily == skillFamily)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00086594 File Offset: 0x00084794
		private static int FindVariantIndex(SkillFamily skillFamily, SkillDef skillDef)
		{
			SkillFamily.Variant[] variants = skillFamily.variants;
			for (int i = 0; i < variants.Length; i++)
			{
				if (variants[i].skillDef == skillDef)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000865D0 File Offset: 0x000847D0
		private static bool HasSkillVariantEnabled(Loadout loadout, BodyIndex bodyIndex, SkillFamily skillFamily, SkillDef skillDef)
		{
			int num = CharacterSelectSurvivorPreviewDisplayController.FindSkillSlotIndex(bodyIndex, skillFamily);
			int num2 = CharacterSelectSurvivorPreviewDisplayController.FindVariantIndex(skillFamily, skillDef);
			return num != -1 && num2 != -1 && (ulong)loadout.bodyLoadoutManager.GetSkillVariant(bodyIndex, num) == (ulong)((long)num2);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00086609 File Offset: 0x00084809
		private void Refresh()
		{
			if (this && this.networkUser)
			{
				this.OnLoadoutChangedGlobal(this.networkUser);
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0008662C File Offset: 0x0008482C
		private void OnLoadoutChangedGlobal(NetworkUser changedNetworkUser)
		{
			if (changedNetworkUser != this.networkUser)
			{
				return;
			}
			Loadout loadout = Loadout.RequestInstance();
			changedNetworkUser.networkLoadout.CopyLoadout(loadout);
			BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(this.bodyPrefab);
			if (bodyIndex == BodyIndex.None)
			{
				return;
			}
			foreach (CharacterSelectSurvivorPreviewDisplayController.SkillChangeResponse skillChangeResponse in this.skillChangeResponses)
			{
				int num = CharacterSelectSurvivorPreviewDisplayController.HasSkillVariantEnabled(this.currentLoadout, bodyIndex, skillChangeResponse.triggerSkillFamily, skillChangeResponse.triggerSkill) ? 1 : 0;
				bool flag = CharacterSelectSurvivorPreviewDisplayController.HasSkillVariantEnabled(loadout, bodyIndex, skillChangeResponse.triggerSkillFamily, skillChangeResponse.triggerSkill);
				if (num == 0 && flag)
				{
					UnityEvent response = skillChangeResponse.response;
					if (response != null)
					{
						response.Invoke();
					}
				}
			}
			foreach (CharacterSelectSurvivorPreviewDisplayController.SkinChangeResponse skinChangeResponse in this.skinChangeResponses)
			{
				uint num2 = (uint)SkinCatalog.FindLocalSkinIndexForBody(bodyIndex, skinChangeResponse.triggerSkin);
				uint skinIndex = this.currentLoadout.bodyLoadoutManager.GetSkinIndex(bodyIndex);
				uint skinIndex2 = loadout.bodyLoadoutManager.GetSkinIndex(bodyIndex);
				if (skinIndex != skinIndex2 && skinIndex2 == num2)
				{
					UnityEvent response2 = skinChangeResponse.response;
					if (response2 != null)
					{
						response2.Invoke();
					}
				}
			}
			loadout.Copy(this.currentLoadout);
			Loadout.ReturnInstance(loadout);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00086754 File Offset: 0x00084954
		private void RunDefaultResponses()
		{
			BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(this.bodyPrefab);
			if (bodyIndex == BodyIndex.None)
			{
				return;
			}
			foreach (CharacterSelectSurvivorPreviewDisplayController.SkillChangeResponse skillChangeResponse in this.skillChangeResponses)
			{
				if (CharacterSelectSurvivorPreviewDisplayController.HasSkillVariantEnabled(this.currentLoadout, bodyIndex, skillChangeResponse.triggerSkillFamily, skillChangeResponse.triggerSkill))
				{
					UnityEvent response = skillChangeResponse.response;
					if (response != null)
					{
						response.Invoke();
					}
				}
			}
			foreach (CharacterSelectSurvivorPreviewDisplayController.SkinChangeResponse skinChangeResponse in this.skinChangeResponses)
			{
				uint num = (uint)SkinCatalog.FindLocalSkinIndexForBody(bodyIndex, skinChangeResponse.triggerSkin);
				if (this.currentLoadout.bodyLoadoutManager.GetSkinIndex(bodyIndex) == num)
				{
					UnityEvent response2 = skinChangeResponse.response;
					if (response2 != null)
					{
						response2.Invoke();
					}
				}
			}
		}

		// Token: 0x040024C8 RID: 9416
		public GameObject bodyPrefab;

		// Token: 0x040024C9 RID: 9417
		public CharacterSelectSurvivorPreviewDisplayController.SkillChangeResponse[] skillChangeResponses;

		// Token: 0x040024CA RID: 9418
		public CharacterSelectSurvivorPreviewDisplayController.SkinChangeResponse[] skinChangeResponses;

		// Token: 0x040024CC RID: 9420
		private Loadout currentLoadout;

		// Token: 0x0200064C RID: 1612
		[Serializable]
		public struct SkillChangeResponse
		{
			// Token: 0x040024CD RID: 9421
			public SkillFamily triggerSkillFamily;

			// Token: 0x040024CE RID: 9422
			public SkillDef triggerSkill;

			// Token: 0x040024CF RID: 9423
			public UnityEvent response;
		}

		// Token: 0x0200064D RID: 1613
		[Serializable]
		public struct SkinChangeResponse
		{
			// Token: 0x040024D0 RID: 9424
			public SkinDef triggerSkin;

			// Token: 0x040024D1 RID: 9425
			public UnityEvent response;
		}
	}
}
