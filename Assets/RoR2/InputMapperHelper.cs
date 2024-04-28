using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Rewired;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000932 RID: 2354
	public class InputMapperHelper : IDisposable
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000E1134 File Offset: 0x000DF334
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000E113C File Offset: 0x000DF33C
		public bool isListening { get; private set; }

		// Token: 0x06003537 RID: 13623 RVA: 0x000E1145 File Offset: 0x000DF345
		public InputMapperHelper(MPEventSystem eventSystem)
		{
			this.eventSystem = eventSystem;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000E1178 File Offset: 0x000DF378
		private InputMapper AddInputMapper()
		{
			InputMapper inputMapper = new InputMapper();
			inputMapper.ConflictFoundEvent += this.InputMapperOnConflictFoundEvent;
			inputMapper.CanceledEvent += this.InputMapperOnCanceledEvent;
			inputMapper.ErrorEvent += this.InputMapperOnErrorEvent;
			inputMapper.InputMappedEvent += this.InputMapperOnInputMappedEvent;
			inputMapper.StartedEvent += this.InputMapperOnStartedEvent;
			inputMapper.StoppedEvent += this.InputMapperOnStoppedEvent;
			inputMapper.TimedOutEvent += this.InputMapperOnTimedOutEvent;
			inputMapper.options = new InputMapper.Options
			{
				allowAxes = true,
				allowButtons = true,
				allowKeyboardKeysWithModifiers = false,
				allowKeyboardModifierKeyAsPrimary = true,
				checkForConflicts = true,
				checkForConflictsWithAllPlayers = false,
				checkForConflictsWithPlayerIds = Array.Empty<int>(),
				checkForConflictsWithSelf = true,
				checkForConflictsWithSystemPlayer = false,
				defaultActionWhenConflictFound = InputMapper.ConflictResponse.Add,
				holdDurationToMapKeyboardModifierKeyAsPrimary = 0f,
				ignoreMouseXAxis = true,
				ignoreMouseYAxis = true,
				timeout = float.PositiveInfinity
			};
			this.inputMappers.Add(inputMapper);
			return inputMapper;
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000E1290 File Offset: 0x000DF490
		private void RemoveInputMapper(InputMapper inputMapper)
		{
			inputMapper.ConflictFoundEvent -= this.InputMapperOnConflictFoundEvent;
			inputMapper.CanceledEvent -= this.InputMapperOnCanceledEvent;
			inputMapper.ErrorEvent -= this.InputMapperOnErrorEvent;
			inputMapper.InputMappedEvent -= this.InputMapperOnInputMappedEvent;
			inputMapper.StartedEvent -= this.InputMapperOnStartedEvent;
			inputMapper.StoppedEvent -= this.InputMapperOnStoppedEvent;
			inputMapper.TimedOutEvent -= this.InputMapperOnTimedOutEvent;
			this.inputMappers.Remove(inputMapper);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000E1328 File Offset: 0x000DF528
		public void Start(Player player, IList<Controller> controllers, InputAction action, AxisRange axisRange)
		{
			this.Stop();
			this.isListening = true;
			this.currentPlayer = player;
			this.currentAction = action;
			this.currentAxisRange = axisRange;
			this.maps = (from controller in controllers
			select player.controllers.maps.GetFirstMapInCategory(controller, 0) into map
			where map != null
			select map).Distinct<ControllerMap>().ToArray<ControllerMap>();
			Debug.Log(this.maps.Length);
			foreach (ControllerMap controllerMap in this.maps)
			{
				InputMapper.Context mappingContext = new InputMapper.Context
				{
					actionId = action.id,
					controllerMap = controllerMap,
					actionRange = this.currentAxisRange
				};
				this.AddInputMapper().Start(mappingContext);
			}
			this.dialogBox = SimpleDialogBox.Create(this.eventSystem);
			this.timer = this.timeout;
			this.UpdateDialogBoxString();
			RoR2Application.onUpdate += this.Update;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000E1444 File Offset: 0x000DF644
		public void Stop()
		{
			if (!this.isListening)
			{
				return;
			}
			this.maps = Array.Empty<ControllerMap>();
			this.currentPlayer = null;
			this.currentAction = null;
			for (int i = this.inputMappers.Count - 1; i >= 0; i--)
			{
				InputMapper inputMapper = this.inputMappers[i];
				inputMapper.Stop();
				this.RemoveInputMapper(inputMapper);
			}
			if (this.dialogBox)
			{
				UnityEngine.Object.Destroy(this.dialogBox.rootObject);
				this.dialogBox = null;
			}
			this.isListening = false;
			RoR2Application.onUpdate -= this.Update;
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000E14E4 File Offset: 0x000DF6E4
		private void Update()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (this.isListening)
			{
				this.timer -= unscaledDeltaTime;
				if (this.timer < 0f)
				{
					this.Stop();
					return;
				}
				if (this.currentPlayer.GetButtonDown(25))
				{
					this.Stop();
					SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(this.eventSystem);
					simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair("OPTION_REBIND_DIALOG_TITLE", Array.Empty<object>());
					simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair("OPTION_REBIND_CANCELLED_DIALOG_DESCRIPTION", Array.Empty<object>());
					simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
					return;
				}
				this.UpdateDialogBoxString();
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000E1588 File Offset: 0x000DF788
		private void UpdateDialogBoxString()
		{
			if (this.dialogBox && this.timer >= 0f)
			{
				string @string = Language.GetString(InputCatalog.GetActionNameToken(this.currentAction.name, AxisRange.Full));
				this.dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
				{
					token = CommonLanguageTokens.optionRebindDialogTitle,
					formatParams = Array.Empty<object>()
				};
				this.dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
				{
					token = CommonLanguageTokens.optionRebindDialogDescription,
					formatParams = new object[]
					{
						@string,
						this.timer
					}
				};
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000E1637 File Offset: 0x000DF837
		private void InputMapperOnTimedOutEvent(InputMapper.TimedOutEventData timedOutEventData)
		{
			Debug.Log("InputMapperOnTimedOutEvent");
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000E1643 File Offset: 0x000DF843
		private void InputMapperOnStoppedEvent(InputMapper.StoppedEventData stoppedEventData)
		{
			Debug.Log("InputMapperOnStoppedEvent");
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000E164F File Offset: 0x000DF84F
		private void InputMapperOnStartedEvent(InputMapper.StartedEventData startedEventData)
		{
			Debug.Log("InputMapperOnStartedEvent");
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000E165C File Offset: 0x000DF85C
		private void InputMapperOnInputMappedEvent(InputMapper.InputMappedEventData inputMappedEventData)
		{
			Debug.Log("InputMapperOnInputMappedEvent");
			InputMapperHelper.<>c__DisplayClass23_0 CS$<>8__locals1;
			CS$<>8__locals1.incomingActionElementMap = inputMappedEventData.actionElementMap;
			CS$<>8__locals1.incomingActionId = inputMappedEventData.actionElementMap.actionId;
			CS$<>8__locals1.incomingElementIndex = inputMappedEventData.actionElementMap.elementIndex;
			CS$<>8__locals1.incomingElementType = inputMappedEventData.actionElementMap.elementType;
			CS$<>8__locals1.map = inputMappedEventData.actionElementMap.controllerMap;
			foreach (ControllerMap controllerMap in this.maps)
			{
				if (controllerMap != CS$<>8__locals1.map)
				{
					controllerMap.DeleteElementMapsWithAction(CS$<>8__locals1.incomingActionId);
				}
			}
			while (InputMapperHelper.<InputMapperOnInputMappedEvent>g__DeleteFirstConflictingElementMap|23_1(ref CS$<>8__locals1))
			{
			}
			MPEventSystem mpeventSystem = this.eventSystem;
			if (mpeventSystem != null)
			{
				LocalUser localUser = mpeventSystem.localUser;
				if (localUser != null)
				{
					localUser.userProfile.RequestEventualSave();
				}
			}
			Debug.Log("Mapping accepted.");
			for (int j = 0; j < InputBindingDisplayController.instances.Count; j++)
			{
				InputBindingDisplayController.instances[j].Refresh(true);
			}
			this.Stop();
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000E175C File Offset: 0x000DF95C
		private void InputMapperOnErrorEvent(InputMapper.ErrorEventData errorEventData)
		{
			Debug.Log("InputMapperOnErrorEvent");
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000E1768 File Offset: 0x000DF968
		private void InputMapperOnCanceledEvent(InputMapper.CanceledEventData canceledEventData)
		{
			Debug.Log("InputMapperOnCanceledEvent");
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000E1774 File Offset: 0x000DF974
		private void InputMapperOnConflictFoundEvent(InputMapper.ConflictFoundEventData conflictFoundEventData)
		{
			Debug.Log("InputMapperOnConflictFoundEvent");
			InputMapper.ConflictResponse obj;
			if (conflictFoundEventData.conflicts.Any((ElementAssignmentConflictInfo elementAssignmentConflictInfo) => InputMapperHelper.forbiddenElements.Contains(elementAssignmentConflictInfo.elementIdentifier.name)))
			{
				obj = InputMapper.ConflictResponse.Ignore;
			}
			else
			{
				obj = InputMapper.ConflictResponse.Add;
			}
			conflictFoundEventData.responseCallback(obj);
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000E17C9 File Offset: 0x000DF9C9
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000E1860 File Offset: 0x000DFA60
		[CompilerGenerated]
		internal static bool <InputMapperOnInputMappedEvent>g__ActionElementMapConflicts|23_0(ActionElementMap actionElementMap, ref InputMapperHelper.<>c__DisplayClass23_0 A_1)
		{
			if (actionElementMap == A_1.incomingActionElementMap)
			{
				return false;
			}
			bool flag = actionElementMap.elementIndex == A_1.incomingElementIndex && actionElementMap.elementType == A_1.incomingElementType;
			bool flag2 = actionElementMap.actionId == A_1.incomingActionId && actionElementMap.axisContribution == A_1.incomingActionElementMap.axisContribution;
			return flag || flag2;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000E18C0 File Offset: 0x000DFAC0
		[CompilerGenerated]
		internal static bool <InputMapperOnInputMappedEvent>g__DeleteFirstConflictingElementMap|23_1(ref InputMapperHelper.<>c__DisplayClass23_0 A_0)
		{
			foreach (ActionElementMap actionElementMap in A_0.map.AllMaps)
			{
				if (InputMapperHelper.<InputMapperOnInputMappedEvent>g__ActionElementMapConflicts|23_0(actionElementMap, ref A_0))
				{
					Debug.LogFormat("Deleting conflicting mapping {0}", new object[]
					{
						actionElementMap
					});
					A_0.map.DeleteElementMap(actionElementMap.id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400360A RID: 13834
		private readonly MPEventSystem eventSystem;

		// Token: 0x0400360B RID: 13835
		private readonly List<InputMapper> inputMappers = new List<InputMapper>();

		// Token: 0x0400360C RID: 13836
		private ControllerMap[] maps = Array.Empty<ControllerMap>();

		// Token: 0x0400360D RID: 13837
		private SimpleDialogBox dialogBox;

		// Token: 0x0400360E RID: 13838
		public float timeout = 5f;

		// Token: 0x0400360F RID: 13839
		private float timer;

		// Token: 0x04003610 RID: 13840
		private Player currentPlayer;

		// Token: 0x04003611 RID: 13841
		private InputAction currentAction;

		// Token: 0x04003612 RID: 13842
		private AxisRange currentAxisRange;

		// Token: 0x04003614 RID: 13844
		private Action<InputMapper.ConflictResponse> conflictResponseCallback;

		// Token: 0x04003615 RID: 13845
		private static readonly HashSet<string> forbiddenElements = new HashSet<string>
		{
			"Left Stick X",
			"Left Stick Y",
			"Right Stick X",
			"Right Stick Y",
			"Mouse Horizontal",
			"Mouse Vertical",
			Keyboard.GetKeyName(KeyCode.Escape),
			Keyboard.GetKeyName(KeyCode.KeypadEnter),
			Keyboard.GetKeyName(KeyCode.Return)
		};
	}
}
