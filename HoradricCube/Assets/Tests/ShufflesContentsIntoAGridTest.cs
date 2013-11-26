using UnityEngine;
using System.Collections;
using System;
using Require;
using Shouldly;

public class ShufflesContentsIntoAGridTest : TestBehaviour
{
    ShufflesContentsIntoAGrid it;
    bool success = false;

    public override void Spec()
    {
        Given("it shuffles its contents into a 2 by 2 grid")
            .And("there is a 1 by 1 item at 0.5 0.5")
            .And("there is a 1 by 1 item at 1.5 0.5")
            .When("you try to add a 1 by 2 item")
            .Then("it should fit")
            .Because("items should rearrange themselves to create space");

        Given("it shuffles its contents into a 2 by 2 grid")
            .And("there is a 2 by 1 item at 1.0 0.5")
            .When("you try to add a 1 by 2 item")
            .Then("it should not fit")
            .Because("items cannot be added if there is no room even with shuffling");

        Given("it shuffles its contents into a 4 by 4 grid")
            .And("there is a 2 by 2 item at 1.0 1.0")
            .And("there is a 2 by 2 item at 3.0 1.0")
            .When("you try to add a 1 by 3 item")
            .Then("it should fit")
            .Because("it should be able to solve difficult knapsack problems");

        Given("it shuffles its contents into a 2 by 2 grid")
            .And("there is a 1 by 2 item at 0.5 1.0")
            .And("there is a 1 by 1 item at 1.5 0.5")
            .When("you try to add a 2 by 1 item")
            .Then("it should contain a 1 by 2 item")
            .And("it should contain a 1 by 1 item")
            .Because("failing to add an item should not affect the items already in the inventory");
    }

    public void ItShufflesItsContentsIntoA__By__Grid(int columns, int rows)
    {
        transform.Require<StoresContentsInAGrid>().size = new Vector2(columns, rows);
        it = transform.Require<ShufflesContentsIntoAGrid>();
    }

    public void ThereIsA__By__ItemAt____(int width, int height, float x, float y)
    {
        Transform item = new GameObject().transform;
        item.Require<BoxCollider>();
        item.parent = transform;
        item.localScale = new Vector3(width, height, 1.0f);
        item.localPosition = new Vector3(x, y, 0.0f);
    }

    public void YouTryToAddA__By__Item(int width, int height)
    {
        Transform item = new GameObject().transform;
        item.Require<BoxCollider>();
        item.localScale = new Vector3(width, height, 1.0f);
        success = it.TryToAdd(item);
        Destroy(item.gameObject);
    }

    public void ItShouldNotFit()
    {
        success.ShouldBe(false);
    }

    public void ItShouldFit()
    {
        success.ShouldBe(true);
    }

    public void ItShouldContainA__By__Item(int width, int height)
    {
        string children = "";
        foreach (Transform child in transform)
        {
            children += (Vector2)child.collider.bounds.size + " ";

            if ((Vector2)child.collider.bounds.size == new Vector2(width, height))
            {
                return;
            }
        }

        throw new Exception("inventory should contain a " + width + " by " + height + " item but only contained " + children);
    }
}
