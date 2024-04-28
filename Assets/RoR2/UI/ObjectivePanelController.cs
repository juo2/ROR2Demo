using System;
using System.Collections.Generic;
using EntityStates.Missions.Goldshores;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D56 RID: 3414
	public class ObjectivePanelController : MonoBehaviour
	{
		// Token: 0x06004E46 RID: 20038 RVA: 0x00143108 File Offset: 0x00141308
		public void SetCurrentMaster(CharacterMaster newMaster)
		{
			if (newMaster == this.currentMaster)
			{
				return;
			}
			for (int i = this.objectiveTrackers.Count - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(this.objectiveTrackers[i].stripObject);
			}
			this.objectiveTrackers.Clear();
			this.objectiveSourceToTrackerDictionary.Clear();
			this.currentMaster = newMaster;
			this.RefreshObjectiveTrackers();
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00143178 File Offset: 0x00141378
		private void AddObjectiveTracker(ObjectivePanelController.ObjectiveTracker objectiveTracker)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectiveTrackerPrefab, this.objectiveTrackerContainer);
			gameObject.SetActive(true);
			objectiveTracker.owner = this;
			objectiveTracker.SetStrip(gameObject);
			this.objectiveTrackers.Add(objectiveTracker);
			this.objectiveSourceToTrackerDictionary.Add(objectiveTracker.sourceDescriptor, objectiveTracker);
			this.AddEnterAnimation(objectiveTracker);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x001431D1 File Offset: 0x001413D1
		private void RemoveObjectiveTracker(ObjectivePanelController.ObjectiveTracker objectiveTracker)
		{
			this.objectiveTrackers.Remove(objectiveTracker);
			this.objectiveSourceToTrackerDictionary.Remove(objectiveTracker.sourceDescriptor);
			objectiveTracker.Retire();
			this.AddExitAnimation(objectiveTracker);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00143200 File Offset: 0x00141400
		private void RefreshObjectiveTrackers()
		{
			foreach (ObjectivePanelController.ObjectiveTracker objectiveTracker in this.objectiveTrackers)
			{
				objectiveTracker.isRelevant = false;
			}
			if (this.currentMaster)
			{
				this.GetObjectiveSources(this.currentMaster, this.objectiveSourceDescriptors);
				foreach (ObjectivePanelController.ObjectiveSourceDescriptor objectiveSourceDescriptor in this.objectiveSourceDescriptors)
				{
					ObjectivePanelController.ObjectiveTracker objectiveTracker2;
					if (this.objectiveSourceToTrackerDictionary.TryGetValue(objectiveSourceDescriptor, out objectiveTracker2))
					{
						objectiveTracker2.isRelevant = true;
					}
					else
					{
						ObjectivePanelController.ObjectiveTracker objectiveTracker3 = ObjectivePanelController.ObjectiveTracker.Instantiate(objectiveSourceDescriptor);
						objectiveTracker3.isRelevant = true;
						this.AddObjectiveTracker(objectiveTracker3);
					}
				}
			}
			for (int i = this.objectiveTrackers.Count - 1; i >= 0; i--)
			{
				if (!this.objectiveTrackers[i].isRelevant)
				{
					this.RemoveObjectiveTracker(this.objectiveTrackers[i]);
				}
			}
			foreach (ObjectivePanelController.ObjectiveTracker objectiveTracker4 in this.objectiveTrackers)
			{
				objectiveTracker4.UpdateStrip();
			}
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00143360 File Offset: 0x00141560
		private void GetObjectiveSources(CharacterMaster master, [NotNull] List<ObjectivePanelController.ObjectiveSourceDescriptor> output)
		{
			output.Clear();
			WeeklyRun weeklyRun = Run.instance as WeeklyRun;
			if (weeklyRun && weeklyRun.crystalsRequiredToKill > weeklyRun.crystalsKilled)
			{
				output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					source = Run.instance,
					master = master,
					objectiveType = typeof(ObjectivePanelController.DestroyTimeCrystals)
				});
			}
			TeleporterInteraction instance = TeleporterInteraction.instance;
			if (instance)
			{
				Type type = null;
				if (instance.isCharged && !instance.isInFinalSequence)
				{
					type = typeof(ObjectivePanelController.FinishTeleporterObjectiveTracker);
				}
				else if (instance.isIdle)
				{
					type = typeof(ObjectivePanelController.FindTeleporterObjectiveTracker);
				}
				if (type != null)
				{
					output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
					{
						source = instance,
						master = master,
						objectiveType = type
					});
				}
			}
			if (GoldshoresMissionController.instance)
			{
				Type type2 = GoldshoresMissionController.instance.entityStateMachine.state.GetType();
				if ((type2 == typeof(ActivateBeacons) || type2 == typeof(GoldshoresBossfight)) && GoldshoresMissionController.instance.beaconsActive < GoldshoresMissionController.instance.beaconCount)
				{
					output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
					{
						source = GoldshoresMissionController.instance,
						master = master,
						objectiveType = typeof(ObjectivePanelController.ActivateGoldshoreBeaconTracker)
					});
				}
			}
			if (ArenaMissionController.instance && ArenaMissionController.instance.clearedRounds < ArenaMissionController.instance.totalRoundsMax)
			{
				output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					source = ArenaMissionController.instance,
					master = master,
					objectiveType = typeof(ObjectivePanelController.ClearArena)
				});
			}
			Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>> action = ObjectivePanelController.collectObjectiveSources;
			if (action == null)
			{
				return;
			}
			action(master, output);
		}

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06004E4B RID: 20043 RVA: 0x00143534 File Offset: 0x00141734
		// (remove) Token: 0x06004E4C RID: 20044 RVA: 0x00143568 File Offset: 0x00141768
		public static event Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>> collectObjectiveSources;

		// Token: 0x06004E4D RID: 20045 RVA: 0x0014359B File Offset: 0x0014179B
		private void Update()
		{
			this.RefreshObjectiveTrackers();
			this.RunExitAnimations();
			this.RunEnterAnimations();
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x001435B0 File Offset: 0x001417B0
		private void AddExitAnimation(ObjectivePanelController.ObjectiveTracker objectiveTracker)
		{
			for (int i = 0; i < this.enterAnimations.Count; i++)
			{
				if (this.enterAnimations[i].objectiveTracker == objectiveTracker)
				{
					this.enterAnimations.RemoveAt(i);
					break;
				}
			}
			this.exitAnimations.Add(new ObjectivePanelController.StripExitAnimation(objectiveTracker));
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00143606 File Offset: 0x00141806
		private void AddEnterAnimation(ObjectivePanelController.ObjectiveTracker objectiveTracker)
		{
			this.enterAnimations.Add(new ObjectivePanelController.StripEnterAnimation(objectiveTracker));
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0014361C File Offset: 0x0014181C
		private void RunExitAnimations()
		{
			float deltaTime = Time.deltaTime;
			float num = 7f;
			float num2 = deltaTime / num;
			for (int i = this.exitAnimations.Count - 1; i >= 0; i--)
			{
				ObjectivePanelController.StripExitAnimation stripExitAnimation = this.exitAnimations[i];
				float num3 = Mathf.Min(stripExitAnimation.t + num2, 1f);
				this.exitAnimations[i].SetT(num3);
				if (num3 >= 1f)
				{
					UnityEngine.Object.Destroy(stripExitAnimation.objectiveTracker.stripObject);
					this.exitAnimations.RemoveAt(i);
				}
			}
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x001436A8 File Offset: 0x001418A8
		private void RunEnterAnimations()
		{
			float deltaTime = Time.deltaTime;
			float num = 7f;
			float num2 = deltaTime / num;
			for (int i = this.enterAnimations.Count - 1; i >= 0; i--)
			{
				float num3 = Mathf.Min(this.enterAnimations[i].t + num2, 1f);
				this.enterAnimations[i].SetT(num3);
				if (num3 >= 1f)
				{
					this.enterAnimations.RemoveAt(i);
				}
			}
		}

		// Token: 0x04004AF5 RID: 19189
		public RectTransform objectiveTrackerContainer;

		// Token: 0x04004AF6 RID: 19190
		public GameObject objectiveTrackerPrefab;

		// Token: 0x04004AF7 RID: 19191
		public Sprite checkboxActiveSprite;

		// Token: 0x04004AF8 RID: 19192
		public Sprite checkboxSuccessSprite;

		// Token: 0x04004AF9 RID: 19193
		public Sprite checkboxFailSprite;

		// Token: 0x04004AFA RID: 19194
		private CharacterMaster currentMaster;

		// Token: 0x04004AFB RID: 19195
		private readonly List<ObjectivePanelController.ObjectiveTracker> objectiveTrackers = new List<ObjectivePanelController.ObjectiveTracker>();

		// Token: 0x04004AFC RID: 19196
		private Dictionary<ObjectivePanelController.ObjectiveSourceDescriptor, ObjectivePanelController.ObjectiveTracker> objectiveSourceToTrackerDictionary = new Dictionary<ObjectivePanelController.ObjectiveSourceDescriptor, ObjectivePanelController.ObjectiveTracker>(EqualityComparer<ObjectivePanelController.ObjectiveSourceDescriptor>.Default);

		// Token: 0x04004AFD RID: 19197
		private readonly List<ObjectivePanelController.ObjectiveSourceDescriptor> objectiveSourceDescriptors = new List<ObjectivePanelController.ObjectiveSourceDescriptor>();

		// Token: 0x04004AFF RID: 19199
		private readonly List<ObjectivePanelController.StripExitAnimation> exitAnimations = new List<ObjectivePanelController.StripExitAnimation>();

		// Token: 0x04004B00 RID: 19200
		private readonly List<ObjectivePanelController.StripEnterAnimation> enterAnimations = new List<ObjectivePanelController.StripEnterAnimation>();

		// Token: 0x02000D57 RID: 3415
		public struct ObjectiveSourceDescriptor : IEquatable<ObjectivePanelController.ObjectiveSourceDescriptor>
		{
			// Token: 0x06004E53 RID: 20051 RVA: 0x00143770 File Offset: 0x00141970
			public override int GetHashCode()
			{
				return (((this.source != null) ? this.source.GetHashCode() : 0) * 397 ^ ((this.master != null) ? this.master.GetHashCode() : 0)) * 397 ^ ((this.objectiveType != null) ? this.objectiveType.GetHashCode() : 0);
			}

			// Token: 0x06004E54 RID: 20052 RVA: 0x001437DF File Offset: 0x001419DF
			public static bool Equals(ObjectivePanelController.ObjectiveSourceDescriptor a, ObjectivePanelController.ObjectiveSourceDescriptor b)
			{
				return a.source == b.source && a.master == b.master && a.objectiveType == b.objectiveType;
			}

			// Token: 0x06004E55 RID: 20053 RVA: 0x001437DF File Offset: 0x001419DF
			public bool Equals(ObjectivePanelController.ObjectiveSourceDescriptor other)
			{
				return this.source == other.source && this.master == other.master && this.objectiveType == other.objectiveType;
			}

			// Token: 0x06004E56 RID: 20054 RVA: 0x0014381A File Offset: 0x00141A1A
			public override bool Equals(object obj)
			{
				return obj != null && obj is ObjectivePanelController.ObjectiveSourceDescriptor && this.Equals((ObjectivePanelController.ObjectiveSourceDescriptor)obj);
			}

			// Token: 0x04004B01 RID: 19201
			public UnityEngine.Object source;

			// Token: 0x04004B02 RID: 19202
			public CharacterMaster master;

			// Token: 0x04004B03 RID: 19203
			public Type objectiveType;
		}

		// Token: 0x02000D58 RID: 3416
		public class ObjectiveTracker
		{
			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06004E57 RID: 20055 RVA: 0x00143837 File Offset: 0x00141A37
			// (set) Token: 0x06004E58 RID: 20056 RVA: 0x0014383F File Offset: 0x00141A3F
			public GameObject stripObject { get; private set; }

			// Token: 0x06004E59 RID: 20057 RVA: 0x00143848 File Offset: 0x00141A48
			public void SetStrip(GameObject stripObject)
			{
				this.stripObject = stripObject;
				this.label = stripObject.transform.Find("Label").GetComponent<TextMeshProUGUI>();
				this.checkbox = stripObject.transform.Find("Checkbox").GetComponent<Image>();
				this.UpdateStrip();
			}

			// Token: 0x06004E5A RID: 20058 RVA: 0x00143898 File Offset: 0x00141A98
			public string GetString()
			{
				if (this.IsDirty())
				{
					this.cachedString = this.GenerateString();
				}
				return this.cachedString;
			}

			// Token: 0x06004E5B RID: 20059 RVA: 0x001438B4 File Offset: 0x00141AB4
			protected virtual string GenerateString()
			{
				return Language.GetString(this.baseToken);
			}

			// Token: 0x06004E5C RID: 20060 RVA: 0x001438C1 File Offset: 0x00141AC1
			protected virtual bool IsDirty()
			{
				return this.cachedString == null;
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x06004E5D RID: 20061 RVA: 0x001438CC File Offset: 0x00141ACC
			protected virtual bool shouldConsiderComplete
			{
				get
				{
					return this.retired;
				}
			}

			// Token: 0x06004E5E RID: 20062 RVA: 0x001438D4 File Offset: 0x00141AD4
			public void Retire()
			{
				this.retired = true;
				this.OnRetired();
				this.UpdateStrip();
			}

			// Token: 0x06004E5F RID: 20063 RVA: 0x000026ED File Offset: 0x000008ED
			protected virtual void OnRetired()
			{
			}

			// Token: 0x06004E60 RID: 20064 RVA: 0x001438EC File Offset: 0x00141AEC
			public virtual void UpdateStrip()
			{
				if (this.label)
				{
					this.label.text = this.GetString();
					this.label.color = (this.retired ? Color.gray : Color.white);
					if (this.retired)
					{
						this.label.fontStyle |= FontStyles.Strikethrough;
					}
				}
				if (this.checkbox)
				{
					bool shouldConsiderComplete = this.shouldConsiderComplete;
					this.checkbox.sprite = (shouldConsiderComplete ? this.owner.checkboxSuccessSprite : this.owner.checkboxActiveSprite);
					this.checkbox.color = (shouldConsiderComplete ? Color.yellow : Color.white);
				}
			}

			// Token: 0x06004E61 RID: 20065 RVA: 0x001439A8 File Offset: 0x00141BA8
			public static ObjectivePanelController.ObjectiveTracker Instantiate(ObjectivePanelController.ObjectiveSourceDescriptor sourceDescriptor)
			{
				if (sourceDescriptor.objectiveType != null && sourceDescriptor.objectiveType.IsSubclassOf(typeof(ObjectivePanelController.ObjectiveTracker)))
				{
					ObjectivePanelController.ObjectiveTracker objectiveTracker = (ObjectivePanelController.ObjectiveTracker)Activator.CreateInstance(sourceDescriptor.objectiveType);
					objectiveTracker.sourceDescriptor = sourceDescriptor;
					return objectiveTracker;
				}
				string format = "Bad objectiveType {0}";
				object[] array = new object[1];
				int num = 0;
				Type objectiveType = sourceDescriptor.objectiveType;
				array[num] = ((objectiveType != null) ? objectiveType.FullName : null);
				Debug.LogFormat(format, array);
				return null;
			}

			// Token: 0x04004B04 RID: 19204
			public ObjectivePanelController.ObjectiveSourceDescriptor sourceDescriptor;

			// Token: 0x04004B05 RID: 19205
			public ObjectivePanelController owner;

			// Token: 0x04004B06 RID: 19206
			public bool isRelevant;

			// Token: 0x04004B08 RID: 19208
			protected Image checkbox;

			// Token: 0x04004B09 RID: 19209
			protected TextMeshProUGUI label;

			// Token: 0x04004B0A RID: 19210
			protected string cachedString;

			// Token: 0x04004B0B RID: 19211
			protected string baseToken = "";

			// Token: 0x04004B0C RID: 19212
			protected bool retired;
		}

		// Token: 0x02000D59 RID: 3417
		private class FindTeleporterObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06004E63 RID: 20067 RVA: 0x00143A2B File Offset: 0x00141C2B
			public FindTeleporterObjectiveTracker()
			{
				this.baseToken = "OBJECTIVE_FIND_TELEPORTER";
			}
		}

		// Token: 0x02000D5A RID: 3418
		private class ActivateGoldshoreBeaconTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00143A3E File Offset: 0x00141C3E
			private GoldshoresMissionController missionController
			{
				get
				{
					return this.sourceDescriptor.source as GoldshoresMissionController;
				}
			}

			// Token: 0x06004E65 RID: 20069 RVA: 0x00143A50 File Offset: 0x00141C50
			public ActivateGoldshoreBeaconTracker()
			{
				this.baseToken = "OBJECTIVE_GOLDSHORES_ACTIVATE_BEACONS";
			}

			// Token: 0x06004E66 RID: 20070 RVA: 0x00143A74 File Offset: 0x00141C74
			private bool UpdateCachedValues()
			{
				int beaconsActive = this.missionController.beaconsActive;
				int beaconCount = this.missionController.beaconCount;
				if (beaconsActive != this.cachedActiveBeaconCount || beaconCount != this.cachedRequiredBeaconCount)
				{
					this.cachedActiveBeaconCount = beaconsActive;
					this.cachedRequiredBeaconCount = beaconCount;
					return true;
				}
				return false;
			}

			// Token: 0x06004E67 RID: 20071 RVA: 0x00143ABC File Offset: 0x00141CBC
			protected override string GenerateString()
			{
				this.UpdateCachedValues();
				return string.Format(Language.GetString(this.baseToken), this.cachedActiveBeaconCount, this.cachedRequiredBeaconCount);
			}

			// Token: 0x06004E68 RID: 20072 RVA: 0x00143AEB File Offset: 0x00141CEB
			protected override bool IsDirty()
			{
				return !(this.sourceDescriptor.source as GoldshoresMissionController) || this.UpdateCachedValues();
			}

			// Token: 0x04004B0D RID: 19213
			private int cachedActiveBeaconCount = -1;

			// Token: 0x04004B0E RID: 19214
			private int cachedRequiredBeaconCount = -1;
		}

		// Token: 0x02000D5B RID: 3419
		private class ClearArena : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06004E69 RID: 20073 RVA: 0x00143B0C File Offset: 0x00141D0C
			public ClearArena()
			{
				this.baseToken = "OBJECTIVE_CLEAR_ARENA";
			}

			// Token: 0x06004E6A RID: 20074 RVA: 0x00143B20 File Offset: 0x00141D20
			protected override string GenerateString()
			{
				ArenaMissionController instance = ArenaMissionController.instance;
				return string.Format(Language.GetString(this.baseToken), instance.clearedRounds, instance.totalRoundsMax);
			}

			// Token: 0x06004E6B RID: 20075 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool IsDirty()
			{
				return true;
			}
		}

		// Token: 0x02000D5C RID: 3420
		private class DestroyTimeCrystals : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06004E6C RID: 20076 RVA: 0x00143B59 File Offset: 0x00141D59
			public DestroyTimeCrystals()
			{
				this.baseToken = "OBJECTIVE_WEEKLYRUN_DESTROY_CRYSTALS";
			}

			// Token: 0x06004E6D RID: 20077 RVA: 0x00143B6C File Offset: 0x00141D6C
			protected override string GenerateString()
			{
				WeeklyRun weeklyRun = Run.instance as WeeklyRun;
				return string.Format(Language.GetString(this.baseToken), weeklyRun.crystalsKilled, weeklyRun.crystalsRequiredToKill);
			}

			// Token: 0x06004E6E RID: 20078 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool IsDirty()
			{
				return true;
			}
		}

		// Token: 0x02000D5D RID: 3421
		private class FinishTeleporterObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06004E6F RID: 20079 RVA: 0x00143BAA File Offset: 0x00141DAA
			public FinishTeleporterObjectiveTracker()
			{
				this.baseToken = "OBJECTIVE_FINISH_TELEPORTER";
			}
		}

		// Token: 0x02000D5E RID: 3422
		private class StripExitAnimation
		{
			// Token: 0x06004E70 RID: 20080 RVA: 0x00143BC0 File Offset: 0x00141DC0
			public StripExitAnimation(ObjectivePanelController.ObjectiveTracker objectiveTracker)
			{
				if (objectiveTracker.stripObject)
				{
					this.objectiveTracker = objectiveTracker;
					this.layoutElement = objectiveTracker.stripObject.GetComponent<LayoutElement>();
					this.canvasGroup = objectiveTracker.stripObject.GetComponent<CanvasGroup>();
					this.originalHeight = this.layoutElement.minHeight;
				}
			}

			// Token: 0x06004E71 RID: 20081 RVA: 0x00143C1C File Offset: 0x00141E1C
			public void SetT(float newT)
			{
				if (this.objectiveTracker.stripObject)
				{
					this.t = newT;
					float alpha = Mathf.Clamp01(Util.Remap(this.t, 0.5f, 0.75f, 1f, 0f));
					this.canvasGroup.alpha = alpha;
					float num = Mathf.Clamp01(Util.Remap(this.t, 0.75f, 1f, 1f, 0f));
					num *= num;
					this.layoutElement.minHeight = num * this.originalHeight;
					this.layoutElement.preferredHeight = this.layoutElement.minHeight;
					this.layoutElement.flexibleHeight = 0f;
				}
			}

			// Token: 0x04004B0F RID: 19215
			public float t;

			// Token: 0x04004B10 RID: 19216
			private readonly float originalHeight;

			// Token: 0x04004B11 RID: 19217
			public readonly ObjectivePanelController.ObjectiveTracker objectiveTracker;

			// Token: 0x04004B12 RID: 19218
			private readonly LayoutElement layoutElement;

			// Token: 0x04004B13 RID: 19219
			private readonly CanvasGroup canvasGroup;
		}

		// Token: 0x02000D5F RID: 3423
		private class StripEnterAnimation
		{
			// Token: 0x06004E72 RID: 20082 RVA: 0x00143CD8 File Offset: 0x00141ED8
			public StripEnterAnimation(ObjectivePanelController.ObjectiveTracker objectiveTracker)
			{
				if (objectiveTracker.stripObject)
				{
					this.objectiveTracker = objectiveTracker;
					this.layoutElement = objectiveTracker.stripObject.GetComponent<LayoutElement>();
					this.canvasGroup = objectiveTracker.stripObject.GetComponent<CanvasGroup>();
					this.finalHeight = this.layoutElement.minHeight;
				}
			}

			// Token: 0x06004E73 RID: 20083 RVA: 0x00143D34 File Offset: 0x00141F34
			public void SetT(float newT)
			{
				if (this.objectiveTracker.stripObject)
				{
					this.t = newT;
					float alpha = Mathf.Clamp01(Util.Remap(1f - this.t, 0.5f, 0.75f, 1f, 0f));
					this.canvasGroup.alpha = alpha;
					float num = Mathf.Clamp01(Util.Remap(1f - this.t, 0.75f, 1f, 1f, 0f));
					num *= num;
					this.layoutElement.minHeight = num * this.finalHeight;
					this.layoutElement.preferredHeight = this.layoutElement.minHeight;
					this.layoutElement.flexibleHeight = 0f;
				}
			}

			// Token: 0x04004B14 RID: 19220
			public float t;

			// Token: 0x04004B15 RID: 19221
			private readonly float finalHeight;

			// Token: 0x04004B16 RID: 19222
			public readonly ObjectivePanelController.ObjectiveTracker objectiveTracker;

			// Token: 0x04004B17 RID: 19223
			private readonly LayoutElement layoutElement;

			// Token: 0x04004B18 RID: 19224
			private readonly CanvasGroup canvasGroup;
		}
	}
}
