Feature: NewPageAccessed

A short summary of the feature

@regression
Scenario: I access a different page
	Given I access the main Wikipedia page
	When I tap on the first article
	Then I get to a different Wikipedia page
