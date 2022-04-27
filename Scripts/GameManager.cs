using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool hasWateringCan;
    public Transform canLocation;
    public Transform waterLocation;
    public GameObject wateringCan;
    public MouseCursor mC;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "WateringCan")
                {
                    if (hasWateringCan)
                    {
                        Debug.Log("Had Watering Can");
                        mC.setAsDefault();
                        hasWateringCan = false;
                    }
                    else
                    {
                        Debug.Log("Picking Up Watering Can");
                        mC.setAsCan(); ;
                        hasWateringCan = true;
                    }
                }
            }

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Plant")
                {
                    if (hasWateringCan)
                    {
                        if (hit.collider.GetComponentInParent<PlantFunctions>().isWatered)
                        {
                            Debug.Log("This plant has already been watered");
                        }
                        else
                        {
                            wateringCan.transform.position = hit.collider.GetComponentInParent<PlantFunctions>().canLocation.position;
                            wateringCan.transform.parent.transform.Rotate(0, 0, -60f, Space.Self);
                            GameObject waterEffect = Instantiate(Resources.Load("Prefabs/PART_WaterDrops") as GameObject, new Vector3(waterLocation.position.x, waterLocation.position.y, waterLocation.position.z), Quaternion.identity, waterLocation);
                            if (hit.collider.GetComponentInParent<PlantFunctions>().index > 1)
                            {
                                wateringCan.transform.Rotate(wateringCan.transform.rotation.x, 90f, wateringCan.transform.rotation.z, Space.Self);
                                StartCoroutine(returnWateringCan(1, waterEffect));
                            }
                            else
                            {
                                StartCoroutine(returnWateringCan(0, waterEffect));
                            }
                            hit.collider.GetComponentInParent<PlantFunctions>().changeToWatered();
                        }
                    }
                    else
                    {
                        Debug.Log("Pick up the watering can to water this plant");
                    }
                }
            }
        }
    }

    IEnumerator returnWateringCan(int indx, GameObject gO)
    {
        yield return new WaitForSeconds(2.5f);

        wateringCan.transform.position = canLocation.position;
        wateringCan.transform.parent.transform.Rotate(0, 0, 60f, Space.Self);
        Destroy(gO);

        if (indx == 1)
        {
            wateringCan.transform.Rotate(wateringCan.transform.rotation.x, -90f, wateringCan.transform.rotation.z, Space.Self);
        }
    }
}
