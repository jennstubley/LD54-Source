using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipController ttController;
    private VoyageController voyageController;
    [SerializeField] private string text;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        voyageController = FindObjectOfType<VoyageController>();
        ttController = FindObjectOfType<TooltipController>();
        Supplies supplies = GetComponent<Supplies>();
        if (supplies != null)
        {
            string foodType = "Water";
            if (supplies.FoodType == FoodType.Basic)
            {
                foodType = "Simple Food";
            }
            else if (supplies.FoodType == FoodType.Luxury)
            {
                foodType = "Luxury Food";
            }
            text = "Cost: $" + supplies.Cost + " \n" + supplies.FoodAmount + " " + foodType;
        }

        Passenger passenger = GetComponent<Passenger>();
        if (passenger != null)
        {
            text = (passenger.TicketPrice == 200 ? "First Class" : "Second Class") + "\n  Fare: $" + passenger.TicketPrice + "\n  " + (passenger.TicketPrice == 200 ? "Luxury Food" : "Simple Food");
        }

        Cargo cargo = GetComponent<Cargo>();
        if (cargo != null)
        {
            text = "Reward: $" + cargo.Reward;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (voyageController.IsPaused()) return;
        ttController.ShowTooltip(text, transform.position + offset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ttController.HideTooltip();
    }
}
