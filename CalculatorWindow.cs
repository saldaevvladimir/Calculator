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

        [UI] private Label labelNumber = null;

        private bool Plus_Pressed = false;

        private bool Minus_Pressed = false;

        private bool Mult_Pressed = false;

        private bool Div_Pressed = false;

        private bool SqrtX_Pressed = false;

        private bool DegreeY_Pressed = false;

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


        [UI] private Button Point = null;

        [UI] private Button Clear = null;

        [UI] private Button Mult = null;

        [UI] private Button Div = null;

        [UI] private Button Plus = null;

        [UI] private Button Minus = null;

        [UI] private Button Calc = null;

        [UI] private Button Sqrt = null;

        [UI] private Button Square = null;

        [UI] private Button DegreeY = null;

        [UI] private Button Factorial = null;

        [UI] private Button SqrtX = null;

        [UI] private Button MPlus = null;

        [UI] private Button MMinus = null;

        [UI] private Button MMult = null;

        [UI] private Button MDiv = null;

        [UI] private Button ChangeSign = null;
        
        [UI] private Button MRC = null;

        #endregion

        #endregion

        #region Constructors

        public CalculatorWindow() : this(new Builder("CalculatorWindow.glade")) { }

        private CalculatorWindow(Builder builder) : base(builder.GetRawOwnedObject("CalculatorWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            Build();
        }

        #endregion

        #region Methods

        protected void Build()
        {
            calculator = new Calculator();

            MRC_СlickCount = 0;

            InitializeButtons();

            AddButtonClickHandlers();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void CorrectNumber()
        {
            //если есть знак "бесконечность" - не даёт писать цифры после него
            if (labelNumber.Text.IndexOf("∞") != -1)
                labelNumber.Text = labelNumber.Text.Substring(0, labelNumber.Text.Length - 1);

            //ситуация: слева ноль, а после него НЕ запятая, тогда ноль можно удалить
            if (labelNumber.Text[0] == '0' && (labelNumber.Text.IndexOf(",") != 1))
                labelNumber.Text = labelNumber.Text.Remove(0, 1);

            //аналогично предыдущему, только для отрицательного числа
            if (labelNumber.Text[0] == '-')
                if (labelNumber.Text[1] == '0' && (labelNumber.Text.IndexOf(",") != 2))
                    labelNumber.Text = labelNumber.Text.Remove(1, 1);
        }

        //проверяет не нажата ли еще какая-либо из кнопок мат.операций
        private bool CanPress()
        {   
            if (Mult_Pressed)
                return false;

            if (Div_Pressed)
                return false;

            if (Plus_Pressed)
                return false;

            if (Minus_Pressed)
                return false;

            if (SqrtX_Pressed)
                return false;

            if (DegreeY_Pressed)
                return false;

            return true;
        }

        private void turnOff_Click(Object sender, EventArgs e)
        {
            this.Destroy();

            Application.Quit();
        }

        private void digit0_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "0";

            CorrectNumber();
        }

        private void digit1_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "1";

            CorrectNumber();
        }

        private void digit2_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "2";

            CorrectNumber();
        }

        private void digit3_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "3";

            CorrectNumber();
        }

        private void digit4_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "4";

            CorrectNumber();
        }

        private void digit5_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "5";

            CorrectNumber();
        }

        private void digit6_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "6";

            CorrectNumber();
        }

        private void digit7_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "7";

            CorrectNumber();
        }

        private void digit8_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "8";

            CorrectNumber();
        }

        private void digit9_Click(Object sender, EventArgs e)
        {
            labelNumber.Text += "9";

            CorrectNumber();
        }

        private void Point_Click(Object sender, EventArgs e)
        {
            if ((labelNumber.Text.IndexOf(".") == -1) && (labelNumber.Text.IndexOf("∞") == -1))
                labelNumber.Text += ".";
        }

        private void Clear_Click(Object sender, EventArgs e)
        {
            labelNumber.Text = "0";

            calculator.Clear_A();

            FreeButtons();

            MRC_СlickCount = 0;
        }

        private void Mult_Click(Object sender, EventArgs e)
        {
            if(CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                Mult_Pressed = true;

                labelNumber.Text = "0";
            }
        }

        private void Div_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                Div_Pressed = true;

                labelNumber.Text = "0";
            }
        }

        private void Plus_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                Plus_Pressed = true;

                labelNumber.Text = "0";
            }
        }

        private void Minus_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                Minus_Pressed = true;

                labelNumber.Text = "0";
            }
        }

        private void Calc_Click(Object sender, EventArgs e)
        {
            if (Mult_Pressed)
                labelNumber.Text = calculator.Multiplication(Convert.ToDouble(labelNumber.Text)).ToString();

            if (Div_Pressed)
                labelNumber.Text = calculator.Division(Convert.ToDouble(labelNumber.Text)).ToString();

            if (Plus_Pressed)
                labelNumber.Text = calculator.Sum(Convert.ToDouble(labelNumber.Text)).ToString();

            if (Minus_Pressed)
                labelNumber.Text = calculator.Subtraction(Convert.ToDouble(labelNumber.Text)).ToString();

            if (SqrtX_Pressed)
                labelNumber.Text = calculator.SqrtX(Convert.ToDouble(labelNumber.Text)).ToString();

            if (DegreeY_Pressed)
                labelNumber.Text = calculator.DegreeY(Convert.ToDouble(labelNumber.Text)).ToString();

            calculator.Clear_A();

            FreeButtons();

            MRC_СlickCount = 0;
        }

        private void Sqrt_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                labelNumber.Text = calculator.Sqrt().ToString();

                calculator.Clear_A();

                FreeButtons();
            }
        }

        private void Square_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                labelNumber.Text = calculator.Square().ToString();

                calculator.Clear_A();

                FreeButtons();
            }
        }

        private void DegreeY_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                DegreeY_Pressed = true;

                labelNumber.Text = "0";
            }
        }

        private void Factorial_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                if ((Convert.ToDouble(labelNumber.Text) == (int)(Convert.ToDouble(labelNumber.Text))) && 
                    ((Convert.ToDouble(labelNumber.Text) >= 0.0)))
                {
                    calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                    labelNumber.Text = calculator.Factorial().ToString();

                    calculator.Clear_A();
                    FreeButtons();
                }
                else
                {
                    //MessageBox.Show("Число должно быть >= 0 и целым!");
                
                    MessageDialog messageDialog = new MessageDialog(this, 
                        DialogFlags.Modal, 
                        MessageType.Error, 
                        ButtonsType.Ok, 
                        "Введите целое число >= 0");
                        
                    messageDialog.Run();
                    messageDialog.Destroy();
                }
            }
        }

        private void SqrtX_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                calculator.Put_A(Convert.ToDouble(labelNumber.Text));

                SqrtX_Pressed = true;

                labelNumber.Text = "0";
            }
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
            if (labelNumber.Text[0] == '-')
                labelNumber.Text = labelNumber.Text.Remove('-');
            else
                labelNumber.Text = "-" + labelNumber.Text;
        }

        private void MRC_Click(Object sender, EventArgs e)
        {
            if (CanPress())
            {
                MRC_СlickCount++;

                if (MRC_СlickCount == 1)
                    labelNumber.Text = calculator.MemoryShow().ToString();

                if (MRC_СlickCount == 2)
                {
                    calculator.Memory_Clear();

                    labelNumber.Text = "0";

                    MRC_СlickCount = 0;
                }
            }
        }

        private void FreeButtons()
        {
            Mult_Pressed = false;

            Div_Pressed = false;

            Plus_Pressed = false;

            Minus_Pressed = false;

            SqrtX_Pressed = false;

            DegreeY_Pressed = false;
        }

        private void InitializeButtons()
        {
            turnOff = new Button("Exit");


            digit0 = new Button("0");

            digit1 = new Button("1");

            digit2 = new Button("2");

            digit3 = new Button("3");

            digit4 = new Button("4");

            digit5 = new Button("5");

            digit6 = new Button("6");

            digit7 = new Button("7");

            digit8 = new Button("8");

            digit9 = new Button("9");


            Point = new Button(".");

            Clear = new Button("Clr");

            Mult = new Button("*");

            Div = new Button("/");

            Plus = new Button("+");

            Minus = new Button("-");

            Calc = new Button("=");

            Sqrt = new Button("√");

            Square = new Button("^2");

            DegreeY = new Button("^y");

            Factorial = new Button("n!");

            SqrtX = new Button("n√");

            MPlus = new Button("M+");

            MMinus = new Button("M-");

            MMult = new Button("M*");
            
            MDiv = new Button("M/");

            ChangeSign = new Button("+/-");

            MRC = new Button("MRC");
        }

        private void AddButtonClickHandlers()
        {
            turnOff.Clicked += turnOff_Click;


            digit0.Clicked += digit0_Click;

            digit1.Clicked += digit2_Click;

            digit2.Clicked += digit2_Click;

            digit3.Clicked += digit3_Click;

            digit4.Clicked += digit4_Click;

            digit5.Clicked += digit5_Click;

            digit6.Clicked += digit6_Click;

            digit7.Clicked += digit7_Click;

            digit8.Clicked += digit8_Click;

            digit9.Clicked += digit9_Click;


            Point.Clicked += Point_Click;

            Clear.Clicked += Clear_Click;

            Mult.Clicked += Mult_Click;

            Div.Clicked += Div_Click;

            Plus.Clicked += Plus_Click;

            Minus.Clicked += Minus_Click;

            Calc.Clicked += Calc_Click;

            Sqrt.Clicked += Sqrt_Click;

            Square.Clicked += Square_Click;

            DegreeY.Clicked += DegreeY_Click;

            Factorial.Clicked += Factorial_Click;

            SqrtX.Clicked += SqrtX_Click;

            MPlus.Clicked += MPlus_Click;

            MMinus.Clicked += MMinus_Click;

            MMult.Clicked += MMult_Click;

            MDiv.Clicked += MDiv_Click;

            ChangeSign.Clicked += ChangeSign_Click;

            MRC.Clicked += MRC_Click;
        }

        #endregion
    }
}