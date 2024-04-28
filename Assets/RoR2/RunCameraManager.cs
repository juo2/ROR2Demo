using System;
using System.Collections.ObjectModel;
using RoR2.CameraModes;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000864 RID: 2148
	public class RunCameraManager : MonoBehaviour
	{
		// Token: 0x06002F10 RID: 12048 RVA: 0x000C89BC File Offset: 0x000C6BBC
		private static GameObject GetNetworkUserBodyObject(NetworkUser networkUser)
		{
			if (networkUser.masterObject)
			{
				CharacterMaster component = networkUser.masterObject.GetComponent<CharacterMaster>();
				if (component)
				{
					return component.GetBodyObject();
				}
			}
			return null;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000C89F4 File Offset: 0x000C6BF4
		private static TeamIndex GetNetworkUserTeamIndex(NetworkUser networkUser)
		{
			if (networkUser.masterObject)
			{
				CharacterMaster component = networkUser.masterObject.GetComponent<CharacterMaster>();
				if (component)
				{
					return component.teamIndex;
				}
			}
			return TeamIndex.Neutral;
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000C8A2C File Offset: 0x000C6C2C
		private void Update()
		{
			bool flag = Stage.instance;
			if (flag)
			{
				int i = 0;
				int count = CameraRigController.readOnlyInstancesList.Count;
				while (i < count)
				{
					if (CameraRigController.readOnlyInstancesList[i].suppressPlayerCameras)
					{
						return;
					}
					i++;
				}
			}
			if (flag)
			{
				int num = 0;
				ReadOnlyCollection<NetworkUser> readOnlyLocalPlayersList = NetworkUser.readOnlyLocalPlayersList;
				for (int j = 0; j < readOnlyLocalPlayersList.Count; j++)
				{
					NetworkUser networkUser = readOnlyLocalPlayersList[j];
					CameraRigController cameraRigController = this.cameras[num];
					if (!cameraRigController)
					{
						cameraRigController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Main Camera")).GetComponent<CameraRigController>();
						this.cameras[num] = cameraRigController;
					}
					cameraRigController.viewer = networkUser;
					networkUser.cameraRigController = cameraRigController;
					GameObject networkUserBodyObject = RunCameraManager.GetNetworkUserBodyObject(networkUser);
					ForceSpectate forceSpectate = InstanceTracker.FirstOrNull<ForceSpectate>();
					if (forceSpectate)
					{
						cameraRigController.nextTarget = forceSpectate.target;
						cameraRigController.cameraMode = CameraModePlayerBasic.spectator;
					}
					else if (networkUserBodyObject)
					{
						cameraRigController.nextTarget = networkUserBodyObject;
						cameraRigController.cameraMode = CameraModePlayerBasic.playerBasic;
					}
					else if (!cameraRigController.disableSpectating)
					{
						cameraRigController.cameraMode = CameraModePlayerBasic.spectator;
						if (!cameraRigController.target)
						{
							cameraRigController.nextTarget = CameraRigControllerSpectateControls.GetNextSpectateGameObject(networkUser, null);
						}
					}
					else
					{
						cameraRigController.cameraMode = CameraModeNone.instance;
					}
					num++;
				}
				int num2 = num;
				for (int k = num; k < this.cameras.Length; k++)
				{
					ref CameraRigController ptr = ref this.cameras[num];
					if (ptr != null)
					{
						if (ptr)
						{
							UnityEngine.Object.Destroy(this.cameras[num].gameObject);
						}
						ptr = null;
					}
				}
				Rect[] array = RunCameraManager.screenLayouts[num2];
				for (int l = 0; l < num2; l++)
				{
					this.cameras[l].viewport = array[l];
				}
				return;
			}
			for (int m = 0; m < this.cameras.Length; m++)
			{
				if (this.cameras[m])
				{
					UnityEngine.Object.Destroy(this.cameras[m].gameObject);
				}
			}
		}

		// Token: 0x040030FC RID: 12540
		private readonly CameraRigController[] cameras = new CameraRigController[RoR2Application.maxLocalPlayers];

		// Token: 0x040030FD RID: 12541
		private static readonly Rect[][] screenLayouts = new Rect[][]
		{
			new Rect[0],
			new Rect[]
			{
				new Rect(0f, 0f, 1f, 1f)
			},
			new Rect[]
			{
				new Rect(0f, 0.5f, 1f, 0.5f),
				new Rect(0f, 0f, 1f, 0.5f)
			},
			new Rect[]
			{
				new Rect(0f, 0.5f, 1f, 0.5f),
				new Rect(0f, 0f, 0.5f, 0.5f),
				new Rect(0.5f, 0f, 0.5f, 0.5f)
			},
			new Rect[]
			{
				new Rect(0f, 0.5f, 0.5f, 0.5f),
				new Rect(0.5f, 0.5f, 0.5f, 0.5f),
				new Rect(0f, 0f, 0.5f, 0.5f),
				new Rect(0.5f, 0f, 0.5f, 0.5f)
			}
		};
	}
}
