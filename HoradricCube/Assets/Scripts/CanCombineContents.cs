using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

[Serializable]
public class Recipe
{
    public List<string> ingredients;
    public Transform result;

    public Recipe(List<string> ingredients, Transform result)
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
    }

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
