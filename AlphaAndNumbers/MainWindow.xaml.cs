using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Win32Helpers.Icons;
using System.Speech.Synthesis;

namespace AlphaAndNumbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields
        FlashCards flashCards = new FlashCards();
        List<string> fcValues = new List<string>(
            new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I",
            "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"});
        int fcCursor = 0;
        const int MAX_CURSOR = 25;
        List<Button> randButtons = new List<Button>();
        SpeechSynthesizer speaker = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();

            randButtons.Add(RandButton1);
            randButtons.Add(RandButton2);
            randButtons.Add(RandButton3);

            speaker.Rate = 1;
            speaker.Volume = 100;

            //Disable Next & Previous buttons
            NextButton.Visibility = Visibility.Hidden;
            PreviousButton.Visibility = Visibility.Hidden;
        }

        //Main Windows has loaded
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            FlashCardImage.Source = flashCards.GetImageSource("A");
            SetRandButtons();

            //Set voice to anything other than default, if possible
            IEnumerable<InstalledVoice> voices = speaker.GetInstalledVoices();
            IEnumerator<InstalledVoice> voiceItr = voices.GetEnumerator();

            if(voiceItr.MoveNext() && voices.Count() > 1)
                voiceItr.MoveNext();

            //Set the voice
            speaker.SelectVoice(voiceItr.Current.VoiceInfo.Name);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
            base.OnSourceInitialized(e);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //Randomize game
            Random rnd = new Random();

            fcCursor = rnd.Next(MAX_CURSOR);

            FlashCardImage.Source = flashCards.GetImageSource(fcValues[fcCursor]);
            SetRandButtons();

            //Use for Sequential games
            /*
            if (fcCursor < MAX_CURSOR)
            {
                fcCursor++; //Increment cursor position

                //Set source image
                FlashCardImage.Source = flashCards.GetImageSource(fcValues[fcCursor]);
                SetRandButtons();
            }
            else if( fcCursor == MAX_CURSOR)
            {
                //Signial Win
                speaker.Speak("Good Job!");

                fcCursor = 0; //Reset cursor to inital position

                //Set image
                FlashCardImage.Source = flashCards.GetImageSource(fcValues[fcCursor]);
                SetRandButtons();
            }
            */
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if(fcCursor > 0)
            {
                fcCursor--;

                //Set source image
                FlashCardImage.Source = flashCards.GetImageSource(fcValues[fcCursor]);
                SetRandButtons();
            }
            else
            {
                fcCursor = MAX_CURSOR; //Set cursor to last position

                //Set source image
                FlashCardImage.Source = flashCards.GetImageSource(fcValues[fcCursor]);
                SetRandButtons();
            }
        }

        //On Image Click, speech
        private void FlashCardImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Say the letter
            //speaker.SpeakAsync(fcValues[fcCursor].ToString());
        }

        private void RandButton1_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer((Button)sender);
        }

        private void RandButton2_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer((Button)sender);
        }

        private void RandButton3_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer((Button)sender);
        }

        private void CheckAnswer(Button btn)
        {
            //Extract answer
            string answer = btn.Content.ToString().Substring(0, 1);

            if(answer == fcValues[fcCursor])
            {
                speaker.SpeakAsync("Correct");
                NextButton_Click(btn, null);
                
            }
            else
            {
                speaker.SpeakAsync("Try again");
                SetRandButtons();
            }
        }

        private void SetRandButtons()
        {
            Random rnd = new Random(); //Random object
            //ShuffleMethods.Shuffle<Button>(randButtons); //Shuffle the order of the buttons
            randButtons.Shuffle<Button>();

            //Make copy of fcValues, to remove possible duplicate answers
            List<string> copyfcValues = new List<string>(fcValues);
            
            copyfcValues.Remove(fcValues[fcCursor]); //Removed to avoid duplicate answers
            randButtons[0].Content = fcValues[fcCursor]; //set this one to correct answer
            randButtons[0].Content += ", " + randButtons[0].Content.ToString().ToLower();

            int fcRand1 = rnd.Next(copyfcValues.Count); //Get next random
            randButtons[1].Content = copyfcValues[fcRand1]; //Set button content
            copyfcValues.Remove(fcValues[fcRand1]); //Remove answer from pool
            randButtons[1].Content += ", " + randButtons[1].Content.ToString().ToLower();

            int fcRand2 = rnd.Next(copyfcValues.Count);
            randButtons[2].Content = copyfcValues[fcRand2];
            randButtons[2].Content += ", " + randButtons[2].Content.ToString().ToLower();
        }


    }

    /// <summary>
    /// Shuffle Methods Helper Class
    /// </summary>
    static class ShuffleMethods
    {
        /// <summary>
        /// Extension Method
        /// Fisher-Yates shuffle
        /// Used to shuffle any List based containers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
