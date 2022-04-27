using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;
using System;
using TMPro;

public class PlantManager : MonoBehaviour
{
    Action<string> _createPlantsCallback;
    public Transform[] potTransforms;
    public Sprite fig;
    public Sprite peaceLily;
    public Sprite aloe;
    public Sprite snakePlant;
    public Sprite bamboo;
    public Sprite basil;
    public Sprite lavender;
    public Sprite monstera;

    // Start is called before the first frame update
    void Start()
    {

        _createPlantsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreatePlantsRoutine(jsonArrayString));
        };

        CreatePlants();
    }

    public void CreatePlants()
    {
        string userId = Main.instance.UserInfo.UserID;
        StartCoroutine(Main.instance.Web.GetPlantIDs(userId, _createPlantsCallback));
        //Main.instance.Inventory.SetActive(false);
    }

    IEnumerator CreatePlantsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string plantId = jsonArray[i].AsObject["plantID"];
            string id = jsonArray[i].AsObject["ID"];

            JSONObject plantInfoJson = new JSONObject();

            Action<string> getPlantInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                plantInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.instance.Web.GetPlant(plantId, getPlantInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject plantGameObj = Instantiate(Resources.Load("Prefabs/Plant") as GameObject);
            Plant plant = plantGameObj.AddComponent<Plant>();

            GameObject plantPot = Instantiate(Resources.Load("Prefabs/PlantPot") as GameObject, new Vector3(potTransforms[i].position.x, potTransforms[i].position.y, potTransforms[i].position.z), Quaternion.identity);
            if (i == 0 || i == 1)
            {
                plantPot.transform.Rotate(potTransforms[i].rotation.x, -90f, potTransforms[i].rotation.z, Space.Self);
            }
            Plant plantPott = plantPot.AddComponent<Plant>();
            plantPot.gameObject.GetComponent<PlantFunctions>().index = i;
            if (i == 0 || i == 1)
            {
                plantPot.gameObject.GetComponent<PlantFunctions>().changeToSunny();
            }

            plantPot.gameObject.GetComponent<PlantFunctions>().needsSun = plantInfoJson["needSun"];

            plant.ID = id;
            plant.plantID = plantId;

            switch (plant.plantID)
            {
                case "1":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = fig;
                    break;
                case "2":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = peaceLily;
                    break;
                case "3":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = aloe;
                    break;
                case "4":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = snakePlant;
                    break;
                case "5":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = bamboo;
                    break;
                case "6":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = basil;
                    break;
                case "8":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = lavender;
                    break;
                case "9":
                    plantPot.gameObject.transform.Find("Canvas").transform.Find("Plant").GetComponent<Image>().sprite = monstera;
                    break;
            }

            plantGameObj.transform.SetParent(this.transform);
            plantGameObj.transform.localScale = Vector3.one;
            plantGameObj.transform.localPosition = Vector3.zero;

            plantGameObj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = plantInfoJson["name"];
            plantGameObj.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = plantInfoJson["description"];
            plantGameObj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = plantInfoJson["sellPrice"];

            int imgVer = plantInfoJson["imgVer"].AsInt;

            byte[] bytes = ImageManager.instance.LoadImage("p" + plantId, imgVer);

            if (bytes.Length == 0)
            {
                Action<byte[]> getPlantIconCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.instance.BytesToSprite(downloadedBytes);
                    plantGameObj.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.instance.SaveImage(plantId, downloadedBytes, imgVer);
                    ImageManager.instance.SaveVersionJson();
                };
                StartCoroutine(Main.instance.Web.GetPlantIcon(plantId, getPlantIconCallback));
            }
            else
            {
                Debug.Log("lOaDiNg PlAnT iCoN fRoM dEvIcE: " + plantId);
                Sprite sprite = ImageManager.instance.BytesToSprite(bytes);

                plantGameObj.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }

            plantGameObj.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInPlantsInventory = id;
                string pId = plantId;
                string userId = Main.instance.UserInfo.UserID;
                StartCoroutine(Main.instance.Web.SellPlant(idInPlantsInventory, plantId, userId));
            });
        }
    }
}
