# JSON schema for marching band shows
This core of this project is the JSON schema show.schema.json. The described JSON structure allows to describe a show program for marching bands. 
The goal is to provide a data structure that on one hand does not get too complicated for small and simple shows and on the other hand does not limit the ideas of all the creative minds out there creating shows. Everything should be possible!

# Project structure
The schema file show.schema.json lies in the root directory.
The sample C# implementation for a show simulation program contains the following projects:
  - ShowEditor.Data: This project contains the data structure for the show and can parse and export shows
  - ShowEditor.Simulator: In this project there is a ShowSimulator class and multiple templates for creating shows programmatically 
  - ShowEditor.WinFormsPlayer: This Windows Forms application uses the Simulator and just draws the positions after each step
