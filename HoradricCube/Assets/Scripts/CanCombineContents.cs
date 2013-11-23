using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

[Serializable]
public class Recipe
{
    public string[] ingredients;
    public Transform result;

    public Recipe(string[] ingredients, Transform result)
    {
        this.ingredients = ingredients;
        this.result = result;
    }
}

public class CanCombineContents : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();

    public void Combine()
    {
        List<string> contents = new List<string>();

        foreach (CanBeCombined content in GetComponentsInChildren<CanBeCombined>())
        {
            contents.Add(content.ingredientName);
        }

        foreach (Recipe recipe in recipes)
        {
            bool missingIngredient = false;
            List<string> accountedFor = new List<string>(contents);

            foreach (string ingredient in recipe.ingredients)
            {
                if (accountedFor.Contains(ingredient))
                {
                    accountedFor.Remove(ingredient);
                }
                else
                {
                    missingIngredient = true;
                    break;
                }
            }

            if (!missingIngredient && accountedFor.Count == 0)
            {
                foreach (CanBeCombined content in GetComponentsInChildren<CanBeCombined>())
                {
                    Destroy(content.gameObject);
                }

                (Instantiate(recipe.result, transform.position, Quaternion.identity) as Transform).parent = transform;
                return;
            }
        }
    }
}
