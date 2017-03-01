using System;

using Windows.ApplicationModel.Background;
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Diagnostics;
using System.Xml.Linq;
using Windows.Devices.Gpio;
using Windows.Networking.Connectivity;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SmartCar
{
    public sealed class StartupTask : IBackgroundTask
    {
        private SmartCar _car;
        private  GpioPin _turnGpioA;
        private  GpioPin _turnGpiob;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //
            // Prevent from exit
              taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;
        //    StartListening();
       //     MoveForward();
            try
            {
                 // Testinginitialization();
                // MoveForward();

               // var controller = GpioController.GetDefault();
               // var test =

               // // bool testing = controller.TryOpenPin(3, GpioSharingMode.Exclusive, out _turnGpioA , out GpioOpenStatus.PinOpened);
               // _turnGpioA = controller.OpenPin(3);
               // _turnGpiob = controller.OpenPin(5);
               // _turnGpioA.Write(GpioPinValue.High);
               // _turnGpiob.Write(GpioPinValue.Low);
               // _turnGpioA.SetDriveMode(GpioPinDriveMode.Output);
               // _turnGpiob.SetDriveMode(GpioPinDriveMode.Output);
               //// _turnGpioA.SharingMode = GpioSharingMode.SharedReadOnly;

               // for (int i = 0; i < 500000; i++)
               // {
               //     _turnGpioA.Write(GpioPinValue.High);
               //     _turnGpiob.Write(GpioPinValue.Low);
                    
               // }


            
                

            }
            catch (Exception e)
            {
                var x = e.ToString();
            }
            //     private readonly GpioPin _motorGpioPinA;
            //private readonly GpioPin _motorGpioPinB;

            //public Motor(int gpioPinIn1, int gpioPinIn2)
            //{
            //    var gpio = GpioController.GetDefault();

            //    _motorGpioPinA = gpio.OpenPin(gpioPinIn1);
            //    _motorGpioPinB = gpio.OpenPin(gpioPinIn2);
            //    _motorGpioPinA.Write(GpioPinValue.Low);
            //    _motorGpioPinB.Write(GpioPinValue.Low);
            //    _motorGpioPinA.SetDriveMode(GpioPinDriveMode.Output);
            //    _motorGpioPinB.SetDriveMode(GpioPinDriveMode.Output);
            //}

           // Prevent from exit
         //  taskInstance.GetDeferral();
           // var result = await PreventPrematureClosing();
            //MainDrivingScreen screen = new MainDrivingScreen();
            //deferral.Complete();
            //screen.

        }

        //private async Task PreventPrematureClosing()
        //{
        //     return true;
        //}

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            Debug.Write(reason);
        }

        public void Testinginitialization()
        {
            Motor motor1 = new Motor(17,18);
            Motor motor2 = new Motor(27,22);
            TwoMotorsDriver driver = new TwoMotorsDriver(motor1, motor2);
            var x = 0;

            while (x <= 150500)
            {
                //driver.MoveForward();
                driver.Stop();
                driver.MoveBackward();
                x++;
            }



        }

        public async void MoveForward()
        {
            try
            {
                var x = 0;

                while (x <= 5000000)
                {
                    _car = new SmartCar();
                    _car.FowardBackword(Direction.Backward);
                    //                _car.SpeedTest();

                    x++;
                }
            }
            catch (Exception e)
            {

                var x = e.ToString();
            }

        }


        public async void StartListening()
        {
            foreach (HostName localHostInfo in NetworkInformation.GetHostNames())
            {
                if (localHostInfo.IPInformation != null)
                {
                    DatagramSocket socket = new DatagramSocket();
                    socket.MessageReceived += Sock_MessageReceived;

                    await socket.BindEndpointAsync(localHostInfo, "8888");
                }
            }
        }


        private void Sock_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            using (DataReader reader = args.GetDataReader())
            {
                string value = reader.ReadString(reader.UnconsumedBufferLength);
                switch (value.Trim())
                {
                    case "create":
                        _car = new SmartCar();
                        break;
                    case "forward":
                        if (_car != null)
                        {
                            _car.FowardBackword(Direction.Forward);
                        }
                        break;
                    case "backward":
                        if (_car != null)
                        {
                            _car.FowardBackword(Direction.Backward);
                        }
                        break;
                    case "turnright":
                        if (_car != null)
                        {
                            _car.TurnRight();
                        }
                        break;
                    case "turnleft":
                        if (_car != null)
                        {
                            _car.TurnLeft();
                        }
                        break;
                    case "backright":
                        if (_car != null)
                        {
                            _car.TurnBackwardRight();
                        }
                        break;
                    case "backleft":
                        if (_car != null)
                        {
                            _car.TurnBackwardLeft();
                        }
                        break;
                    case "stop":
                        if (_car != null)
                        {
                            _car.Stop();
                        }
                        break;
                    case "speed":
                        if (_car != null)
                        {
                            _car.SpeedTest();
                        }
                        break;
                }
            }
        }



    }
}
