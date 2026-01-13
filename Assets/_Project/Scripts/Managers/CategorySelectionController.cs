using UnityEngine;
using UnityEngine.SceneManagement;

public class CategorySelectionController : MonoBehaviour
{
    public void SelectNormal()
    {
        SelectCategory(GameCategory.Normal);
    }

    public void SelectParty()
    {
        SelectCategory(GameCategory.Party);
    }

    public void SelectSpicy()
    {
        SelectCategory(GameCategory.Spicy);
    }

    public void SelectExtreme()
    {
        SelectCategory(GameCategory.Extreme);
    }

    private void SelectCategory(GameCategory category)
    {
        //GameSetupController.Instance.SetCategory(category);

        //Debug.Log($"Categoría seleccionada: {category}");
        GameSession.Instance.Data.SelectedCategory = category;

        Debug.Log($"Categoría seleccionada: {category}");

        SceneManager.LoadScene("PlayerSetup");
    }
}
