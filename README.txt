#### DOORWAY EFFECT EXPERIMENT ####

### APPLICATION DESCRIPTION ###

This application was created to investigate the doorway effect in Virtual Reality while using different locomotion techniques.
For the implementation we used Unity version 2020.3.30f1 together with the Oculus Quest 2 and the latest Oculus Integration Package for Unity.
The way that this application works is that it generates large or small rooms in 3D space, with some randomly placed furniture in them and six random colored shapes placed on a virtual table
that the user must try to remember. The rooms that we generate have the same dimensions with the actual room we used for the user to physically walk inside.
The goal is to try to remember all of these colored shapes and answer accurately at some questions about them while walking or teleporting between rooms.
The player must use the Oculus Quest 2 controllers to grab the shapes, put them inside a virtual box and the physically walk to another table that is in the other side of the same room or in a different room.
Setting up this project is fairly simple. After starting the application we have to press the right oculus joystick, enter a participant ID and then choose between two different
locomotion techniques, Walking and Teleportation. After that, the participant must complete 40 randomly generated memory trials while we collect the data we need.


#### MAIN APPLICATION SCRIPTS ####

Most of the application functions are being handled from the CreateRandomObject script. This is the main script where we call the functions to generate the random rooms, the objects, the prompts
for each trial while we also write them in the CSV file.

The SLRoomSpawner script generates a sequence of 40 small/large rooms in a way that no more than 4 consecutive rooms appear.

The colored shapes are being generated from a list of 10 objects and 10 colors, in the CreateRandomgObject script each time the user drops the closed box on the correct table. 
Here we also want to have each one of the six objects being a different combination of color-shape for each trial.

In the QuestionsController script we take care of the prompt questions by creating associate question prompts (about objects that are actually inside the box),
and negative question prompts (about objects that are not in the box). Here we are also waiting for the user to answer the these questions pressing the "A" or "X" 
buttons of the Oculus Quest 2 controller.

The teleportation locomotion function is being implemented in the Teleport script. An important matter that we had to fix here was to try to combine user's physical 
walking with teleportation. As the user walks inside the room its virtual character capsule doesn't follow the movement but instead it stays still where is spawn for the first time.
This created a problem when we wanted the virtual character to teleport in the virtual room as there was a difference between the actual positions of the user and its virtual character.
To overcome this issue we had to update the capsule's position in every frame to follow the movement of the user.
