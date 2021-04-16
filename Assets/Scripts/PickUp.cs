using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] TextMeshProUGUI pickupText;
    [SerializeField] TextMeshProUGUI unlockDoorText;
    [SerializeField] TextMeshProUGUI openAndCloseDoorText;
    [SerializeField] float maxDistanceForInteraction = 1f;
    [SerializeField] Image keyImage;
    Key itemToPick;
    Door door;
    public bool keyFound;

    private void Start()
    {
        keyFound = false;
    }

    void Update()

    {
        InteractionAlgorithm();
    }

    void InteractionAlgorithm()
    {
        Ray ray = gameCamera.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxDistanceForInteraction, LayerMask.GetMask("Key")))
        {
            var hitObject = hitInfo.collider.GetComponent<Key>();

            if(hitObject == null)
            {
                itemToPick = null;
            }
            else if (hitObject != null && hitObject != itemToPick)
            {
                itemToPick = hitObject;
                pickupText.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpTheKey();
            }
        }
     
        else if (Physics.Raycast(ray, out hitInfo, maxDistanceForInteraction, LayerMask.GetMask("Door")))
        {
            var hitObject = hitInfo.collider.GetComponent<Door>();

            if (hitObject == null)
            {
                door = null;
            }
            else if (hitObject != null && hitObject != door)
            {
                door = hitObject;
                if (!door.isDoorUnlocked)
                { unlockDoorText.gameObject.SetActive(true); }
                else 
                {
                    openAndCloseDoorText.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseTheKey();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
        else
        {
            itemToPick = null;
            door = null;
            pickupText.gameObject.SetActive(false);
            unlockDoorText.gameObject.SetActive(false);
            openAndCloseDoorText.gameObject.SetActive(false);
        }
    }

    void PickUpTheKey()
    {
            Destroy(itemToPick.gameObject);
            keyImage.color = new Color(255, 255, 255, 255);
            keyFound = true;
    }
    void UseTheKey()
    {
        FindObjectOfType<Door>().UnlockTheDoor();
    }
    void OpenDoor()
    {
        Debug.Log("Open");    
        FindObjectOfType<Door>().OpenTheDoor();
    }

    }
