# Contracts

The ```Contracts``` folder contains the data structures that our api uses to communicate with the outside world. 

> That means that any change in the items in this folder could potentially cause breaking changes and backwards compatibility issues!

The data structures contained should be as simple as possible and **should not** have any sort of logic. They exist only to act as protection against changes that might happen in the core of the application.

> Renaming the property of a domain class should not change the json schema response.

I was thinking of having the contracts be in the same folder as the controller they correspond to but in the end I opted to put all these items in a folder of their own for two reasons:

- In case we want to require extra permissions before merging into master.

    Depending on the PR system that we have setup, we could require some sort of extra permissions before changes to this folder were merged into master. Either from a Lead Developer, Technical Architect, etc... 
    
    More eyeballs should be looking at PRs affecting this!

- In case a breaking change has happened (and we were caught unaware!)

    Let's say that the worse has happened. We missed the breaking change lurking in a PR and now the people that use our api have issues with their applications.

    Having all these files in one directory allows us to filter the git commits to only those that have affected this directory with the following git command.

    ```
    git log -- src/Api/Contracts/*
    ```

    Depending on the amount of commits that could potentially cut down our search by quite a bit.
