Extended Inspector

Thank you for purchasing Extended Inspector. It is my hope that this piece of software
will save you time and effort now and in the future.


##Using Extended Inspector##
Using Extended Inspector (EI) is pretty easy, once imported into your project a toolbar tab
called Editor should appear giving you the option of to run Extended Inspector or its Options.

Currently EI options include the ability to switch between 2 view modes, Drop Down or the classic
Title bar look. There is also the option to have the ability to switch between looking at fields,
having fields always showing or permanently hiding.

Using the actual Extended Inspector is easy. I recommend attaching it below the standard Unity
Inspector or completely replacing the standard inspector, but it will work anywhere. All options
are pretty straight forward and self-explained. All options should have full standard UNDO support
even for batch changes, so if you need to undo a change simply go to the toolbar under edit, and
hit undo or use your local keyboard shortcut.


##Current features##
There is a range of stuff this extension allows you to do.
Here is a list of various features:

-Cut, copy, paste components to multiple objects while retaining* values.
-Remove components from multiple scripts
-Batch tag, layer, static
-Global position, rotation.
-View actual scale of an object.
-Batch set values of any component, unity or user made, to all children, selection or both.
-Better prefab support, proper revert.
-Includes ability to break prefab connection.
-More value types supported like 'byte'.
-View uint. Can edit while playing.
-View dictionaries. Can edit while playing.
-View private fields. Can edit while playing.
-View auto properties. Can edit while playing.
-Modify any meshes pivot point
-Mass create arrays of gameobjects with the array tool, including modifying rotation and scale.

* Please note value retaining currently only works on fields not properties. This means it should work
  on user scripts but not built-in unity components (ie. collider)

Always regularly save work.


##Change Log##
v1.3
- Unity 3.5 support

v1.2
- Fixed bug that prevented name changing in hierarchy.
- Fixed a huge variety of minor bugs.
- Added math functions (you can now add, subtract, multiply or divide all transforms by a constant). Let's you shift a group of objects say 20 units left.
- Ability to change mesh pivot point to anywhere (not restricted to within bounds)
- Added array tool
- Added missing batch buttons from transform and rigidbody.
- Batch button order is now consistant

v1.1
- Fixed a major error when dictionaries weren't initialized.
- Added support to edit, add and remove from dictionaries. Please note changes won't be saved by unity, but is very useful for debugging.
