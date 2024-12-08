Instructions:
- Open Solution in VS2022 >= Version 17.12
  Solution uses .NET9 release on 22.Nov 2024

- Rebuild both Projects in the Solution
- Set "FileGeneratorForMainApp" as Startup Project and run it !!

- If there were no issues, "AdventOfCodeNet9"-Project should have reloaded
  On some of my tests the "2020"-Folder looked a bit weird, just restart VS2022 
- Rebuild both Projects in the Solution

- Set "AdventOfCodeNet9" as Startup Project !!

- Press some random buttons to see if you get "0" everywhere.
- You can now delete the FileGeneratorForMainApp-Project from the solution, 
  we'll never need it again.

- foreach Part of each Day in Every-Year of AOC
  you can write your C#-code in a dedicated class.
- Insert "Input" and "Test-Example-Input" for this day and Rebuild
- Pressing the corresponding button on the MainForm, 
  the Day-Part will be executed with both of them,
  measuring the time only for the "real Input"

Best Regards
Collin

PS: Try compressing the folder again and see how it compares to the initial ".zip" :-)
