﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Rewired.Integration.UnityUI;
using Rewired.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D47 RID: 3399
	[RequireComponent(typeof(MPInput))]
	public class MPInputModule : RewiredStandaloneInputModule
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x001408B6 File Offset: 0x0013EAB6
		private bool useCursor
		{
			get
			{
				return ((MPEventSystem)base.eventSystem).isCursorVisible;
			}
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x001408C8 File Offset: 0x0013EAC8
		protected override void Awake()
		{
			this.m_InputOverride = base.GetComponent<MPInput>();
			this.m_MouseState = (RewiredPointerInputModule.MouseState)MPInputModule.m_MouseStateField.GetValue(this);
			base.Awake();
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x001408F2 File Offset: 0x0013EAF2
		protected override void Start()
		{
			base.Start();
			base.ClearMouseInputSources();
			base.AddMouseInputSource((MPInput)this.m_InputOverride);
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x00140914 File Offset: 0x0013EB14
		protected void UpdateHover(List<RaycastResult> raycastResults)
		{
			this.isHovering = false;
			if (!this.useCursor)
			{
				return;
			}
			foreach (RaycastResult raycastResult in raycastResults)
			{
				if (raycastResult.gameObject)
				{
					Selectable componentInParent = raycastResult.gameObject.GetComponentInParent<Selectable>();
					if (componentInParent != null && this.<UpdateHover>g__IsHoverable|9_0(componentInParent))
					{
						this.isHovering = true;
						break;
					}
				}
			}
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x001409A4 File Offset: 0x0013EBA4
		protected override RewiredPointerInputModule.MouseState GetMousePointerEventData(int playerId, int mouseIndex)
		{
			IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return null;
			}
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = base.GetPointerData(playerId, mouseIndex, -1, out playerPointerEventData, true, PointerEventType.Mouse);
			playerPointerEventData.Reset();
			if (pointerData)
			{
				playerPointerEventData.position = base.input.mousePosition;
			}
			Vector2 mousePosition = base.input.mousePosition;
			if (mouseInputSource.locked || !mouseInputSource.enabled)
			{
				playerPointerEventData.position = new Vector2(-1f, -1f);
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = mousePosition - playerPointerEventData.position;
				playerPointerEventData.position = mousePosition;
			}
			playerPointerEventData.scrollDelta = mouseInputSource.wheelDelta;
			playerPointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.UpdateHover(this.m_RaycastResultCache);
			this.m_RaycastResultCache.Clear();
			PlayerPointerEventData playerPointerEventData2;
			base.GetPointerData(playerId, mouseIndex, -2, out playerPointerEventData2, true, PointerEventType.Mouse);
			base.CopyFromTo(playerPointerEventData, playerPointerEventData2);
			playerPointerEventData2.button = PointerEventData.InputButton.Right;
			PlayerPointerEventData playerPointerEventData3;
			base.GetPointerData(playerId, mouseIndex, -3, out playerPointerEventData3, true, PointerEventType.Mouse);
			base.CopyFromTo(playerPointerEventData, playerPointerEventData3);
			playerPointerEventData3.button = PointerEventData.InputButton.Middle;
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				PlayerPointerEventData playerPointerEventData4;
				base.GetPointerData(playerId, mouseIndex, -2147483520 + i, out playerPointerEventData4, true, PointerEventType.Mouse);
				base.CopyFromTo(playerPointerEventData, playerPointerEventData4);
				playerPointerEventData4.button = (PointerEventData.InputButton)(-1);
			}
			this.m_MouseState.SetButtonState(0, base.StateForMouseButton(playerId, mouseIndex, 0), playerPointerEventData);
			this.m_MouseState.SetButtonState(1, base.StateForMouseButton(playerId, mouseIndex, 1), playerPointerEventData2);
			this.m_MouseState.SetButtonState(2, base.StateForMouseButton(playerId, mouseIndex, 2), playerPointerEventData3);
			for (int j = 3; j < mouseInputSource.buttonCount; j++)
			{
				PlayerPointerEventData data;
				base.GetPointerData(playerId, mouseIndex, -2147483520 + j, out data, false, PointerEventType.Mouse);
				this.m_MouseState.SetButtonState(j, base.StateForMouseButton(playerId, mouseIndex, j), data);
			}
			return this.m_MouseState;
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x00140B94 File Offset: 0x0013ED94
		protected override void ProcessMove(PlayerPointerEventData pointerEvent)
		{
			GameObject newEnterTarget = (!this.useCursor) ? null : pointerEvent.pointerCurrentRaycast.gameObject;
			base.HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x00140BC4 File Offset: 0x0013EDC4
		protected override void ProcessDrag(PlayerPointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || !this.useCursor || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && MPInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x00140CF0 File Offset: 0x0013EEF0
		[CompilerGenerated]
		private bool <UpdateHover>g__IsHoverable|9_0(Selectable selectable)
		{
			MPButton mpbutton = selectable as MPButton;
			return !mpbutton || mpbutton.InputModuleIsAllowed(this);
		}

		// Token: 0x04004A8B RID: 19083
		private RewiredPointerInputModule.MouseState m_MouseState;

		// Token: 0x04004A8C RID: 19084
		private static readonly FieldInfo m_MouseStateField = typeof(RewiredPointerInputModule).GetField("m_MouseState", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04004A8D RID: 19085
		private static readonly MPInputModule.ShouldStartDragDelegate ShouldStartDrag = (MPInputModule.ShouldStartDragDelegate)typeof(RewiredPointerInputModule).GetMethod("ShouldStartDrag", BindingFlags.Static | BindingFlags.NonPublic).CreateDelegate(typeof(MPInputModule.ShouldStartDragDelegate));

		// Token: 0x04004A8E RID: 19086
		public bool isHovering;

		// Token: 0x02000D48 RID: 3400
		// (Invoke) Token: 0x06004DD6 RID: 19926
		private delegate bool ShouldStartDragDelegate(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold);
	}
}
