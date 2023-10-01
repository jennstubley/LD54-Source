using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSailValidation : MonoBehaviour
{
    private Button button;
    private Ship ship;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        ship = FindObjectOfType<Ship>();
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = ValidateShip();
        text.text = ValidateShip() ? "Set Sail" : "<color=#808080>Supplies needed";
    }

    private bool ValidateShip()
    {
        foreach (FoodType foodType in Enum.GetValues(typeof(FoodType))) {

            int storedFood = ship.GetTotalFood(foodType);
            int neededFood = ship.GetRequiredFood(foodType);
            if (neededFood > storedFood)
            {
                return false;
            }
        }
        return true;
    }
}
