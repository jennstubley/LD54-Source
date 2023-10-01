using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodInfoUI : MonoBehaviour
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
        SetText(transform.Find("Simple Panel").GetComponentInChildren<TMP_Text>(), FoodType.Basic);
        SetText(transform.Find("Luxury Panel").GetComponentInChildren<TMP_Text>(), FoodType.Luxury);
        SetText(transform.Find("Water Panel").GetComponentInChildren<TMP_Text>(), FoodType.Water);
    }

    private void SetText(TMP_Text text, FoodType foodType)
    {
        int storedFood = ship.GetTotalFood(foodType);
        int neededFood = ship.GetRequiredFood(foodType);
        string colorName = neededFood > storedFood ? "red" : "black";
        text.text = "<color=\"" + colorName + "\">" + storedFood + "/" + neededFood;
    }
}
