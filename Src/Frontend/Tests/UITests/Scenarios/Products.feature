Feature: Products
	To ensure data is displayed correctly I want to verify all information is displayed

Background: 
	Given the following product information is available
		| ProductId | Name       | ImageId        | Count |
		| 1XS5      | Beach ball | beach-ball.png | 100   |
		| 556X      | Bear       | bear.png       | 2     |
		| A2        | Bike       | bike.png       | 10    |
		| T42       | Plank      | plank.png      | 76    |
		| T21       | Stunt Step | stuntstep.png  | 901   |

@products
Scenario: Display all products
	When I look at the 'home' page
	Then I see the following products
		| ProductId | Name       | ImageId        | Count |
		| 1XS5      | Beach ball | beach-ball.png | 100   |
		| 556X      | Bear       | bear.png       | 2     |
		| A2        | Bike       | bike.png       | 10    |
		| T42       | Plank      | plank.png      | 76    |
		| T21       | Stunt Step | stuntstep.png  | 901   |

@products
Scenario: Display products with missing images
	Given the following product information is available
		| ProductId | Name  | ImageId                | Count |
		| SCRW      | Screw | non_existing_image.png | 2     |
	When I look at the 'home' page
	Then I see the following products
		| ProductId | Name  | ImageText          | Count |
		| SCRW      | Screw | No image available | 2     |

@products
Scenario: Display no products when backend is unavailable
	Given the backend is unavailable
	When I look at the 'home' page
	Then I see the message 'No products found :('
