# The Wex Assessment Solution

This solution consist of several projects that make up the Wex assessment project. The solution utilized the clean architecture. Other design principles are implemented, such as SOLID principles as well.

The list of projects included in this solution are as follows;

    ## DuendeIdentityServer
    This project provides the identity information for security of api's (run independent to other projects)


    ## WebAssessmentApi
    This project is the main web project. It contain all the code for the api's

    ## Application
    This project is a dependency of WebAssessmentApi. It is empty at this time but is there for future development

    ## Persistence
    This project is a dependency of Application. It contains interfaces and repository classes that will be used throughout the solution

    ##Infrustructure
    This project is a dependency of Persistence. It is empty at this time but is there for future development

    ## Domain
    This project is a dependency of Infrustructure. It contain all entity classes that represent data.

For process guidance, follow these steps:

1. Download the solution
2. Install the solution
3. Execute the "Process" button (answer a few questions)
4. The solution will start the DuendeIdentityServer project first and then the WexAssessmentApi project

A swagger page will display that shows all the api's. The Duende identity server will display in the background.
Use the regiser api to obtain a bearer token. This will be needed for api's with security on them.
The thought here is, get your bearer token for any of the api's that have the [Authorize] attribute.

## SOLID design principles utilized throughout the code.

# Single Responsibility

    You will note throughout the solution that projects and methods have one responsibiliy and thats it. Even projects have one purpose. That is why there is an Identity server project and an assessment project.

# Open/Closed

    The modules created in such a way that if we need to do something different, we would just extend the class and not need to be modified.

# Liskov's Principle

    By utilizing interfaces and having my classes inherit from those interfaces we are utilizing this principle.

# Interface Segregation

    This principle is utilized by creating the IEntity interface in the Domain.Primitives folder. This is as bare as an interface can get.

# Dependency Inversion

    This principle is used when it comes to the logger that we are using. Also, the in the use of the various services.
