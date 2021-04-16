using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lockedDoorText;
    [SerializeField] TextMeshProUGUI unlockedDoorText;
    [SerializeField] GameObject correctKey; 
    Animator animator;
    public bool isDoorUnlocked;
    bool isDoorOpened;

    private void Start()
    {
        animator = GetComponent<Animator>();
        correctKey = GetComponent<GameObject>();
    }
    public void UnlockTheDoor()
    {
            if (FindObjectOfType<PickUp>().keyFound )
            {
                animator.SetTrigger("Unlock");
                unlockedDoorText.gameObject.SetActive(true);
                isDoorUnlocked = true;
            }
            else if (!FindObjectOfType<PickUp>().keyFound)
            {
                StartCoroutine(ShowHint());
            }
    }

    public void OpenTheDoor()
    {
        if(!isDoorOpened)
        {
            animator.SetTrigger("Open");
            isDoorOpened = true;
        }
        else
        {
            animator.SetTrigger("Close");
            isDoorOpened = false;
        }
    }
    
    IEnumerator ShowHint()
        {
            lockedDoorText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            lockedDoorText.gameObject.SetActive(false);
         }
   
}
