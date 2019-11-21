using Annex.Scenes;

namespace Annex.Graphics.Events
{
    public class JoystickEvent : HardwareEvent
    {
        public uint JoystickID;
    }

    public class JoystickMovedEvent : JoystickEvent
    {
        public JoystickAxis Axis;
        public float Delta;
    }

    public class JoystickButtonPressedEvent : JoystickEvent
    {
        public JoystickButton Button;
    }

    public class JoystickButtonReleasedEvent : JoystickEvent
    {
        public JoystickButton Button;
    }

    public class JoystickConnectedEvent : JoystickEvent
    {
        
    }
   
    public class JoystickDisconnectedEvent : JoystickEvent
    {

    }
}
