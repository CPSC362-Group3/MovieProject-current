using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using MovieTheater.Properties;

namespace MovieTheater
{
    public partial class homepage : Form
    {
        bool adminLogged = false;
        bool logged = false;
        bool addBtns = false;

        public homepage()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            tabControl2.Appearance = TabAppearance.FlatButtons;
            tabControl2.ItemSize = new Size(0, 1);
            tabControl2.SizeMode = TabSizeMode.Fixed;

            checkVisPos(new1);
            checkVisPos(new2);
            checkVisPos(new3);
            checkVisPos(new4);
            checkVisPos(new5);
            changeMovie();

            InfoBar.BringToFront();

        }

        //Helper Functions---------------------------------------------------------------------------------

        //Changes the movie poster
        public void changeMovie()
        {
            if (File.Exists("movieInfo.xml"))
            {
                int iter = 0;
                XDocument movie = XDocument.Load("movieInfo.xml");
                var movInfo = from m in movie.Descendants("Movie")
                select new
                {
                   pos = m.Element("Poster").Value,
                };

                foreach (var m in movInfo)
                {
                    switch (iter)
                    {
                        case 0:
                            new1.ImageLocation = m.pos;
                            new1.Visible = true;
                            break;
                        case 1:
                            new2.ImageLocation = m.pos;
                            new2.Visible = true;
                            break;
                        case 2:
                            new3.ImageLocation = m.pos;
                            break;
                    }
                    iter++;
                }
            }
        }

        //Make posters visible only when added by admin
        public void checkVisPos(PictureBox pos)
        {
            if (pos.Image == null)
            {
                if (adminLogged == true)
                    pos.Visible = true;
                else
                    pos.Visible = false;
            }
            else
                pos.Visible = true;
        }

        //Save Poster location to file, loads file for use next time.
        public void savePoster(string Filename)
        {
            string PATH = "movieInfo.xml";
            XmlDocument doc = new XmlDocument();

            //If there is no current file, then create a new one
            if (Filename != null || Filename != "openFileDialog1")
            {
                if (!System.IO.File.Exists(PATH))
                {
                    //Create neccessary nodes
                    XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    XmlComment comment = doc.CreateComment("Movie Info");
                    XmlElement root = doc.CreateElement("Movies");
                    XmlElement mov = doc.CreateElement("Movie");
                    XmlElement pos = doc.CreateElement("Poster");

                    //Add the values for each nodes
                    pos.InnerText = Filename;

                    //Construct the document
                    doc.AppendChild(declaration);
                    doc.AppendChild(comment);
                    doc.AppendChild(root);
                    root.AppendChild(mov);
                    mov.AppendChild(pos);

                    doc.Save(PATH);
                }
                else //If there is already a file
                {
                    //Load the XML File
                    doc.Load(PATH);

                    //Get the root element
                    XmlElement root = doc.DocumentElement;
                    XmlElement mov = doc.CreateElement("Movie");
                    XmlElement pos = doc.CreateElement("Poster");

                    //Add the values for each nodes
                    pos.InnerText = Filename;


                    //Add the New person element to the end of the root element
                    root.AppendChild(mov);
                    mov.AppendChild(pos);

                    //Save the document
                    doc.Save(PATH);
                }
            }
        }

        //Allows Admin to change the poster and load it from a file.
        public void changePoster(PictureBox pos)
        {
            if (adminLogged == true)
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName == "openFileDialog1")
                    pos.ImageLocation = null;
                else
                    pos.ImageLocation = openFileDialog1.FileName;
                savePoster(pos.ImageLocation);
            }

            else if (adminLogged == false)
            {
                tabControl2.SelectedTab = MovieDetailsTab;
                movDetails.ImageLocation = pos.ImageLocation;
            }
        }

        //--------------------------------------------------------------------------------------------------
        //Create Account Page
        private void button3_Click(object sender, EventArgs e) //Next button
        {
            if ((string.IsNullOrEmpty(AddrTxt.Text)) || (string.IsNullOrEmpty(CityTxt.Text))
                || (string.IsNullOrEmpty(StateTxt.Text)) || (string.IsNullOrEmpty(UserTxt.Text))
                || (string.IsNullOrEmpty(passTxt.Text)) || (string.IsNullOrEmpty(lNameTxt.Text))
                || (string.IsNullOrEmpty(fNameTxt.Text)))
                MessageBox.Show("Required Fields Missing, please enter data.");

            else
            {
                tabControl2.SelectedTab = PaymentInfoTab;
            }

        }

        //Enter Payment information to finish create account. Page inaccessible unless previous account info filled.
        private void button2_Click(object sender, EventArgs e)
        {
            string PATH = "accountInfo.xml";
            XmlDocument doc = new XmlDocument();

            //If there is no current file, then create a new one
            if (!System.IO.File.Exists(PATH))
            {
                //Create neccessary nodes
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                XmlComment comment = doc.CreateComment("Account Info");
                XmlElement root = doc.CreateElement("Accounts");
                XmlElement acc = doc.CreateElement("Account");
                XmlAttribute fName = doc.CreateAttribute("First_Name");
                XmlAttribute lName = doc.CreateAttribute("Last_Name");
                XmlElement address = doc.CreateElement("Address");
                XmlElement city = doc.CreateElement("City");
                XmlElement state = doc.CreateElement("State");

                XmlElement username = doc.CreateElement("Username");
                XmlElement password = doc.CreateElement("Password");

                //Add the values for each nodes
                fName.Value = fNameTxt.Text;
                lName.Value = lNameTxt.Text;
                address.InnerText = AddrTxt.Text;
                city.InnerText = CityTxt.Text;
                state.InnerText = StateTxt.Text;
                username.InnerText = UserTxt.Text;
                password.InnerText = passTxt.Text;



                //Construct the document
                doc.AppendChild(declaration);
                doc.AppendChild(comment);
                doc.AppendChild(root);
                root.AppendChild(acc);
                acc.Attributes.Append(fName);
                acc.Attributes.Append(lName);
                acc.AppendChild(address);
                acc.AppendChild(city);
                acc.AppendChild(state);
                acc.AppendChild(username);
                acc.AppendChild(password);

                doc.Save(PATH);
            }
            else //If there is already a file
            {
                //Load the XML File
                doc.Load(PATH);

                //Get the root element
                XmlElement root = doc.DocumentElement;

                XmlElement acc = doc.CreateElement("Account");
                XmlAttribute fName = doc.CreateAttribute("First_Name");
                XmlAttribute lName = doc.CreateAttribute("Last_Name");
                XmlElement address = doc.CreateElement("Address");
                XmlElement city = doc.CreateElement("City");
                XmlElement state = doc.CreateElement("State");

                XmlElement username = doc.CreateElement("Username");
                XmlElement password = doc.CreateElement("Password");

                //Add the values for each nodes
                fName.Value = fNameTxt.Text;
                lName.Value = lNameTxt.Text;
                address.InnerText = AddrTxt.Text;
                city.InnerText = CityTxt.Text;
                state.InnerText = StateTxt.Text;
                username.InnerText = UserTxt.Text;
                password.InnerText = passTxt.Text;

                //Construct the Person element
                acc.Attributes.Append(fName);
                acc.Attributes.Append(lName);
                acc.AppendChild(address);
                acc.AppendChild(city);
                acc.AppendChild(state);
                acc.AppendChild(username);
                acc.AppendChild(password);

                //Add the New person element to the end of the root element
                root.AppendChild(acc);

                //Save the document
                doc.Save(PATH);
            }
            MessageBox.Show("Successfully created account!");

            tabControl2.SelectedTab = LoginTab; //Return to login screen

        }
        //----------------------------------------------------------------------------------------------------
        //Login Page
        private void label1_Click(object sender, EventArgs e) //Login Button
        {
            if (logged == false)
                tabControl2.SelectedTab = LoginTab;
            else
            {
                logBtn.Text = "Log Out";
                logged = false;
                adminLogged = false;
                MessageBox.Show("Successfully logged out");
                logBtn.Text = "Log in";
                usernameTxt.Clear();
                passwordTxt.Clear();
                changeMovie();
                checkVisPos(new1);
                checkVisPos(new2);
                checkVisPos(new3);
                checkVisPos(new4);
                checkVisPos(new5);


            }
        }

        private void label4_Click(object sender, EventArgs e) //Clicked Create Account label
        {
            tabControl2.SelectedTab = AccountTab;
        }

        //Log into system with a created account
        private void button1_Click(object sender, EventArgs e) //Clicked Login Button
        {
            if (File.Exists("accountInfo.xml"))
            {
                XDocument accounts = XDocument.Load("accountInfo.xml");
                var accInfo = from acc in accounts.Descendants("Account")
                              select new
                              {
                                  Username = acc.Element("Username").Value,
                                  Password = acc.Element("Password").Value,
                              };
                foreach (var acc in accInfo)
                {
                    if (usernameTxt.Text == acc.Username && passwordTxt.Text == acc.Password)
                    {
                        if (usernameTxt.Text == "Admin")
                        {
                            adminLogged = true;
                            addBtns = true;
                        }
                        MessageBox.Show("Successfully signed in!");
                        logged = true;
                        logBtn.Text = "Log Out";
                        tabControl2.SelectedTab = HomeTab;
                        checkVisPos(new1);
                        checkVisPos(new2);
                        checkVisPos(new3);
                        checkVisPos(new4);
                        checkVisPos(new5);

                        break;
                    }
                    else
                        continue;
                }
                if (logged == false)
                    MessageBox.Show("Incorrect username/password. Please try again.");
            }
            else
                MessageBox.Show("Login in system temporarily unavailable, check back later.");
        }
        //-----------------------------------------------------------------------------------------------------
        //Movie button always up, access from any page.
        private void label2_Click(object sender, EventArgs e)//Movie Button
        {
            tabControl2.SelectedTab = HomeTab;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            tabControl2.SelectedTab = SearchTab;
        }

        //----------------------------------------------------------------------------------------------------------------
        //Admin clicks on poster to change it.
        private void new1_Click(object sender, EventArgs e)
        {
            changePoster(new1);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            changePoster(new2);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            changePoster(new3);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            changePoster(new4);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            changePoster(new5);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            changePoster(now1);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            changePoster(now2);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            changePoster(now3);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            changePoster(now4);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            changePoster(now5);
        }

        //----------------------------------------------------------------------------------------------------
        // MOVIE DETAILS
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl2.SelectedTab = Seating;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl2.SelectedTab = Seating;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl2.SelectedTab = Seating;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl2.SelectedTab = Seating;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl2.SelectedTab = Seating;
        }


        //-----------------------------------------------------------------------------------------------------
        //BUY SEATING PAGE
        private void button1_Click_1(object sender, EventArgs e)
        {
            //BUTTON TAKES USERS TO THE BUY TICKETS PAGE
            tabControl2.SelectedTab = Ticket;
        }

        private void Ticket_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {
            //ADULT LABEL

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ADULT LABEL
            double adultTickets;
            double totalPrice;
            adultTickets = Convert.ToDouble(comboBox1.Text);
            totalPrice = 10 * adultTickets;
            label28.Text = totalPrice.ToString("$0.00");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SENIOR LABEL
            double seniorTickets;
            double totalPrice;
            seniorTickets = Convert.ToDouble(comboBox2.Text);
            totalPrice = 10 * seniorTickets;
            label29.Text = totalPrice.ToString("$0.00");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CHILD LABEL
            double childTickets;
            double totalPrice;
            childTickets = Convert.ToDouble(comboBox3.Text);
            totalPrice = 10 * childTickets;
            label30.Text = totalPrice.ToString("$0.00");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //BUY TICKETS OFFICIALLY
            //TAKES YOUR TO PAYMENT PAGE
            tabControl2.SelectedTab = Purchase;
        }

        //-----------------------------------------------------------------------------------------------------
    }
}
