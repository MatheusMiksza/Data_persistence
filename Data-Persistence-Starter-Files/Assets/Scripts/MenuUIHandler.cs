using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField inputName;
    // Start is called before the first frame update
    void Awake()
    {
        if (!string.IsNullOrEmpty(GameManeger.Instance.playerName.Name))
        {
            inputName.text = GameManeger.Instance.playerName.Name;
        }
    }

    public void OnclikStart()
    {
        GameManeger.Instance.SaveName();
        SceneManager.LoadScene(1);
    }

    public void ReadInputName(string s)
    {
        GameManeger.Instance.playerName.Name = s;
        Debug.Log(GameManeger.Instance.playerName.Name);
    }


    public void OnClickExit()
    {
       
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
