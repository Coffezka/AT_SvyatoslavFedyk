Feature: Test site swapi.dev
	
Scenario: Make HTTPRequest to site
	Given I have url https://swapi.dev/
	Given I send GET Request to endponit "api/vehicles/"
	When the request is succesfull

Scenario: Save HTTPRequest in file and validate
	Given I have url https://swapi.dev/
	Given I send GET Request to endponit "api/vehicles/"
	When save response in file
	Given I send GET Request to endponit "api/vehicles/"
	When Compare responce


Scenario: VehichlesApi scema test
	Given I have url https://swapi.dev/
	Given I send GET Request to endponit "api/vehicles/"
	When the request is succesfull
	Then compare actual response to expected scema

Scenario: Check car
	Given I have url https://swapi.dev/
	Given I send GET Request to endponit "api/vehicles/"
	Given Car "X-34 landspeeder" exists
	When the request is succesfull
