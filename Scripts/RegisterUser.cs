using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public Button submitButton;
    public TextMeshProUGUI errorMessage;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(() =>
        {
            if (passwordInput.text == confirmPasswordInput.text)
            {
                StartCoroutine(Main.instance.Web.RegisterUser(usernameInput.text, passwordInput.text));
            }
            else
            {
                Debug.Log("Passwords do not match.");
                errorMessage.text = "Passwords do not match.";
            }
        });
    }
}
