using UnityEngine;

public class LoadPlayer: MonoBehaviour
{
    public static LoadPlayer Instance;

    public CharacterDatabase characterDatabase;

    public SpriteRenderer characterSprite;

    public int selectedOption = 0;

    public string SelectedPlayer = "";


    private void Awake()
    {
        characterSprite = GetComponent<SpriteRenderer>();

        if (Instance != null)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            LoadCharacter();
        }
        UpdateCharacter(selectedOption);
    }

    public void UpdateCharacter(int selectedOption)
    {
        Character charater = characterDatabase.GetCharacter(selectedOption);
        characterSprite.sprite = charater.characterSprite;
        if (selectedOption == 0)
        {
            transform.position = new(-2.0f, -2.2f);
            transform.GetComponent<BoxCollider2D>().size = new(2.0f, 5.0f);
        }

        SelectedPlayer = characterSprite.sprite.name;
    }

    public void LoadCharacter() => selectedOption = PlayerPrefs.GetInt("selectedOption");
}