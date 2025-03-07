using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject bossIntroAni;

    [SerializeField] 
    CinemachineVirtualCamera bossCamera;

    [SerializeField]
    BossHPUI bossHPBar;
    
    BoxCollider2D boxColl;



    void Start()
    {
        TryGetComponent<BoxCollider2D>(out boxColl);
        boss.SetActive(false);
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
        bossIntroAni.SetActive(false);
        bossHPBar.Hide();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {            
            CloseDoor();
            Destroy(boxColl);
           StartCoroutine (BossActive());
        }
    }
    void CloseDoor()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
    IEnumerator BossActive()
    {
        Time.timeScale = 0f;
        boss.SetActive(true);
        bossCamera.Priority = 15;

        Boss bossScript = boss.GetComponent<Boss>();
        bossScript.Initialize(bossHPBar);
        bossIntroAni.SetActive(true);        
        yield return new WaitForSecondsRealtime(4f);

        bossHPBar.Show();
        bossCamera.Priority = 5;
        Time.timeScale = 1f;
        bossIntroAni.SetActive(false);
        
    }
}
