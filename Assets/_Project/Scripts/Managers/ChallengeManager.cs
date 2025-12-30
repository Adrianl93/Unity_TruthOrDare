using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance { get; private set; }

    private ChallengeCategoryData currentCategory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadCategory(string categoryName)
    {
        currentCategory = ChallengeLoader.LoadCategory(categoryName);

        if (currentCategory == null)
        {
            Debug.LogError("No se pudo cargar la categoría de desafíos");
        }
    }

    public ChallengeCard GetRandomChallenge(WheelType type, Difficulty difficulty)
    {
        if (currentCategory == null)
        {
            Debug.LogError("ChallengeManager: categoría no cargada");
            return null;
        }

        bool isTruth = type == WheelType.Truth;

        var group = isTruth ? currentCategory.Truth : currentCategory.Dare;

        var list = difficulty switch
        {
            Difficulty.VeryEasy => group.VeryEasy,
            Difficulty.Easy => group.Easy,
            Difficulty.Medium => group.Medium,
            Difficulty.Hard => group.Hard,
            _ => null
        };

        if (list == null || list.Count == 0)
            return null;

        return list[Random.Range(0, list.Count)];
    }

}
