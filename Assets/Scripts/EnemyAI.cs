using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region Public 
    public int score;
    public List<GameObject> Actions = new List<GameObject>();
    public GameObject previousSymbol;
    public List<ParticleSystem> muzzleEffecft = new List<ParticleSystem>();
    public float health, range, force, attackRate, numberOfBullets, timeBetweenBullets;
    public List<float> offsetY = new List<float>();
    public enum EnemyState { chase, attack, patroling };
    public float moveSpeed, attackDistance, chaseDistance, enemyMovement;
    public int damage;
    public LayerMask playerMask, ground, wall;

    #endregion

    #region Private

    AudioSource audioSource;
    SoundFiles soundFiles;
    Vector3 patrolDestination;
    bool isPatroling;
    int bulletsFired;
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject rayShoot;

    [SerializeField]
    List<GameObject> attackPosition = new List<GameObject>();

    float lastAttack = 0.0f;

    //AnimationScript animationScript;
    NavMeshAgent navMeshAgent;
    EnemyState enemyState;

    AnimationScript animationScript;


    Transform playerTransform;
    #endregion

    private void Start()
    {
        bulletsFired = 0;
        enemyState = EnemyState.patroling;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationScript = GetComponent<AnimationScript>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        soundFiles = GetComponent<SoundFiles>();
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(enemyState);
        //Debug.Log(isPatroling);

        if (enemyState == EnemyState.chase)
        {
            Chase();
            SetSymbol(1);
        }
        else if (enemyState == EnemyState.attack)
        {
            Attack();
            SetSymbol(2);
        }
        else
        {
            patrol();
            SetSymbol(0);   
        }
    }

    void SetSymbol(int index)
    {
        previousSymbol.SetActive(false);
        Actions[index].SetActive(true);
        previousSymbol = Actions[index];
    }

    void patrol()
    {
        if (isPatroling)
        {
            navMeshAgent.SetDestination(patrolDestination);
            if(Vector3.Distance(transform.position, playerTransform.position)<chaseDistance)
            {
                isPatroling = false;
                enemyState = EnemyState.chase;
            }
            Debug.DrawRay(transform.position, transform.forward * 4f);
            Debug.DrawRay(transform.position, transform.right * 4f);
            Debug.DrawRay(transform.position, -transform.right * 4f);

            Ray Forward = new Ray(transform.position, transform.forward);
            Ray Right = new Ray(transform.position, transform.right);
            Ray Left = new Ray(transform.position, -transform.right);

            if (Vector3.Distance(transform.position, patrolDestination) < 1f || Physics.Raycast(Forward, 4f, wall) || Physics.Raycast(Left, 4f, wall) || Physics.Raycast(Right, 4f, wall))
                searchPlayer();
        }
        else
            searchPlayer();
    }



    void searchPlayer()
    {
        
        float x = Random.Range(-enemyMovement, enemyMovement);
        float z = Random.Range(-enemyMovement, enemyMovement);
        patrolDestination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        Debug.DrawRay(transform.position, -transform.up*4f, Color.red);

        if (Physics.Raycast(transform.position, -transform.up, 4f, ground))
        {
            isPatroling = true;
        }
    }

    void Chase()
    {
        navMeshAgent.transform.LookAt(playerTransform.transform);
        animationScript.Attack(false);
        navMeshAgent.SetDestination(playerTransform.transform.position);
        navMeshAgent.speed = moveSpeed;    
        if(Vector3.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            enemyState = EnemyState.patroling;
        }
        if(Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
        {
            enemyState = EnemyState.attack;
        }
    }

    void ShootBullet()
    {

        //if (health == 350)
        //    soundFiles.ShotGunSound(audioSource);
        //else if (health == 250)
        //    soundFiles.RiffleSound(audioSource);
        //else if (health == 200)
        //    soundFiles.BotSound(audioSource);
        

        navMeshAgent.transform.LookAt(playerTransform.transform);
        Vector3 direction;
        RaycastHit hit;
        for (int i = 0; i < attackPosition.Count; i++)
        {
            
            //Debug.Log("shoot");
            direction = rayShoot.transform.forward;

            //Debug.DrawRay(rayShoot.transform.position, direction * range, Color.red);
            if (Physics.Raycast(rayShoot.transform.position, direction, out hit, range, playerMask))
            {
                Vector3 bulletDirection = hit.point - attackPosition[i].transform.position + new Vector3(0f, offsetY[i], 0f);

                //playerTransform.gameObject.GetComponent<playerMovement>().TakeDamage(damage);
                muzzleEffecft[i].Play();
                GameObject currentBullet = Instantiate(bullet, attackPosition[i].transform.position, Quaternion.LookRotation(-hit.normal));
                currentBullet.GetComponent<Rigidbody>().AddForce(force * bulletDirection.normalized, ForceMode.Impulse);
                Destroy(currentBullet, 3f);
            }
        }

        bulletsFired++;
        
        if (bulletsFired <= numberOfBullets)
        {
            Invoke("ShootBullet", timeBetweenBullets);
        }
        else
            bulletsFired = 0;
    }


    void Attack()
    {
        navMeshAgent.transform.LookAt(playerTransform.transform);
        if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance && Time.time > lastAttack + attackRate)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            animationScript.Attack(true);
            ShootBullet();
            lastAttack = Time.time;
        } 
        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            navMeshAgent.isStopped = false;
            enemyState = EnemyState.chase;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            playerMovement.score += score;
            Destroy(gameObject);
        }
    }
}
