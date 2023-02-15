Feature: EAAppTest

@smoke
Scenario: Test login operation of EA App
	Given I navigate to the app
	When I enter the following login details
	| Username | Password |
	| admintest    | passwordtest |
	Then I get to examplecom
	
	