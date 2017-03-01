
using System;
using Windows.Devices.Gpio;

namespace SmartCar
{
    public enum enmWheel
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public enum Direction
    {
        None,
        Forward,
        Backward
    }
    class Wheel
    {
        private static GpioController controller = GpioController.GetDefault();

        private GpioPin _highPin;
        private GpioPin _lowPin;

        public enmWheel Name { get; set; }
        public float Speed { get; set; }

        public Wheel(enmWheel name, int highPin, int lowPin)
        {
            try
            {
                Name = name;

                _highPin = controller.OpenPin(highPin);
                _lowPin = controller.OpenPin(lowPin);

                _highPin.SetDriveMode(GpioPinDriveMode.Output);
                _lowPin.SetDriveMode(GpioPinDriveMode.Output);
            }
            catch (Exception e)
            {
                var x = e.ToString();
                throw;
            }
        }

        public void Trigger(Direction direction, float speed)
        {
            switch (direction)
            {
                case Direction.Forward:
                    _highPin.Write(GpioPinValue.High);
                    _lowPin.Write(GpioPinValue.Low);
                    break;
                case Direction.Backward:
                    _highPin.Write(GpioPinValue.Low);
                    _lowPin.Write(GpioPinValue.High);
                    break;
                case Direction.None:
                default:
                    _highPin.Write(GpioPinValue.Low);
                    _lowPin.Write(GpioPinValue.Low);
                    break;
            }
        }
    }
}
