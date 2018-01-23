Feature: Products
	To ensure data is displayed correctly I want to verify all information is displayed

Background: 
	Given the following product information is available
		| ProductId | Name         | ImageId     | Count |
		| 1XS5      | Wooden plank | plank.png   | 100   |
		| 556X      | Steel plank  | splank.png  | 2     |
		| A2        | X12 Screws   | screws.png  | 10    |
		| T42       | Heavy tiles  | h_tiles.png | 76    |
		| T21       | Light tiles  | l_tiles.png | 901   |

@products
Scenario: Display all products
	When I visit the homepage
	Then I see the following products
		| ProductId | Name         | ImageId     | Count |
		| 1XS5      | Wooden plank | plank.png   | 100   |
		| 556X      | Steel plank  | splank.png  | 2     |
		| A2        | X12 Screws   | screws.png  | 10    |
		| T42       | Heavy tiles  | h_tiles.png | 76    |
		| T21       | Light tiles  | l_tiles.png | 901   |

@products
Scenario: Display products with missing images
	Given the following product information is available
		| ProductId | Name       | ImageId                | Count |
		| BB1       | Beach ball | non_existing_image.png | 2     |
	When I visit the homepage
		| ProductId | Name       | ImageText          | Count |
		| BB1       | Beach ball | No image available | 2     |