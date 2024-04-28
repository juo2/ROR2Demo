using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006CE RID: 1742
	public class EventFunctions : MonoBehaviour
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x0009431E File Offset: 0x0009251E
		public void DestroySelf()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0006AA2B File Offset: 0x00068C2B
		public void DestroyGameObject(GameObject obj)
		{
			UnityEngine.Object.Destroy(obj);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0009432B File Offset: 0x0009252B
		public void UnparentTransform(Transform transform)
		{
			if (transform)
			{
				transform.SetParent(null);
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0009433C File Offset: 0x0009253C
		public void ToggleGameObjectActive(GameObject obj)
		{
			obj.SetActive(!obj.activeSelf);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00094350 File Offset: 0x00092550
		public void CreateLocalEffect(GameObject effectObj)
		{
			EffectManager.SpawnEffect(effectObj, new EffectData
			{
				origin = base.transform.position
			}, false);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0009437C File Offset: 0x0009257C
		public void CreateNetworkedEffect(GameObject effectObj)
		{
			EffectManager.SpawnEffect(effectObj, new EffectData
			{
				origin = base.transform.position
			}, true);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000943A8 File Offset: 0x000925A8
		public void OpenURL(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000943B0 File Offset: 0x000925B0
		public void PlaySound(string soundString)
		{
			Util.PlaySound(soundString, base.gameObject);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000943BF File Offset: 0x000925BF
		public void PlayUISound(string soundString)
		{
			Util.PlaySound(soundString, RoR2Application.instance.gameObject);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000943D2 File Offset: 0x000925D2
		public void PlayNetworkedUISound(NetworkSoundEventDef nseDef)
		{
			if (nseDef)
			{
				EffectManager.SimpleSoundEffect(nseDef.index, Vector3.zero, true);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000943ED File Offset: 0x000925ED
		public void RunSetFlag(string flagName)
		{
			if (NetworkServer.active)
			{
				Run instance = Run.instance;
				if (instance == null)
				{
					return;
				}
				instance.SetEventFlag(flagName);
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00094406 File Offset: 0x00092606
		public void RunResetFlag(string flagName)
		{
			if (NetworkServer.active)
			{
				Run instance = Run.instance;
				if (instance == null)
				{
					return;
				}
				instance.ResetEventFlag(flagName);
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00094420 File Offset: 0x00092620
		public void DisableAllChildren()
		{
			for (int i = base.transform.childCount - 1; i >= 0; i--)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0009445C File Offset: 0x0009265C
		public void DisableAllChildrenExcept(GameObject objectToEnable)
		{
			for (int i = base.transform.childCount - 1; i >= 0; i--)
			{
				GameObject gameObject = base.transform.GetChild(i).gameObject;
				if (!(gameObject == objectToEnable))
				{
					gameObject.SetActive(false);
				}
			}
			objectToEnable.SetActive(true);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000944AA File Offset: 0x000926AA
		public void BeginEnding(GameEndingDef gameEndingDef)
		{
			if (NetworkServer.active)
			{
				Run.instance.BeginGameOver(gameEndingDef);
			}
		}
	}
}
