using System;
using Rewired;
using RoR2.Stats;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200095D RID: 2397
	public class LocalUser
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000E5AC8 File Offset: 0x000E3CC8
		// (set) Token: 0x06003645 RID: 13893 RVA: 0x000E5AD0 File Offset: 0x000E3CD0
		public Player inputPlayer
		{
			get
			{
				return this._inputPlayer;
			}
			set
			{
				if (this._inputPlayer == value)
				{
					return;
				}
				if (this._inputPlayer != null)
				{
					this.OnRewiredPlayerLost(this._inputPlayer);
				}
				this._inputPlayer = value;
				this.eventSystem = MPEventSystemManager.FindEventSystem(this._inputPlayer);
				if (this._inputPlayer != null)
				{
					this.OnRewiredPlayerDiscovered(this._inputPlayer);
				}
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000E5B27 File Offset: 0x000E3D27
		// (set) Token: 0x06003647 RID: 13895 RVA: 0x000E5B2F File Offset: 0x000E3D2F
		public MPEventSystem eventSystem { get; private set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000E5B38 File Offset: 0x000E3D38
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x000E5B40 File Offset: 0x000E3D40
		public UserProfile userProfile
		{
			get
			{
				return this._userProfile;
			}
			set
			{
				this._userProfile = value;
				this.ApplyUserProfileBindingsToRewiredPlayer();
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000E5B4F File Offset: 0x000E3D4F
		static LocalUser()
		{
			ReInput.ControllerConnectedEvent += LocalUser.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += LocalUser.OnControllerDisconnected;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000E5B74 File Offset: 0x000E3D74
		private static void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			foreach (LocalUser localUser in LocalUserManager.readOnlyLocalUsersList)
			{
				if (localUser.inputPlayer.controllers.ContainsController(args.controllerType, args.controllerId))
				{
					localUser.OnControllerDiscovered(ReInput.controllers.GetController(args.controllerType, args.controllerId));
				}
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000E5BF4 File Offset: 0x000E3DF4
		private static void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			foreach (LocalUser localUser in LocalUserManager.readOnlyLocalUsersList)
			{
				if (localUser.inputPlayer.controllers.ContainsController(args.controllerType, args.controllerId))
				{
					localUser.OnControllerLost(ReInput.controllers.GetController(args.controllerType, args.controllerId));
				}
			}
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000E5C74 File Offset: 0x000E3E74
		private void OnRewiredPlayerDiscovered(Player player)
		{
			foreach (Controller controller in player.controllers.Controllers)
			{
				this.OnControllerDiscovered(controller);
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000E5CC8 File Offset: 0x000E3EC8
		private void OnRewiredPlayerLost(Player player)
		{
			foreach (Controller controller in player.controllers.Controllers)
			{
				this.OnControllerLost(controller);
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000E5D1C File Offset: 0x000E3F1C
		private void OnControllerDiscovered(Controller controller)
		{
			this.ApplyUserProfileBindingstoRewiredController(controller);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000E5D25 File Offset: 0x000E3F25
		private void OnControllerLost(Controller controller)
		{
			this.inputPlayer.controllers.maps.ClearMapsForController(controller.type, controller.id, true);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000E5D4C File Offset: 0x000E3F4C
		private void ApplyUserProfileBindingstoRewiredController(Controller controller)
		{
			if (this.userProfile == null)
			{
				return;
			}
			ControllerMap controllerMap = null;
			switch (controller.type)
			{
			case ControllerType.Keyboard:
				controllerMap = this.userProfile.keyboardMap;
				break;
			case ControllerType.Mouse:
				controllerMap = this.userProfile.mouseMap;
				break;
			case ControllerType.Joystick:
				controllerMap = this.userProfile.joystickMap;
				break;
			}
			if (controllerMap != null)
			{
				this.inputPlayer.controllers.maps.AddMap(controller, controllerMap);
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000E5DC4 File Offset: 0x000E3FC4
		public void ApplyUserProfileBindingsToRewiredPlayer()
		{
			if (this.inputPlayer == null)
			{
				return;
			}
			if (this.userProfile != null)
			{
				this.inputPlayer.controllers.maps.ClearAllMaps(false);
				foreach (Controller controller in this.inputPlayer.controllers.Controllers)
				{
					this.inputPlayer.controllers.maps.LoadMap(controller.type, controller.id, 2, 0);
					this.ApplyUserProfileBindingstoRewiredController(controller);
				}
				this.inputPlayer.controllers.maps.SetAllMapsEnabled(true);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000E5E80 File Offset: 0x000E4080
		public bool isUIFocused
		{
			get
			{
				return this.eventSystem.currentSelectedGameObject;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000E5E92 File Offset: 0x000E4092
		// (set) Token: 0x06003655 RID: 13909 RVA: 0x000E5E9A File Offset: 0x000E409A
		public NetworkUser currentNetworkUser { get; private set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x000E5EA3 File Offset: 0x000E40A3
		// (set) Token: 0x06003657 RID: 13911 RVA: 0x000E5EAB File Offset: 0x000E40AB
		public PlayerCharacterMasterController cachedMasterController { get; private set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06003658 RID: 13912 RVA: 0x000E5EB4 File Offset: 0x000E40B4
		// (set) Token: 0x06003659 RID: 13913 RVA: 0x000E5EBC File Offset: 0x000E40BC
		public CharacterMaster cachedMaster { get; private set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000E5EC5 File Offset: 0x000E40C5
		// (set) Token: 0x0600365B RID: 13915 RVA: 0x000E5ECD File Offset: 0x000E40CD
		public GameObject cachedMasterObject { get; private set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000E5ED6 File Offset: 0x000E40D6
		// (set) Token: 0x0600365D RID: 13917 RVA: 0x000E5EDE File Offset: 0x000E40DE
		public CharacterBody cachedBody { get; private set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000E5EE7 File Offset: 0x000E40E7
		// (set) Token: 0x0600365F RID: 13919 RVA: 0x000E5EEF File Offset: 0x000E40EF
		public GameObject cachedBodyObject { get; private set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000E5EF8 File Offset: 0x000E40F8
		// (set) Token: 0x06003661 RID: 13921 RVA: 0x000E5F00 File Offset: 0x000E4100
		public PlayerStatsComponent cachedStatsComponent { get; private set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000E5F09 File Offset: 0x000E4109
		// (set) Token: 0x06003663 RID: 13923 RVA: 0x000E5F14 File Offset: 0x000E4114
		public CameraRigController cameraRigController
		{
			get
			{
				return this._cameraRigController;
			}
			set
			{
				if (this._cameraRigController == value)
				{
					return;
				}
				if (this._cameraRigController != null)
				{
					Action<CameraRigController> action = this.onCameraLost;
					if (action != null)
					{
						action(this._cameraRigController);
					}
				}
				this._cameraRigController = value;
				if (this._cameraRigController != null)
				{
					Action<CameraRigController> action2 = this.onCameraDiscovered;
					if (action2 == null)
					{
						return;
					}
					action2(this._cameraRigController);
				}
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000E5F70 File Offset: 0x000E4170
		public void RebuildControlChain()
		{
			PlayerCharacterMasterController cachedMasterController = this.cachedMasterController;
			this.cachedMasterController = null;
			this.cachedMasterObject = null;
			this.cachedMaster = null;
			this.cachedStatsComponent = null;
			CharacterBody cachedBody = this.cachedBody;
			this.cachedBody = null;
			this.cachedBodyObject = null;
			if (this.currentNetworkUser)
			{
				this.cachedMasterObject = this.currentNetworkUser.masterObject;
				if (this.cachedMasterObject)
				{
					this.cachedMasterController = this.cachedMasterObject.GetComponent<PlayerCharacterMasterController>();
				}
				if (this.cachedMasterController)
				{
					this.cachedMaster = this.cachedMasterController.master;
					if (this.cachedMaster)
					{
						this.cachedStatsComponent = this.cachedMaster.playerStatsComponent;
					}
					this.cachedBody = this.cachedMaster.GetBody();
					if (this.cachedBody)
					{
						this.cachedBodyObject = this.cachedBody.gameObject;
					}
				}
			}
			if (cachedBody != this.cachedBody)
			{
				Action action = this.onBodyChanged;
				if (action != null)
				{
					action();
				}
			}
			if (cachedMasterController != this.cachedMasterController)
			{
				Action action2 = this.onMasterChanged;
				if (action2 == null)
				{
					return;
				}
				action2();
			}
		}

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06003665 RID: 13925 RVA: 0x000E6090 File Offset: 0x000E4290
		// (remove) Token: 0x06003666 RID: 13926 RVA: 0x000E60C8 File Offset: 0x000E42C8
		public event Action onBodyChanged;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06003667 RID: 13927 RVA: 0x000E6100 File Offset: 0x000E4300
		// (remove) Token: 0x06003668 RID: 13928 RVA: 0x000E6138 File Offset: 0x000E4338
		public event Action onMasterChanged;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06003669 RID: 13929 RVA: 0x000E6170 File Offset: 0x000E4370
		// (remove) Token: 0x0600366A RID: 13930 RVA: 0x000E61A8 File Offset: 0x000E43A8
		public event Action<CameraRigController> onCameraDiscovered;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x0600366B RID: 13931 RVA: 0x000E61E0 File Offset: 0x000E43E0
		// (remove) Token: 0x0600366C RID: 13932 RVA: 0x000E6218 File Offset: 0x000E4418
		public event Action<CameraRigController> onCameraLost;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x0600366D RID: 13933 RVA: 0x000E6250 File Offset: 0x000E4450
		// (remove) Token: 0x0600366E RID: 13934 RVA: 0x000E6288 File Offset: 0x000E4488
		public event Action<NetworkUser> onNetworkUserFound;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x0600366F RID: 13935 RVA: 0x000E62C0 File Offset: 0x000E44C0
		// (remove) Token: 0x06003670 RID: 13936 RVA: 0x000E62F8 File Offset: 0x000E44F8
		public event Action<NetworkUser> onNetworkUserLost;

		// Token: 0x06003671 RID: 13937 RVA: 0x000E632D File Offset: 0x000E452D
		public void LinkNetworkUser(NetworkUser newNetworkUser)
		{
			if (this.currentNetworkUser)
			{
				return;
			}
			this.currentNetworkUser = newNetworkUser;
			newNetworkUser.localUser = this;
			Action<NetworkUser> action = this.onNetworkUserFound;
			if (action == null)
			{
				return;
			}
			action(newNetworkUser);
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000E635C File Offset: 0x000E455C
		public void UnlinkNetworkUser()
		{
			Action<NetworkUser> action = this.onNetworkUserLost;
			if (action != null)
			{
				action(this.currentNetworkUser);
			}
			this.currentNetworkUser.localUser = null;
			this.currentNetworkUser = null;
			this.cachedMasterController = null;
			this.cachedMasterObject = null;
			this.cachedBody = null;
			this.cachedBodyObject = null;
		}

		// Token: 0x040036F2 RID: 14066
		private Player _inputPlayer;

		// Token: 0x040036F4 RID: 14068
		private UserProfile _userProfile;

		// Token: 0x040036F5 RID: 14069
		public int id;

		// Token: 0x040036FD RID: 14077
		private CameraRigController _cameraRigController;
	}
}
