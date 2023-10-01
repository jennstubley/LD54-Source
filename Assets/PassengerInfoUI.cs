using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassengerInfoUI : MonoBehaviour
{
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.Find("Total Panel").GetComponentInChildren<TMP_Text>().text = (ship.GetRequiredFoodPerDay(FoodType.Basic) + ship.GetRequiredFoodPerDay(FoodType.Luxury)).ToString() + "/day";
        transform.Find("Simple Panel").GetComponentInChildren<TMP_Text>().text = ship.GetRequiredFood(FoodType.Basic).ToString();
        transform.Find("Luxury Panel").GetComponentInChildren<TMP_Text>().text = ship.GetRequiredFood(FoodType.Luxury).ToString();
        transform.Find("Water Panel").GetComponentInChildren<TMP_Text>().text = ship.GetRequiredFood(FoodType.Water).ToString();
    }
}
