Feature: APITesting

A short summary of the feature

@tag1
Scenario: Basic get
	When I send a get request to https://api.spacexdata.com/v3/capsules
	Then the response contains 200
	
@tag1
Scenario: Basic post
	When I send a post request to https://restful-booker.herokuapp.com/auth
	Then the response contains token
	And the token is 15 characters long