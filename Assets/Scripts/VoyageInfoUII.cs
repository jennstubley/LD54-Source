using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoyageInfoUII : MonoBehaviour
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
       // transform.Find("Length").GetComponent<TMP_Text>().text = "** " + voyageController.NextVoyage.Length + " days **";
    }
}
