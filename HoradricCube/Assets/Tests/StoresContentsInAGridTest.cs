using UnityEngine;
using System.Collections;
using Require;
using Shouldly;

public class StoresContentsInAGridTest : TestBehaviour
{
    StoresContentsInAGrid it;
    bool success;
    Vector3 addedAt;

    public override void Spec()
    {
        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 1 by 1 item")
            .Then("it should be added at 0.5 0.5")
            .Because("it should start putting new items in the top left corner");

        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 2 by 2 item")
            .Then("it should be added at 1.0 1.0")
            .Because("items should stay inside the bounds of the inventory");

        Given("it stores its contents in a 4 by 4 grid")
            .And("there is a 2 by 2 item at 1.0 1.0")
            .When("you try to add a 1 by 2 item")
            .Then("it should be added at 2.5 1.0")
            .Because("existing items take up space");

        Given("it stores its contents in a 4 by 4 grid")
            .And("there is a 4 by 4 item at 2.0 2.0")
            .When("you try to add a 1 by 1 item")
            .Then("it should not fit")
            .Because("items cannot be placed in a full inventory");

        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 1 by 1 item at 1.5 0.5")
            .Then("it should be added at 1.5 0.5")
            .Because("items should be added where explicitly specified");

        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 1 by 1 item at 1.75 0.25")
            .Then("it should be added at 1.5 0.5")
            .Because("items should snap to the grid if placed on uneven boundaries");

        Given("it stores its contents in a 4 by 4 grid")
            .And("there is a 2 by 2 item at 1.0 1.0")
            .When("you try to add a 1 by 1 item at 0.5 0.5")
            .Then("it should not fit")
            .Because("you cannot place an item over another");

        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 1 by 2 item at 2.5 2.5")
            .Then("it should be added at 0.5 1.0")
            .Because("items before the bounds snap to inside of the bounds");

        Given("it stores its contents in a 4 by 4 grid")
            .When("you try to add a 1 by 2 item at 2.5 2.5")
            .Then("it should be added at 3.5 3.0")
            .Because("items past the bounds snap to inside of the bounds");

        Given("it stores its contents in a 2 by 2 grid")
            .And("there is a 1 by 1 item at 0.5 0.5")
            .And("there is a 1 by 1 item at 1.5 0.5")
            .When("you try to add a 1 by 2 item")
            .Then("it should not fit")
            .Because("items cannot be placed if no empty space fits their shape");
    }

    public void ItStoresItsContentsInA__By__Grid(int columns, int rows)
    {
        it = transform.Require<StoresContentsInAGrid>();
        it.size = new Vector2(columns, rows);
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
        addedAt = item.localPosition;
        Destroy(item.gameObject);
    }

    public void YouTryToAddA__By__ItemAt____(int width, int height, float x, float y)
    {
        Transform item = new GameObject().transform;
        item.Require<BoxCollider>();
        item.localScale = new Vector3(width, height, 1.0f);
        success = it.TryToAddAt(item, new Vector2(x, y));
        addedAt = item.localPosition;
        Destroy(item.gameObject);
    }

    public void ItShouldBeAddedAt____(float x, float y)
    {
        addedAt.ShouldBe(new Vector3(x, y, 0));
    }

    public void ItShouldNotFit()
    {
        success.ShouldBe(false);
    }

    public void ItShouldFit()
    {
        success.ShouldBe(true);
    }
}
