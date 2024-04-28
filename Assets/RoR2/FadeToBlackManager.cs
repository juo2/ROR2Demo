using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020005B5 RID: 1461
	public static class FadeToBlackManager
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00071AA8 File Offset: 0x0006FCA8
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/ScreenTintCanvas"), RoR2Application.instance.mainCanvas.transform);
			FadeToBlackManager.alpha = 0f;
			FadeToBlackManager.image = gameObject.transform.GetChild(0).GetComponent<Image>();
			FadeToBlackManager.UpdateImageAlpha(FadeToBlackManager.alpha);
			RoR2Application.onUpdate += FadeToBlackManager.Update;
			SceneManager.sceneUnloaded += FadeToBlackManager.OnSceneUnloaded;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00071B1E File Offset: 0x0006FD1E
		public static bool fullyFaded
		{
			get
			{
				return FadeToBlackManager.alpha == 2f;
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00071B2C File Offset: 0x0006FD2C
		public static void OnSceneUnloaded(Scene scene)
		{
			FadeToBlackManager.ForceFullBlack();
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00071B33 File Offset: 0x0006FD33
		public static void ForceFullBlack()
		{
			FadeToBlackManager.alpha = 2f;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00071B40 File Offset: 0x0006FD40
		private static void Update()
		{
			float target = 2f;
			float num = 4f;
			if (FadeToBlackManager.fadeCount <= 0)
			{
				target = 0f;
				num *= 0.25f;
			}
			FadeToBlackManager.alpha = Mathf.MoveTowards(FadeToBlackManager.alpha, target, Time.unscaledDeltaTime * num);
			float num2 = 0f;
			List<FadeToBlackOffset> instancesList = InstanceTracker.GetInstancesList<FadeToBlackOffset>();
			for (int i = 0; i < instancesList.Count; i++)
			{
				FadeToBlackOffset fadeToBlackOffset = instancesList[i];
				num2 += fadeToBlackOffset.value;
			}
			FadeToBlackManager.UpdateImageAlpha(FadeToBlackManager.alpha + num2);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00071BC8 File Offset: 0x0006FDC8
		private static void UpdateImageAlpha(float finalAlpha)
		{
			Color color = FadeToBlackManager.image.color;
			Color color2 = color;
			color2.a = finalAlpha;
			FadeToBlackManager.image.color = color2;
			FadeToBlackManager.image.raycastTarget = (color2.a > color.a);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00071C0D File Offset: 0x0006FE0D
		public static void ForceClear()
		{
			FadeToBlackManager.fadeCount = 0;
			FadeToBlackManager.alpha = 0f;
			TransitionCommand.ForceClearFadeToBlack();
		}

		// Token: 0x0400208D RID: 8333
		private static Image image;

		// Token: 0x0400208E RID: 8334
		public static int fadeCount;

		// Token: 0x0400208F RID: 8335
		private static float alpha;

		// Token: 0x04002090 RID: 8336
		private const float fadeDuration = 0.25f;

		// Token: 0x04002091 RID: 8337
		private const float inversefadeDuration = 4f;
	}
}
