using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Web : MonoBehaviour
{
    public TextMeshProUGUI errorMessage;
    string url = "https://sim421-group.000webhostapp.com/";
    string urlHeader;
    string urlPath;

    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/GetDate.php"));
        //StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/GetUsers.php"));
        //StartCoroutine(Login("testuser3", "123456"));
        //StartCoroutine(RegisterUser("testuser3", "123456"));


        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    public void ShowUserItems()
    {
        //StartCoroutine(GetItemsIDs(Main.instance.UserInfo.UserID));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "GetUsers.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(username);
                //Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                errorMessage.text = www.downloadHandler.text;
                Main.instance.UserInfo.SetCredentials(username, password);
                Main.instance.UserInfo.SetID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("Wrong credentials.") || www.downloadHandler.text.Contains("Username does not exist."))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    // If we logged in correctly
                    Main.instance.UserProfile.SetActive(true);
                    Main.instance.Login.gameObject.SetActive(false);
                    //Main.instance.Inventory.SetActive(false);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                errorMessage.text = www.downloadHandler.text;
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetItemsIDs.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Print Item ID: Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetPlantIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetPlantIDs.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Get usersplants Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Get  Item Info Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetPlant(string plantID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("plantID", plantID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetPlant.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Get Plant Info Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator SellItem(string ID, string itemID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "SellItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                // Call callback function to pass results
                //callback("");
            }
        }
    }

    public IEnumerator SellPlant(string ID, string plantID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("plantID", plantID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "SellPlant.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                // Call callback function to pass results
                //callback("");
            }
        }
    }

    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("urlPath", url);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetItemIcon.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(urlPath);
                Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                Debug.Log("DOWNLOADING ICON: " + itemID);
                // Results by byte array
                byte[] bytes = www.downloadHandler.data;

                callback(bytes);
            }
        }
    }

    public IEnumerator GetPlantIcon(string plantID, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("plantID", plantID);
        form.AddField("urlPath", url);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetPlantIcon.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(urlPath);
                Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                Debug.Log("DOWNLOADING ICON: " + plantID);
                // Results by byte array
                byte[] bytes = www.downloadHandler.data;

                callback(bytes);
            }
        }
    }

    public IEnumerator GetUserInfo(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetUserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetUserLevel(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "GetUserLevel.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                errorMessage.text = www.error;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // Call callback function to pass results
                //callback(jsonArray);
            }
        }
    }
}
