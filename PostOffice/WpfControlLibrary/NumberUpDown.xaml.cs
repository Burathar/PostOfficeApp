using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NumberUpDown
    {
        public static readonly DependencyProperty MaxNumberProperty = DependencyProperty.Register(
            name: "MaxNumber",
            propertyType: typeof(int),
            ownerType: typeof(NumberUpDown),
            typeMetadata: new PropertyMetadata(0, (d, e) =>
                {
                    (d as NumberUpDown).MaxNumber = (int)e.NewValue;
                })
            );

        public static readonly DependencyProperty MinNumberProperty = DependencyProperty.Register(
            name: "MinNumber",
            propertyType: typeof(int),
            ownerType: typeof(NumberUpDown),
            typeMetadata: new PropertyMetadata(0, (d, e) =>
                {
                    (d as NumberUpDown).MinNumber = (int)e.NewValue;
                })
            );

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            name: "Number",
            propertyType: typeof(int),
            ownerType: typeof(NumberUpDown),
            typeMetadata: new PropertyMetadata(0, (d, e) =>
                {
                    (d as NumberUpDown).Number = (int)e.NewValue;
                })
            );

        public static readonly DependencyProperty StepSizeProperty = DependencyProperty.Register(
            name: "StepSize",
            propertyType: typeof(int),
            ownerType: typeof(NumberUpDown),
            typeMetadata: new PropertyMetadata(1, (d, e) =>
                {
                    (d as NumberUpDown).StepSize = (int)e.NewValue;
                })
            );

        private int _maxNumber = 100;
        private int _minNumber = 0;
        private int _number;

        public NumberUpDown()
        {
            InitializeComponent();
            TbNumber.Text = _number.ToString();
        }

        public delegate void NotifyValueChanged(object sender, RoutedEventArgs e);

        public event NotifyValueChanged ValueChanged;

        public int MaxNumber
        {
            get => _maxNumber;
            set
            {
                _maxNumber = value < MinNumber ? MinNumber : value;
                UpdateNumber();
            }
        }

        public int MinNumber
        {
            get => _minNumber;
            set
            {
                _minNumber = value > MaxNumber ? MaxNumber : value;
                UpdateNumber();
            }
        }

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                UpdateNumber();
            }
        }

        public int StepSize { get; set; } = 1;

        /// <summary>
        /// Makes sure Number is within bounds of Min- and MaxNumber
        /// </summary>
        /// <returns>Returns True if Number was not changed</returns>
        private bool EnforceBounds()
        {
            if (Number < MinNumber)
            {
                Number = MinNumber;
                return false;
            }
            if (Number > MaxNumber)
            {
                Number = MaxNumber;
                return false;
            }
            return true;
        }

        private void CmdDown_Click(object sender, RoutedEventArgs e)
        {
            Number -= StepSize;
        }

        private void CmdUp_Click(object sender, RoutedEventArgs e)
        {
            Number += StepSize;
        }

        private void TbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbNumber == null)
            {
                return;
            }
            if (TbNumber.Text == Number.ToString()) return;
            if (!int.TryParse(TbNumber.Text, out _number))
                TbNumber.Text = Number.ToString();

            UpdateNumber();
        }

        private void TxtNum_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    Number++;
                    break;

                case Key.Down:
                    Number--;
                    break;
            }
        }

        private void UpdateNumber()
        {
            if (!EnforceBounds()) return;
            TbNumber.Text = Number.ToString();
            ValueChanged?.Invoke(this, null);
        }
    }
}