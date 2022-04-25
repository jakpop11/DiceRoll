using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace DiceRoll
{
    public class Dice : INotifyPropertyChanged
    {


        // Variables
        public int sides { get; }
        public string imgSrc { get; }
        public int minCount { get; }

        public int multiplier { get; }

        int quantity = 0;


        // Managers 
        public string Quantity
        {
            get => (quantity == 0) ? string.Empty : quantity.ToString();
            set
            {
                try
                {
                    int q = Int32.Parse(value);
                    quantity = (q >= 0 && q <= 10) ? q : 0;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Error:{e}");
                    quantity = 0;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error:{e}");
                    quantity = 0;
                }

                OnPropertyChanged(nameof(Quantity));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Constructors
        public Dice(int sides)
        {
            this.sides = sides;
            this.imgSrc = string.Empty;
            this.minCount = 1;
            this.multiplier = 1;
        }

        public Dice(int sides, string imgSrc)
        {
            this.sides = sides;
            this.imgSrc = imgSrc;
            this.minCount = 1;
            this.multiplier = 1;
        }

        public Dice(int sides, string imgSrc, int minCount)
        {
            this.sides = sides;
            this.imgSrc = imgSrc;
            this.minCount = minCount;
            this.multiplier = 1;
        }
        public Dice(int sides, string imgSrc, int minCount, int muliplier)
        {
            this.sides = sides;
            this.imgSrc = imgSrc;
            this.minCount = minCount;
            this.multiplier = muliplier;
        }

        public Dice(int sides, int minCount)
        {
            this.sides = sides;
            this.imgSrc = string.Empty;
            this.minCount = minCount;
            this.multiplier = 1;
        }


        // Methods
        public int GetIntQuantity()
        {
            return quantity;
        }


    }
}
