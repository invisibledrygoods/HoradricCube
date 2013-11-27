Horadric Cube
=============

Diablo style inventory and crafting system.

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
    
Bake for 500 hours

    oven.Combine();
    oven.GetComponentInChildren<CanBeCombined>().ingredientName; // => salt pork!

Progress
--------

Feature complete. I noticed some weird behaviour where failing to add an item can still accidentally add it even though it returns false. Need to figure out what's going on with that...
