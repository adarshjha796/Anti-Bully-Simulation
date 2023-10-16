using UnityEngine;

public class ArrowPopUp : MonoBehaviour
{
    public GameObject arrowSprite;
    public CharacterManager characterManager;

    public void OnMouseOver()
    {
        arrowSprite.SetActive(true);

        //if (arrowSprite.name == characterManager.characterDatabase.character[characterManager.selectedOption].characterName)
        //{
        //    //if (characterManager.selectedOption >= characterManager.characterDatabase.CharacterCount)
        //{
        //    characterManager.selectedOption = 0;
        //}
        characterManager.selectedOption = gameObject.GetComponent<CharacterManager>().selectedOption;
            characterManager.UpdateCharacter(characterManager.selectedOption);
            characterManager.SaveCharacter();
            print("CN : " + characterManager.characterDatabase.character[characterManager.selectedOption].characterName);
        //}
        //else
        //{
        //    print("Nooooooo");
        //}
    }

    public void OnMouseExit()
    {
        arrowSprite.SetActive(false);
    }

    public void OnMouseDown()
    {
        characterManager.ChangeScene(2);
    }
}