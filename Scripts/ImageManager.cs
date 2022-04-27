using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class ImageManager : MonoBehaviour
{
    public static ImageManager instance;

    string _basePath;
    string _versionJsonPath;
    JSONObject _versionJson;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        instance = this;

        _basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        _versionJson = new JSONObject();
        _versionJsonPath = _basePath + "VersionJson";

        if (File.Exists(_versionJsonPath))
        {
            string jsonString = File.ReadAllText(_versionJsonPath);
            _versionJson = JSON.Parse(jsonString) as JSONObject;
        }
    }

    bool ImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    public void SaveImage(string name, byte[] bytes, int imgVer)
    {
        File.WriteAllBytes(_basePath + name, bytes);
        UpdateVersionJson(name, imgVer);
    }

    public byte[] LoadImage(string name, int imgVer)
    {
        byte[] bytes = new byte[0];

        if (!IsImageUpToDate(name, imgVer))
        {
            return bytes;
        }

        if (ImageExists(name))
        {
            bytes = File.ReadAllBytes(_basePath + name);
        }
        return bytes;
    }

    public Sprite BytesToSprite(byte[] bytes)
    {
        // Create texture2D
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        // Create sprite (to be placed in UI)
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }

    void UpdateVersionJson(string name, int ver)
    {
        _versionJson[name] = ver;
    }

    bool IsImageUpToDate(string name, int ver)
    {
        if (_versionJson[name] != null)
        {
            return _versionJson[name].AsInt == ver;
        }
        return false;
    }

    public void SaveVersionJson()
    {
        File.WriteAllText(_versionJsonPath, _versionJson.ToString());
    }

}
