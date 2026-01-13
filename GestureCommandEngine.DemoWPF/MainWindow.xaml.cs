using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.DemoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICommandsRepository _commandsRepository;
        private readonly IGestureCommandsHandler _gestureCommandsHandler;

        private bool _capturing = false;
        private List<Point2D> _capturePoints = new List<Point2D>();

        private Color _penColor = Colors.Black;

        public MainWindow(ICommandsRepository commandsRepository, IGestureCommandsHandler gestureCommandsHandler)
        {
            InitializeComponent();

            _commandsRepository = commandsRepository;
            _gestureCommandsHandler = gestureCommandsHandler;

            _gestureCommandsHandler.GestureCommandInvoked += OnGestureCommandInvoke;
            _gestureCommandsHandler.MouseGestureNotRecognized += OnMouseGestureNotRecognized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetupCommands();
            SetupGestureCommands();
            ShowCommandsInfo();
        }

        private void OnGestureCommandInvoke(object? sender, GestureCommandEventArgs e)
        {
            currentGestureLabel.Content = e.GestureString;
            InvokeGestureCommand(e.CommandId);
        }
        private void OnMouseGestureNotRecognized(object? sender, MouseGestureEventArgs e)
        {
            currentGestureLabel.Content = e.GestureString + "?!";
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MyCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _capturing = true;

            _capturePoints = new List<Point2D>();

            var p = e.GetPosition(MyCanvas);

            _capturePoints.Add(new Point2D(p.X, p.Y));
        }

        private void MyCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            MyCanvas.Children.Clear();

            try
            {
                _gestureCommandsHandler.Handle(_capturePoints);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Gesture recognizer error", MessageBoxButton.OK, MessageBoxImage.Error);
                currentGestureLabel.Content = "---";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Gesture recognizer error", MessageBoxButton.OK, MessageBoxImage.Error);
                currentGestureLabel.Content = "---";
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Command manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Command manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _capturing = false;
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_capturing)
            {
                var p = e.GetPosition(MyCanvas);
                var dp = new Point2D(p.X, p.Y);

                DrawLineOnCanvas(_capturePoints.Last(), dp);

                int dX = Math.Abs(_capturePoints.Last().X - dp.X);
                int dY = Math.Abs(_capturePoints.Last().Y - dp.Y);

                // ignore random hand shake while moving mouse
                if (dX < 6 && dY < 6)
                {
                    return;
                }

                _capturePoints.Add(new Point2D(p.X, p.Y));
            }
        }

        private void SetupCommands()
        {
            _commandsRepository.Add("ChangeBgToGreen",  "Change bg to green");
            _commandsRepository.Add("ChangeBgToYellow", "Change bg to yellow");
            _commandsRepository.Add("ChangeBgToBlue",   "Change bg to blue");
            _commandsRepository.Add("ChangeBgToViolet", "Change bg to violet");
            _commandsRepository.Add("ChangeBgToLime",   "Change bg to lime");
            _commandsRepository.Add("ChangeBgToOrange", "Change bg to orange");
            _commandsRepository.Add("ChangeBgToIndigo", "Change bg to indigo");
            _commandsRepository.Add("ChangeBgToWhite",  "Change bg to white");
            _commandsRepository.Add("ChangeBgToKhaki",  "Change bg to khaki");
            _commandsRepository.Add("ChangePenToBlack", "Change pen to black");
            _commandsRepository.Add("ChangePenToRed",   "Change pen to red");
        }

        private void SetupGestureCommands()
        {
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToGreen",  "U");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToYellow", "D");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToBlue",   "L");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToViolet", "R");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToLime",   "LR");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToOrange", "UD");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToIndigo", "LU");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToWhite",  "RD");
            _gestureCommandsHandler.AddGestureCommand("ChangeBgToKhaki",  "LUR");
            _gestureCommandsHandler.AddGestureCommand("ChangePenToBlack", "LRL");
            _gestureCommandsHandler.AddGestureCommand("ChangePenToRed",   "RLR");
        }

        private void ShowCommandsInfo()
        {
            var commands = _gestureCommandsHandler.GetGesturesInfo();

            availableGesturesLabel.Content = "Available gestures:";
            foreach (var cmd in commands)
            {
                availableGesturesLabel.Content += $"{Environment.NewLine}{cmd.Key}: {cmd.Value}";
            }
        }

        private void InvokeGestureCommand(string commandId)
        {
            switch (commandId)
            {
                case "ChangeBgToGreen":
                    UpdateBackgroundColor(Colors.Green);
                    break;
                case "ChangeBgToYellow":
                    UpdateBackgroundColor(Colors.Yellow);
                    break;
                case "ChangeBgToBlue":
                    UpdateBackgroundColor(Colors.Blue);
                    break;
                case "ChangeBgToViolet":
                    UpdateBackgroundColor(Colors.Violet);
                    break;
                case "ChangeBgToLime":
                    UpdateBackgroundColor(Colors.Lime);
                    break;
                case "ChangeBgToOrange":
                    UpdateBackgroundColor(Colors.Orange);
                    break;
                case "ChangeBgToIndigo":
                    UpdateBackgroundColor(Colors.Indigo);
                    break;
                case "ChangeBgToWhite":
                    UpdateBackgroundColor(Colors.White);
                    break;
                case "ChangeBgToKhaki":
                    UpdateBackgroundColor(Colors.Khaki);
                    break;
                case "ChangePenToBlack":
                    UpdatePenColor(Colors.Black);
                    break;
                case "ChangePenToRed":
                    UpdatePenColor(Colors.Red);
                    break;
                default:
                    break;
            }
        }

        private void UpdateBackgroundColor(Color color)
        {
            MyCanvas.Background = new SolidColorBrush(color);
        }

        private void UpdatePenColor(Color color)
        {
            _penColor = color;
        }

        private void DrawLineOnCanvas(Point2D p1, Point2D p2)
        {
            var line = new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(_penColor)
            };

            MyCanvas.Children.Add(line);
        }
    }
}