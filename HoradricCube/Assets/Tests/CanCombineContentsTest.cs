using UnityEngine;
using System.Collections.Generic;
using System;
using Require;
using Shouldly;

public class CanCombineContentsTest : TestBehaviour
{
    CanCombineContents it;

    public override void Spec()
    {
        Given("it can combine its contents")
            .And("it has a recipe for salt pork")
            .And("it contains 'salt'")
            .And("it contains 'pork'")
            .When("its contents are combined")
            .Then("it should contain 'salt pork'")
            .Because("it should follow recipes");

        Given("it can combine its contents")
            .And("it has a recipe for salt pork")
            .And("it contains 'salt'")
            .And("it contains 'pork'")
            .When("its contents are combined")
            .Then("it should not contain 'salt'")
            .And("it should not contain 'pork'")
            .Because("the original contents should be removed after combining");

        Given("it can combine its contents")
            .And("it contains 'salt'")
            .And("it contains 'pork'")
            .When("its contents are combined")
            .Then("it should contain 'salt'")
            .And("it should contain 'pork'")
            .And("it should not contain 'salt pork'")
            .Because("it should not combine items if there is no recipe");

        Given("it can combine its contents")
            .And("it has a recipe for salt pork")
            .And("it contains 'salt'")
            .And("it contains 'pork'")
            .And("it contains 'poison'")
            .When("its contents are combined")
            .Then("it should contain 'salt'")
            .And("it should contain 'pork'")
            .And("it should contain 'poison'")
            .And("it should not contain 'salt pork'")
            .Because("recipes should fail if there are extra ingredients");

        Given("it can combine its contents")
            .And("it has a recipe for salt pork")
            .And("it contains 'salt'")
            .When("its contents are combined")
            .Then("it should contain 'salt'")
            .And("it should not contain 'salt pork'")
            .Because("recipes should fail if there are too few ingredients");
    }

    public void ItCanCombineItsContents()
    {
        it = transform.Require<CanCombineContents>();
    }

    public void ItHasARecipeForSaltPork()
    {
        GameObject saltPork = new GameObject();
        saltPork.transform.Require<CanBeCombined>().ingredientName = "salt pork";

        List<string> ingredients = new List<string>();
        ingredients.Add("salt");
        ingredients.Add("pork");

        it.recipes.Add(new Recipe(ingredients, saltPork.transform));
    }

    public void ItContains__(string ingredient)
    {
        GameObject obj = new GameObject();
        obj.transform.Require<CanBeCombined>().ingredientName = ingredient;
        obj.transform.parent = transform;
    }

    public void ItsContentsAreCombined()
    {
        it.Combine();
    }

    public void ItShouldContain__(string ingredient)
    {
        List<string> ingredients = new List<string>();

        foreach (CanBeCombined item in GetComponentsInChildren<CanBeCombined>())
        {
            ingredients.Add(item.ingredientName);

            if (item.ingredientName == ingredient)
            {
                return;
            }
        }

        throw new Exception("inventory should contain " + ingredient + " but contained " + String.Join(", ", ingredients.ToArray()));
    }

    public void ItShouldNotContain__(string ingredient)
    {
        foreach (CanBeCombined item in GetComponentsInChildren<CanBeCombined>())
        {
            item.ingredientName.ShouldNotBe(ingredient);
        }
    }
}
