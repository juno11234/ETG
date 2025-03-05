using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    
    public List<Image> heartImages;
   
    public void UdateHPUI(int currenHP)
    {
        for(int i=0; i < heartImages.Count; i++)
        {
            int heartHP = (i + 1) * 2;

            if (currenHP >= heartHP)
            {
                heartImages[i].sprite = fullHeart;
                heartImages[i].gameObject.SetActive(true);
            }
            else if (currenHP == heartHP - 1)
            {
                heartImages[i].sprite = halfHeart;
                heartImages[i].gameObject.SetActive(true);
            }
            else
            {
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }
    
}
