The following FormationTypes are available:

# Base Formation
FormationTypeIdentifier: ""

Has no additional information or functionality

# RowsFormation
FormationTypeIdentifier: "RowsFormation"

Represents a formation consisting of multiple rows with the same amount of positions, spreaded evenly with the specified depth and sidemargin.

Parameters:

| Name | Type | Description |
|-|-|-|
| Rows | int | Number of rows in the formation |
| Columns | int | Number of columns (positions per row) in the formation |
| Depth | double | Distance between two rows |
| SideMargin | double | Distance between two columns |

The positions are indexed in the following order: Each row is indexed from the left to the right, from the first row to the end. Position 0 is therefore the leftmost position in the firstrow. 
Example formation with 5 rows and 3 columns looking to the right:

| 12 | 09 | 6 | 3 | 0 | <br/>
| 13 | 10 | 7 | 4 | 1 | --> <br/>
| 14 | 11 | 8 | 5 | 2 | <br/>
