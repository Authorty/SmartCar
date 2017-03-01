using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace SmartCar
{
    class SmartCar
    {
        private GpioPin _PWMPin;

        public SmartCar()
        {
            //GpioController controller = GpioController.GetDefault();
            //_PWMPin = controller.OpenPin(6);
            //_PWMPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        //init four wheels
        private static List<Wheel> wheels = new List<Wheel>
        {
            //new Wheel(enmWheel.TopLeft, 11, 12),
            //new Wheel(enmWheel.TopRight, 13, 15),
            new Wheel(enmWheel.BottomLeft, 17, 18),
            new Wheel(enmWheel.BottomRight, 27, 22)
        };

        public bool FowardBackword(Direction driection)
        {
            try
            {
                foreach (Wheel wheel in wheels)
                {
                    wheel.Trigger(driection, 1);
                }
                //Wheel test = new Wheel(enmWheel.BottomRight, 27,22);
                //test.Trigger(driection, 1);

                return true;
            }
            catch (Exception e)
            {
                var x = e.ToString();
                throw;
            }
        }

        public void Stop()
        {
            wheels.ForEach(wheel => wheel.Trigger(Direction.None, 0));
        }

        public void TurnLeft()
        {
            TriggerWheel(enmWheel.TopLeft, Direction.None, 0);
            TriggerWheel(enmWheel.TopRight, Direction.Forward, 0);
            TriggerWheel(enmWheel.BottomLeft, Direction.None, 0);
            TriggerWheel(enmWheel.BottomRight, Direction.Forward, 0);
        }

        public void TurnRight()
        {
            TriggerWheel(enmWheel.TopLeft, Direction.Forward, 0);
            TriggerWheel(enmWheel.TopRight, Direction.None, 0);
            TriggerWheel(enmWheel.BottomLeft, Direction.Forward, 0);
            TriggerWheel(enmWheel.BottomRight, Direction.None, 0);
        }

        public void TurnBackwardRight()
        {
            TriggerWheel(enmWheel.TopLeft, Direction.Backward, 0);
            TriggerWheel(enmWheel.TopRight, Direction.None, 0);
            TriggerWheel(enmWheel.BottomLeft, Direction.Backward, 0);
            TriggerWheel(enmWheel.BottomRight, Direction.None, 0);
        }

        public void TurnBackwardLeft()
        {
            TriggerWheel(enmWheel.TopLeft, Direction.None, 0);
            TriggerWheel(enmWheel.TopRight, Direction.Backward, 0);
            TriggerWheel(enmWheel.BottomLeft, Direction.None, 0);
            TriggerWheel(enmWheel.BottomRight, Direction.Backward, 0);
        }

        public void TriggerWheel(enmWheel wheel, Direction direction, float speed)
        {
            wheels.First(itm => itm.Name == wheel).Trigger(direction, speed);
        }

        //simulate PWM
        public async void SpeedTest()
        {
            while (true)
            {
                _PWMPin.Write(GpioPinValue.Low);
                await Task.Delay(100);
                _PWMPin.Write(GpioPinValue.High);
                await Task.Delay(100);
            }
        }
    }
}
