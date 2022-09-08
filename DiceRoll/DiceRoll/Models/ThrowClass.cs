using System;
using System.Collections.Generic;
using System.Text;




namespace DiceRoll.Models
{
    public class ThrowClass
    {
        int quantity;
        int sides;

        List<int> rollList;


        public ThrowClass(Dice dice, List<int> rolls)
        {
            quantity = dice.GetIntQuantity();
            sides = dice.sides;
            rollList = rolls;
        }


        public int GetScore()
        {
            int score = 0;
            foreach(int r in rollList)
            {
                score += r;
            }

            return score;
        }


        override public string ToString()
        {
            return $"{((quantity == 0)? 1 : quantity)}D{sides}({String.Join(", ", rollList)})";
        }


        public static string StringJoin(string sep, List<ThrowClass> throws)
        {
            string result = string.Empty;
            for(int i=0; i< throws.Count-1; i++)
            {
                result += throws[i].ToString() + sep;
            }
            result += throws[throws.Count-1].ToString();

            return result;
        }
    }
}
