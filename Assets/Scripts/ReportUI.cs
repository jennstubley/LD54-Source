using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReportUI : MonoBehaviour
{
    private VoyageController voyageController;

    // Start is called before the first frame update
    void Start()
    {
        voyageController = FindObjectOfType<VoyageController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (voyageController.LastVoyageReport != null)
        {
            transform.Find("First Class Panel").GetComponentInChildren<TMP_Text>().text = "$" + voyageController.LastVoyageReport.FirstClassIncome;
            transform.Find("Second Class Panel").GetComponentInChildren<TMP_Text>().text = "$" + voyageController.LastVoyageReport.SecondClassIncome;
            transform.Find("Cargo Panel").GetComponentInChildren<TMP_Text>().text = "$" + voyageController.LastVoyageReport.CargoIncome;
            transform.Find("Income Panel").GetComponentInChildren<TMP_Text>().text = "$" + voyageController.LastVoyageReport.TotalIncome();
            transform.Find("Supplies Panel").GetComponentInChildren<TMP_Text>().text = "-$" + voyageController.LastVoyageReport.SupplyCost;
            transform.Find("Expenses Panel").GetComponentInChildren<TMP_Text>().text = "-$" + voyageController.LastVoyageReport.TotalExpenses();
            int netProfit = voyageController.LastVoyageReport.NetProfit();
            transform.Find("Total Panel").GetComponentInChildren<TMP_Text>().text = (netProfit >= 0 ? "" : "-") + "$" + Mathf.Abs(netProfit);
        }
    }

}
