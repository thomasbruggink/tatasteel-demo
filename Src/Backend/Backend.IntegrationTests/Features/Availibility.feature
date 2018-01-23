Feature: Availibility
	To ensure the service gives responds in the correct way I want to ensure different calls are processed correctly

	When the productId received is unknown the service should return -1

Background: 
	Given the following product information is available
		| ProductId | Count |
		| 1         | 5     |
		| A478C     | 100   |

@availibility
Scenario Outline: Get the product count
	When I request the product with id '<productId>'
	Then the received product count is '<response>'

	Examples:
	# The productId - is send as an empty string
		| productId    | response |
		| 1            | 5        |
		| A478C        | 100      |
		| DOESNOTEXIST | -1       |
		| -            | -1       |
