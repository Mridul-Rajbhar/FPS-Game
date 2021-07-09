using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerMovement : MonoBehaviour
{

    #region Private Variables
    [HideInInspector]
    public int currentTime;
    CharacterController characterController;

    [HideInInspector]
    public int healthPlayer;

    [SerializeField]
    float gravity = 20f;
    
    [SerializeField]
    float verticalVelocity = 0f;
    Vector3 movePlayer;
    #endregion

    #region Public Variables
    public int totalHealth, getHealthTime;
    public static int score;
    public float speed, jumpForce;
    public TMP_Text text ,scoreText, timeText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ShowTime();
        currentTime = getHealthTime;
        totalHealth = healthPlayer;

        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    { 
        scoreText.text = "Score: " + score;
        text.text = "Health: " + healthPlayer+"/"+totalHealth;
        Move();
    }

    void ShowTime()
    {
        timeText.text = "Time: " + currentTime;
        currentTime--;
        Invoke("ShowTime", 1f);
    }

    public void TakeDamage(int damage)
    {
        healthPlayer -= damage;
    }

    void Move()
    {
        movePlayer = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        movePlayer = transform.TransformDirection(movePlayer);
        verticalVelocity -= gravity * Time.deltaTime;
        
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
        movePlayer.y = verticalVelocity*Time.deltaTime;
        
        
        //Debug.Log(movePlayer.y);
        characterController.Move(movePlayer);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="health")
        {
            if (healthPlayer < totalHealth - 25)
                healthPlayer += 25;
            else
                healthPlayer = totalHealth;
            Destroy(other.gameObject);
            GamePlay.lastHealthSpawn = Time.time;
        }

        if(other.gameObject.tag == "bonus")
        {
            score += 25;
            currentTime = getHealthTime;
            Destroy(other.gameObject);
        }
    }

}
