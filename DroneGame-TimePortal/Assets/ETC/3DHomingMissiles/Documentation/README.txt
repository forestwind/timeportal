3D Homing Missiles Asset for Unity3D README
===========================================

Asset contents
==============
1. Proportional constant homing missile script for 3D missiles + supporting scripts
2. 3D Missile model, texture and prefab template
3. Smoke/fire particle effect trail for missiles
4. Demo scene to demonstrate dynamic instantiation of new missiles along with two different camera views, missile contact/explosions
5. Free explosion audio effect and basic explosion particle effect
6. Free "Starfield Space" Skybox bundled in (2048x2048px textured)
6. This readme file

Web demo: http://hobbyistcoder.com/demos/3DHomingMissilesDemo/3DHomingMissilesDemo.html

Introduction
============

Website: http://hobbyistcoder.com

The 3D Homing Missiles asset for Unity gives you realistically moving missiles to use in your games and prototypes along with smoke/fire particle trails. Missiles should never move directly towards their targets, this is not how they work in real life. Too many times we see simple missile follow scripts being used to allow missiles to track targets.

With the 3D Homing Missiles asset, a special proportional controller algorithm (PID controller) is used to move missiles intelligently, instructing missiles to reach their target, rather than just move directly to their targets. A desired state is constantly fed to the missile, as it's target moves, steering the missile in the right direction in order for it to reach it's target. This results in super realistic (and entertaining to watch) missile patterns. Fire single missiles off, or send out a swarm of missiles, and watch as they dynamically track down and realistically intercept their targets.

Usage
=====

In order to setup these missiles, simply drop a missile, or many missiles into your scene (using the included prefab), and drag a gameobject target onto the "target" property of the root missile gameobject. As soon as the missile has a target it will begin to intercept it.

Ideally you will want to instantiate or activate new missiles in your games whenever they are needed, at instantiation, you will simply give the MissileController3D.cs script a target reference to get them moving. A default maximum speed and propotional constant value are assigned to missiles to get them going. Tweak these to adjust missile performance - i.e. how 'swingy' or precise and fast they are.

There is a demo scene which allows you to click to add missiles at any position. They automatically target the central space platform which flies around randomly in the scene. Two sliders allow you to adjust missile performance to see how they react in flight. After adjusting the sliders, any new missiles will inherit the new properties.

Note: If a missile loses it's target reference, it will detonate immediately. There is also a boolean property on the MissileController3D.cs script that allows you to specify if a missile should destroy it's target's gameobject upon collision. By default this is disabled.

Explosions: A basic explosion particle effect that uses Unity's built in particle systems and particle assets is included. A GameObject property is setup on the MissileController3D.cs script to drag and drop an explosion prefab of your own to. Once this is done, if the missile is destroyed, it will automatically instantiate an explosion at it's detonation point, instead of the default effect included. I recommend you try out the Detonator framework (free explosion asset for Unity) - simply download the asset, import into your game, and drag and drop a detonator prefab onto the missile "explosion" property point. You may need to adjust the detonator explosion prefab size and properties to get an explosion of the right size and look for your missiles.

Missile fuel: A missile fuel property is included. To use this, your missiles should enable the "usesFuel" boolean flag. This is a public field which will be visible on your missile under the MissileController3D script. When this is enabled, every tick of the update loop, fuel will run out on each missile that has this enabled.
Upon reaching 0, the missile will detonate. The fuel settings are not enabled by default, so just check the flag on, or set the boolean flag to true in your launcher script to use this feature. Don't forget to set an amount of fuel the missile has to start (default is 4.0f).

Tweaking performance when using lots of missiles
================================================

To tweak performance of your final game, you should consider how many missiles you use on screen at any point in time. If the number is high, consider using an object pool pattern to activate missiles. You can also tweak the smoke trail renderer's maximum number of particles and rate to gain more performance at the cost of visual impact.
