using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Voyage
{
    public List<GameObject> supplies;
    public List<GameObject> passengers;
    public List<GameObject> cargo;
}

public class ReportInfo
{
    public int FirstClassIncome;
    public int SecondClassIncome;
    public int CargoIncome;
    public int SupplyCost;

    public int TotalIncome()
    {
        return FirstClassIncome + SecondClassIncome + CargoIncome;
    }
    public int TotalExpenses()
    {
        return SupplyCost;
    }

    public int NetProfit()
    {
        return TotalIncome() - TotalExpenses();
    }
}


public class VoyageController : MonoBehaviour
{
    public Voyage NextVoyage;
    public int VoyageNum = 0;
    public List<Voyage> Voyages;
    public ReportInfo LastVoyageReport;
    public int Money;
    public float Reputation; // 0 to 100.
    private Ship ship;
    [SerializeField] private GameObject reportUI;
    [SerializeField] private GameObject lossUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject pausePanel;
    //[SerializeField] private List<GameObject> passengers;
    //[SerializeField] private List<GameObject> supplies;
    [SerializeField] private GameObject passengerParent;
    [SerializeField] private GameObject supplyParent;
    [SerializeField] private GameObject cargoParent;


    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPaused()
    {
        return pausePanel.activeSelf;
    }

    public void SetSail()
    {
        LastVoyageReport = new ReportInfo();

        LastVoyageReport.FirstClassIncome = ship.GetPassengerProfit(true);
        LastVoyageReport.SecondClassIncome = ship.GetPassengerProfit(false);
        LastVoyageReport.CargoIncome = ship.GetCargoProfit();

        LastVoyageReport.SupplyCost = ship.GetSupplyCost();
        Money += (LastVoyageReport.TotalIncome() - LastVoyageReport.TotalExpenses());
        reportUI.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void GoToPort()
    {
        reportUI.SetActive(false);
        pausePanel.SetActive(false);
        if (Money >= 1000)
        {
            winUI.SetActive(true);
            pausePanel.SetActive(true);
            return;
        }
        else if (VoyageNum == Voyages.Count)
        {
            Debug.Log("You lost! You didn't make it to $1000 :(");
            lossUI.SetActive(true);
            pausePanel.SetActive(true);
            return;
        }
        ship.ClearCargo();
        NextVoyage = Voyages[VoyageNum];
        VoyageNum++;
        ResetCargo();
    }

    public void ResetCargo() {
        foreach (Transform child in passengerParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in supplyParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in cargoParent.transform)
        {
            Destroy(child.gameObject);
        }
        float offsetX = 0;
        foreach (GameObject passengerPrefab in NextVoyage.passengers)
        {
            GameObject passenger = GameObject.Instantiate(passengerPrefab);
            passenger.transform.SetParent(passengerParent.transform, false);
            passenger.transform.localPosition = new Vector3(offsetX, 0.5f, 0);
            offsetX += 0.8f;
        }
        offsetX = 0;
        foreach (GameObject supplyPrefab in NextVoyage.supplies)
        {
            GameObject supply = GameObject.Instantiate(supplyPrefab);
            supply.transform.SetParent(supplyParent.transform, false);
            supply.transform.localPosition = new Vector3(offsetX, 0.5f, 0);
            offsetX += 1f;
        }
        offsetX = 0;
        foreach (GameObject cargoPrefab in NextVoyage.cargo)
        {
            GameObject supply = GameObject.Instantiate(cargoPrefab);
            supply.transform.SetParent(cargoParent.transform, false);
            supply.transform.localPosition = new Vector3(offsetX, 0.5f, 0);
            offsetX += 1f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
