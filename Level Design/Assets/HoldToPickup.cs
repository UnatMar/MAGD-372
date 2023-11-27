using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldToPickup : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float pickupTime;
    [SerializeField]
    private RectTransform pickupImageRoot;
    [SerializeField]
    private Image pickupProgressImage;
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Item itemBeingPickedUp;
    private float currentPickupTimerElapsed;

    private void Update()
    {

        SelectItemBeingPickedUpFromRay();

        if(HasItemTargetted())
        {
            pickupImageRoot.gameObject.SetActive(true);

            if(Input.GetButton("Ekey"))
            {
                IncerementPickupProcessAndTryComplete();
            }
            else
            {
                currentPickupTimerElapsed = 0f;
            }

            UpdatePickupProgressImage();
        }
        else
        {
            pickupImageRoot.gameObject.SetActive(false);
            currentPickupTimerElapsed = 0f;
        }

    }

    private void UpdatePickupProgressImage()
    {
        float pct = currentPickupTimerElapsed / pickupTime;
        pickupProgressImage.fillAmount = pct;
    }

    private void IncerementPickupProcessAndTryComplete()
    {
        currentPickupTimerElapsed += Time.deltaTime;
        if(currentPickupTimerElapsed >= pickupTime)
        {
            MoveItemToInventory();
        }
    }

    private void MoveItemToInventory()
    {
        Destroy(itemBeingPickedUp.gameObject);
        itemBeingPickedUp = null;
    }

    private bool HasItemTargetted()
    {
        return itemBeingPickedUp != null;
    }

    private void SelectItemBeingPickedUpFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction  * 2f, Color.red);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 2f, layerMask))
        {
            var hitItem = hitInfo.collider.GetComponent<Item>();

            if(hitItem == null) 
            {
                itemBeingPickedUp = null;
            }
            else if (itemBeingPickedUp != null && hitItem != itemBeingPickedUp)
            {
                itemBeingPickedUp = hitItem;
                itemNameText.text = "Pickup " + itemBeingPickedUp.gameObject.name;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }
}
