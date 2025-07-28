using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEnemyController : MonoBehaviour
{

    [SerializeField] private GameObject PlayerGO; //
    [SerializeField] private GameObject ParticleBloodGO; //
    [SerializeField] private GameObject BoneGO; //

    private Transform PlayerT;
    private Vector2 HomePos;
    private Vector2 MovementDir;
    UnityEngine.AI.NavMeshAgent agent;


    private float TargetDist;
    [SerializeField] private float followRange = 8f;


    private Animator enemyBoarAnim;
    [SerializeField] private float EnemyHurtDamage = 40f;
    [SerializeField] private float max_Health = 100f;
    [SerializeField] private float min_Health = 0f;
    [SerializeField] private float curr_Health = 60f;




    void Start()
    {

        if (PlayerGO == null)
            PlayerGO = GameObject.FindWithTag("Player");
        
        
        PlayerT = PlayerGO.transform;
        HomePos = new Vector2(this.transform.position.x, this.transform.position.y);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemyBoarAnim = this.GetComponent<Animator>();
    }


    void Update()
    {
        if(PlayerGO == null)
        {
            PlayerGO = GameObject.FindWithTag("Player");
            PlayerT = PlayerGO.transform;
        }
        TargetDist = Vector2.Distance(PlayerT.position, this.transform.position);
        if (TargetDist <= followRange)
        {
            agent.SetDestination(PlayerT.position);
        }
        else
        {
            agent.SetDestination(HomePos);
        }
        MovementDir = agent.velocity / agent.speed;
        //Debug.Log(MovementDir);
        float magn = Mathf.Sqrt(MovementDir.x * MovementDir.x + MovementDir.y * MovementDir.y);
        if (magn > 0.015f)
        {
            enemyBoarAnim.SetBool("Bool_IsIdle", false);
            enemyBoarAnim.SetFloat("MovX", MovementDir.x);
            enemyBoarAnim.SetFloat("MovY", MovementDir.y);
        }
        else
        {
            enemyBoarAnim.SetBool("Bool_IsIdle", true);
        }


        //if (Vector2.magnitude<(MovementDir))
            
            enemyBoarAnim.SetFloat("MovX", MovementDir.x);
            enemyBoarAnim.SetFloat("MovY", MovementDir.y);
        
        //Debug.Log(agent.velocity / agent.speed);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Enemy   -   "+col.gameObject.name + " :  " + gameObject.name + "obje sudur : " + Time.time);
        if (col.CompareTag("Bullet"))
        {
            
            float damage=col.gameObject.GetComponent<ScHandGunBullet>().getBulletDamage();
            this.setHealth(this.getCurrHealth() -damage);
            Debug.Log("Enemy Current Health" + this.getCurrHealth());
            

            Destroy(col.gameObject); // Mermi yok olma

            if (this.getCurrHealth() <= 0)
            {
                //Particle Blood
                //Instantiate(ParticleBloodGO, this.transform.position, Quaternion.identity); //eksik
                Destroy(this.gameObject);
                

                //Bone Appear Chance
                float chance = Random.Range(0.0f, 100.0f);
                if(chance>50f)
                Instantiate(BoneGO, this.transform.position, Quaternion.identity); //eksik

                PlayerGO.GetComponent<ScPlayerDM>().IncreasePlayerScore(); // eksik
                
               this.GetComponent<Collider2D>().enabled=false;

                Destroy(this.gameObject); //Enemy Dies
                //Destroy(this.gameObject,0.2f); //eksik Enemy Dies
            }
        }
    }
    void OnCollisonEnter2D(Collision2D col)
    {
        // Debug.Log("Enemy Collision   -   " + col.gameObject.name + " :  " + gameObject.name + ": " + Time.time);

        if (col.gameObject.tag == "Player")
        {

            // Destroy(col.gameObject);
            Destroy(this.gameObject);
            //col.gameObject.SendMessage("ApplyDamage",10);     

        }

    }
    public float getEnemyHurtDamage(){return EnemyHurtDamage;}

    public float getMinHealth() { return this.min_Health; }
    public float getCurrHealth() { return this.curr_Health; }
    public void setHealth(float HealthAmount)
    {
        if (HealthAmount > this.max_Health)
            this.curr_Health = this.max_Health;
         else
            this.curr_Health = HealthAmount;
    }
}
