using System;
using System.Collections.ObjectModel;
using Rewired;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200060F RID: 1551
	[RequireComponent(typeof(CameraRigController))]
	public class CameraRigControllerSpectateControls : MonoBehaviour
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x00079962 File Offset: 0x00077B62
		private void Awake()
		{
			this.cameraRigController = base.GetComponent<CameraRigController>();
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00079970 File Offset: 0x00077B70
		private static bool CanUserSpectateBody(NetworkUser viewer, CharacterBody body)
		{
			return Util.LookUpBodyNetworkUser(body.gameObject);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00079984 File Offset: 0x00077B84
		public static GameObject GetNextSpectateGameObject(NetworkUser viewer, GameObject currentGameObject)
		{
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			if (readOnlyInstancesList.Count == 0)
			{
				return null;
			}
			CharacterBody characterBody = currentGameObject ? currentGameObject.GetComponent<CharacterBody>() : null;
			int num = characterBody ? readOnlyInstancesList.IndexOf(characterBody) : 0;
			for (int i = num + 1; i < readOnlyInstancesList.Count; i++)
			{
				if (CameraRigControllerSpectateControls.CanUserSpectateBody(viewer, readOnlyInstancesList[i]))
				{
					return readOnlyInstancesList[i].gameObject;
				}
			}
			for (int j = 0; j <= num; j++)
			{
				if (CameraRigControllerSpectateControls.CanUserSpectateBody(viewer, readOnlyInstancesList[j]))
				{
					return readOnlyInstancesList[j].gameObject;
				}
			}
			return null;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00079A24 File Offset: 0x00077C24
		public static GameObject GetPreviousSpectateGameObject(NetworkUser viewer, GameObject currentGameObject)
		{
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			if (readOnlyInstancesList.Count == 0)
			{
				return null;
			}
			CharacterBody characterBody = currentGameObject ? currentGameObject.GetComponent<CharacterBody>() : null;
			int num = characterBody ? readOnlyInstancesList.IndexOf(characterBody) : 0;
			for (int i = num - 1; i >= 0; i--)
			{
				if (CameraRigControllerSpectateControls.CanUserSpectateBody(viewer, readOnlyInstancesList[i]))
				{
					return readOnlyInstancesList[i].gameObject;
				}
			}
			for (int j = readOnlyInstancesList.Count - 1; j >= num; j--)
			{
				if (CameraRigControllerSpectateControls.CanUserSpectateBody(viewer, readOnlyInstancesList[j]))
				{
					return readOnlyInstancesList[j].gameObject;
				}
			}
			return null;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00079AC8 File Offset: 0x00077CC8
		private void Update()
		{
			LocalUser localUserViewer = this.cameraRigController.localUserViewer;
			Player player = (localUserViewer != null) ? localUserViewer.inputPlayer : null;
			if (this.cameraRigController.cameraMode != null && this.cameraRigController.cameraMode.IsSpectating(this.cameraRigController) && player != null)
			{
				if (player.GetButtonDown(7))
				{
					this.cameraRigController.nextTarget = CameraRigControllerSpectateControls.GetNextSpectateGameObject(this.cameraRigController.viewer, this.cameraRigController.target);
				}
				if (player.GetButtonDown(8))
				{
					this.cameraRigController.nextTarget = CameraRigControllerSpectateControls.GetPreviousSpectateGameObject(this.cameraRigController.viewer, this.cameraRigController.target);
				}
			}
		}

		// Token: 0x04002262 RID: 8802
		private CameraRigController cameraRigController;
	}
}
