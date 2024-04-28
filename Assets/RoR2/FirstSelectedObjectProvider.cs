using System;
using System.Collections.Generic;
using RoR2.UI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
public class FirstSelectedObjectProvider : MonoBehaviour
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00004B88 File Offset: 0x00002D88
	private void OnEnable()
	{
		if (this.takeAbsolutePriority)
		{
			FirstSelectedObjectProvider.priorityHolder = this;
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00004B98 File Offset: 0x00002D98
	private void OnDisable()
	{
		if (this.takeAbsolutePriority && FirstSelectedObjectProvider.priorityHolder == this)
		{
			FirstSelectedObjectProvider.priorityHolder = null;
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00004BB8 File Offset: 0x00002DB8
	private GameObject getInteractableFirstSelectedObject()
	{
		if (this.firstSelectedObject && this.firstSelectedObject.GetComponent<Selectable>().interactable && this.firstSelectedObject.activeInHierarchy)
		{
			return this.firstSelectedObject;
		}
		if (this.fallBackFirstSelectedObjects == null)
		{
			return null;
		}
		for (int i = 0; i < this.fallBackFirstSelectedObjects.Length; i++)
		{
			if (this.fallBackFirstSelectedObjects[i].GetComponent<Selectable>().interactable && this.fallBackFirstSelectedObjects[i].activeInHierarchy)
			{
				return this.fallBackFirstSelectedObjects[i];
			}
		}
		return null;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00004C44 File Offset: 0x00002E44
	public void EnsureSelectedObject()
	{
		if (FirstSelectedObjectProvider.priorityHolder != null && FirstSelectedObjectProvider.priorityHolder != this)
		{
			return;
		}
		GameObject interactableFirstSelectedObject = this.getInteractableFirstSelectedObject();
		if (interactableFirstSelectedObject != null)
		{
			MPEventSystemLocator component = interactableFirstSelectedObject.GetComponent<MPEventSystemLocator>();
			if (component)
			{
				MPEventSystem eventSystem = component.eventSystem;
				if (eventSystem)
				{
					GameObject currentSelectedGameObject = eventSystem.currentSelectedGameObject;
					if (currentSelectedGameObject == null)
					{
						if (this.lastSelected != null)
						{
							eventSystem.SetSelectedGameObject(this.lastSelected);
						}
						else
						{
							if (eventSystem.firstSelectedGameObject == null)
							{
								eventSystem.firstSelectedGameObject = interactableFirstSelectedObject;
							}
							Debug.Log("FSOP B: SetSelectedGameObject => " + interactableFirstSelectedObject.name);
							eventSystem.SetSelectedGameObject(interactableFirstSelectedObject);
						}
					}
					else
					{
						bool flag = true;
						if (this.enforceCurrentSelectionIsInList != null && this.enforceCurrentSelectionIsInList.Length != 0)
						{
							flag = false;
							GameObject[] array = this.enforceCurrentSelectionIsInList;
							for (int i = 0; i < array.Length; i++)
							{
								if (array[i] == currentSelectedGameObject)
								{
									flag = true;
								}
							}
						}
						if (!flag)
						{
							Selectable component2 = currentSelectedGameObject.GetComponent<Selectable>();
							if (component2 && (!component2.interactable || !component2.isActiveAndEnabled))
							{
								eventSystem.SetSelectedGameObject(interactableFirstSelectedObject);
							}
						}
					}
					if (component.eventSystem.currentSelectedGameObject != null)
					{
						this.lastSelected = component.eventSystem.currentSelectedGameObject;
					}
				}
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00004D99 File Offset: 0x00002F99
	public void ResetLastSelected()
	{
		Debug.Log("FSOP.ResetLastSelected()");
		this.lastSelected = null;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00004DAC File Offset: 0x00002FAC
	public void AddObject(GameObject obj, bool enforceCurrentSelection = false)
	{
		if (this.firstSelectedObject == null)
		{
			this.firstSelectedObject = obj;
		}
		else if (this.fallBackFirstSelectedObjects == null)
		{
			this.fallBackFirstSelectedObjects = new GameObject[1];
			this.fallBackFirstSelectedObjects[0] = obj;
		}
		else if (this.fallBackFirstSelectedObjects.Length >= 0)
		{
			this.fallBackFirstSelectedObjects = new List<GameObject>(this.fallBackFirstSelectedObjects)
			{
				obj
			}.ToArray();
		}
		if (enforceCurrentSelection)
		{
			if (this.enforceCurrentSelectionIsInList == null)
			{
				this.enforceCurrentSelectionIsInList = new GameObject[1];
				this.enforceCurrentSelectionIsInList[0] = obj;
				return;
			}
			if (this.enforceCurrentSelectionIsInList.Length >= 0)
			{
				this.enforceCurrentSelectionIsInList = new List<GameObject>(this.enforceCurrentSelectionIsInList)
				{
					obj
				}.ToArray();
			}
		}
	}

	// Token: 0x040000BE RID: 190
	public GameObject firstSelectedObject;

	// Token: 0x040000BF RID: 191
	public GameObject[] fallBackFirstSelectedObjects;

	// Token: 0x040000C0 RID: 192
	private GameObject lastSelected;

	// Token: 0x040000C1 RID: 193
	public GameObject[] enforceCurrentSelectionIsInList;

	// Token: 0x040000C2 RID: 194
	public bool takeAbsolutePriority;

	// Token: 0x040000C3 RID: 195
	public static FirstSelectedObjectProvider priorityHolder;
}
