Feature: Login

Background: 
	Given I navigate to the app

@regression
Scenario Outline: Test a generic login operation
	When I enter the following username: <Username> and password: <Password>
	Then I get to examplecom

	Examples: 
	| Username  | Password     |
	| admintest | passwordtest |
	| user      | password     |

@regression
Scenario: Test a specific login operation
	When I enter the following username: admin and password: admin
	Then I get to examplecom