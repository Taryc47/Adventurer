using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public string description;
    // public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{
    public Item item;

    public float healthChange;
    public int heartChange;
    public float damageChange;
    public float speedChange;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.collectedAmount++;
            PlayerController.health += healthChange;
            PlayerController.numOfHearts += heartChange;
            PlayerController.damage += damageChange;
            PlayerController.speed += speedChange;
            
           /* player.health += healthChange;
            player.numOfHearts += heartChange;
            player.damage += damageChange;
            player.speed += speedChange;*/
            Destroy(gameObject);
        }
    }
}
