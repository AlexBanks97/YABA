# 💸 &nbsp;&nbsp; Y A B A &nbsp;&nbsp; [![Build Status](https://travis-ci.com/praffn/YABA.svg?token=ygVuHxbjsy4ghqmgtPST&branch=develop)](https://travis-ci.com/praffn/YABA)

## What is YABA?
Yet Another Budgeting App (YABA), is a tool that allows you to create budgets for yourself and track spendings of various expenses under different categories.
Furthermore YABA has a feature called *Tabs*, which allows two users to create a tab where they can track spendings with each other. 
YABA has been created as an exam product for the course [BDSA](https://mit.itu.dk/ucs/cb_www/course.sml?course_id=1921735&mode=search&lang=en&print_friendly_p=t&goto=1513816107.000), fall semester of 2017 at the IT University of Copenhagen.

## Database migrations
To run migrations make sure you are scoped to the `Yaba.Entities`.

* `dotnet ef database update` <small>(if using dotnet cli tools)</small>
* `Update-Database` <small>(using PowerShell)</small>

## Git Strategy:


### Requester
When implementing a feature, fixing a bug or anything else:

* Update develop branch to latest
* Create a local branch (namespace it with your initials, e.g. `phin-impl-linked-list`)
* Do your edits
  * Try to group commits in meaningful chunks, it makes reviewing easier
  * And try to make a lot of commits, not just one huge one
* Push your local branch to remote, e.g. `git push -u origin phin-impl-linked-list`
  * The `-u` flag sets up remote tracking on your branch. Super smart if reviewer requests changes
* If ready, create a pull request to merge your branch into develop
* Assign someone to review it

### Reviewer
* Pull the branch and make sure all tests pass
  * (Eventually @phin will set up CI to do this)
* If tests fail, figure out why and request changes to the pr
* Otherwise, quickly skim through changed files. If anything is out of the ordinary, look through the code.
* If everything looks good, merge the branch
  * Squash commit!
* Delete the branch from remote
