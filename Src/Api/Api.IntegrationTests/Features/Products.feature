Feature: Products
	In order to ensure all product information can be retrieved correctly I want to make sure the API works as expected
	The product information is stored in a local .json file and is read and parsed when the api is called

	To verify the availability of a product a third party system is called.
	To make sure product information is always available downtime on the availability system cannot disrupt product information.

Background: 
	Given the following product information is available
		| ProductId | Name         | ImageId     |
		| 1XS5      | Wooden plank | plank.png   |
		| 556X      | Steel plank  | splank.png  |
		| A2        | X12 Screws   | screws.png  |
		| T42       | Heavy tiles  | h_tiles.png |
		| T21       | Light tiles  | l_tiles.png |
	# This information is returned by the third party system
	And the following item count is available
		| ProductId | Count |
		| 1XS5      | 100   |
		| 556X      | 2     |
		| A2        | 10    |
		| T42       | 76    |
		| T21       | 901   |

@products
Scenario: Requesting products from the API
	When I request products
	Then the following products are returned
		| ProductId | Name         | ImageId     | Count |
		| 1XS5      | Wooden plank | plank.png   | 100   |
		| 556X      | Steel plank  | splank.png  | 2     |
		| A2        | X12 Screws   | screws.png  | 10    |
		| T42       | Heavy tiles  | h_tiles.png | 76    |
		| T21       | Light tiles  | l_tiles.png | 901   |

@products @paged
Scenario: Requesting a specific amount of products
To make sure not an abnormal amount of data is returned from the API I want to get data in pages
	When I request '2' products
	Then the following products are returned
		| ProductId | Name         | ImageId     | Count |
		| 1XS5      | Wooden plank | plank.png   | 100   |
		| 556X      | Steel plank  | splank.png  | 2     |

@products @paged
Scenario: Requesting a specific amount of products from a second page
To make sure not an abnormal amount of data is returned from the API I want to get data in pages
	When I request '2' products from page '3'
	Then the following products are returned
		| ProductId | Name         | ImageId     | Count |
		| T21       | Light tiles  | l_tiles.png | 901   |

@products
Scenario: Items are still returned when the availibilty system is offline
To ensure we are always able to get product information the downtime of the third party system should not affect this API
	Given the availibility system is not available
	When I request products
	# This HTTP code 206 to let our clients know some data is missing
	Then the product api returned the 'Partial Content' response
	And the following products are returned
		| ProductId | Name         | ImageId     | Count |
		| 1XS5      | Wooden plank | plank.png   | -     |
		| 556X      | Steel plank  | splank.png  | -     |
		| A2        | X12 Screws   | screws.png  | -     |
		| T42       | Heavy tiles  | h_tiles.png | -     |
		| T21       | Light tiles  | l_tiles.png | -     |
