using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{

    #region Private Variables
    private int currentEnemies;
    [HideInInspector]
    private GameObject player;

    [SerializeField]
    private GameObject weapon, gameUI, PlayAgainMenu;
    public static float lastHealthSpawn = 0.0f;
    #endregion

    #region Public Variables

    public int totalEnemies;
    public float healthSpawnRate;
    public GameObject[] enemies;
    public GameObject LifeObs, BonusObj;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentEnemies = totalEnemies;   
    }
    

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        weapon.SetActive(true);
        gameUI.SetActive(true);
        PlayAgainMenu.SetActive(false);
        player.GetComponent<playerMovement>().enabled = true;
        player.GetComponent<mouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMovement.score = 0;
        player.GetComponent<playerMovement>().healthPlayer = 1500;
        player.GetComponent<playerMovement>().currentTime = player.GetComponent<playerMovement>().getHealthTime;

    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetComponent<playerMovement>().healthPlayer <=0 || player.GetComponent<playerMovement>().currentTime<=0)
        {
            weapon.SetActive(false);
            gameUI.SetActive(false);
            PlayAgainMenu.SetActive(true);
            player.GetComponent<playerMovement>().enabled = false;
            player.GetComponent<mouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        if (GameObject.FindGameObjectsWithTag("health").Length < 1)
        {
            if (Time.time > lastHealthSpawn + healthSpawnRate)
            {
                InstantiateObjects(LifeObs, 5f);
            }
        }

        if (GameObject.FindGameObjectsWithTag("bonus").Length < 1)
        {
                InstantiateObjects(BonusObj, 5f);
        }

        int randomEnemy = Random.Range(0, 3);
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(currentEnemies<totalEnemies)
        {
            InstantiateObjects(enemies[randomEnemy], 5f);
            
        }
    }

    void InstantiateObjects(GameObject inst, float y)
    {
        float x = Random.Range(-80f, 80f);
        float z = Random.Range(-80f, 80f);
        GameObject newHealthSpawn = Instantiate(inst, new Vector3(x, y, z), Quaternion.identity);
    }
}
