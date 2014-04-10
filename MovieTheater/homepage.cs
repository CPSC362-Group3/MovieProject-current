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

namespace MovieTheater
{
    public partial class homepage : Form
    {
        bool adminLogged = false;
        bool logged = false;

        public homepage()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            tabControl2.Appearance = TabAppearance.FlatButtons;
            tabControl2.ItemSize = new Size(0, 1);
            tabControl2.SizeMode = TabSizeMode.Fixed;
        }

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

        private void label1_Click(object sender, EventArgs e) //Login Button
        {
            if (logged == false)
                tabControl2.SelectedTab = LoginTab;
            else
            {
                label1.Text = "Home";
                tabControl2.SelectedTab = HomeTab;
            }
        }

        private void label2_Click(object sender, EventArgs e)//Movie Button
        {
            tabControl2.SelectedTab = SearchTab;
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

        private void label4_Click(object sender, EventArgs e) //Clicked Create Account label
        {
            tabControl2.SelectedTab = AccountTab;
        }

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
                    if (textBox1.Text == acc.Username && textBox2.Text == acc.Password)
                    {
                        if (textBox1.Text == "Admin")
                            adminLogged = true;
                        MessageBox.Show("Successfully signed in!");
                        logged = true;
                        label1.Text = "Home";
                        tabControl2.SelectedTab = HomeTab;
                    }
                    else
                        MessageBox.Show("Username/Password entry incorrect.");

                }
            }
            else
                MessageBox.Show("Login in system temporarily unavailable, check back later.");
        }

        private void poster1_Click(object sender, EventArgs e)
        {
            if (adminLogged == true)
            {
                openFileDialog1.ShowDialog();
                poster1.ImageLocation = openFileDialog1.FileName;
            }
        }
    }
}
