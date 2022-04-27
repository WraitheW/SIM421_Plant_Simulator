using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback;

    // Start is called before the first frame update
    void Start()
    {

        _createItemsCallback = (jsonArrayString) => 
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.instance.UserInfo.UserID;
        StartCoroutine(Main.instance.Web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        // Parsing json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            // Create local variables
            bool isDone = false;    // Are we done downloading?
            string itemId = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];

            JSONObject itemInfoJson = new JSONObject();

            // Create a callback to get the information from Web.cs
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.instance.Web.GetItem(itemId, getItemInfoCallback));

            // Wait until the callback is called from WEB (info finished downloading)
            yield return new WaitUntil(() => isDone == true);

            // Instantiate GameObject (item prefab)
            GameObject itemGameObj = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item = itemGameObj.AddComponent<Item>();

            item.ID = id;
            item.ItemID = itemId;

            itemGameObj.transform.SetParent(this.transform);
            itemGameObj.transform.localScale = Vector3.one;
            itemGameObj.transform.localPosition = Vector3.zero;

            // Fill information
            itemGameObj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            itemGameObj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            itemGameObj.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];

            int imgVer = itemInfoJson["imgVer"].AsInt;

            byte[] bytes = ImageManager.instance.LoadImage(itemId, imgVer);

            // Download from web
            if (bytes.Length == 0)
            {
                // Create a callback to get the SRITE from Web.cs
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    // 5. Convert bytes into sprite here
                    Sprite sprite = ImageManager.instance.BytesToSprite(downloadedBytes);
                    itemGameObj.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.instance.SaveImage(itemId, downloadedBytes, imgVer);
                    ImageManager.instance.SaveVersionJson();
                };
                StartCoroutine(Main.instance.Web.GetItemIcon(itemId, getItemIconCallback));
            }
            // Load from device
            else
            {
                Debug.Log("LOADING ICON FROM DEVICE: " + itemId);
                Sprite sprite = ImageManager.instance.BytesToSprite(bytes);

                itemGameObj.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }


            // Set sell button
            itemGameObj.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInInventory = id;
                string iId = itemId;
                string userId = Main.instance.UserInfo.UserID;
                StartCoroutine(Main.instance.Web.SellItem(idInInventory, itemId, userId));
            });

            // Continuing to next item
        }
    }
}
