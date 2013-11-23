Horadric Cube
=============

Work in progress on a Diablo style inventory and crafting system.
Allowing items that are more than one tile in size might not be a priority though.

Lets cook some salt pork together
---------------------------------

Buy an oven

    CanCombineContents oven = new GameObject().transform.Require<CanCombineContents>();

Add a recipe to your cook book

    Transform saltPorkPrefab = new GameObject().transform;
    saltPorkPrefab.Require<CanBeCombined>().ingredientName = "salt pork";
    oven.recipes.Add(new Recipe(new [] { "salt", "pork" }, saltPorkPrefab)); 
    
Add the ingredients to the oven

    Transform salt = new GameObject().transform;
    salt.Require<CanBeCombined>().ingredientName = "salt";
    salt.parent = oven;
    
    Transform pork = new GameObject().transform;
    pork.Require<CanBeCombined>().ingredientName = "pork";
    pork.parent = oven;
    
And bake for 500 hours

    oven.Combine();
    oven.GetComponentInChildren<CanBeCombined>().ingredientName; // => salt pork!
