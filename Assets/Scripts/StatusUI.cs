using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
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
        transform.Find("Money").GetComponent<TMP_Text>().text = "$" + voyageController.Money;
        transform.Find("Reputation").GetComponent<TMP_Text>().text = "Rep: " + voyageController.Reputation;
    }
}
