LittleHelper
====

This extension helps out people who suffer from Unity's poor UX.<br>
It mostly does not add particular features, it just make Unity editor to act more naturally.

* Unity 2019 is strongly recommended

Config
----
![CONFIG](img/config.png)

You can turn on/off each features in config window.

UI
----
### Distance Ruler
![img](img/ruler.png)

Measuring ditance between more than 2 objects by selecting. This is useful when you are working with an UI guidelines (such as zepplin).

### Object Size
![img](img/sizeview.png)

Unity does not show up object's size in fill mode(RectTransform). So I made it.

### Quick Select
![gif](gif/quickselect.gif)

Press `Alt + 1` to select all texts with same __font__.<br>
Press `Alt + 2` to select all texts with same __size__.

### Remember last property for Text
![gif](gif/textdata.gif)

Nobody wants to use `Arial` and ugly gray color as a default text property.

### AutoSize for Text
![gif](gif/autofit.gif)

### Unit(1px) movement with Keyboard
![gif](gif/ui_arrow.gif)

A tiny joy for UI designers(devs).

### Keep inspector on empty selection
![gif](gif/keep_selection.gif)

Editor
----
### Restore inspector on Drag
Most desired feature for me, and also the reason why I made this project. <br>
When you clicked the asset to drag, inspector window will be changed to the asset's config. This made me crazy all the time.

![gif](gif/drag.gif)

### Move to Top, Move to Bottom
![gif](gif/move_to_top.gif)

You don't need to press `Move Up/Down` button multiple times!

### Stick to Ground
![gif](gif/sticktoground.gif)

Press `SpaceBar` to place n floating object on nearest ground.


Scripting
----

### Script template
Create a `template.tcs` file to override Untiy's default template. This can be applied to each directory.
