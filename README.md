# Doorway Effect
This is a project created in Unity 2020.3.30f1 together with the Oculus Quest 2 to investigate the Doorway Effect in Virtual Reality using different locomotion techniques.


## What is the doorway effect?
The “doorway effect” predicts that it is more difficult to remember recent objects and tasks after crossing a salient environmental boundary. Many people will have experienced this in real life, by walking into another room and suddenly having forgotten what they were supposed to do there. The effect has been demonstrated by using virtual environments on a computer with a variety of experimental settings, as well as in real life. However, for immersive Virtual Reality (VR) the effect is still unclear. Furthermore, many popular VR locomotion techniques - such as teleportation - can bypass typical environmental boundaries altogether. In this project we investigate the effect of using teleportation and real walking on object recognition performance. The results confirm that the presence of a door makes it more difficult to remember objects, regardless of the locomotion technique used. Furthermore, the results suggests that visual effects, like the teleportation animation, do not trigger the effect.
You can read our paper about the doorway effect here (insert link for Doorway Paper)

## Project Description
The way this application works is that it generates large or small rooms in 3D space, with some randomly placed furniture in them and six random colored 3D shapes placed on a virtual table that the user must try to remember. The rooms that we generate have the same dimensions as the actual room we used for the user to physically walk inside. The goal is to try to remember all of these colored shapes and answer accurately some questions about them while walking or teleporting between rooms. The player must use the Oculus Quest 2 controllers to grab the shapes, put them inside a virtual box and then physically walk to another virtual table that is on the other side of the same room or in a different room. The participant must complete 40 randomly generated memory trials while we collect the data we need.

### Setup Project
Setting up this project is fairly simple. After the application starts we have to press the right oculus joystick, enter a participant ID and then choose between two different locomotion techniques, Walking or Teleportation.

## Videos


<video src = "https://github.com/IoannisKalai/DoorwayEffect/assets/25771385/d8480dd0-7b50-45ce-98c6-5e2f1666bdae" controls width="300">kala</video>



https://github.com/IoannisKalai/DoorwayEffect/assets/25771385/c1b5bd64-042d-47e7-b8ca-d2c133fa829d

