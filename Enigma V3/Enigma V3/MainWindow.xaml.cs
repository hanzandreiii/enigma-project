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

namespace Enigma_V3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Label[] butts = new Label[92];
        public static int[] alpha = {32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 95, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125};
        public static int[,] beta = new int[,] { };
        public static int limit = 0;
        public static int ringlimit = 0;     
        public static int[] timearray = new int[limit];
        public static int[] ringnumbers = { 0, 0, 0 };
        public static int[] timenumbers = { 0, 0, 0 };
        public static int[] offset = { 0, 0, 0 };
        public static bool[] con1 = { false, false, false };

        public MainWindow()
        {
            InitializeComponent();
            /*for (int x = 0; x < limit; x++)
                boxone.Text += beta[ringnumbers[0], x] + " ";
            for (int x = 0; x < limit; x++)
                boxone_Copy.Text += beta[ringnumbers[1], x] + " ";
            for (int x = 0; x < limit; x++)
                boxone_Copy1.Text += beta[ringnumbers[2], x] + " ";*/
            keebGen();

        }

        /// <summary>
        /// This generates the keyboard 
        /// </summary>
        public void keebGen()
        {
            int count = 0;
            int topmarg = 600;

            for (int x = 0; x < 4; x++)
            {
                int leftmarg = 275;
                for (int y = 0; y < 23; y++)
                {
                    Label butt = new Label();

                    butt.Width = 50;
                    butt.Height = 50;
                    butt.Opacity = .7;
                    butt.Margin = new Thickness(leftmarg, topmarg, 0, 0);
                    butt.VerticalAlignment = VerticalAlignment.Top;
                    butt.BorderBrush = Brushes.Black;
                    butt.Background = Brushes.LightSlateGray;
                    butt.FontSize = 20;
                    butt.FontFamily = new FontFamily("Britannic Bold");
                    butt.FontWeight = FontWeights.Bold;
                    butt.BorderThickness = new Thickness(2,2,2,2);
                    butt.HorizontalAlignment = HorizontalAlignment.Left;
                    butt.Name = "Label" + alpha[count].ToString();
                    butt.Content = "  " + ((char)alpha[count]).ToString() + " ";
                    butts[count] = butt;

                    TheGrid.Children.Add(butt);
                    leftmarg += 60;
                    count++;
                }
                topmarg += 60;
            }
        }

        /// <summary>
        /// This is the event for input changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            string lastinput = input.Text.Substring(input.Text.Length - 1, 1);
            char lastchar = char.Parse(lastinput);
            int lastint = Convert.ToInt32(lastchar);
            bool charexist = false;
            for (int x = 0; x < limit; x++)
            {
                if (lastint == beta[0,x])
                    charexist = true;
            }

            int temp = 0;


            if (charexist)
            {
                for (int x = 0; x < butts.Length; x++)
                {
                    if (butts[x].Name == "Label" + lastint.ToString())
                        butts[x].Background = Brushes.LightGreen;
                    else
                        butts[x].Background = Brushes.LightSlateGray;
                }

                firstlbl.Content = (char)lastint;
                ///MessageBox.Show(lastint + "");
                temp = encrypt(lastint, ringnumbers[0]);
                ///MessageBox.Show(temp + " ");
                temp = encrypt(temp, ringnumbers[1]);
                ///MessageBox.Show(temp + " ");
                temp = encrypt(temp, ringnumbers[2]);
                ///MessageBox.Show(temp + " ");
                temp = encrypt(temp, ringnumbers[2]);
                ///MessageBox.Show(temp + " ");
                temp = encrypt(temp, ringnumbers[1]);
                ///MessageBox.Show(temp + " ");
                temp = encrypt(temp, ringnumbers[0]);
                ///MessageBox.Show(temp + " ");
                secondlbl.Content = (char)temp;
                boxone.Text += (char)temp;
                ///MessageBox.Show((char)lastint + "" + (char)temp);

                tickMove(0,false);

                if (timenumbers[0] >= limit)
                {
                    timenumbers[0] = 0;
                    tickMove(1,false);
                    if (timenumbers[1]  >= limit)
                    {
                        timenumbers[1] = 0;
                        tickMove(2,false);
                        if (timenumbers[2] >= limit)
                        {
                            timenumbers[2] = 0;
                        }
                    }
                }
                
                /*boxone_Copy.Text = "";
                boxone_Copy1.Text = "";
                boxone_Copy2.Text = "";
                for (int x = 0; x < limit; x++)
                    boxone_Copy.Text += beta[ringnumbers[0], x] + " ";
                for (int x = 0; x < limit; x++)
                    boxone_Copy1.Text += beta[ringnumbers[1], x] + " ";
                for (int x = 0; x < limit; x++)
                    boxone_Copy2.Text += beta[ringnumbers[2], x] + " ";*/

            }
      
            else
            {
                input.Text = input.Text.Substring(0, input.Text.Length - 1);
                input.Select(input.Text.Length, 0);
            }
        }

        private string[] stringSplitter(string stringToSplit, char[] splitChars)
        {
            return stringToSplit.Split(splitChars);
        }

        public void readFile(string filePath)
        {
            int count = 0;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] sets = stringSplitter(line, new char[] {','});
                        if (count == 0)
                        {
                            beta = new int[int.Parse(sets[0]) + 1, int.Parse(sets[1])];
                            limit = int.Parse(sets[1]);
                            ringlimit = int.Parse(sets[0]);
                            timearray = new int[limit];
                        }
                        for (int x = 0; x <= ringlimit; x++)
                        {
                            if (count > 1)
                            {
                                beta[x, count - 2] = int.Parse(sets[x]);
                            }
                            else if (count == limit)
                            {
                                count = 0;
                            }
                        }
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void tickMove(int pref, bool mode)
        {
            int merge = offset[pref];
            int thing = 0;
            int ylimit = 0;
            if (mode == false)
            {
                ylimit = 1;
            }
            else
            {
                ylimit = offset[pref];
            }
            for (int x = 0; x < ylimit; x++)
            {
                for (int y = 0; y < limit; y++)
                {

                    thing = y + 1;

                    if (y == 0)
                    {
                        for (int z = 0; z < limit; z++)
                        {
                            timearray[z] = beta[ringnumbers[pref], z];
                        }
                    }
                    if (thing < limit)
                        beta[ringnumbers[pref], y] = timearray[thing];
                    else
                    {
                        beta[ringnumbers[pref], y] = timearray[thing - limit];
                    }
                }
                timenumbers[pref]++;
                
            }

            timelbl.Content = limit + "     " + timenumbers[0] + " : " + timenumbers[1] + " : " + timenumbers[2];
        }

        public int encrypt(int input, int pref)
        {
            int retnum = 0;
            for(int x = 0; x < limit; x++)
            {
                if(input == beta[0, x])
                {
                    retnum = beta[pref, x];
                    ///MessageBox.Show("activate " + pref + " " + x + " pref " + retnum + " " + beta[pref, x] + " " + limit);
                }
            }
            ///MessageBox.Show(retnum + "");
            return retnum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ringnumbers[0] != ringnumbers[1] && ringnumbers[0] != ringnumbers[2] && ringnumbers[1] != ringnumbers[2])
            {
                for (int x = 0; x < limit; x++)
                {
                    off1.Items.Add(x);
                    off2.Items.Add(x);
                    off3.Items.Add(x);
                }
                r1.IsEnabled = false;
                r2.IsEnabled = false;
                r3.IsEnabled = false;
                off1.IsEnabled = true;
                off2.IsEnabled = true;
                off3.IsEnabled = true;
                Lock1.IsEnabled = false;
                Lock2.IsEnabled = true;
            }
            else
                MessageBox.Show("Can't have the same rings.");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Lock2.IsEnabled = false;
            off1.IsEnabled = false;
            off2.IsEnabled = false;
            off3.IsEnabled = false;
            input.IsReadOnly = false;

            ///MessageBox.Show(offset[0] + " " + offset[1] + " " + offset[2]);
            ///MessageBox.Show(ringnumbers[0] + " " + ringnumbers[1] + " " + ringnumbers[2]);
            tickMove(0, true);
            tickMove(1, true);
            tickMove(2, true);
        }

        private void r1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ringnumbers[0] = Convert.ToInt32(r1.Items.GetItemAt(r1.SelectedIndex)) + 1;
        }

        private void r2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
  
            ringnumbers[1] = Convert.ToInt32(r2.Items.GetItemAt(r2.SelectedIndex)) + 1;
        }

        private void r3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ringnumbers[2] = Convert.ToInt32(r3.Items.GetItemAt(r3.SelectedIndex)) + 1;
        }

        private void off1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            offset[0] = Convert.ToInt32(off1.Items.GetItemAt(off1.SelectedIndex));
        }

        private void off2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            offset[1] = Convert.ToInt32(off2.Items.GetItemAt(off2.SelectedIndex));
        }

        private void off3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            offset[2] = Convert.ToInt32(off3.Items.GetItemAt(off3.SelectedIndex));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Textthing.Text != "")
                Lock2_Copy.IsEnabled = true;
            else
                Lock2_Copy.IsEnabled = false;   
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            readFile(Textthing.Text);
            r1.IsEnabled = true;
            r2.IsEnabled = true;
            r3.IsEnabled = true;
            Textthing.IsReadOnly = true;
            for(int x = 0; x < ringlimit; x++)
            {
                r1.Items.Add(x);
                r2.Items.Add(x);
                r3.Items.Add(x);
            }
            Lock2_Copy.IsEnabled = false;
            Lock1.IsEnabled = true;           
        }
    }
}
