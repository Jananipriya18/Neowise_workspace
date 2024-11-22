using System;

namespace SimpleDelegateEventDemo
{
    public delegate void ButtonClickHandler();

    class Button
    {
        public event ButtonClickHandler ButtonClicked;

        public void Click()
        {
            Console.WriteLine("Button was clicked!");
            ButtonClicked?.Invoke();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Button button = new Button();
            button.ButtonClicked += OnButtonClicked;
            Console.WriteLine("Press Enter to 'click' the button.");
            Console.ReadLine();
            button.Click();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        static void OnButtonClicked()
        {
            Console.WriteLine("Event Triggered: ButtonClickHandler invoked!");
        }
    }
}
