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
            if ((string.IsNullOrEmpty(textBox7.Text)) || (string.IsNullOrEmpty(textBox8.Text))
                || (string.IsNullOrEmpty(textBox9.Text)) || (string.IsNullOrEmpty(textBox10.Text))
                || (string.IsNullOrEmpty(textBox11.Text)) || (string.IsNullOrEmpty(textBox12.Text))
                || (string.IsNullOrEmpty(textBox13.Text)))
                MessageBox.Show("Required Fields Missing, please enter data.");

            else
            {
                tabControl2.SelectedTab = PaymentInfoTab;
            }

        }

        private void label1_Click(object sender, EventArgs e) //Login Button
        {
            tabControl2.SelectedTab = LoginTab;
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
                fName.Value = textBox5.Text;
                lName.Value = textBox6.Text;
                address.InnerText = textBox7.Text;
                city.InnerText = textBox8.Text;
                state.InnerText = textBox9.Text;
                username.InnerText = textBox10.Text;
                password.InnerText = textBox11.Text;



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
                fName.Value = textBox5.Text;
                lName.Value = textBox6.Text;
                address.InnerText = textBox7.Text;
                city.InnerText = textBox8.Text;
                state.InnerText = textBox9.Text;
                username.InnerText = textBox10.Text;
                password.InnerText = textBox11.Text;

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
                        MessageBox.Show("Successfully signed in!");
                    }
                    else
                        MessageBox.Show("Username/Password entry incorrect.");

                }
            }
            else
                MessageBox.Show("Login in system temporarily unavailable, check back later.");
        }
    }
}
