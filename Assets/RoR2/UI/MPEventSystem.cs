using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Rewired;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D41 RID: 3393
	[RequireComponent(typeof(RewiredStandaloneInputModule))]
	public class MPEventSystem : EventSystem
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x0013FD67 File Offset: 0x0013DF67
		// (set) Token: 0x06004D84 RID: 19844 RVA: 0x0013FD6E File Offset: 0x0013DF6E
		public static int activeCount { get; private set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x0013FD76 File Offset: 0x0013DF76
		// (set) Token: 0x06004D86 RID: 19846 RVA: 0x0013FD7E File Offset: 0x0013DF7E
		public int cursorOpenerCount { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x0013FD87 File Offset: 0x0013DF87
		// (set) Token: 0x06004D88 RID: 19848 RVA: 0x0013FD8F File Offset: 0x0013DF8F
		public int cursorOpenerForGamepadCount { get; set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x0013FD98 File Offset: 0x0013DF98
		public bool isHovering
		{
			get
			{
				return base.currentInputModule && ((MPInputModule)base.currentInputModule).isHovering;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x0013FDB9 File Offset: 0x0013DFB9
		public bool isCursorVisible
		{
			get
			{
				return this.cursorIndicatorController.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x0013FDCC File Offset: 0x0013DFCC
		public static MPEventSystem FindByPlayer(Player player)
		{
			foreach (MPEventSystem mpeventSystem in MPEventSystem.instancesList)
			{
				if (mpeventSystem.player == player)
				{
					return mpeventSystem;
				}
			}
			return null;
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x0013FE28 File Offset: 0x0013E028
		protected override void Update()
		{
			EventSystem current = EventSystem.current;
			EventSystem.current = this;
			base.Update();
			EventSystem.current = current;
			this.ValidateCurrentSelectedGameobject();
			if (this.player.GetButtonDown(25) && (PauseScreenController.instancesList.Count == 0 || SimpleDialogBox.instancesList.Count == 0))
			{
				Console.instance.SubmitCmd(null, "pause", false);
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x0013FE89 File Offset: 0x0013E089
		protected override void Awake()
		{
			base.Awake();
			MPEventSystem.instancesList.Add(this);
			this.cursorIndicatorController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/CursorIndicator"), base.transform).GetComponent<CursorIndicatorController>();
			this.inputMapperHelper = new InputMapperHelper(this);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0013FEC8 File Offset: 0x0013E0C8
		private void ValidateCurrentSelectedGameobject()
		{
			if (base.currentSelectedGameObject)
			{
				MPButton component = base.currentSelectedGameObject.GetComponent<MPButton>();
				if (component && (!component.CanBeSelected() || (this.currentInputSource == MPEventSystem.InputSource.Gamepad && component.navigation.mode == Navigation.Mode.None)))
				{
					base.SetSelectedGameObject(null);
				}
			}
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0013FF1E File Offset: 0x0013E11E
		public void SetSelectedObject(GameObject o)
		{
			base.SetSelectedGameObject(o);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0013FF27 File Offset: 0x0013E127
		private static void OnActiveSceneChanged(Scene scene1, Scene scene2)
		{
			MPEventSystem.RecenterCursors();
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0013FF30 File Offset: 0x0013E130
		private static void RecenterCursors()
		{
			foreach (MPEventSystem mpeventSystem in MPEventSystem.instancesList)
			{
				if (mpeventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad && mpeventSystem.currentInputModule)
				{
					((MPInput)mpeventSystem.currentInputModule.input).CenterCursor();
				}
			}
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0013FFA8 File Offset: 0x0013E1A8
		protected override void OnDestroy()
		{
			this.player.controllers.RemoveLastActiveControllerChangedDelegate(new PlayerActiveControllerChangedDelegate(this.OnLastActiveControllerChanged));
			MPEventSystem.instancesList.Remove(this);
			this.inputMapperHelper.Dispose();
			base.OnDestroy();
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0013FFE4 File Offset: 0x0013E1E4
		protected override void Start()
		{
			base.Start();
			this.SetCursorIndicatorEnabled(false);
			if (base.currentInputModule && base.currentInputModule.input)
			{
				((MPInput)base.currentInputModule.input).CenterCursor();
			}
			this.player.controllers.AddLastActiveControllerChangedDelegate(new PlayerActiveControllerChangedDelegate(this.OnLastActiveControllerChanged));
			this.OnLastActiveControllerChanged(this.player, this.player.controllers.GetLastActiveController());
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0014006A File Offset: 0x0013E26A
		protected override void OnEnable()
		{
			base.OnEnable();
			MPEventSystem.activeCount++;
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0014007E File Offset: 0x0013E27E
		protected override void OnDisable()
		{
			this.SetCursorIndicatorEnabled(false);
			base.OnDisable();
			MPEventSystem.activeCount--;
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x00140099 File Offset: 0x0013E299
		// (set) Token: 0x06004D97 RID: 19863 RVA: 0x001400A1 File Offset: 0x0013E2A1
		public MPEventSystem.InputSource currentInputSource { get; private set; }

		// Token: 0x06004D98 RID: 19864 RVA: 0x001400AC File Offset: 0x0013E2AC
		protected void LateUpdate()
		{
			bool flag = false;
			if (base.currentInputModule && base.currentInputModule.input)
			{
				if (this.currentInputSource == MPEventSystem.InputSource.Gamepad)
				{
					flag = (this.cursorOpenerForGamepadCount > 0);
				}
				else
				{
					flag = (this.cursorOpenerCount > 0);
				}
			}
			this.SetCursorIndicatorEnabled(flag);
			MPInputModule mpinputModule = base.currentInputModule as MPInputModule;
			if (flag)
			{
				CursorIndicatorController.CursorSet cursorSet = this.cursorIndicatorController.noneCursorSet;
				MPEventSystem.InputSource currentInputSource = this.currentInputSource;
				if (currentInputSource != MPEventSystem.InputSource.MouseAndKeyboard)
				{
					if (currentInputSource == MPEventSystem.InputSource.Gamepad)
					{
						cursorSet = this.cursorIndicatorController.gamepadCursorSet;
					}
				}
				else
				{
					cursorSet = this.cursorIndicatorController.mouseCursorSet;
				}
				this.cursorIndicatorController.SetCursor(cursorSet, this.isHovering ? CursorIndicatorController.CursorImage.Hover : CursorIndicatorController.CursorImage.Pointer, this.GetColor());
				this.cursorIndicatorController.SetPosition(mpinputModule.input.mousePosition);
			}
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0014017C File Offset: 0x0013E37C
		private void OnLastActiveControllerChanged(Player player, Controller controller)
		{
			if (controller == null)
			{
				return;
			}
			switch (controller.type)
			{
			case ControllerType.Keyboard:
				this.currentInputSource = MPEventSystem.InputSource.MouseAndKeyboard;
				return;
			case ControllerType.Mouse:
				this.currentInputSource = MPEventSystem.InputSource.MouseAndKeyboard;
				return;
			case ControllerType.Joystick:
				this.currentInputSource = MPEventSystem.InputSource.Gamepad;
				return;
			default:
				return;
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x001401C0 File Offset: 0x0013E3C0
		private void SetCursorIndicatorEnabled(bool cursorIndicatorEnabled)
		{
			if (this.cursorIndicatorController.gameObject.activeSelf != cursorIndicatorEnabled)
			{
				this.cursorIndicatorController.gameObject.SetActive(cursorIndicatorEnabled);
				if (cursorIndicatorEnabled)
				{
					((MPInput)((MPInputModule)base.currentInputModule).input).CenterCursor();
				}
			}
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0014020E File Offset: 0x0013E40E
		public Color GetColor()
		{
			if (MPEventSystem.activeCount <= 1)
			{
				return Color.white;
			}
			return ColorCatalog.GetMultiplayerColor(this.playerSlot);
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00140229 File Offset: 0x0013E429
		public bool GetCursorPosition(out Vector2 position)
		{
			if (base.currentInputModule)
			{
				position = base.currentInputModule.input.mousePosition;
				return true;
			}
			position = Vector2.zero;
			return false;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x0014025C File Offset: 0x0013E45C
		public Rect GetScreenRect()
		{
			LocalUser localUser = this.localUser;
			CameraRigController cameraRigController = (localUser != null) ? localUser.cameraRigController : null;
			if (!cameraRigController)
			{
				return new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			}
			return cameraRigController.viewport;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x001402A8 File Offset: 0x0013E4A8
		private static Vector2 RandomOnCircle()
		{
			float value = UnityEngine.Random.value;
			return new Vector2(Mathf.Cos(value * 3.1415927f * 2f), Mathf.Sin(value * 3.1415927f * 2f));
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x001402E4 File Offset: 0x0013E4E4
		private static Vector2 CalculateCursorPushVector(Vector2 positionA, Vector2 positionB)
		{
			Vector2 vector = positionA - positionB;
			if (vector == Vector2.zero)
			{
				vector = MPEventSystem.RandomOnCircle();
			}
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude >= 576f)
			{
				return Vector2.zero;
			}
			float num = Mathf.Sqrt(sqrMagnitude);
			float num2 = num * 0.041666668f;
			float d = 1f - num2;
			return vector / num * d * 10f * 0.5f;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x0014035C File Offset: 0x0013E55C
		private static void PushCursorsApart()
		{
			if (MPEventSystem.activeCount <= 1)
			{
				return;
			}
			int count = MPEventSystem.instancesList.Count;
			if (MPEventSystem.pushInfos.Length < MPEventSystem.activeCount)
			{
				MPEventSystem.pushInfos = new MPEventSystem.PushInfo[MPEventSystem.activeCount];
			}
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (MPEventSystem.instancesList[i].enabled)
				{
					Vector2 position;
					MPEventSystem.instancesList[i].GetCursorPosition(out position);
					MPEventSystem.pushInfos[num++] = new MPEventSystem.PushInfo
					{
						index = i,
						position = position
					};
				}
			}
			for (int j = 0; j < MPEventSystem.activeCount; j++)
			{
				MPEventSystem.PushInfo pushInfo = MPEventSystem.pushInfos[j];
				for (int k = j + 1; k < MPEventSystem.activeCount; k++)
				{
					MPEventSystem.PushInfo pushInfo2 = MPEventSystem.pushInfos[k];
					Vector2 b = MPEventSystem.CalculateCursorPushVector(pushInfo.position, pushInfo2.position);
					pushInfo.pushVector += b;
					pushInfo2.pushVector -= b;
					MPEventSystem.pushInfos[k] = pushInfo2;
				}
				MPEventSystem.pushInfos[j] = pushInfo;
			}
			for (int l = 0; l < MPEventSystem.activeCount; l++)
			{
				MPEventSystem.PushInfo pushInfo3 = MPEventSystem.pushInfos[l];
				MPEventSystem mpeventSystem = MPEventSystem.instancesList[pushInfo3.index];
				if (mpeventSystem.allowCursorPush && mpeventSystem.currentInputModule)
				{
					((MPInput)mpeventSystem.currentInputModule.input).internalMousePosition += pushInfo3.pushVector;
				}
			}
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x0014051C File Offset: 0x0013E71C
		static MPEventSystem()
		{
			RoR2Application.onUpdate += MPEventSystem.PushCursorsApart;
			SceneManager.activeSceneChanged += MPEventSystem.OnActiveSceneChanged;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x0014056E File Offset: 0x0013E76E
		// (set) Token: 0x06004DA3 RID: 19875 RVA: 0x00140576 File Offset: 0x0013E776
		public InputMapperHelper inputMapperHelper { get; private set; }

		// Token: 0x04004A62 RID: 19042
		private static readonly List<MPEventSystem> instancesList = new List<MPEventSystem>();

		// Token: 0x04004A63 RID: 19043
		public static ReadOnlyCollection<MPEventSystem> readOnlyInstancesList = new ReadOnlyCollection<MPEventSystem>(MPEventSystem.instancesList);

		// Token: 0x04004A67 RID: 19047
		public int playerSlot = -1;

		// Token: 0x04004A68 RID: 19048
		[NonSerialized]
		public bool allowCursorPush = true;

		// Token: 0x04004A69 RID: 19049
		[NonSerialized]
		public bool isCombinedEventSystem;

		// Token: 0x04004A6B RID: 19051
		private CursorIndicatorController cursorIndicatorController;

		// Token: 0x04004A6C RID: 19052
		[NotNull]
		public Player player;

		// Token: 0x04004A6D RID: 19053
		[CanBeNull]
		public LocalUser localUser;

		// Token: 0x04004A6E RID: 19054
		public TooltipProvider currentTooltipProvider;

		// Token: 0x04004A6F RID: 19055
		public TooltipController currentTooltip;

		// Token: 0x04004A70 RID: 19056
		private static MPEventSystem.PushInfo[] pushInfos = Array.Empty<MPEventSystem.PushInfo>();

		// Token: 0x04004A71 RID: 19057
		private const float radius = 24f;

		// Token: 0x04004A72 RID: 19058
		private const float invRadius = 0.041666668f;

		// Token: 0x04004A73 RID: 19059
		private const float radiusSqr = 576f;

		// Token: 0x04004A74 RID: 19060
		private const float pushFactor = 10f;

		// Token: 0x02000D42 RID: 3394
		public enum InputSource
		{
			// Token: 0x04004A77 RID: 19063
			MouseAndKeyboard,
			// Token: 0x04004A78 RID: 19064
			Gamepad
		}

		// Token: 0x02000D43 RID: 3395
		private struct PushInfo
		{
			// Token: 0x04004A79 RID: 19065
			public int index;

			// Token: 0x04004A7A RID: 19066
			public Vector2 position;

			// Token: 0x04004A7B RID: 19067
			public Vector2 pushVector;
		}
	}
}
