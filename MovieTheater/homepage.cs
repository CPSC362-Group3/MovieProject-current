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
        //bool

        string NewReleasesPath = "NewReleases.xml";
        string NowShowingPath = "NowShowing.xml";
        XmlDocument NewReleasesDocument = new XmlDocument();
        XmlDocument NowShowingDocument = new XmlDocument();

        public homepage()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            BodyTabControl.Appearance = TabAppearance.FlatButtons;
            BodyTabControl.ItemSize = new Size(0, 1);
            BodyTabControl.SizeMode = TabSizeMode.Fixed;

            // New Release posters
            NRTitleLabel1.Parent = BackgroundHome;
            NRTitleLabel2.Parent = BackgroundHome;
            NRTitleLabel3.Parent = BackgroundHome;
            NRTitleLabel4.Parent = BackgroundHome;
            NRTitleLabel5.Parent = BackgroundHome;
            NRReleaseDateLabel1.Parent = BackgroundHome;
            NRReleaseDateLabel2.Parent = BackgroundHome;
            NRReleaseDateLabel3.Parent = BackgroundHome;
            NRReleaseDateLabel4.Parent = BackgroundHome;
            NRReleaseDateLabel5.Parent = BackgroundHome;

            // Now Showing posters
            NSTitleLabel1.Parent = BackgroundHome;
            NSTitleLabel2.Parent = BackgroundHome;
            NSTitleLabel3.Parent = BackgroundHome;
            NSTitleLabel4.Parent = BackgroundHome;
            NSTitleLabel5.Parent = BackgroundHome;
            NSTitleLabel6.Parent = BackgroundHome;
            NSTitleLabel7.Parent = BackgroundHome;
            NSTitleLabel8.Parent = BackgroundHome;
            NSTitleLabel9.Parent = BackgroundHome;
            NSTitleLabel10.Parent = BackgroundHome;
            NSReleaseDateLabel1.Parent = BackgroundHome;
            NSReleaseDateLabel2.Parent = BackgroundHome;
            NSReleaseDateLabel3.Parent = BackgroundHome;
            NSReleaseDateLabel4.Parent = BackgroundHome;
            NSReleaseDateLabel5.Parent = BackgroundHome;
            NSReleaseDateLabel6.Parent = BackgroundHome;
            NSReleaseDateLabel7.Parent = BackgroundHome;
            NSReleaseDateLabel8.Parent = BackgroundHome;
            NSReleaseDateLabel9.Parent = BackgroundHome;
            NSReleaseDateLabel10.Parent = BackgroundHome;
            
            //InitHomeTab();

            updateHomePage();



            //checkVisPos(new1);
            //checkVisPos(new2);
            //checkVisPos(new3);
            //checkVisPos(new4);
            //checkVisPos(new5);
            //changeMovie();

            //InfoBar.BringToFront();

        }

        // GLOBAL FUNCTIONS ---------------------------------------------------

        private void AdminLabel_Click(object sender, EventArgs e)
        {
            BodyTabControl.SelectedTab = AdminTab;
        }

        /* Home button click */
        private void homeBtn_Click(object sender, EventArgs e)
        {
            updateHomePage();
            BodyTabControl.SelectedTab = HomeTab;
        }

        /* Home button enter */
        private void homeBtn_MouseEnter(object sender, EventArgs e)
        {
            homeBtn.Image = new Bitmap("../../Resources/home_button_hover.jpg");
        }

        /* Home button exit */
        private void homeBtn_MouseLeave(object sender, EventArgs e)
        {
            homeBtn.Image = new Bitmap("../../Resources/home_button.jpg");
        }


        /* Login button click */
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            BodyTabControl.SelectedTab = LoginTab;
        }

        /* Login button enter */
        private void LoginBtn_MouseEnter(object sender, EventArgs e)
        {
            LoginBtn.Image = new Bitmap("../../Resources/login_button_hover.jpg");
        }

        /* Login button exit */
        private void LoginBtn_MouseLeave(object sender, EventArgs e)
        {
            LoginBtn.Image = new Bitmap("../../Resources/login_button.jpg");
        }

        /* browse button click */
        private void browseBtn_Click(object sender, EventArgs e)
        {

        }

        /* browse button enter */
        private void browseBtn_MouseEnter(object sender, EventArgs e)
        {
            browseBtn.Image = new Bitmap("../../Resources/browse_button_hover.jpg");
        }

        /* browse button exit */
        private void browseBtn_MouseLeave(object sender, EventArgs e)
        {
            browseBtn.Image = new Bitmap("../../Resources/browse_button.jpg");
        }

        /* Contact Us button click */
        private void contactUsBtn_Click(object sender, EventArgs e)
        {

        }

        /* Contact Us button enter */
        private void contactUsBtn_MouseEnter(object sender, EventArgs e)
        {
            contactUsBtn.Image = new Bitmap("../../Resources/contactus_button_hover.jpg");
        }

        /* Contact Us button exit */
        private void contactUsBtn_MouseLeave(object sender, EventArgs e)
        {
            contactUsBtn.Image = new Bitmap("../../Resources/contactus_button.jpg");
        }
        // --------------------------------------------------------------------

        // HOME TAB -----------------------------------------------------------

        public void updateHomePage()
        {
            RefreshNewReleases();
            RefreshNowShowing();
        }

        public void InitHomeTab()
        {
            //If there is no current file, then create a new one
            if (!System.IO.File.Exists(NewReleasesPath))
            {
                // Populate the document.
                XmlDeclaration Declaration = NewReleasesDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                NewReleasesDocument.AppendChild(Declaration);

                XmlComment Comment = NewReleasesDocument.CreateComment("MovieInfo");
                NewReleasesDocument.AppendChild(Comment);

                XmlElement Root = NewReleasesDocument.CreateElement("Movies");
                NewReleasesDocument.AppendChild(Root);

                XmlElement Movie = NewReleasesDocument.CreateElement("Movie");
                Root.AppendChild(Movie);

                XmlElement Title = NewReleasesDocument.CreateElement("Title");
                Movie.AppendChild(Title);
                Title.InnerText = "Movie Title";

                XmlElement Length = NewReleasesDocument.CreateElement("Length");
                Movie.AppendChild(Length);
                Length.InnerText = "1 hours 23 minutes";

                XmlElement Synopsis = NewReleasesDocument.CreateElement("Synopsis");
                Movie.AppendChild(Synopsis);
                Synopsis.InnerText = "A short summary of what the movie is about";

                XmlElement Description = NewReleasesDocument.CreateElement("Description");
                Movie.AppendChild(Description);
                Description.InnerText = "A long description of what the movie is about";

                XmlElement Genre = NewReleasesDocument.CreateElement("Genre");
                Movie.AppendChild(Genre);
                Genre.InnerText = "Movie Genre";

                XmlElement Rating = NewReleasesDocument.CreateElement("Rating");
                Movie.AppendChild(Rating);
                Rating.InnerText = "PG-13.";

                XmlElement Actor = NewReleasesDocument.CreateElement("Actor");
                Movie.AppendChild(Actor);
                Actor.InnerText = "An actor";

                XmlElement Director = NewReleasesDocument.CreateElement("Director");
                Movie.AppendChild(Director);
                Director.InnerText = "Director";

                XmlElement ReleaseDate = NewReleasesDocument.CreateElement("ReleaseDate");
                Movie.AppendChild(ReleaseDate);
                ReleaseDate.InnerText = "Release Date";

                XmlElement PosterPath = NewReleasesDocument.CreateElement("PosterPath");
                Movie.AppendChild(PosterPath);
                PosterPath.InnerText = "Poster Path";

                NewReleasesDocument.Save(NewReleasesPath);
            }
            else
            {
                // load the document
                NewReleasesDocument.Load(NewReleasesPath);

                XmlNodeList elementList = NewReleasesDocument.GetElementsByTagName("Movie");

                if (elementList.Count < 4)
                {
                    Console.WriteLine(elementList.Count);
                    XmlElement Root = NewReleasesDocument.DocumentElement;

                    XmlElement Movie = NewReleasesDocument.CreateElement("Movie");
                    Root.AppendChild(Movie);

                    XmlElement Title = NewReleasesDocument.CreateElement("Title");
                    Movie.AppendChild(Title);
                    Title.InnerText = "Movie Title";

                    XmlElement Length = NewReleasesDocument.CreateElement("Length");
                    Movie.AppendChild(Length);
                    Length.InnerText = "1 hours 23 minutes";

                    XmlElement Synopsis = NewReleasesDocument.CreateElement("Synopsis");
                    Movie.AppendChild(Synopsis);
                    Synopsis.InnerText = "A short summary of what the movie is about";

                    XmlElement Description = NewReleasesDocument.CreateElement("Description");
                    Movie.AppendChild(Description);
                    Description.InnerText = "A long description of what the movie is about";

                    XmlElement Genre = NewReleasesDocument.CreateElement("Genre");
                    Movie.AppendChild(Genre);
                    Genre.InnerText = "Movie Genre";

                    XmlElement Rating = NewReleasesDocument.CreateElement("Rating");
                    Movie.AppendChild(Rating);
                    Rating.InnerText = "PG-13.";

                    XmlElement Actor = NewReleasesDocument.CreateElement("Actor");
                    Movie.AppendChild(Actor);
                    Actor.InnerText = "An actor";

                    XmlElement Director = NewReleasesDocument.CreateElement("Director");
                    Movie.AppendChild(Director);
                    Director.InnerText = "Director";

                    XmlElement ReleaseDate = NewReleasesDocument.CreateElement("ReleaseDate");
                    Movie.AppendChild(ReleaseDate);
                    ReleaseDate.InnerText = "Release Date";

                    XmlElement PosterPath = NewReleasesDocument.CreateElement("PosterPath");
                    Movie.AppendChild(PosterPath);
                    PosterPath.InnerText = "Poster Path";

                    NewReleasesDocument.Save(NewReleasesPath);
                }
            }
            NewReleasesDocument.Load(NewReleasesPath);
        }

        public void RefreshNewReleases () 
        {
            /* make all posters and labels invisible */
            NRReleaseDateLabel1.Visible = false;
            NRReleaseDateLabel2.Visible = false;
            NRReleaseDateLabel3.Visible = false;
            NRReleaseDateLabel4.Visible = false;
            NRReleaseDateLabel5.Visible = false;
            NRTitleLabel1.Visible = false;
            NRTitleLabel2.Visible = false;
            NRTitleLabel3.Visible = false;
            NRTitleLabel4.Visible = false;
            NRTitleLabel5.Visible = false;
            NRPoster1.Visible = false;
            NRPoster2.Visible = false;
            NRPoster3.Visible = false;
            NRPoster4.Visible = false;
            NRPoster5.Visible = false;

            // Load the document. 
            // Load every time in case its modified while the program is running.
            if (System.IO.File.Exists(NewReleasesPath))
            {
                NewReleasesDocument.Load(NewReleasesPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                return;
            }

            // get the needed element lists for displaying the posters
            int numMovies = NewReleasesDocument.GetElementsByTagName("Movie").Count;
            XmlNodeList titleElemList = NewReleasesDocument.GetElementsByTagName("Title");
            XmlNodeList RatingElemList = NewReleasesDocument.GetElementsByTagName("Rating");
            XmlNodeList ReleaseDateElemList = NewReleasesDocument.GetElementsByTagName("ReleaseDate");

            // depending on how many movies we have, fill out the posters information.
            // Make each movie visible after setting the posters information
            if (numMovies >= 1)
            {
                NRReleaseDateLabel1.Text = ReleaseDateElemList[0].InnerText; // release date label
                NRTitleLabel1.Text = titleElemList[0].InnerText + " (" + RatingElemList[0].InnerText + ")";
                NRReleaseDateLabel1.Visible = true; // release date label visible
                NRTitleLabel1.Visible = true; // title label visible
                NRPoster1.Visible = true; // poster visible
            }
            if (numMovies >= 2)
            {
                NRReleaseDateLabel2.Text = ReleaseDateElemList[1].InnerText;
                NRTitleLabel2.Text = titleElemList[1].InnerText + " (" + RatingElemList[1].InnerText + ")";
                NRReleaseDateLabel2.Visible = true;
                NRTitleLabel2.Visible = true;
                NRPoster2.Visible = true;
            }
            if (numMovies >= 3)
            {
                NRReleaseDateLabel3.Text = ReleaseDateElemList[2].InnerText;
                NRTitleLabel3.Text = titleElemList[2].InnerText + " (" + RatingElemList[2].InnerText + ")";
                NRReleaseDateLabel3.Visible = true;
                NRTitleLabel3.Visible = true;
                NRPoster3.Visible = true;
            }
            if (numMovies >= 4)
            {
                NRReleaseDateLabel4.Text = ReleaseDateElemList[3].InnerText;
                NRTitleLabel4.Text = titleElemList[3].InnerText + " (" + RatingElemList[3].InnerText + ")";
                NRReleaseDateLabel4.Visible = true;
                NRTitleLabel4.Visible = true;
                NRPoster4.Visible = true;
            }
            if (numMovies >= 5)
            {
                NRReleaseDateLabel5.Text = ReleaseDateElemList[4].InnerText;
                NRTitleLabel5.Text = titleElemList[4].InnerText + " (" + RatingElemList[4].InnerText + ")";
                NRReleaseDateLabel5.Visible = true;
                NRTitleLabel5.Visible = true;
                NRPoster5.Visible = true;
            }

        }

        public void RefreshNowShowing ()
        {
            /* make all posters and labels invisible */
            NSReleaseDateLabel1.Visible = false;
            NSReleaseDateLabel2.Visible = false;
            NSReleaseDateLabel3.Visible = false;
            NSReleaseDateLabel4.Visible = false;
            NSReleaseDateLabel5.Visible = false;
            NSReleaseDateLabel6.Visible = false;
            NSReleaseDateLabel7.Visible = false;
            NSReleaseDateLabel8.Visible = false;
            NSReleaseDateLabel9.Visible = false;
            NSReleaseDateLabel10.Visible = false;
            NSTitleLabel1.Visible = false;
            NSTitleLabel2.Visible = false;
            NSTitleLabel3.Visible = false;
            NSTitleLabel4.Visible = false;
            NSTitleLabel5.Visible = false;
            NSTitleLabel6.Visible = false;
            NSTitleLabel7.Visible = false;
            NSTitleLabel8.Visible = false;
            NSTitleLabel9.Visible = false;
            NSTitleLabel10.Visible = false;
            NSPoster1.Visible = false;
            NSPoster2.Visible = false;
            NSPoster3.Visible = false;
            NSPoster4.Visible = false;
            NSPoster5.Visible = false;
            NSPoster6.Visible = false;
            NSPoster7.Visible = false;
            NSPoster8.Visible = false;
            NSPoster9.Visible = false;
            NSPoster10.Visible = false;

            // Load the document. 
            // Load every time in case its modified while the program is running.
            if (System.IO.File.Exists(NowShowingPath))
            {
                NowShowingDocument.Load(NowShowingPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                return;
            }

            // get the needed element lists for displaying the posters
            int numMovies = NowShowingDocument.GetElementsByTagName("Movie").Count;
            XmlNodeList titleElemList = NowShowingDocument.GetElementsByTagName("Title");
            XmlNodeList RatingElemList = NowShowingDocument.GetElementsByTagName("Rating");
            XmlNodeList ReleaseDateElemList = NowShowingDocument.GetElementsByTagName("ReleaseDate");

            // depending on how many movies we have, fill out the posters information.
            // Make each movie visible after setting the posters information
            if (numMovies >= 1)
            {
                NSReleaseDateLabel1.Text = ReleaseDateElemList[0].InnerText;
                NSTitleLabel1.Text = titleElemList[0].InnerText + " (" + RatingElemList[0].InnerText + ")";
                NSReleaseDateLabel1.Visible = true;
                NSTitleLabel1.Visible = true;
                NSPoster1.Visible = true;
            }
            if (numMovies >= 2)
            {
                NSReleaseDateLabel2.Text = ReleaseDateElemList[1].InnerText;
                NSTitleLabel2.Text = titleElemList[1].InnerText + " (" + RatingElemList[1].InnerText + ")";
                NSReleaseDateLabel2.Visible = true;
                NSTitleLabel2.Visible = true;
                NSPoster2.Visible = true;
            }
            if (numMovies >= 3)
            {
                NSReleaseDateLabel3.Text = ReleaseDateElemList[2].InnerText;
                NSTitleLabel3.Text = titleElemList[2].InnerText + " (" + RatingElemList[2].InnerText + ")";
                NSReleaseDateLabel3.Visible = true;
                NSTitleLabel3.Visible = true;
                NSPoster3.Visible = true;
            }
            if (numMovies >= 4)
            {
                NSReleaseDateLabel4.Text = ReleaseDateElemList[3].InnerText;
                NSTitleLabel4.Text = titleElemList[3].InnerText + " (" + RatingElemList[3].InnerText + ")";
                NSReleaseDateLabel4.Visible = true;
                NSTitleLabel4.Visible = true;
                NSPoster4.Visible = true;
            }
            if (numMovies >= 5)
            {
                NSReleaseDateLabel5.Text = ReleaseDateElemList[4].InnerText;
                NSTitleLabel5.Text = titleElemList[4].InnerText + " (" + RatingElemList[4].InnerText + ")";
                NSReleaseDateLabel5.Visible = true;
                NSTitleLabel5.Visible = true;
                NSPoster5.Visible = true;
            }
            if (numMovies >= 6)
            {
                NSReleaseDateLabel6.Text = ReleaseDateElemList[5].InnerText;
                NSTitleLabel6.Text = titleElemList[5].InnerText + " (" + RatingElemList[5].InnerText + ")";
                NSReleaseDateLabel1.Visible = true;
                NSTitleLabel1.Visible = true;
                NSPoster1.Visible = true;
            }
            if (numMovies >= 7)
            {
                NSReleaseDateLabel7.Text = ReleaseDateElemList[6].InnerText;
                NSTitleLabel7.Text = titleElemList[6].InnerText + " (" + RatingElemList[6].InnerText + ")";
                NSReleaseDateLabel2.Visible = true;
                NSTitleLabel2.Visible = true;
                NSPoster2.Visible = true;
            }
            if (numMovies >= 8)
            {
                NSReleaseDateLabel8.Text = ReleaseDateElemList[7].InnerText;
                NSTitleLabel8.Text = titleElemList[7].InnerText + " (" + RatingElemList[7].InnerText + ")";
                NSReleaseDateLabel3.Visible = true;
                NSTitleLabel3.Visible = true;
                NSPoster3.Visible = true;
            }
            if (numMovies >= 9)
            {
                NSReleaseDateLabel9.Text = ReleaseDateElemList[8].InnerText;
                NSTitleLabel9.Text = titleElemList[8].InnerText + " (" + RatingElemList[8].InnerText + ")";
                NSReleaseDateLabel4.Visible = true;
                NSTitleLabel4.Visible = true;
                NSPoster4.Visible = true;
            }
            if (numMovies >= 10)
            {
                NSReleaseDateLabel10.Text = ReleaseDateElemList[9].InnerText;
                NSTitleLabel10.Text = titleElemList[9].InnerText + " (" + RatingElemList[9].InnerText + ")";
                NSReleaseDateLabel5.Visible = true;
                NSTitleLabel5.Visible = true;
                NSPoster5.Visible = true;
            }
        }

        // --------------------------------------------------------------------

        // ADMIN TAB ----------------------------------------------------------

        /* Add Movie button click */
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(NowShowingPath))
            {
                XmlDeclaration Declaration = NowShowingDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                XmlComment Comment = NowShowingDocument.CreateComment("MovieInfo");
                XmlElement Root = NowShowingDocument.CreateElement("Movies");

                XmlElement Movie = NowShowingDocument.CreateElement("Movie");
                XmlElement Title = NowShowingDocument.CreateElement("Title");
                XmlElement Length = NowShowingDocument.CreateElement("Length");
                XmlElement Synopsis = NowShowingDocument.CreateElement("Synopsis");
                XmlElement Description = NowShowingDocument.CreateElement("Description");
                XmlElement Genre = NowShowingDocument.CreateElement("Genre");
                XmlElement Rating = NowShowingDocument.CreateElement("Rating");
                XmlElement Actor = NowShowingDocument.CreateElement("Actor");
                XmlElement Director = NowShowingDocument.CreateElement("Director");
                XmlElement ReleaseDate = NowShowingDocument.CreateElement("ReleaseDate");
                XmlElement PosterPath = NowShowingDocument.CreateElement("PosterPath");

                NowShowingDocument.AppendChild(Declaration);
                NowShowingDocument.AppendChild(Comment);
                NowShowingDocument.AppendChild(Root);
                Root.AppendChild(Movie);
                Movie.AppendChild(Title);
                Movie.AppendChild(Length);
                Movie.AppendChild(Synopsis);
                Movie.AppendChild(Description);
                Movie.AppendChild(Genre);
                Movie.AppendChild(Rating);
                Movie.AppendChild(Actor);
                Movie.AppendChild(Director);
                Movie.AppendChild(ReleaseDate);
                Movie.AppendChild(PosterPath);
                
                Title.InnerText = TitleBox.Text;
                Length.InnerText = LengthBox.Text;
                Synopsis.InnerText = SynopsisBox.Text;
                Description.InnerText = DescriptionBox.Text;
                Genre.InnerText = GenreBox.Text;
                Rating.InnerText = RatingBox.Text;
                Actor.InnerText = ActorBox.Text;
                Director.InnerText = DirectorBox.Text;
                ReleaseDate.InnerText = ReleaseDateBox.Text;
                PosterPath.InnerText = PosterPathBox.Text;

                NowShowingDocument.Save(NowShowingPath);
            }
            else
            {
                NowShowingDocument.Load(NowShowingPath);

                XmlElement Root = NowShowingDocument.DocumentElement;
                XmlElement Movie = NowShowingDocument.CreateElement("Movie");
                XmlElement Title = NowShowingDocument.CreateElement("Title");
                XmlElement Length = NowShowingDocument.CreateElement("Length");
                XmlElement Synopsis = NowShowingDocument.CreateElement("Synopsis");
                XmlElement Description = NowShowingDocument.CreateElement("Description");
                XmlElement Genre = NowShowingDocument.CreateElement("Genre");
                XmlElement Rating = NowShowingDocument.CreateElement("Rating");
                XmlElement Actor = NowShowingDocument.CreateElement("Actor");
                XmlElement Director = NowShowingDocument.CreateElement("Director");
                XmlElement ReleaseDate = NowShowingDocument.CreateElement("ReleaseDate");
                XmlElement PosterPath = NowShowingDocument.CreateElement("PosterPath");

                Root.AppendChild(Movie);
                Movie.AppendChild(Title);
                Movie.AppendChild(Length);
                Movie.AppendChild(Synopsis);
                Movie.AppendChild(Description);
                Movie.AppendChild(Genre);
                Movie.AppendChild(Rating);
                Movie.AppendChild(Actor);
                Movie.AppendChild(Director);
                Movie.AppendChild(ReleaseDate);
                Movie.AppendChild(PosterPath);

                Title.InnerText = TitleBox.Text;
                Length.InnerText = LengthBox.Text;
                Synopsis.InnerText = SynopsisBox.Text;
                Description.InnerText = DescriptionBox.Text;
                Genre.InnerText = GenreBox.Text;
                Rating.InnerText = RatingBox.Text;
                Actor.InnerText = ActorBox.Text;
                Director.InnerText = DirectorBox.Text;
                ReleaseDate.InnerText = ReleaseDateBox.Text;
                PosterPath.InnerText = PosterPathBox.Text;

                NowShowingDocument.Save(NowShowingPath);
            }

            TitleBox.Clear();
            LengthBox.Clear();
            SynopsisBox.Clear();
            DescriptionBox.Clear();
            GenreBox.ResetText();
            RatingBox.ResetText();
            ActorBox.Clear();
            DirectorBox.Clear();
            ReleaseDateBox.Clear();
            PosterPathBox.Clear();

        }
        // --------------------------------------------------------------------



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
                            NRPoster1.ImageLocation = m.pos;
                            NRPoster1.Visible = true;
                            break;
                        case 1:
                            NRPoster2.ImageLocation = m.pos;
                            NRPoster2.Visible = true;
                            break;
                        case 2:
                            NRPoster3.ImageLocation = m.pos;
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
                BodyTabControl.SelectedTab = MovieDetailsTab;
                //movDetails.ImageLocation = pos.ImageLocation;
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
                BodyTabControl.SelectedTab = PaymentInfoTab;
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

            BodyTabControl.SelectedTab = LoginTab; //Return to login screen

        }
        //----------------------------------------------------------------------------------------------------
        //Login Page
       /* private void label1_Click(object sender, EventArgs e) //Login Button
        {
            if (logged == false)
                BodyTabControl.SelectedTab = LoginTab;
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
        }*/

        private void label4_Click(object sender, EventArgs e) //Clicked Create Account label
        {
            BodyTabControl.SelectedTab = AccountTab;
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
                        //logBtn.Text = "Log Out";
                        BodyTabControl.SelectedTab = HomeTab;
                        checkVisPos(NRPoster1);
                        checkVisPos(NRPoster2);
                        checkVisPos(NRPoster3);
                        checkVisPos(NRPoster4);
                        checkVisPos(NRPoster5);

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


        //----------------------------------------------------------------------------------------------------
        // MOVIE DETAILS
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyTabControl.SelectedTab = Seating;
        }


        //-----------------------------------------------------------------------------------------------------
        //BUY SEATING PAGE
        private void button1_Click_1(object sender, EventArgs e)
        {
            //BUTTON TAKES USERS TO THE BUY TICKETS PAGE
            BodyTabControl.SelectedTab = Ticket;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ADULT LABEL
            double adultTickets;
            double totalPrice;
            int ticketA;
            adultTickets = Convert.ToDouble(comboBox1.Text);
            ticketA = Convert.ToInt32(comboBox1.Text);
            totalPrice = 10 * adultTickets;
            label28.Text = totalPrice.ToString("$0.00");
            label38.Text = totalPrice.ToString("0.00");
            label55.Text = ticketA.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SENIOR LABEL
            double seniorTickets;
            double totalPrice;
            int ticketS;
            seniorTickets = Convert.ToDouble(comboBox2.Text);
            ticketS = Convert.ToInt32(comboBox2.Text);
            totalPrice = 9 * seniorTickets;
            label29.Text = totalPrice.ToString("$0.00");
            label39.Text = totalPrice.ToString("0.00");
            label54.Text = ticketS.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CHILD LABEL
            double childTickets;
            double totalPriceChild;
            int ticketC;
            childTickets = Convert.ToDouble(comboBox3.Text);
            ticketC = Convert.ToInt32(comboBox3.Text);
            totalPriceChild = 9 * childTickets;
            label30.Text = totalPriceChild.ToString("$0.00");
            label40.Text = totalPriceChild.ToString("0.00");
            label53.Text = ticketC.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //BUY TICKETS OFFICIALLY
            //TAKES YOU TO PAYMENT PAGE
            BodyTabControl.SelectedTab = Purchase;
            
            //SETS UP EVERYTHING ON THE NEXT PAGE
            double subtotal, child, senior, adult;
            child = Convert.ToDouble(label40.Text);
            senior = Convert.ToDouble(label39.Text);
            adult = Convert.ToDouble(label38.Text);
            subtotal = child + senior + adult;
            label34.Text = subtotal.ToString("$0.00");
        }


        //------------------------------------------------------------------------------------------------------
        //  PURCHASE PAGE
        private void button5_Click(object sender, EventArgs e)
        {
            //Officially buys tickets
            MessageBox.Show("You have purchased tickets!");
            BodyTabControl.SelectedTab = HomeTab;
            comboBox1.ResetText();
            comboBox2.ResetText();
            comboBox3.ResetText();
        }

        //-----------------------------------------------------------------------------------------------------
    }
}
