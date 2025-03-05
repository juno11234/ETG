using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{    
    public List<Image> bulletImages;

    public void UpdateAmmo(int currentAmmo)
    {
        for(int i = 0; i < bulletImages.Count; i++)
        {
            if (i < currentAmmo)
            {
                bulletImages[i].gameObject.SetActive(true);
            }
            else
            {
                bulletImages[i].gameObject.SetActive(false);
            }
        }
    }
}
