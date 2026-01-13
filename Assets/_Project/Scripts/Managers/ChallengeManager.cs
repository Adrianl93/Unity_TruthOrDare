using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance { get; private set; }

    private ChallengeCategoryData currentCategory;
    public bool IsInitialized { get; private set; }

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

    // ---------------------------------
    // INITIALIZATION (FROM SESSION)
    // ---------------------------------

    public void InitializeFromSession()
    {
        ResetManager();

        var session = GameSession.Instance;

        if (session == null || session.Data == null)
        {
            Debug.LogError("ChallengeManager: GameSession no inicializada");
            return;
        }

        var category = session.Data.SelectedCategory;

        currentCategory = LoadCategory(category);

        if (currentCategory == null)
        {
            Debug.LogError($"No se pudo cargar la categoría: {category}");
            return;
        }

        IsInitialized = true;
        Debug.Log($"Categoría cargada correctamente: {currentCategory.category}");
    }

    // ---------------------------------
    // CHALLENGE LOADING
    // ---------------------------------

    private ChallengeCategoryData LoadCategory(GameCategory category)
    {
        TextAsset json = Resources.Load<TextAsset>($"Categories/{category}");

        if (json == null)
        {
            Debug.LogError($"ChallengeManager: No se encontró Categories/{category}.json");
            return null;
        }

        return JsonUtility.FromJson<ChallengeCategoryData>(json.text);
    }

    // ---------------------------------
    // GAMEPLAY API
    // ---------------------------------

    public ChallengeCard GetRandomChallenge(WheelType type, Difficulty difficulty)
    {
        if (!IsInitialized || currentCategory == null)
        {
            Debug.LogError("ChallengeManager: categoría no inicializada");
            return null;
        }

        bool isTruth = type == WheelType.Truth;
        var group = isTruth ? currentCategory.Truth : currentCategory.Dare;

        if (group == null)
            return null;

        var list = difficulty switch
        {
            Difficulty.VeryEasy => group.VeryEasy,
            Difficulty.Easy => group.Easy,
            Difficulty.Medium => group.Medium,
            Difficulty.Hard => group.Hard,
            _ => null
        };

        if (list == null || list.Count == 0)
        {
            Debug.LogWarning($"No hay desafíos para {type} - {difficulty}");
            return null;
        }

        return list[Random.Range(0, list.Count)];
    }

    // ---------------------------------
    // RESET
    // ---------------------------------

    public void ResetManager()
    {
        currentCategory = null;
        IsInitialized = false;

        Debug.Log("ChallengeManager: reset completo");
    }
}
