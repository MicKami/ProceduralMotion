
# Procedural Motion

The ProceduralMotion class is a tool for creating smooth, responsive animations in Unity. It simulates a spring-mass-damper system, which allows you to apply natural-looking motion to any object based on 3 parameters:

-   `F`: the natural frequency of the system. Higher values will create faster, more responsive motion.
-   `Z`: the damping ratio of the system. Higher values will create more damped motion, which will settle faster but may appear less natural.
-   `R`: the initial response of the system. Negative values will create an effect of anticipation, where the object moves in the opposite direction first before accelerating towards target.
&nbsp;

![](https://i.imgur.com/gd8E4ZY.png)
## How to Use

The ProceduralMotion class has four methods for updating different types of values: `UpdateFloat`, `UpdateVector2`, `UpdateVector3`, and `UpdateQuaternion`.

Each of these methods takes three arguments:

-   `current`: the current value of the object you want to animate.
-   `target`: the value you want to animate the object towards.
-   `dt`: the amount of time that has passed since the last update. Typically just `Time.deltaTime`.

To use the class you can expose the reference to `ProceduralMotion` in the editor. It gives you an access to its variables and allows you to see the response graph in realtime. Then you just call the appropriate update method in your `Update` loop.

For example, to animate the position of a `Transform`, you might write something like this:

```csharp
public class Example : MonoBehaviour
{
    [SerializeField]
    ProceduralMotion motion;
    Transform target;

    void Update()
    {
        if(target)
        {        
            transform.position = motion.UpdateVector3(transform.position, target.position, Time.deltaTime);
        }
    }
}
```
## Examples


This projects includes few example scenes. Just download it and have fun tweaking the parameters.

Updating position: 
![](https://imgur.com/vsi8K4Y.gif)
&nbsp;
Updating rotation:  
![](https://imgur.com/4yzW8w4.gif)
&nbsp;
Updating scale:  
![](https://i.imgur.com/9M4PNGK.gif)
&nbsp;
Updating UI element:  
![](https://i.imgur.com/U0GHMjk.gif)
## Credits
Inspired by a [video](https://youtu.be/KPoeNZZ6H4s) tutorial by [@t3ssel8r](https://twitter.com/t3ssel8r).
