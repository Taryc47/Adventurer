using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float damage;
    public static float speed;
    public static float health;
    public static int numOfHearts;

    public Text CollectedText;
    public static int collectedAmount = 0;

    public Image[] hearts;
    public Sprite fullHeart;
    // public Sprite halfHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        // Base stats
        damage = 1f;
        speed = 5f;
        health = 3f;
        numOfHearts = 3;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth();
        CollectedText.text = "Items collected: " + collectedAmount;
    }

    void PlayerHealth()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
