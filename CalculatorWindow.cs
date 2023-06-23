using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


namespace Calculator
{
    class CalculatorWindow : Window
    {
        #region Fields

        Calculator calculator;

        private int MRC_СlickCount;

        private Button lastPressedButton = null;

        [UI] private Label labelNumber = null;

        #region Buttons

        [UI] private Button turnOff = null;

        [UI] private Button digit0 = null;

        [UI] private Button digit1 = null;

        [UI] private Button digit2 = null;

        [UI] private Button digit3 = null;

        [UI] private Button digit4 = null;

        [UI] private Button digit5 = null;

        [UI] private Button digit6 = null;

        [UI] private Button digit7 = null;

        [UI] private Button digit8 = null;

        [UI] private Button digit9 = null;


        [UI] private Button point = null;

        [UI] private Button clear = null;

        [UI] private Button mult = null;

        [UI] private Button div = null;

        [UI] private Button plus = null;

        [UI] private Button minus = null;

        [UI] private Button calc = null;

        [UI] private Button sqrt = null;

        [UI] private Button degreeY = null;

        [UI] private Button factorial = null;

        [UI] private Button M_plus = null;

        [UI] private Button M_minus = null;

        [UI] private Button M_mult = null;

        [UI] private Button M_div = null;

        [UI] private Button changeSign = null;

        [UI] private Button MRC = null;

        #endregion

        #endregion

        #region Constructors

        public CalculatorWindow() : this(new Builder("CalculatorWindow.glade")) { }

        private CalculatorWindow(Builder builder) : base(builder.GetRawOwnedObject("CalculatorWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            calculator = new Calculator();

            MRC_СlickCount = 0;

            InitializeButtons(builder);

            AddButtonClickHandlers();
        }

        #endregion

        #region Methods

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void CorrectNumber()
        {
            if (labelNumber.Text.IndexOf("∞") != -1)
                labelNumber.Text = labelNumber.Text.Substring(0, labelNumber.Text.Length - 1);

            if (labelNumber.Text[0] == '0' && (labelNumber.Text.IndexOf(",") != 1))
                labelNumber.Text = labelNumber.Text.Remove(0, 1);

            if (labelNumber.Text[0] == '-')
                if (labelNumber.Text[1] == '0' && (labelNumber.Text.IndexOf(",") != 2))
                    labelNumber.Text = labelNumber.Text.Remove(1, 1);
        }

        private double GetCurrentNumber(bool operationButtonPressed)
        {
            if (!operationButtonPressed)
                return Convert.ToDouble(labelNumber.Text);
            else
                return calculator.GetFirstArgument();
        }

        private void HandleLastPressedButton()
        {
            if (lastPressedButton == mult)
                calculator.SetFirstArgument(calculator.Multiplication(Convert.ToDouble(labelNumber.Text)));

            if (lastPressedButton == div)
                calculator.SetFirstArgument(calculator.Division(Convert.ToDouble(labelNumber.Text)));

            if (lastPressedButton == plus)
                calculator.SetFirstArgument(calculator.Sum(Convert.ToDouble(labelNumber.Text)));

            if (lastPressedButton == minus)
                calculator.SetFirstArgument(calculator.Subtraction(Convert.ToDouble(labelNumber.Text)));

            if (lastPressedButton == degreeY)
                calculator.SetFirstArgument(calculator.DegreeY(Convert.ToDouble(labelNumber.Text)));

            lastPressedButton = null;
        }

        private void Operation2Arg_Click(Object sender, EventArgs e, Button button)
        {
            HandleTrigger();

            lastPressedButton = button;

            ClearLabel();
        }

        private void ClearLabel()
        {
            labelNumber.Text = "0";
        }

        private void HandleTrigger()
        {
            if (lastPressedButton == null)
                calculator.SetFirstArgument(Convert.ToDouble(labelNumber.Text));
            else
                HandleLastPressedButton();
        }

        private void Message(string messageText)
        {
            MessageDialog messageDialog = new MessageDialog(this,
                DialogFlags.Modal,
                MessageType.Error,
                ButtonsType.Ok,
                messageText);

            messageDialog.Run();
            messageDialog.Destroy();
        }

        private void TurnOff_Click(Object sender, EventArgs e)
        {
            this.Destroy();

            Application.Quit();
        }

        private void Digit_Click(Object sender, EventArgs e, int digit)
        {
            labelNumber.Text += digit;

            CorrectNumber();
        }

        private void Point_Click(Object sender, EventArgs e)
        {
            if ((labelNumber.Text.IndexOf(",") == -1) && (labelNumber.Text.IndexOf("∞") == -1))
                labelNumber.Text += ",";
        }

        private void Clear_Click(Object sender, EventArgs e)
        {
            ClearLabel();

            calculator.ClearFirstArgument();

            lastPressedButton = null;

            MRC_СlickCount = 0;
        }

        private void Calc_Click(Object sender, EventArgs e)
        {
            bool operationButtonPressed = (lastPressedButton != null);

            HandleLastPressedButton();

            labelNumber.Text = GetCurrentNumber(operationButtonPressed).ToString();

            calculator.ClearFirstArgument();

            lastPressedButton = null;

            MRC_СlickCount = 0;
        }

        private void Sqrt_Click(Object sender, EventArgs e)
        {
            if (Math.Sign(Convert.ToDouble(labelNumber.Text)) == 1)
                labelNumber.Text = calculator.Sqrt(Convert.ToDouble(labelNumber.Text)).ToString();
            else
                Message("введите положительное число");
        }

        private void Factorial_Click(Object sender, EventArgs e)
        {
            if (Double.TryParse(labelNumber.Text, out var num) && (num == (int)num) && (Math.Sign(num) != -1))
                labelNumber.Text = calculator.Factorial(Convert.ToDouble(labelNumber.Text)).ToString();
            else
                Message("введите целое число >= 0");
        }

        private void MPlus_Click(Object sender, EventArgs e)
        {
            calculator.M_Sum(Convert.ToDouble(labelNumber.Text));
        }

        private void MMinus_Click(Object sender, EventArgs e)
        {
            calculator.M_Subtraction(Convert.ToDouble(labelNumber.Text));
        }

        private void MMult_Click(Object sender, EventArgs e)
        {
            calculator.M_Multiplication(Convert.ToDouble(labelNumber.Text));
        }

        private void MDiv_Click(Object sender, EventArgs e)
        {
            calculator.M_Division(Convert.ToDouble(labelNumber.Text));
        }

        private void ChangeSign_Click(Object sender, EventArgs e)
        {
            if (Math.Sign(Convert.ToDouble(labelNumber.Text)) == -1)
                labelNumber.Text = labelNumber.Text.Replace("-", "");
            else if (Math.Sign(Convert.ToDouble(labelNumber.Text)) != 0)
                labelNumber.Text = "-" + labelNumber.Text;
        }

        private void MRC_Click(Object sender, EventArgs e)
        {
            MRC_СlickCount++;

            if (MRC_СlickCount == 1)
                labelNumber.Text = calculator.M_Show().ToString();

            if (MRC_СlickCount == 2)
            {
                calculator.M_Clear();

                ClearLabel();

                MRC_СlickCount = 0;
            }
        }

        private void InitializeButtons(Builder builder)
        {
            turnOff = (Button)builder.GetObject("TurnOff");


            digit0 = (Button)builder.GetObject("digit0");

            digit1 = (Button)builder.GetObject("digit1");

            digit2 = (Button)builder.GetObject("digit2");

            digit3 = (Button)builder.GetObject("digit3");

            digit4 = (Button)builder.GetObject("digit4");

            digit5 = (Button)builder.GetObject("digit5");

            digit6 = (Button)builder.GetObject("digit6");

            digit7 = (Button)builder.GetObject("digit7");

            digit8 = (Button)builder.GetObject("digit8");

            digit9 = (Button)builder.GetObject("digit9");


            point = (Button)builder.GetObject("Point");

            clear = (Button)builder.GetObject("Clear");

            mult = (Button)builder.GetObject("Mult");

            div = (Button)builder.GetObject("Div");

            plus = (Button)builder.GetObject("Plus");

            minus = (Button)builder.GetObject("Minus");

            calc = (Button)builder.GetObject("Calc");

            sqrt = (Button)builder.GetObject("Cqrt");

            degreeY = (Button)builder.GetObject("DegreeY");

            factorial = (Button)builder.GetObject("Factorial");

            changeSign = (Button)builder.GetObject("ChangeSign");

            M_plus = (Button)builder.GetObject("MPlus");

            M_minus = (Button)builder.GetObject("MMinus");

            M_mult = (Button)builder.GetObject("MMult");

            M_div = (Button)builder.GetObject("MDiv");

            MRC = (Button)builder.GetObject("MRC");
        }

        private void AddButtonClickHandlers()
        {
            digit0.Clicked += (s, a) => Digit_Click(s, a, 0);

            digit1.Clicked += (s, a) => Digit_Click(s, a, 1);

            digit2.Clicked += (s, a) => Digit_Click(s, a, 2);

            digit3.Clicked += (s, a) => Digit_Click(s, a, 3);

            digit4.Clicked += (s, a) => Digit_Click(s, a, 4);

            digit5.Clicked += (s, a) => Digit_Click(s, a, 5);

            digit6.Clicked += (s, a) => Digit_Click(s, a, 6);

            digit7.Clicked += (s, a) => Digit_Click(s, a, 7);

            digit8.Clicked += (s, a) => Digit_Click(s, a, 8);

            digit9.Clicked += (s, a) => Digit_Click(s, a, 9);


            plus.Clicked += (s, a) => Operation2Arg_Click(s, a, plus);

            mult.Clicked += (s, a) => Operation2Arg_Click(s, a, mult);

            div.Clicked += (s, a) => Operation2Arg_Click(s, a, div);

            minus.Clicked += (s, a) => Operation2Arg_Click(s, a, minus);

            degreeY.Clicked += (s, a) => Operation2Arg_Click(s, a, degreeY);
        }

        #endregion
    }
}