Feature: APITesting

A short summary of the feature

@tag1
Scenario: Basic get
	Given I send a get request to https://api.spacexdata.com/v3/capsules
	Then the response contains 200
	
