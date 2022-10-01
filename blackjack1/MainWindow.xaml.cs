using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace blackjack1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ImageSource[] cardpictures = new BitmapImage[54];
        
        
        List<int> dealer = new List<int>();
        List<int> player = new List<int>();

        string[] numbers = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };


        int pscore = 0;
        int dscore = 0; 
        int pcardno = 0;
        int dcardno = 0;
        int rand = 0;
        Random sequence = new Random();
        
        public MainWindow()
        {
            

            string[] suites = {"diamonds", "spades", "hearts", "clubs"};
            const string loc = "C:\\Users\\17hfoster\\OneDrive - Bury Grammar Schools\\A level\\Computer science\\blackjack1\\cards\\";
            
            for (int i=0; i < suites.Length; i++) {

                for (int j = 0; j < numbers.Length; j++) { 

                    cardpictures[j+i*13] = new BitmapImage(new Uri(loc + numbers[j] + suites[i]  + ".png"));
                }
            }
            cardpictures[52] = new BitmapImage(new Uri(loc  + "back.png"));
            

            InitializeComponent();

            newgame();
            
        }
        public void newgame() { 
            pcardno = 0;
            dcardno = 0;
            pscore = 0;
            dscore = 0;
            player.Clear();
            dealer.Clear();

            dealercard1.Source = cardpictures[genrand(0)];
            dealercard2.Source = cardpictures[52];
            dealercard3.Source = cardpictures[52];
            dealercard4.Source = cardpictures[52];
            dealercard5.Source = cardpictures[52];
            dealercard6.Source = cardpictures[52];
            dealercard7.Source = cardpictures[52];

            playercard1.Source = cardpictures[genrand(1)];
            playercard2.Source = cardpictures[genrand(1)];
            playercard3.Source = cardpictures[52];
            playercard4.Source = cardpictures[52];
            playercard5.Source = cardpictures[52];
            playercard6.Source = cardpictures[52];
            playercard7.Source = cardpictures[52];


        }

        private void stand_Click(object sender, RoutedEventArgs e)
        {
            dealergen();
            endgame();
        }

        public void endgame() {
            if (pscore > 21)
            {
                MessageBox.Show("You lose, get better");
            }
            else if (dscore > 21)
            {
                MessageBox.Show("You win, you're not so bad after all");
            }
            else if (dscore > pscore)
            {
                MessageBox.Show("You lose");
            }
            else {
                MessageBox.Show("You win");
            }
            
            newgame();
        }

        private void dealergen() {

            dealercard2.Source = cardpictures[genrand(0)];

            while (dscore < 13 || dcardno < 4) {
                dscore++;
                if (dcardno == 1)
                {
                    dealercard3.Source = cardpictures[genrand(0)];
                    DealerScoreLabel.Content = dscore.ToString();

                }
                else if (dcardno == 2)
                {
                    dealercard4.Source = cardpictures[genrand(0)];
                    DealerScoreLabel.Content = dscore.ToString();
                }
                else if (dcardno == 3)
                {
                    dealercard5.Source = cardpictures[genrand(0)];
                    DealerScoreLabel.Content = dscore.ToString();
                }
                else if (dcardno == 4)
                {
                    dealercard6.Source = cardpictures[genrand(0)];
                    DealerScoreLabel.Content = dscore.ToString();
                }
                else
                {
                    dealercard7.Source = cardpictures[genrand(0)];
                    DealerScoreLabel.Content = dscore.ToString();
                }
            }
        }
        private void hitme_Click(object sender, RoutedEventArgs e)
        {
            pcardno++;
            if (pcardno == 1) {  
                playercard3.Source = cardpictures[genrand(1)];
            
            }
            else if (pcardno == 2){                 
                playercard4.Source = cardpictures[genrand(1)];
            }
            else if (pcardno == 3){                
                playercard5.Source = cardpictures[genrand(1)];
            }
            else if (pcardno == 4){                
                playercard6.Source = cardpictures[genrand(1)];
            }
            else {                 
                playercard7.Source = cardpictures[genrand(1)];
                endgame();
            }
        }

        public void addscore(int rand, int case1) {

            if (case1 == 1)
            {
                if (((rand + 1) % 13) >= 10 || ((rand + 1) % 13) == 0)
                {
                    pscore += 10;
                }

                else if (((rand + 1) % 13) == 1)
                {
                    if (pscore + 11 > 21)
                    {
                        pscore += 1;
                    }
                    else
                    {
                        pscore += 11;
                    }
                }

                else
                {
                    pscore += ((rand + 1) % 13);
                }
                if (pscore > 21)
                {

                    if (checkforcard("ace", 1) == true && (pscore-10 < 21))
                    {
                        pscore -= 10;
                    }
                    else {
                        endgame();
                    }
                }

            }

            else
            {
                if (((rand + 1) % 13) >= 10 || ((rand + 1) % 13) == 0)
                {
                    dscore += 10;
                }

                else if (((rand + 1) % 13) == 1)
                {
                    if (dscore + 11 > 21)
                    {
                        dscore += 1;
                    }
                    else
                    {
                        dscore += 11;
                    }
                }

                else
                {
                    dscore += ((rand + 1) % 13);
                }
                if (dscore > 21)
                {

                    if (checkforcard("ace", 1) == true && (dscore - 10 < 21))
                    {
                        dscore -= 10;
                    }
                    else
                    {
                        endgame();
                    }
                }
            }


            }
        

    public bool checkforcard(string card, int case1)
        {
            int index = Array.IndexOf(numbers, card);
            int counter = 0;

            List<int> indexes = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                indexes.Add(index + i * 13);
            }

            if (case1 == 1)
            {

                indexes.ForEach(delegate (int num) {
                    if (player.Contains(num))
                    {
                        counter += 1;
                    }
                });
            }

            else
            {
                indexes.ForEach(delegate (int num) {
                    if (dealer.Contains(num))
                    {
                        counter += 1;
                    }
                });
            }

            if (counter > 1)
            {
                return true;
            }

            return false;
        }

        public int genrand(int case1) { 
            
            rand = sequence.Next(0, 52);
            while (player.Contains(rand) || dealer.Contains(rand)){ 
                rand = sequence.Next(0, 52);
            }
            if (case1 == 1){
                
                player.Add(rand);
                addscore(rand, 1);
                PlayerScoreLabel.Content = pscore.ToString();
                
            }

            else { 
                dealer.Add(rand);
                addscore(rand, 0);
            }
            
            return rand;
        }
    }
}
