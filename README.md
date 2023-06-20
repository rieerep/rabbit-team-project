# Team Rabbit Weather TDD API Project

## Overview
This is a group project where we've built a React client along with a minimal API with emphasis on test driven development and CI/CD pipelines. The purpose of this project is to learn and understand both CI/CD and TDD and their roles within modern software development.

## Contributors
- [Danilo](https://github.com/Danilo-Acosta5389)
- [Dariusz](https://github.com/T140K)
- [Nshoan](https://github.com/ChasAcademy-Nshoan-Abdlwafa)
- [Rickard](https://github.com/rieerep)

## Tech Stack
- C#
- JavaScript (React.js)
- Minimal API (ASP.NET Core)

## Structure
The project is structured from a TDD standpoint, where there is a main project and a unit testing project inside the same solution. Before adding new functionality to the main project test methods are created and tested inside the unit testing project.  Depending on the results of the tests we either continue development or keep running tests until we're satisfied with the results. We make sure to commit every result to the repository, whether it fails or succeeds. We also make sure that objects and methods are only created depending on the current needs.

As this is a project that uses an API we also make sure that the tests only use objects similar to the format of JSON because the API will always return a serialized JSON.

The other part of the project is the React client. We have used axios to make the API calls and styled components for styling. The front-end part of this project is not as important as the API and therefore not prioritized. 

## Pipeline
Our pipeline can be broken down into several stages:

1. We start off development by creating tests based on the requirements of our current backlog. The tests are very useful as they show us if something will work or not right away, saving us time. Once a test has passed the group then goes on to code the actual functionality that makes up the application.

2. If the code is considered functional and complete the code will then be pushed to a separate branch as our main branch has branch protection turned on. A pull request is then created waiting for a review and approval from another group member.

3. Once a pull request has been reviewed and approved the merge to main is usually done by the member that opened the pull request as they are able to verify that everything is correct. We try to do it this way just in case a member no longer wants to go through with their pull request for reasons such as wanting to make some additional changes first. This prevents unneccesary pull requests from going through which saves us some time.

4. If the group is 100% satisfied with the current code it is then deployed to the server. We usually only deploy larger chunks of code that make up important functions that has gone through several iterations and checks by the group instead of deploying several, smaller parts of code as to minimize the risk of errors and/or mistakes.
