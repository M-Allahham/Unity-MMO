using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public bool isLeft;
    public bool isRight;

    public GameObject currWeaponModel;

    public void UnloadAndDestroy()
    {
        if (currWeaponModel != null)
        {
            Destroy(currWeaponModel);
        }
    }

    public void UnloadWeapon()
    {
        if (currWeaponModel != null)
        {
            currWeaponModel.SetActive(false);
        }
    }

    public void LoadWeaponModel(EquipmentItem equipItem)
    {
        UnloadAndDestroy();

        if (equipItem == null)
        {
            UnloadWeapon();
            return;
        }

        GameObject model = Instantiate(equipItem.modelPrefab) as GameObject;

        if (model != null)
        {
            if (parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currWeaponModel = model;
    }
}
