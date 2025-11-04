using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AMManager : MonoBehaviour
{
    public ShopInventory shopInventory;
    public TextMeshProUGUI roundNumberText;
    public TextMeshProUGUI roundPhaseText;
    public TextMeshProUGUI roundTimerText;
    public TextMeshProUGUI playerCoinsText;
    public UnityEngine.UI.Image playerCoinsIcon;
    public SpawnEnemiesAM spawnEnemiesScript;

    //Sprites
    public Sprite healthPotionSprite;
    public Sprite silverArrowSprite;
    public Sprite explosiveArrowSprite;
    public Sprite bigSwordSprite;
    public Sprite spearSprite;
    public Sprite scytheSprite;

    public bool canOpenShop;

    private int currentRound = 1;
    private int maxRound = 5;
    private float roundTimer = 60f;
    private float shopTimer = 20f;
    private bool startRound = false;

    public void Start()
    {
        //Potion
        shopInventory.AddItemManually("Health Potion", ShopItems.ShopItemType.HealthPotion, 10, healthPotionSprite, new Vector3(0.7f, 0.7f, 0));

        //Arrows
        shopInventory.AddItemManually("SilverArrow", ShopItems.ShopItemType.Arrow, 20, silverArrowSprite, new Vector3(0.9f, 0.5f, 0));
        shopInventory.AddItemManually("ExplosiveArrow", ShopItems.ShopItemType.Arrow, 30, explosiveArrowSprite, new Vector3(0.9f, 0.5f, 0));

        //Weapons
        shopInventory.AddItemManually("BigSword", ShopItems.ShopItemType.Weapon, 50, bigSwordSprite, new Vector3(0.5f, 1.1f, 0));
        shopInventory.AddItemManually("Spear", ShopItems.ShopItemType.Weapon, 35, spearSprite, new Vector3(0.4f, 1.1f, 0));
        shopInventory.AddItemManually("Scythe", ShopItems.ShopItemType.Weapon, 100, scytheSprite, new Vector3(0.8f, 1.1f, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            startRound = true;
            roundNumberText.enabled = true;
            roundPhaseText.enabled = true;
            roundTimerText.enabled = true;
            playerCoinsText.enabled = true;
            playerCoinsText.text = "0";
            playerCoinsIcon.enabled = true;
        }

        if(startRound)
        {
            if (currentRound <= maxRound)
            {
                if (roundTimer > 0)
                {
                    spawnEnemiesScript.canSpawnEnemies = true;

                    roundPhaseText.text = "Protect the tree Phase";
                    roundNumberText.text = "Round: " + currentRound;
                    
                    roundTimer -= Time.deltaTime;

                    int minutes = Mathf.FloorToInt(roundTimer / 60f);
                    int seconds = Mathf.FloorToInt(roundTimer % 60f);

                    roundTimerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
                }
                else
                {
                    if (shopTimer > 0)
                    {
                        // Shop round logic
                        spawnEnemiesScript.canSpawnEnemies = false;

                        roundPhaseText.text = "Shop Phase";
                        canOpenShop = true;
                        shopTimer -= Time.deltaTime;

                        int minutes = Mathf.FloorToInt(shopTimer / 60f);
                        int seconds = Mathf.FloorToInt(shopTimer % 60f);

                        roundTimerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
                    }
                    else
                    {
                        // End of shop round, start a new round
                        currentRound++;
                        canOpenShop = false;
                        spawnEnemiesScript.canSpawnEnemies = true;
                        roundTimer = 60f;
                        shopTimer = 20f;
                    }
                }
            }
            else
            {
                Debug.Log("Player Won");
            }
        }
        else
        {
            spawnEnemiesScript.canSpawnEnemies = false;
        }
    }
}
