To create an empty Solution/Template to solve your puzzles,
you only need to download the 

	AdventOfCodeNet10_ShareWare.zip

Instructions after unziping the ShareWare:

- Open Solution in VS2026 Insiders >= Version 18 (27. Oktober 2025 - [11123.170])
  Solution uses .NET 10 C# 14 release on 11.Nov 2025

- Tools -> Options => Languages -> Default -> Tabs => 
  Tab size      = 2
  Indent size   = 2
  Tab character = Insert spaces 

- Rebuild both Projects in the Solution
- Set "FileGeneratorForAocNet10" as Startup Project and run it !!

- If there were no issues, "AdventOfCodeNet10"-Project should have reloaded
  On some of my tests the "2020"-Folder looked a bit weird, just restart VS2026 
- Rebuild both Projects in the Solution

- Set "AdventOfCodeNet10" as Startup Project !!
- Start "AdventOfCodeNet10"

- Press some random buttons on the GUI to see if you get "0" everywhere.
- You can now delete the FileGeneratorForAocNet10-Project from the solution, 
  we'll never need it again.

Info:
- foreach Part of each Day in Every-Year of AOC
  you can write your C#-code in a dedicated class.
- Insert "Input" and "Test-Example-Input" for this day and Rebuild
- Pressing the corresponding button on the MainForm, 
  the Day-Part will be executed with both of them 
  ("Input" and "Test-Example-Input").
  The Framework will only measure the time for the real "Input"
  
  The Result in the MessageBox is already copied to Clipboard.
  
  So when you're sure it's the result you want to try,
  you can click the link at the top of the class and paste your result.

Best Regards
Collin

PS: Try compressing the folder again and see how it compares to the initial ".zip" :-)
