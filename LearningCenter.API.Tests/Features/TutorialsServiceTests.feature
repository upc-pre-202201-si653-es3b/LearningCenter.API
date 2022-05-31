Feature: TutorialsServiceTests
As a Developer
I want to add new Tutorial through API
In order to make it available for applications.

    Background:
        Given the Endpoint https://localhost:5001/api/v1/tutorials is available

    @tutorial-adding
    Scenario: Add Tutorial with unique Name
        When a Post Request is sent
          | Title   | Description       | CategoryId |
          | Sample | A Sample Tutorial | 100        |
        Then A Response with Status 200 is received
        And a Tutorial Resource is included in Response Body
          | Id | Title   | Description       | CategoryId |
          | 1  | Sample | A Sample Tutorial | 100        |

    @tutorial-adding
    Scenario: Add Tutorial with existing Name
        Given A Tutorial is already stored
          | Id | Title  | Description       | CategoryId |
          | 1  | Sample | A Sample Tutorial | 100        |
        When a Post Request is sent
          | Title   | Description           | CategoryId |
          | Sample | The Ultimate Tutorial | 100        |
        Then A Response with Status 400 is received
        And An Error Message with value "Tutorial Name already exists." is returned