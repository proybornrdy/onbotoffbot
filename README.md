OBJECT STRUCTURE:
------------------------------------------------------------


INTERACTABLE OBJECTS:
base class Toggleable has TurnOn() and TurnOff(), OnButton and OffButton take any Toggleable object as a child.

object OnBlock calls TurnOn()
object OffBlock calls TurnOff()

To derive from Toggleable:

public class <ClassName> : Toggleable
{
    public override void TurnOn()
    {
        ...
    }

    public override void TurnOff()
    {
       ...
    }
}

Why is this forcing this to be a header
------------------------------------------------------------
