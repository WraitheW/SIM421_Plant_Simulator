using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantFunctions : MonoBehaviour
{
    public int index;
    public GameObject gO;
    public int currPosition;
    public bool isWatered;
    public bool isDehydrated;
    public bool isInSun;
    public bool needsSun;
    public bool isDead;

    public Image waterStatusEffect;
    public Image sunStatusEffect;
    public Sprite water;
    public Sprite noStatus;
    public Sprite dehydrated;
    public Sprite sunny;
    public Transform canLocation;

    void Start()
    {
        StartCoroutine(dehydratePlant(GetRandomTime()));
    }

    public void changeToWatered()
    {
        waterStatusEffect.sprite = water;
        isWatered = true;

        StopAllCoroutines();
        StartCoroutine(unWater(GetRandomTime()));
    }

    public void changeToDehydrated()
    {
        waterStatusEffect.sprite = dehydrated;
        isDehydrated = true;

        StartCoroutine(checkForDeadPlant(GetRandomTime()));
    }

    public void changeToNull(Image image)
    {
        image.sprite = noStatus;
        isWatered = false;

        StartCoroutine(dehydratePlant(GetRandomTime()));
    }

    public void changeToSunny()
    {
        sunStatusEffect.sprite = sunny;
        isInSun = true;
    }

    IEnumerator unWater(float time)
    {
        yield return new WaitForSeconds(time);

        changeToNull(waterStatusEffect);
    }

    IEnumerator dehydratePlant(float time)
    {
        yield return new WaitForSeconds(time);

        changeToDehydrated();
    }

    IEnumerator checkForDeadPlant(float time)
    {
        yield return new WaitForSeconds(time);

        killPlant();
    }

    int GetRandomTime()
    {
        return Random.Range(15, 20);
    }

    void killPlant()
    {
        if (isWatered)
        {
            return;
        }
        else
        {
            Debug.Log("A plant has died O.O");
            gO.SetActive(false);
        }
    }
}
