using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private List<Passenger> passengers = new List<Passenger>();
    private List<Supplies> supplyList = new List<Supplies>();
    private List<Cargo> cargoList = new List<Cargo>();
    private VoyageController voyageController;

    // Start is called before the first frame update
    void Start()
    {
        voyageController = GetComponent<VoyageController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCargo(GameObject cargo)
    {
        Passenger passenger = cargo.GetComponent<Passenger>();
        Supplies supplies = cargo.GetComponent<Supplies>();
        Cargo cargoObj = cargo.GetComponent<Cargo>();
        if (passenger != null)
        {
            if (!passengers.Contains(passenger))
            {
                passengers.Add(passenger);
            }
        }
        else if (supplies != null)
        {
            if (!supplyList.Contains(supplies))
            {
                supplyList.Add(supplies);
                //voyageController.Money -= supplies.Cost;
            }
        }
        else if (cargoObj != null)
        {
            if (!cargoList.Contains(cargoObj))
            {
                cargoList.Add(cargoObj);
            }
        }
    }

    public void RemoveCargo(GameObject cargo)
    {
        Passenger passenger = cargo.GetComponent<Passenger>();
        Supplies supplies = cargo.GetComponent<Supplies>();
        Cargo cargoObj = cargo.GetComponent<Cargo>();
        if (passenger != null)
        {
            if (passengers.Contains(passenger))
            {
                passengers.Remove(passenger);
            }
        }
        else if (supplies != null)
        {
            if (supplyList.Contains(supplies))
            {
                supplyList.Remove(supplies);
                //voyageController.Money += supplies.Cost;
            }
        }
        else if (cargoObj != null)
        {
            if (cargoList.Contains(cargoObj))
            {
                cargoList.Remove(cargoObj);
            }
        }
    }

    public int GetRequiredFood(FoodType foodType)
    {
        if (foodType == FoodType.Water) return passengers.Count;

        return passengers.Where(p => p.FoodType == foodType).Count();
    }

    public int GetTotalFood(FoodType foodType)
    {
        int totalFood = 0;
        foreach (Supplies supply in supplyList)
        {
            if (supply.FoodType == foodType)
            {
                totalFood += supply.FoodAmount;
            }
        }
        return totalFood;
    }

    public int GetPassengerCount()
    {
        return passengers.Count;
    }

    public int GetPassengerProfit(bool firstClass)
    {
        return passengers.Where(p => firstClass ? p.TicketPrice == 200 : p.TicketPrice != 200).Sum(p => p.TicketPrice);
    }

    public void ClearCargo()
    {
        passengers.Clear();
        supplyList.Clear();
        cargoList.Clear();
    }

    public bool ContainsSupplies(Supplies supplies)
    {
        return supplyList.Contains(supplies);
    }

    internal List<Passenger> GetPassenegersInPriorityOrder()
    {
        return passengers.OrderByDescending(p => p.FoodType).ToList();
    }

    internal int GetSupplyCost()
    {
        return supplyList.Sum(s => s.Cost);
    }

    internal int GetCargoProfit()
    {
        return cargoList.Sum(s => s.Reward);
    }
}
