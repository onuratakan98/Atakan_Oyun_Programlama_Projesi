using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHandGun : MonoBehaviour
{
    //GameObjet for Bullet
    [SerializeField] private GameObject BulletGO;


    //Starting Position for Bullet
    [SerializeField] private Transform bulletSpawnPoint;
    
    
    
    //float shootTime = 2f;
    // bool canShoot = true;

    /*
    void Update()
    {

        shootTimer = shootTimer - shootTime.deltaTime;

    }
    */

    public void shoot(float angle)
    {
        if(BulletGO!=null)
        {
            Vector3 positionBullet = bulletSpawnPoint.position;
            Quaternion rotationBullet = Quaternion.Euler(0,0,BulletGO.transform.eulerAngles.z+angle);
            GameObject bullet = Instantiate(BulletGO, positionBullet, rotationBullet); //, Transform parent


            bullet.GetComponent<ScHandGunBullet>().shootBullet(angle);
        }
    }
        
    
}
