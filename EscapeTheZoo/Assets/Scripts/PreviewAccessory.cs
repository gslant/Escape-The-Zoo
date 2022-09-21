using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewAccessory : MonoBehaviour
{
    public GameObject currentPreviewPlayerAccessory; // Holds the accessory (preview) of the selected acessory

    private bool isPreviewed; // Determines if preview will show or not

    public void Start()
    {
        currentPreviewPlayerAccessory.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/None");
    }

    // Change the Preview accessory to the image that was clicked
    public void ChangePreviewImage()
    {
        if (currentPreviewPlayerAccessory.activeSelf)
        {
            isPreviewed = true;
            if (currentPreviewPlayerAccessory.GetComponent<Image>().sprite.Equals(this.GetComponent<Image>().sprite))
            {
                isPreviewed = false;
            } 
            else
            {
                isPreviewed = true;
            }
            // If the image is clicked, show the accessory
            // If the image is clicked again, it will set the accessory image to None
            if (isPreviewed)
            {
                currentPreviewPlayerAccessory.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            }
            else
            {
                currentPreviewPlayerAccessory.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/None");
            }
        }
    }
}
