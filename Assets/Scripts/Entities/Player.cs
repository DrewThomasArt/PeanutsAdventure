using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    public Inventory inventory;

    void Awake()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        inventory = healthBar.GetComponent<Inventory>();
        healthBar.character = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:

                    shouldDisappear = inventory.AddItem(hitObject);
                    break;
                    case Item.ItemType.HEALTH:

                    shouldDisappear = AdjustHitPoints(hitObject.quantity);
                    break;
                    default:
                    break;
                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            return true;
        }
        return false;
    }
}