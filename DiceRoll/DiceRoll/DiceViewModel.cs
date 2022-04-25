using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;



namespace DiceRoll
{
    public class DiceViewModel : INotifyPropertyChanged
    {


        // Variables
        public ObservableCollection<Dice> Dices { get; }

        string score = "0";

        string Throw = string.Empty;

        string ThrowHistory = "\n\nHistory of throws will be displayed here. The recent throws will be displayed on top.";

        const string aboutText = @"	__About__
DiceRoll is mobile app for generate random numbers in Cthulhu aestethic.



	__Background license__

<a href='https://www.freepik.com/vectors/background'>Background vector created by freepik - www.freepik.com</a>


	__App icon author__

brand_cthulhu_icon_158944_by_DiemenDesign


	__Icons license__

Icons provided under the Creative Commons 3.0 BY or CC0 if mentioned below.

Each sub-folders in this archive correspond to a different contributor :

- Lorc, http://lorcblog.blogspot.com
- Delapouite, https://delapouite.com
- John Colburn, http://ninmunanmu.com
- Felbrigg, http://blackdogofdoom.blogspot.co.uk
- John Redman, http://www.uniquedicetowers.com
- Carl Olsen, https://twitter.com/unstoppableCarl
- Sbed, http://opengameart.org/content/95-game-icons
- PriorBlue
- Willdabeast, http://wjbstories.blogspot.com
- Viscious Speed, http://viscious-speed.deviantart.com - CC0
- Lord Berandas, http://berandas.deviantart.com
- Irongamer, http://ecesisllc.wix.com/home
- HeavenlyDog, http://www.gnomosygoblins.blogspot.com
- Lucas
- Faithtoken, http://fungustoken.deviantart.com
- Skoll
- Andy Meneely, http://www.se.rit.edu/~andy/
- Cathelineau
- Kier Heyl
- Aussiesim
- Sparker, http://citizenparker.com
- Zeromancer - CC0
- Rihlsul
- Quoting
- Guard13007, https://guard13007.com
- DarkZaitzev, http://darkzaitzev.deviantart.com
- SpencerDub
- GeneralAce135
- Zajkonur
- Catsu
- Starseeker
- Pepijn Poolman
- Pierre Leducq
- Caro Asercion

Please, include a mention 'Icons made by {author}' in your derivative work.

If you use them in one of your project, don't hesitate to drop a message to delapouite@gmail.com or ping @GameIcons on twitter.

More info and icons available at https://game-icons.net



	__Sounds license__

Sounds available on side: https://mixkit.co/
License: Mixkit Sound Effects Free License
Sounds:
	mixkit_light_button_2580.wav
	mixkit_long_pop_2358.wav

__


";

        public string AboutText { get => aboutText; }

        Plugin.SimpleAudioPlayer.ISimpleAudioPlayer audioPlayer = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;


        // Managers
        public string Score
        {
            get => score;
            set
            {
                if (score == value) return;

                score = value;

                // Update properties
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(DisplayScore));
                OnPropertyChanged(nameof(DisplayThrow));
            }
        }

        public string DisplayScore => $"Score: {Score}";
        public string DisplayThrow => $"Throw: {Throw}";

        public string DisplayThrowHistory => ThrowHistory;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Commands
        public ICommand RollCommand => new Command(DiceRoll);
        void DiceRoll(object obj)
        {
            // Generate random no. (dice.quantity)-times, sum them and pass to Score

            Dice rollD = obj as Dice;
            Random random = new Random();


            int s = 0;
            Throw = $"{rollD.Quantity}D{rollD.sides}: (";
            for (int i = 0; i < ((rollD.GetIntQuantity() == 0)? 1: rollD.GetIntQuantity()); i++)
            {
                int roll = (random.Next(0, rollD.sides) + rollD.minCount) * rollD.multiplier;
                s += roll;
                Console.WriteLine($"Throw{i}: {roll}");
                Throw += roll.ToString() + ", ";
            }
            Throw += ")";


            // if there is the same score as previous one it will display combo (x2), then reset count
            if(Score == s.ToString())
            {
                Score = s.ToString() + " (x2)";
            }
            else
            {
                Score = s.ToString();
            }

            // Debug Tests
            //Console.WriteLine($"s: {s}");
            //Console.WriteLine($"imgSrc: {rollD.imgSrc}");
            //Console.WriteLine($"sides: {rollD.sides}");
            //Console.WriteLine($"Score: {Score}");
            //Score = rollD.imgSrc.ToString();


            ThrowHistory = '\t' + DisplayScore + '\n' + Throw + '\n' + ThrowHistory;


            // Play sound
            audioPlayer.Play();

        }


        public ICommand ResetQuantityCommand => new Command(ResetQuantity);
        void ResetQuantity(object obj)
        {
            // Reset all dices' quantity
            
            foreach(Dice dice in Dices)
            {
                dice.Quantity = "0";
            }
        }


        public ICommand AddQuantityCommand => new Command(AddQuantity);
        void AddQuantity(object obj)
        {
            // Add +1 for dice's quantity

            Dice dice = obj as Dice;
            if (dice.Quantity == "10") return;
            dice.Quantity = (dice.Quantity == string.Empty) ? 1.ToString() : (Int32.Parse(dice.Quantity) + 1).ToString();
        }


        public ICommand SubtractQuantityCommand => new Command(SubtractQuantity);
        void SubtractQuantity(object obj)
        {
            // Subtract -1 from dice's quantity

            Dice dice = obj as Dice;
            dice.Quantity = (dice.Quantity == string.Empty) ? string.Empty : (Int32.Parse(dice.Quantity) - 1).ToString();
        }


        public ICommand GoToAboutCommand => new Command(GoToAbout);
        void GoToAbout()
        {
            // Navigate to AboutPage which shares DiceViewModel from MainPage

            Application.Current.MainPage.Navigation.PushModalAsync(new AboutPage(Application.Current.MainPage.BindingContext));
        }


        public ICommand GoBackCommand => new Command(GoBack);
        void GoBack()
        {
            // Navigate back to MainPage

            Application.Current.MainPage.Navigation.PopModalAsync();
        }


        public ICommand GoToHistoryCommand => new Command(GoToHistory);
        void GoToHistory()
        {
            // Navigate to HistoryPage which shares DiceViewModel from MainPage

            Application.Current.MainPage.Navigation.PushModalAsync(new HistoryPage(Application.Current.MainPage.BindingContext));
        }


        // Constructors
        public DiceViewModel()
        {
            Dices = new ObservableCollection<Dice>();

            Dices.Add(new Dice(2, "d2_1.ico", 0));
            Dices.Add(new Dice(4, "d4_by_skoll.ico"));
            Dices.Add(new Dice(6, "d6_edit_by_delapuite.ico"));
            Dices.Add(new Dice(8, "d8_by_delapuite.ico"));
            Dices.Add(new Dice(10, "d100_by_skoll.ico", 0, 10));
            Dices.Add(new Dice(10, "d10_by_skoll.ico", 0));
            Dices.Add(new Dice(12, "d12_by_skoll.ico"));
            Dices.Add(new Dice(20, "d20_by_delapuite.ico"));
            Dices.Add(new Dice(100, "d100_by_skoll.ico"));
            Dices.Add(new Dice(200, "d2_0.ico", 100));

            // Load Soud to play
            audioPlayer.Load("mixkit_light_button_2580.wav");


        }


        // Methods
        




    }
}

/*
 * Add to GitHub 25.04.2022
 * 
 * 
 */