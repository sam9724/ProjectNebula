using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCam : MonoBehaviour
{
    Camera cameraToLookAt;
    IBaseEnemy enemy;
    Image healthBar;

    void Update()
    {
        cameraToLookAt = cameraToLookAt ?? PlayerManager.Instance.gunner.mainCamera;

        enemy = enemy ?? transform.parent.gameObject.GetComponent<IBaseEnemy>();
        healthBar = healthBar ?? transform.Find("Life/lifeBarEnemy").GetComponent<Image>();
        if(cameraToLookAt != null)
        {
            Vector3 v = cameraToLookAt.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(cameraToLookAt.transform.position - v);
            transform.Rotate(0, 180, 0);

            healthBar.fillAmount = enemy.CharStats.health / enemy.MaxHealth;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

