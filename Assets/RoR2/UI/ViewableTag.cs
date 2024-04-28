using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000DAC RID: 3500
	public class ViewableTag : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x0014BF31 File Offset: 0x0014A131
		// (set) Token: 0x06005034 RID: 20532 RVA: 0x0014BF39 File Offset: 0x0014A139
		public string viewableName
		{
			get
			{
				return this._viewableName;
			}
			set
			{
				if (this._viewableName == value)
				{
					return;
				}
				this._viewableName = value;
				this.Refresh();
			}
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0014BF58 File Offset: 0x0014A158
		private bool Check()
		{
			if (LocalUserManager.readOnlyLocalUsersList.Count == 0)
			{
				return false;
			}
			UserProfile userProfile = LocalUserManager.readOnlyLocalUsersList[0].userProfile;
			ViewablesCatalog.Node node = ViewablesCatalog.FindNode(this.viewableName ?? "");
			if (node == null)
			{
				if (ViewableTag.viewablesWarnUndefined.value)
				{
					Debug.LogWarningFormat("Viewable {0} is not defined.", new object[]
					{
						this.viewableName
					});
				}
				return false;
			}
			return node.shouldShowUnviewed(userProfile);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0014BFCF File Offset: 0x0014A1CF
		private void OnEnable()
		{
			ViewableTag.instancesList.Add(this);
			RoR2Application.onNextUpdate += this.CallRefreshIfStillValid;
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0014BFED File Offset: 0x0014A1ED
		private void CallRefreshIfStillValid()
		{
			if (!this)
			{
				return;
			}
			this.Refresh();
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0014C000 File Offset: 0x0014A200
		public void Refresh()
		{
			bool flag = base.enabled && this.Check();
			if (this.tagInstance != flag)
			{
				if (this.tagInstance)
				{
					UnityEngine.Object.Destroy(this.tagInstance);
					this.tagInstance = null;
					return;
				}
				string childName = this.viewableVisualStyle.ToString();
				this.tagInstance = UnityEngine.Object.Instantiate<GameObject>(ViewableTag.tagPrefab, base.transform);
				this.tagInstance.GetComponent<ChildLocator>().FindChild(childName).gameObject.SetActive(true);
			}
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0014C091 File Offset: 0x0014A291
		private void OnDisable()
		{
			ViewableTag.instancesList.Remove(this);
			this.Refresh();
			if (this.markAsViewedOnDisable)
			{
				this.TriggerView();
			}
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0014C0B3 File Offset: 0x0014A2B3
		private void TriggerView()
		{
			ViewableTrigger.TriggerView(this.viewableName);
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0014C0C0 File Offset: 0x0014A2C0
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			ViewableTag.tagPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/NewViewableTag");
			UserProfile.onUserProfileViewedViewablesChanged += delegate(UserProfile userProfile)
			{
				if (!ViewableTag.pendingRefreshAll)
				{
					ViewableTag.pendingRefreshAll = true;
					RoR2Application.onNextUpdate += delegate()
					{
						foreach (ViewableTag viewableTag in ViewableTag.instancesList)
						{
							viewableTag.Refresh();
						}
						ViewableTag.pendingRefreshAll = false;
					};
				}
			};
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0014C0F5 File Offset: 0x0014A2F5
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.markAsViewedOnHover)
			{
				this.TriggerView();
			}
		}

		// Token: 0x04004CDD RID: 19677
		private static readonly List<ViewableTag> instancesList = new List<ViewableTag>();

		// Token: 0x04004CDE RID: 19678
		[SerializeField]
		[FormerlySerializedAs("viewableName")]
		[Tooltip("The path of the viewable that determines whether or not the \"NEW\" tag is activated.")]
		private string _viewableName;

		// Token: 0x04004CDF RID: 19679
		[Tooltip("Marks the named viewable as viewed when this component is disabled.")]
		public bool markAsViewedOnDisable;

		// Token: 0x04004CE0 RID: 19680
		public bool markAsViewedOnHover;

		// Token: 0x04004CE1 RID: 19681
		public ViewableTag.ViewableVisualStyle viewableVisualStyle;

		// Token: 0x04004CE2 RID: 19682
		public static readonly BoolConVar viewablesWarnUndefined = new BoolConVar("viewables_warn_undefined", ConVarFlags.None, "0", "Issues a warning in the console if a viewable is not defined.");

		// Token: 0x04004CE3 RID: 19683
		private static GameObject tagPrefab;

		// Token: 0x04004CE4 RID: 19684
		private GameObject tagInstance;

		// Token: 0x04004CE5 RID: 19685
		private static bool pendingRefreshAll = false;

		// Token: 0x02000DAD RID: 3501
		public enum ViewableVisualStyle
		{
			// Token: 0x04004CE7 RID: 19687
			Button,
			// Token: 0x04004CE8 RID: 19688
			Icon
		}
	}
}
