using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int baseLife = 5;
    public static int currentLife;

    public string baseForm = "base";
    public float baseResistance = 1;
    public float baseMass = 1;
    public AnimatorController baseAnim;

    private static int coins = 0;

    private GameObject PlayerCharacter;
    private GameObject LevelInfos;
    private GameObject DeathInfos;

	// Use this for initialization
	void Start () {
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        LevelInfos = GameObject.Find("LevelInfos");
        DeathInfos = GameObject.Find("DeathInfos");
        Debug.Log(DeathInfos);
        currentLife = baseLife;
        RespawnPlayer();
	}

    public void AddCoin()
    {
        Debug.Log("Add Coin");
        coins++;
        UpdateLevelInfos();
    }

    public IEnumerator KilledPlayer()
    {
        coins = 0;
        if (currentLife == 0)
        {
            currentLife = baseLife;
            DeathInfos.transform.Find("LifeInfos").gameObject.SetActive(false);
            DeathInfos.transform.Find("HeartImage").gameObject.SetActive(false);
            DeathInfos.transform.Find("GameOverText").gameObject.SetActive(true);
            yield return StartCoroutine(DeathInfos.GetComponent<FadeEffects>().FadeCanvas(0f, 1f, 1f));
            yield return new WaitForSeconds(2);
            Application.LoadLevel("Main Menu");
        }
        currentLife--;
        yield return StartCoroutine(DeathAnimation());
        UpdateLevelInfos();
        ResetLevel();
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        PlayerCharacter.GetComponent<PlayerController>().ApplyCostume(baseForm, baseAnim, baseResistance, baseMass);
        PlayerCharacter.transform.position = this.gameObject.transform.position;
        Transform cam = PlayerCharacter.transform.GetChild(0);
        cam.localPosition = new Vector3(0, 0, cam.position.z);
    }

    private IEnumerator DeathAnimation()
    {
        StartCoroutine(LevelInfos.GetComponent<FadeEffects>().FadeCanvas(1f, 0f, 1f));
        yield return StartCoroutine(DeathInfos.GetComponent<FadeEffects>().FadeCanvas(0f, 1f, 1f));
        yield return new WaitForSeconds(1);
        UpdateDeathInfos();
        yield return new WaitForSeconds(1);
        StartCoroutine(DeathInfos.GetComponent<FadeEffects>().FadeCanvas(1f, 0f, 0.2f));
        StartCoroutine(LevelInfos.GetComponent<FadeEffects>().FadeCanvas(0f, 1f, 0.2f));
    }

    private void UpdateDeathInfos()
    {
        Text lifeInfos = DeathInfos.transform.Find("LifeInfos").GetComponent<Text>();
        lifeInfos.text = "x " + currentLife;
    }

    private void UpdateLevelInfos()
    {
        Text coinText = LevelInfos.transform.Find("CoinText").GetComponent<Text>();
        coinText.text = "x " + coins;
    }

    private void ResetLevel() 
    {
        GameObject[] coinsArray = GameObject.FindGameObjectsWithTag("Coin");
        GameObject[] powerUpArray = GameObject.FindGameObjectsWithTag("PowerUp");
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        Spawner[] spawnerArray = Object.FindObjectsOfType<Spawner>();

        foreach (GameObject coin in coinsArray)
        {
            Destroy(coin);
        }

        foreach (GameObject enemy in enemyArray)
        {
            Destroy(enemy);
        }

        foreach (GameObject powerUp in powerUpArray)
        {
            Destroy(powerUp);
        }

        foreach (Spawner spawner in spawnerArray)
        {
            spawner.spawn();
        }
    }



	
	// Update is called once per frame
	void Update () {
        if (coins >= 50)
        {
            currentLife++;
            coins -= 50;
            UpdateLevelInfos();
            UpdateDeathInfos();
        }
	
	}
}
