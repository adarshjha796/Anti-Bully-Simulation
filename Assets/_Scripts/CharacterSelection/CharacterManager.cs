using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;

    public SpriteRenderer characterSprite;

    public int selectedOption = 0;

    private void Start()
    {
        //if (!PlayerPrefs.HasKey("selectedOption"))
        //{
        //    selectedOption = 0;
        //}
        //else
        //{
        //    LoadCharacter();
        //}
    }

    public void UpdateCharacter(int selectedOption)
    {
        Character charater = characterDatabase.GetCharacter(selectedOption);
    }

    public void LoadCharacter()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
