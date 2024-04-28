using System;
using System.Collections.Generic;
using System.Text;
using HG;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AE0 RID: 2784
	public class RemoteGameCardController : MonoBehaviour
	{
		// Token: 0x06003FFD RID: 16381 RVA: 0x00108A1C File Offset: 0x00106C1C
		public void OpenCurrentGameDetails()
		{
			RectTransform parent = (RectTransform)RoR2Application.instance.mainCanvas.transform;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/RemoteGameBrowser/RemoteGameDetailsPanel"), parent);
			RectTransform rectTransform = (RectTransform)gameObject.transform;
			Vector2 vector = -(rectTransform.rect.size / 2f);
			vector.y = -vector.y;
			rectTransform.localPosition = Vector2.zero + vector;
			gameObject.GetComponent<RemoteGameDetailsPanelController>().SetGameInfo(this.currentGameInfo);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x00108AAC File Offset: 0x00106CAC
		public void SetDisplayData(RemoteGameInfo remoteGameInfo)
		{
			this.currentGameInfo = remoteGameInfo;
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			Texture texture = null;
			if (remoteGameInfo.currentSceneIndex != null)
			{
				SceneDef sceneDef = SceneCatalog.GetSceneDef(remoteGameInfo.currentSceneIndex.Value);
				texture = ((sceneDef != null) ? sceneDef.previewTexture : null);
			}
			this.mapImage.enabled = (texture != null);
			this.mapImage.texture = texture;
			this.passwordIconObject.SetActive(remoteGameInfo.hasPassword ?? false);
			this.playerCountLabel.SetText(stringBuilder.Clear().AppendInt(remoteGameInfo.lobbyPlayerCount ?? (remoteGameInfo.serverPlayerCount ?? 0), 1U, uint.MaxValue).Append("/").AppendInt(remoteGameInfo.lobbyMaxPlayers ?? (remoteGameInfo.serverMaxPlayers ?? 0), 1U, uint.MaxValue));
			if (remoteGameInfo.currentDifficultyIndex != null)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(remoteGameInfo.currentDifficultyIndex.Value);
				this.difficultyIcon.sprite = ((difficultyDef != null) ? difficultyDef.GetIconSprite() : null);
				this.difficultyIcon.enabled = true;
			}
			else
			{
				this.difficultyIcon.enabled = false;
			}
			this.nameLabel.SetText(remoteGameInfo.serverName ?? remoteGameInfo.lobbyName, true);
			stringBuilder.Clear();
			if (remoteGameInfo.ping != null)
			{
				stringBuilder.AppendInt(remoteGameInfo.ping ?? -1, 1U, uint.MaxValue);
			}
			else
			{
				stringBuilder.Append("N/A");
			}
			this.pingLabel.SetText(stringBuilder);
			stringBuilder.Clear();
			if (remoteGameInfo.tags != null && remoteGameInfo.tags.Length != 0)
			{
				stringBuilder.Append(remoteGameInfo.tags[0]);
				for (int i = 1; i < remoteGameInfo.tags.Length; i++)
				{
					stringBuilder.Append(", ").Append(remoteGameInfo.tags[i]);
				}
			}
			this.tagsLabel.SetText(stringBuilder);
			RemoteGameCardController.artifactBuffer.Clear();
			foreach (ArtifactDef item in remoteGameInfo.GetEnabledArtifacts())
			{
				RemoteGameCardController.artifactBuffer.Add(item);
			}
			List<ArtifactDef>.Enumerator enumerator2 = RemoteGameCardController.artifactBuffer.GetEnumerator();
			if (this.artifactDisplayPanelController)
			{
				this.artifactDisplayPanelController.SetDisplayData<List<ArtifactDef>.Enumerator>(ref enumerator2);
			}
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x04003E4B RID: 15947
		public TextMeshProUGUI nameLabel;

		// Token: 0x04003E4C RID: 15948
		public TextMeshProUGUI playerCountLabel;

		// Token: 0x04003E4D RID: 15949
		public TextMeshProUGUI pingLabel;

		// Token: 0x04003E4E RID: 15950
		public TextMeshProUGUI tagsLabel;

		// Token: 0x04003E4F RID: 15951
		public TextMeshProUGUI typeLabel;

		// Token: 0x04003E50 RID: 15952
		public ArtifactDisplayPanelController artifactDisplayPanelController;

		// Token: 0x04003E51 RID: 15953
		public RawImage mapImage;

		// Token: 0x04003E52 RID: 15954
		public GameObject passwordIconObject;

		// Token: 0x04003E53 RID: 15955
		public GameObject difficultyIconObject;

		// Token: 0x04003E54 RID: 15956
		public Image difficultyIcon;

		// Token: 0x04003E55 RID: 15957
		private static List<ArtifactDef> artifactBuffer = new List<ArtifactDef>();

		// Token: 0x04003E56 RID: 15958
		private RemoteGameInfo currentGameInfo;
	}
}
