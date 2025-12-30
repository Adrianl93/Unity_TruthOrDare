using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ChallengeLoader
{
    public static ChallengeCategoryData LoadCategory(string categoryName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Challenges/{categoryName}");

        if (json == null)
        {
            Debug.LogError($"No se encontró el JSON de la categoría: {categoryName}");
            return null;
        }

        return JsonUtility.FromJson<ChallengeCategoryData>(json.text);
    }
}
