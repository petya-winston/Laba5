using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public class Car : ICloneable
    {
        public string BodyType { get; set; }
        public string EngineType { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }

        public string ShowConfiguration()
        {
            return $"Body: {BodyType}, Engine: {EngineType}, Transmission: {Transmission}, Color: {Color}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    // Інтерфейс CarBuilder
    public interface ICarBuilder
    {
        void Reset();
        void SetBodyType(string bodyType);
        void SetEngineType(string engineType);
        void SetTransmission(string transmission);
        void SetColor(string color);
        Car GetResult();
    }

    // Конкретний будівник для Sedan
    public class SedanBuilder : ICarBuilder
    {
        private Car _car;

        public SedanBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _car = new Car();
        }

        public void SetBodyType(string bodyType)
        {
            _car.BodyType = bodyType;
        }

        public void SetEngineType(string engineType)
        {
            _car.EngineType = engineType;
        }

        public void SetTransmission(string transmission)
        {
            _car.Transmission = transmission;
        }

        public void SetColor(string color)
        {
            _car.Color = color;
        }

        public Car GetResult()
        {
            return _car;
        }
    }

    // Конкретний будівник для SUV
    public class SUVBuilder : ICarBuilder
    {
        private Car _car;

        public SUVBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _car = new Car();
        }

        public void SetBodyType(string bodyType)
        {
            _car.BodyType = bodyType;
        }

        public void SetEngineType(string engineType)
        {
            _car.EngineType = engineType;
        }

        public void SetTransmission(string transmission)
        {
            _car.Transmission = transmission;
        }

        public void SetColor(string color)
        {
            _car.Color = color;
        }

        public Car GetResult()
        {
            return _car;
        }
    }

    // Director
    public class Director
    {
        private ICarBuilder _builder;

        public void SetBuilder(ICarBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructSedan()
        {
            _builder.Reset();
            _builder.SetBodyType("Sedan");
            _builder.SetEngineType("Petrol");
            _builder.SetTransmission("Automatic");
            _builder.SetColor("Red");
        }

        public void ConstructSUV()
        {
            _builder.Reset();
            _builder.SetBodyType("SUV");
            _builder.SetEngineType("Diesel");
            _builder.SetTransmission("Manual");
            _builder.SetColor("Blue");
        }
    }

    // Головний клас програми
    public partial class MainForm : Form
    {
        private Director _director = new Director();
        private SedanBuilder _sedanBuilder = new SedanBuilder();
        private SUVBuilder _suvBuilder = new SUVBuilder();
        private List<Car> _cars = new List<Car>();

        private Label resultLabel;
        private ListBox carsListBox;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Car Builder with Prototype";
            this.Width = 500;
            this.Height = 400;

            Button sedanButton = new Button { Text = "Build Sedan", Left = 50, Top = 50, Width = 150 };
            sedanButton.Click += SedanButton_Click;
            this.Controls.Add(sedanButton);

            Button suvButton = new Button { Text = "Build SUV", Left = 250, Top = 50, Width = 150 };
            suvButton.Click += SuvButton_Click;
            this.Controls.Add(suvButton);

            Button cloneButton = new Button { Text = "Clone Selected Car", Left = 50, Top = 250, Width = 350 };
            cloneButton.Click += CloneButton_Click;
            this.Controls.Add(cloneButton);

            resultLabel = new Label { Left = 50, Top = 150, Width = 400, Height = 50 };
            this.Controls.Add(resultLabel);

            carsListBox = new ListBox { Left = 50, Top = 300, Width = 350, Height = 100 };
            this.Controls.Add(carsListBox);
        }

        private void SedanButton_Click(object sender, EventArgs e)
        {
            _director.SetBuilder(_sedanBuilder);
            _director.ConstructSedan();
            Car car = _sedanBuilder.GetResult();
            _cars.Add(car);
            UpdateCarsList();
        }

        private void SuvButton_Click(object sender, EventArgs e)
        {
            _director.SetBuilder(_suvBuilder);
            _director.ConstructSUV();
            Car car = _suvBuilder.GetResult();
            _cars.Add(car);
            UpdateCarsList();
        }

        private void CloneButton_Click(object sender, EventArgs e)
        {
            if (carsListBox.SelectedIndex >= 0)
            {
                Car selectedCar = _cars[carsListBox.SelectedIndex];
                Car clonedCar = (Car)selectedCar.Clone();
                _cars.Add(clonedCar);
                UpdateCarsList();
            }
        }

        private void UpdateCarsList()
        {
            carsListBox.Items.Clear();
            foreach (var car in _cars)
            {
                carsListBox.Items.Add(car.ShowConfiguration());
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}