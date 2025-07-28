using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHandGunBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDamage=50f;
    [SerializeField] float bulletLifeTime = 1f;

    public void shootBullet(float angle)
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(angle * Mathf.PI / 180), bulletSpeed * Mathf.Sin(angle * Mathf.PI / 180));
        Destroy(this.gameObject, bulletLifeTime);

    }

    public float getBulletDamage() { return bulletDamage; }

    void OnBecameInvisible()
    {
        Debug.Log("Bullet is out of screen");
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Enemy   -   " + col.gameObject.name + " :  " + gameObject.name + "obje sudur : " + Time.time);
        if (col.CompareTag("Obstacle"))
        {
            
            Collider2D m_ObjectCollider = this.gameObject.GetComponent<Collider2D>();
            m_ObjectCollider.isTrigger = false;
            //Debug.Log("trigger change");
        }

    }
}
