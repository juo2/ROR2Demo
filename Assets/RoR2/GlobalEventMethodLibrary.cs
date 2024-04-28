using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;

namespace RoR2
{
	// Token: 0x0200053B RID: 1339
	[CreateAssetMenu(menuName = "RoR2/Singletons/GlobalEventMethodLibrary")]
	public class GlobalEventMethodLibrary : ScriptableObject
	{
		// Token: 0x06001867 RID: 6247 RVA: 0x0006A9D5 File Offset: 0x00068BD5
		public void RunAdvanceStageServer(SceneDef nextScene)
		{
			if (NetworkServer.active && Run.instance)
			{
				Run.instance.AdvanceStage(nextScene);
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0006A9F5 File Offset: 0x00068BF5
		public void RunBeginGameOverServer(GameEndingDef endingDef)
		{
			if (NetworkServer.active && Run.instance)
			{
				Run.instance.BeginGameOver(endingDef);
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0006AA15 File Offset: 0x00068C15
		public void LogMessage(string message)
		{
			Debug.Log(message);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0006AA1D File Offset: 0x00068C1D
		public void ForcePlayableDirectorFinish(PlayableDirector playableDirector)
		{
			playableDirector.time = playableDirector.duration;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0006AA2B File Offset: 0x00068C2B
		public void DestroyObject(UnityEngine.Object obj)
		{
			UnityEngine.Object.Destroy(obj);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0006AA34 File Offset: 0x00068C34
		public void DisableAllSiblings(Transform transform)
		{
			if (!transform)
			{
				return;
			}
			Transform parent = transform.parent;
			if (!parent)
			{
				return;
			}
			int i = 0;
			int childCount = parent.childCount;
			while (i < childCount)
			{
				Transform child = parent.GetChild(i);
				if (child && child != transform)
				{
					child.gameObject.SetActive(false);
				}
				i++;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006AA92 File Offset: 0x00068C92
		public void DisableAllSiblings(GameObject gameObject)
		{
			if (!gameObject)
			{
				return;
			}
			this.DisableAllSiblings(gameObject.transform);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0006AAA9 File Offset: 0x00068CA9
		public void ActivateGameObjectIfServer(GameObject gameObject)
		{
			if (!gameObject || !NetworkServer.active)
			{
				return;
			}
			gameObject.SetActive(true);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0006AAC2 File Offset: 0x00068CC2
		public void DeactivateGameObjectIfServer(GameObject gameObject)
		{
			if (!gameObject || !NetworkServer.active)
			{
				return;
			}
			gameObject.SetActive(false);
		}
	}
}
