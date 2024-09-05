# GenForce-SW
Genforce-SW is a .NET-based desktop application designed to automate the extraction, calculation, and cost estimation of materials for solar panel projects. By leveraging custom parsing logic and dynamic data handling, the software streamlines the process of managing material quantities, providing users with accurate
cost predictions and minimizing manual errors.<br/>

## Key Features
* Automated Material Extraction: Automatically pulls material data for solar installations and organizes it for further processing.<br/>
* Cost Estimation Wizard: Integrated pricing wizard that calculates material costs based on predefined data, reducing calculation errors and improving efficiency.<br/>
* Custom Data Parsing: Implements custom parsing logic to aggregate material quantities and group them for more accurate cost estimation.<br/>

# TimeLine
## 7/29/2024
1st Table Format (INPUT):
* LETTER | SETS | WIRE SIZE & TYPE | METRIC | MATERIAL | MINIMUM CONDUIT | LENGTH |

Implemented add and delete row buttons and table frame for user. <br/> 
Next Implementations:
* Tool bar (Save, Download, Print, Open Folder/Upload)
* Wire Size & Type parse
* Calculation logic

## 8/1/2024
Implemented function to check each cell for the right format in the input of the user. <br/> 
Red highlight the cell with the error. <br/> 
Next Implementation:
* Tool bar
* Full Calculation
* Dark mode / Settings

## 8/7/2024
Finished first table logic and changed some of the UI colors.<br/>
Finished setting up all materials on second table.
Next Implementation:
* Merge with tool bar branch
* Second table logic
* Change GUI for better look

## 8/9/2024
Split the tables in 2 accessible tabs, Smoother transitions/functionality<br/>
Can now use save as to print/save
Next Implementation:
* Material correlation logic
* Pricing from data base
* More GUI features
  
## 8/12/2024
Tool Bars new now opens a new window, save will save the file, and delete row/add row work accordingly.<br/>
Drop Down menu and instant click acess instead of needing to double click<br/>
Completed error handling for app crashes (need unit tests to look for others)<br/>
Next Implementation:
* Data base check for pricing
* Animation for pressed button
* Remaining logic for material table

## 9/4/2024
Application has been finished, only need to finish merging.<br/>
Added new tab for results and for prices<br/>
* No more implementations for now
