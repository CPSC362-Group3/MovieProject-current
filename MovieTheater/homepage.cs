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
using MovieTheater;
using MovieTheatre;

namespace MovieTheater
{
    public partial class homepage : Form
    {
        //Boolean Flags
        bool adminLogged = false;
        bool logged = false;
        bool editInfo = false;
        
        //Int counters
        int currentInd = 0;
        int showtimes = 2;
        int[] track = new int[72];

        string currentU, currentfN, currentlN;

        //String file paths, xml related
        string MoviesPath = "../../xml/Movies.xml";
        string AccountsPath = "../../xml/accountInfo.xml";
        string TransactionsPath = "../../xml/transactions.xml";
        XmlDocument MoviesDocument = new XmlDocument();
        XmlDocument TransactionsDocument = new XmlDocument();
        string search = ""; // search bar string
         

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

            // Movie details page.
            MDSReleaseDate.Parent = backgroundMD;
            MDReleaseLabel.Parent = backgroundMD;
            MDSLength.Parent = backgroundMD;
            MDLengthLabel.Parent = backgroundMD;
            MDSRating.Parent = backgroundMD;
            MDRatingLabel.Parent = backgroundMD;
            MDSGenre.Parent = backgroundMD;
            MDGenreLabel.Parent = backgroundMD;
            MDSActors.Parent = backgroundMD;
            MDSDirector.Parent = backgroundMD;
            MDActorsLabel.Parent = backgroundMD;
            MDDirectorLabel.Parent = backgroundMD;
            MDSSynopsis.Parent = backgroundMD;
            MDSynopsisLabel.Parent = backgroundMD;

            STitleLabel1.Parent = backgroundS;
            STitleLabel2.Parent = backgroundS;
            STitleLabel3.Parent = backgroundS;
            STitleLabel4.Parent = backgroundS;
            STitleLabel5.Parent = backgroundS;
            STitleLabel6.Parent = backgroundS;

            // contact us page
            CallusLabel.Parent = backgroundCU;
            phoneLabel.Parent = backgroundCU;
            phoneNumberLabel.Parent = backgroundCU;
            HoursLabel.Parent = backgroundCU;
            MonLabel.Parent = backgroundCU;
            TueLabel.Parent = backgroundCU;
            WedLabel.Parent = backgroundCU;
            ThuLabel.Parent = backgroundCU;
            FriLabel.Parent = backgroundCU;
            SatLabel.Parent = backgroundCU;
            SunLabel.Parent = backgroundCU;
            EmailLabel.Parent = backgroundCU;
            emailInfoLabel.Parent = backgroundCU;
            addresslabel.Parent = backgroundCU;
            addressInfo1Label.Parent = backgroundCU;
            addressInfo2Label.Parent = backgroundCU;
            MonTimeLabel.Parent = backgroundCU;
            TueTimeLabel.Parent = backgroundCU;
            WedTimeLabel.Parent = backgroundCU;
            FriTimeLabel.Parent = backgroundCU;
            SatTimeLabel.Parent = backgroundCU;
            SunTimeLabel.Parent = backgroundCU;
            ThuTimeLabel.Parent = backgroundCU;

            // account creation page
            CAPersonalInfoLabel.Parent = backgroundCA;
            CAFirstNameLabel.Parent = backgroundCA;
            CALastNameLabel.Parent = backgroundCA;
            CAAddressLabel.Parent = backgroundCA;
            CACityLabel.Parent = backgroundCA;
            CAStateLabel.Parent = backgroundCA;
            CAAccountInfoLabel.Parent = backgroundCA;
            CAUsernameLabel.Parent = backgroundCA;
            CAPasswordLabel.Parent = backgroundCA;
            CACreditCardInfoLabel.Parent = backgroundCA;
            CACardholderFNLabel.Parent = backgroundCA;
            CACardholderLNLabel.Parent = backgroundCA;
            CACreditCardLabel.Parent = backgroundCA;
            CASecurityLabel.Parent = backgroundCA;
            CAExpirationDateLabel.Parent = backgroundCA;

            displayDatelbl.Text = DateTime.Today.ToLongDateString();

            updateHomePage();

        }
//------------------------------------------------------------------------------------------------------------------------
// GLOBAL FUNCTIONS, Main button controls ///////////////////////////////////////////////////////////////////////////////
//------------------------------------------------------------------------------------------------------------------------

        // get movie title from a label with a rating on it. utility function.
        public string GetMovieTitle(string title)
        {
            int end = title.IndexOf(" (");
            if (end < 0)
            {
                Console.WriteLine("Error invalid movie: {0}", title);
                return null;
            }
            return title.Substring(0, end);
        }

        /* Movies Search bar keydown. - activates the search button when enter is pressed */
        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchBtn_Click(this, e);
                //e.Handled = true;
                //e.SuppressKeyPress = true;
            }
        }

        /* Search Movie button click. Once clicked changes to search tab and searches the string */
        private void searchBtn_Click(object sender, EventArgs e)
        {
            search = searchBar.Text.ToLower();

            // search is the string is not null and does not only contain whitespace.
            if (!String.IsNullOrWhiteSpace(search) && search.Length > 2)
            {
                RefreshSearchMovies(search);
                BodyTabControl.SelectedTab = SearchTab;
            }
            searchBar.Clear();
        }

        /* Admin label click */
        private void AdminLabel_Click(object sender, EventArgs e)
        {
            if (adminLogged == true)
                BodyTabControl.SelectedTab = AdminCtrl;
            else
                BodyTabControl.SelectedTab = LoginTab;
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
            BodyTabControl.SelectedTab = SearchTab;
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
            BodyTabControl.SelectedTab = ContactUsTab;
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

//---------------------------------------------------------------------------------------------------------------------
//Update and refresh movie info////////////////////////////////////////////////////////////////////////////////////////
//----------------------------------------------------------------------------------------------------------------------

        public void updateHomePage()
        {
            RefreshNewReleases();
            RefreshNowShowing();
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
            if (System.IO.File.Exists(MoviesPath))
            {
                MoviesDocument.Load(MoviesPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                return;
            }

            int index = 0;
            bool addMovie = false;
            // get the needed element lists for displaying the posters
            int numMovies = MoviesDocument.GetElementsByTagName("Movie").Count;
            XmlNodeList titleElemList = MoviesDocument.GetElementsByTagName("Title");
            XmlNodeList RatingElemList = MoviesDocument.GetElementsByTagName("Rating");
            XmlNodeList ReleaseDateElemList = MoviesDocument.GetElementsByTagName("ReleaseDate");
            XmlNodeList PosterElemList = MoviesDocument.GetElementsByTagName("PosterPath");
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime;

            // depending on how many movies we have, fill out the posters information.
            // Make each movie visible after setting the posters information

            // ----------------------------------------------------------------
            // this for loop checks if a movie has came out or not based on
            // the current date and the movies date.
            // index is the index of the first movie that has not came out yet.
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) > 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            // We use index here to make the poster visible.
            // We also set the posters information based on this movie.
            if (addMovie) // 1
            {
                NRReleaseDateLabel1.Text = ReleaseDateElemList[index].InnerText; // release date label
                NRTitleLabel1.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NRReleaseDateLabel1.Visible = true; // release date label visible
                NRTitleLabel1.Visible = true; // title label visible
                NRPoster1.Visible = true; // poster visible
                NRPoster1.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            // The next for loop starts with the next element in the lists,
            // so we increment index and continue the foor loop from there.
            // these for loops go until i < numMovies.
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) > 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 2
            {
                NRReleaseDateLabel2.Text = ReleaseDateElemList[index].InnerText;
                NRTitleLabel2.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NRReleaseDateLabel2.Visible = true;
                NRTitleLabel2.Visible = true;
                NRPoster2.Visible = true;
                NRPoster2.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) > 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 3
            {
                NRReleaseDateLabel3.Text = ReleaseDateElemList[index].InnerText;
                NRTitleLabel3.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NRReleaseDateLabel3.Visible = true;
                NRTitleLabel3.Visible = true;
                NRPoster3.Visible = true;
                NRPoster3.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) > 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 4
            {
                NRReleaseDateLabel4.Text = ReleaseDateElemList[index].InnerText;
                NRTitleLabel4.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NRReleaseDateLabel4.Visible = true;
                NRTitleLabel4.Visible = true;
                NRPoster4.Visible = true;
                NRPoster4.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) > 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 5
            {
                NRReleaseDateLabel5.Text = ReleaseDateElemList[index].InnerText;
                NRTitleLabel5.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NRReleaseDateLabel5.Visible = true;
                NRTitleLabel5.Visible = true;
                NRPoster5.Visible = true;
                NRPoster5.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------

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
            if (System.IO.File.Exists(MoviesPath))
            {
                MoviesDocument.Load(MoviesPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                return;
            }

            int index = 0;
            bool addMovie = false;

            // get the needed element lists for displaying the posters
            int numMovies = MoviesDocument.GetElementsByTagName("Movie").Count;
            XmlNodeList titleElemList = MoviesDocument.GetElementsByTagName("Title");
            XmlNodeList RatingElemList = MoviesDocument.GetElementsByTagName("Rating");
            XmlNodeList ReleaseDateElemList = MoviesDocument.GetElementsByTagName("ReleaseDate");
            XmlNodeList PosterElemList = MoviesDocument.GetElementsByTagName("PosterPath");
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime;

            // depending on how many movies we have, fill out the posters information.
            // Make each movie visible after setting the posters information
            // ----------------------------------------------------------------
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 1
            {
                NSTitleLabel1.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel1.Visible = true;
                NSTitleLabel1.Visible = true;
                NSPoster1.Visible = true;
                NSPoster1.ImageLocation = PosterElemList[index].InnerText;
                NSPoster1.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 2
            {
                NSTitleLabel2.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel2.Visible = true;
                NSTitleLabel2.Visible = true;
                NSPoster2.Visible = true;
                NSPoster2.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 3
            {
                NSTitleLabel3.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel3.Visible = true;
                NSTitleLabel3.Visible = true;
                NSPoster3.Visible = true;
                NSPoster3.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 4
            {
                NSTitleLabel4.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel4.Visible = true;
                NSTitleLabel4.Visible = true;
                NSPoster4.Visible = true;
                NSPoster4.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 5
            {
                NSTitleLabel5.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel5.Visible = true;
                NSTitleLabel5.Visible = true;
                NSPoster5.Visible = true;
                NSPoster5.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 6
            {
                NSTitleLabel6.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel1.Visible = true;
                NSTitleLabel1.Visible = true;
                NSPoster1.Visible = true;
                NSPoster6.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 7
            {
                NSTitleLabel7.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel2.Visible = true;
                NSTitleLabel2.Visible = true;
                NSPoster2.Visible = true;
                NSPoster7.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 8
            {
                NSTitleLabel8.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel3.Visible = true;
                NSTitleLabel3.Visible = true;
                NSPoster3.Visible = true;
                NSPoster8.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 9
            {
                NSTitleLabel9.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel4.Visible = true;
                NSTitleLabel4.Visible = true;
                NSPoster4.Visible = true;
                NSPoster9.ImageLocation = PosterElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[i].InnerText);
                if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 10
            {
                NSTitleLabel10.Text = titleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
                NSReleaseDateLabel5.Visible = true;
                NSTitleLabel5.Visible = true;
                NSPoster5.Visible = true;
                NSPoster10.ImageLocation = PosterElemList[index].InnerText;
            }
        }

//---------------------------------------------------------------------------------------------------------------------
//Search Movie page ///////////////////////////////////////////////////////////////////////////////////////////////////
//---------------------------------------------------------------------------------------------------------------------

        public void RefreshSearchMovies(string search)
        {
            searchPoster1.Visible = false;
            searchPoster2.Visible = false;
            searchPoster3.Visible = false;
            searchPoster4.Visible = false;
            searchPoster5.Visible = false;
            searchPoster6.Visible = false;
            STitleLabel1.Visible = false;
            STitleLabel2.Visible = false;
            STitleLabel3.Visible = false;
            STitleLabel4.Visible = false;
            STitleLabel5.Visible = false;
            STitleLabel6.Visible = false;

            // check if a file exists
            if (System.IO.File.Exists(MoviesPath))
            {
                MoviesDocument.Load(MoviesPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                return;
            }

            int index = 0;
            bool addMovie = false;

            int numMovies = MoviesDocument.GetElementsByTagName("Movie").Count;
            XmlNodeList TitleElemList = MoviesDocument.GetElementsByTagName("Title");
            XmlNodeList LengthElemList = MoviesDocument.GetElementsByTagName("Length");
            XmlNodeList RatingElemList = MoviesDocument.GetElementsByTagName("Rating");
            XmlNodeList GenreElemList = MoviesDocument.GetElementsByTagName("Genre");
            XmlNodeList ReleaseElemList = MoviesDocument.GetElementsByTagName("ReleaseDate");
            XmlNodeList ActorsElemList = MoviesDocument.GetElementsByTagName("Actor");
            XmlNodeList DirectorElemList = MoviesDocument.GetElementsByTagName("Director");
            XmlNodeList SynopsisElemList = MoviesDocument.GetElementsByTagName("Synopsis");
            XmlNodeList PosterPath = MoviesDocument.GetElementsByTagName("PosterPath");
            XmlNodeList Showtime1List = MoviesDocument.GetElementsByTagName("Showtime1");
            XmlNodeList Showtime2List = MoviesDocument.GetElementsByTagName("Showtime2");
            XmlNodeList Showtime3List = MoviesDocument.GetElementsByTagName("Showtime3");
            XmlNodeList Showtime4List = MoviesDocument.GetElementsByTagName("Showtime4");
            XmlNodeList Showtime5List = MoviesDocument.GetElementsByTagName("Showtime5");
            XmlNodeList Showtime6List = MoviesDocument.GetElementsByTagName("Showtime6");
            XmlNodeList Showtime7List = MoviesDocument.GetElementsByTagName("Showtime7");
            XmlNodeList Showtime8List = MoviesDocument.GetElementsByTagName("Showtime8");
            XmlNodeList Showtime9List = MoviesDocument.GetElementsByTagName("Showtime9");

            // depending on how many movies we have, fill out the posters information.
            // Make each movie visible after setting the posters information
            // ----------------------------------------------------------------
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 1
            {
                searchPoster1.Visible = true;
                STitleLabel1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText + " (" + RatingElemList[index].InnerText + ")";
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 2
            {
                searchPoster2.Visible = true;
                STitleLabel2.Visible = true;
                STitleLabel2.Text = TitleElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 3
            {
                searchPoster1.Visible = true;
                STitleLabel1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 4
            {
                searchPoster1.Visible = true;
                STitleLabel1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 5
            {
                searchPoster1.Visible = true;
                STitleLabel1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            index++;
            addMovie = false;
            for (int i = index; i < numMovies; i++)
            {
                if (TitleElemList[i].InnerText.ToLower().Contains(search) ||
                    LengthElemList[i].InnerText.ToLower().Contains(search) ||
                    RatingElemList[i].InnerText.ToLower().Contains(search) ||
                    GenreElemList[i].InnerText.ToLower().Contains(search) ||
                    ReleaseElemList[i].InnerText.ToLower().Contains(search) ||
                    ActorsElemList[i].InnerText.ToLower().Contains(search) ||
                    DirectorElemList[i].InnerText.ToLower().Contains(search) ||
                    SynopsisElemList[i].InnerText.ToLower().Contains(search) ||
                    PosterPath[i].InnerText.ToLower().Contains(search) ||
                    Showtime1List[i].InnerText.ToLower().Contains(search) ||
                    Showtime2List[i].InnerText.ToLower().Contains(search) ||
                    Showtime3List[i].InnerText.ToLower().Contains(search) ||
                    Showtime4List[i].InnerText.ToLower().Contains(search) ||
                    Showtime5List[i].InnerText.ToLower().Contains(search) ||
                    Showtime6List[i].InnerText.ToLower().Contains(search) ||
                    Showtime7List[i].InnerText.ToLower().Contains(search) ||
                    Showtime8List[i].InnerText.ToLower().Contains(search) ||
                    Showtime9List[i].InnerText.ToLower().Contains(search))
                {
                    index = i;
                    addMovie = true;
                    break;
                }
            }
            if (addMovie) // 6
            {
                searchPoster1.Visible = true;
                STitleLabel1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
        }

        private void searchPoster1_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel1.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster2_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel2.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster3_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel3.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster4_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel4.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster5_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel5.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster6_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(STitleLabel6.Text));
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

//---------------------------------------------------------------------------------------------------------------------------
//Movie Details/////////////////////////////////////////////////////////////////////////////////////////////////////////////
//--------------------------------------------------------------------------------------------------------------------------

        public void updateMovieDetailsPage(string title)
        {
            int index = 0;
            bool MovieIsValid = false;
            // dont have to check if there is a movie file since you clicked on
            // a movie poster to get here. load the movie xml file.
            MoviesDocument.Load(MoviesPath);

            XmlNodeList TitleElemList = MoviesDocument.GetElementsByTagName("Title");
            XmlNodeList LengthElemList = MoviesDocument.GetElementsByTagName("Length");
            XmlNodeList RatingElemList = MoviesDocument.GetElementsByTagName("Rating");
            XmlNodeList GenreElemList = MoviesDocument.GetElementsByTagName("Genre");
            XmlNodeList ReleaseElemList = MoviesDocument.GetElementsByTagName("ReleaseDate");
            XmlNodeList ActorsElemList = MoviesDocument.GetElementsByTagName("Actor");
            XmlNodeList DirectorElemList = MoviesDocument.GetElementsByTagName("Director");
            XmlNodeList SynopsisElemList = MoviesDocument.GetElementsByTagName("Synopsis");
            XmlNodeList PosterPath = MoviesDocument.GetElementsByTagName("PosterPath");
            XmlNodeList Showtime1 = MoviesDocument.GetElementsByTagName("Showtime1");
            XmlNodeList Showtime2 = MoviesDocument.GetElementsByTagName("Showtime2");
            XmlNodeList Showtime3 = MoviesDocument.GetElementsByTagName("Showtime3");
            XmlNodeList Showtime4 = MoviesDocument.GetElementsByTagName("Showtime4");
            XmlNodeList Showtime5 = MoviesDocument.GetElementsByTagName("Showtime5");
            XmlNodeList Showtime6 = MoviesDocument.GetElementsByTagName("Showtime6");
            XmlNodeList Showtime7 = MoviesDocument.GetElementsByTagName("Showtime7");
            XmlNodeList Showtime8 = MoviesDocument.GetElementsByTagName("Showtime8");
            XmlNodeList Showtime9 = MoviesDocument.GetElementsByTagName("Showtime9");



            for (int i = 0; i < TitleElemList.Count; i++)
            {
                if (title == TitleElemList[i].InnerText)
                {
                    index = i;
                    MovieIsValid = true;
                    break;
                }
            }
            if (MovieIsValid && editInfo == false)
            {
                MDTitleLabel.Text = title;
                MDReleaseLabel.Text = ReleaseElemList[index].InnerText;
                MDLengthLabel.Text = LengthElemList[index].InnerText;
                MDRatingLabel.Text = RatingElemList[index].InnerText;
                MDGenreLabel.Text = GenreElemList[index].InnerText;
                MDActorsLabel.Text = ActorsElemList[index].InnerText;
                MDDirectorLabel.Text = DirectorElemList[index].InnerText;
                MDSynopsisLabel.Text = SynopsisElemList[index].InnerText;
                MDBigPoster.ImageLocation = PosterPath[index].InnerText;
                showtime1.Text = Showtime1[index].InnerText;
                showtime2.Text = Showtime2[index].InnerText;
                showtime3.Text = Showtime3[index].InnerText;
                showtime4.Text = Showtime4[index].InnerText;
                showtime5.Text = Showtime5[index].InnerText;
                showtime6.Text = Showtime6[index].InnerText;
                showtime7.Text = Showtime7[index].InnerText;
                showtime8.Text = Showtime8[index].InnerText;
                showtime9.Text = Showtime9[index].InnerText;
            }
            else if (editInfo == true)
            {
                TitleBox.Text = TitleElemList[index].InnerText;
                LengthBox.Text = LengthElemList[index].InnerText;
                SynopsisBox.Text = SynopsisElemList[index].InnerText;
                GenreBox.Text = GenreElemList[index].InnerText;
                RatingBox.Text = RatingElemList[index].InnerText;
                ActorBox.Text = ActorsElemList[index].InnerText;
                DirectorBox.Text = DirectorElemList[index].InnerText;
                ReleaseDTPicker.Text = ReleaseElemList[index].InnerText;
                posterPathtxt.Text = PosterPath[index].InnerText;
                st1txt.Text = Showtime1[index].InnerText;
                st2txt.Text = Showtime2[index].InnerText;
                st3txt.Text = Showtime3[index].InnerText;
                st4txt.Text = Showtime4[index].InnerText;
                st5txt.Text = Showtime5[index].InnerText;
                st6txt.Text = Showtime6[index].InnerText;
                st7txt.Text = Showtime7[index].InnerText;
                st8txt.Text = Showtime8[index].InnerText;
                st9txt.Text = Showtime9[index].InnerText;
            }
            else
            {
                Console.WriteLine("Error in Movie Details. Cant find selected movie.");
            }

        }

        private void updateMI()//Updates the movie info according to admin edits
        {
            if (editInfo == true)
                BodyTabControl.SelectedTab = AdminTab;
            else
                BodyTabControl.SelectedTab = MovieDetailsTab;

            MoviesDocument.Load(MoviesPath);

            XmlNodeList TitleElemList = MoviesDocument.GetElementsByTagName("Title");

            for (int i = 0; i < TitleElemList.Count; i++)
            {
                if (TitleBox.Text == TitleElemList[i].InnerText)
                {
                    currentInd = i;
                    break;
                }
            }
        }

        /* Poster Clicks */
        private void NRPoster1_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel1.Text));
            updateMI();
        }

        private void NRPoster2_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel2.Text));
            updateMI();
        }

        private void NRPoster3_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel3.Text));
            updateMI();
        }

        private void NRPoster4_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel4.Text));
            updateMI();
        }

        private void NRPoster5_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel5.Text));
            updateMI();
        }

        private void NSPoster1_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel1.Text));
            updateMI();
        }

        private void NSPoster2_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel2.Text));
            updateMI();
        }

        private void NSPoster3_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel3.Text));
            updateMI();
        }

        private void NSPoster4_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel4.Text));
            updateMI();
        }

        private void NSPoster5_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel5.Text));
            updateMI();
        }

        private void NSPoster6_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel6.Text));
            updateMI();
        }

        private void NSPoster7_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel7.Text));
            updateMI();
        }

        private void NSPoster8_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel8.Text));
            updateMI();
        }

        private void NSPoster9_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel9.Text));
            updateMI();
        }

        private void NSPoster10_Click(object sender, EventArgs e)
        {
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel10.Text));
            updateMI();
        }
//-------------------------------------------------------------------------------------------------------------------------
//Create Account Page//////////////////////////////////////////////////////////////////////////////////////////////////////
//-------------------------------------------------------------------------------------------------------------------------
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

        
//-------------------------------------------------------------------------------------------------------------------------
//Login Page///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//-------------------------------------------------------------------------------------------------------------------------
        
        private void label4_Click(object sender, EventArgs e) //Clicked Create Account label
        {
            BodyTabControl.SelectedTab = AccountTab;
        }

        //Log into system with a created account
        private void button1_Click(object sender, EventArgs e) //Clicked Login Button
        {
            if (File.Exists(AccountsPath))
            {
                XDocument accounts = XDocument.Load(AccountsPath);
                var accInfo = from acc in accounts.Descendants("Account")
                select new
                {
                     Username = acc.Element("Username").Value,
                     Password = acc.Element("Password").Value,
                     fName = acc.Attribute("First_Name").Value,
                     lName = acc.Attribute("Last_Name").Value,
                };
                foreach (var acc in accInfo)
                {
                    if (usernameTxt.Text == acc.Username && passwordTxt.Text == acc.Password)
                    {
                        if (usernameTxt.Text == "Admin")
                        {
                            adminLogged = true;
                            BodyTabControl.SelectedTab = AdminCtrl;
                        }
                        else
                        {
                            BodyTabControl.SelectedTab = HomeTab;
                        }

                        currentU = acc.Username;
                        currentfN = acc.fName;
                        currentlN = acc.lName;

                        MessageBox.Show("Successfully signed in!");
                        logged = true;
                        //logBtn.Text = "Log Out";
                        
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
//------------------------------------------------------------------------------------------------------------------------
//Movie Info and Showtimes Page//////////////////////////////////////////////////////////////////////////////////////////
//------------------------------------------------------------------------------------------------------------------------
        public void showMovieInfo()
        {
            movieSeattxt.Text = MDTitleLabel.Text;
            lengthSeattxt.Text = MDLengthLabel.Text;
        }

        
//--------------------------------------------------------------------------------------------------------------------------
//Admin Controls////////////////////////////////////////////////////////////////////////////////////////////////////////////
//--------------------------------------------------------------------------------------------------------------------------
//Provides admin ability to add more showtimes 1 at a time
        private void moreShowingsbtn_Click(object sender, EventArgs e)
        {
            switch (showtimes)
            {
                case 2:
                    {
                        st2lbl.Visible = true;
                        st2txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 3:
                    {
                        st3lbl.Visible = true;
                        st3txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 4:
                    {
                        st4lbl.Visible = true;
                        st4txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 5:
                    {
                        st5lbl.Visible = true;
                        st5txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 6:
                    {
                        st6lbl.Visible = true;
                        st6txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 7:
                    {
                        st7lbl.Visible = true;
                        st7txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 8:
                    {
                        st8lbl.Visible = true;
                        st8txt.Visible = true;
                        showtimes++;
                        break;
                    }
                case 9:
                    {
                        st9lbl.Visible = true;
                        st9txt.Visible = true;
                        showtimes++;
                        break;
                    }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            editInfo = false;
            BodyTabControl.SelectedTab = AdminTab;
        }

        public void resetShowtimes()
        {
            showtimes = 1;

            st1txt.Clear();
            st2txt.Clear();
            st3txt.Clear();
            st4txt.Clear();
            st5txt.Clear();
            st6txt.Clear();
            st7txt.Clear();
            st8txt.Clear();
            st9txt.Clear();

            if (editInfo == false)
            {
                st2lbl.Visible = false;
                st2txt.Visible = false;

                st3lbl.Visible = false;
                st3txt.Visible = false;

                st4lbl.Visible = false;
                st4txt.Visible = false;

                st5lbl.Visible = false;
                st5txt.Visible = false;

                st6lbl.Visible = false;
                st6txt.Visible = false;

                st7lbl.Visible = false;
                st7txt.Visible = false;

                st8lbl.Visible = false;
                st8txt.Visible = false;

                st9lbl.Visible = false;
                st9txt.Visible = false;
            }

            else
            {
                st2lbl.Visible = true;
                st2txt.Visible = true;

                st3lbl.Visible = true;
                st3txt.Visible = true;

                st4lbl.Visible = true;
                st4txt.Visible = true;

                st5lbl.Visible = true;
                st5txt.Visible = true;

                st6lbl.Visible = true;
                st6txt.Visible = true;

                st7lbl.Visible = true;
                st7txt.Visible = true;

                st8lbl.Visible = true;
                st8txt.Visible = true;

                st9lbl.Visible = true;
                st9txt.Visible = true;

                moreShowingsbtn.Visible = false;
            }
        }

        /* Add Movie button click */
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (editInfo == false)
            {
                XmlElement Root;

                if (!System.IO.File.Exists(MoviesPath))
                {
                    XmlDeclaration Declaration = MoviesDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    XmlComment Comment = MoviesDocument.CreateComment("MovieInfo");
                    Root = MoviesDocument.CreateElement("Movies");

                    MoviesDocument.AppendChild(Declaration);
                    MoviesDocument.AppendChild(Comment);
                    MoviesDocument.AppendChild(Root);
                }
                else
                {
                    MoviesDocument.Load(MoviesPath);
                    Root = MoviesDocument.DocumentElement;

                }

                XmlElement Movie = MoviesDocument.CreateElement("Movie");
                XmlElement Title = MoviesDocument.CreateElement("Title");
                XmlElement Length = MoviesDocument.CreateElement("Length");
                XmlElement Synopsis = MoviesDocument.CreateElement("Synopsis");
                XmlElement Description = MoviesDocument.CreateElement("Description");
                XmlElement Genre = MoviesDocument.CreateElement("Genre");
                XmlElement Rating = MoviesDocument.CreateElement("Rating");
                XmlElement Actor = MoviesDocument.CreateElement("Actor");
                XmlElement Director = MoviesDocument.CreateElement("Director");
                XmlElement ReleaseDate = MoviesDocument.CreateElement("ReleaseDate");
                XmlElement PosterPath = MoviesDocument.CreateElement("PosterPath");
                XmlElement Showtime1 = MoviesDocument.CreateElement("Showtime1");
                XmlElement Showtime2 = MoviesDocument.CreateElement("Showtime2");
                XmlElement Showtime3 = MoviesDocument.CreateElement("Showtime3");
                XmlElement Showtime4 = MoviesDocument.CreateElement("Showtime4");
                XmlElement Showtime5 = MoviesDocument.CreateElement("Showtime5");
                XmlElement Showtime6 = MoviesDocument.CreateElement("Showtime6");
                XmlElement Showtime7 = MoviesDocument.CreateElement("Showtime7");
                XmlElement Showtime8 = MoviesDocument.CreateElement("Showtime8");
                XmlElement Showtime9 = MoviesDocument.CreateElement("Showtime9");

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
                Movie.AppendChild(Showtime1);
                Movie.AppendChild(Showtime2);
                Movie.AppendChild(Showtime3);
                Movie.AppendChild(Showtime4);
                Movie.AppendChild(Showtime5);
                Movie.AppendChild(Showtime6);
                Movie.AppendChild(Showtime7);
                Movie.AppendChild(Showtime8);
                Movie.AppendChild(Showtime9);

                Title.InnerText = TitleBox.Text;
                Length.InnerText = LengthBox.Text;
                Synopsis.InnerText = SynopsisBox.Text;
                Description.InnerText = DescriptionBox.Text;
                Genre.InnerText = GenreBox.Text;
                Rating.InnerText = RatingBox.Text;
                Actor.InnerText = ActorBox.Text;
                Director.InnerText = DirectorBox.Text;
                ReleaseDate.InnerText = ReleaseDTPicker.Value.ToString("MM/dd/yyyy");
                PosterPath.InnerText = openFileDialog1.FileName;
                Showtime1.InnerText = st1txt.Text;
                Showtime2.InnerText = st2txt.Text;
                Showtime3.InnerText = st3txt.Text;
                Showtime4.InnerText = st4txt.Text;
                Showtime5.InnerText = st5txt.Text;
                Showtime6.InnerText = st6txt.Text;
                Showtime7.InnerText = st7txt.Text;
                Showtime8.InnerText = st8txt.Text;
                Showtime9.InnerText = st9txt.Text;

                MoviesDocument.Save(MoviesPath);

                TitleBox.Clear();
                LengthBox.Clear();
                SynopsisBox.Clear();
                DescriptionBox.Clear();
                GenreBox.ResetText();
                RatingBox.ResetText();
                ActorBox.Clear();
                DirectorBox.Clear();
                ReleaseDTPicker.ResetText();
                posterPathtxt.Clear();
                resetShowtimes();
            }
            else
            {
                int index = 0;

                // dont have to check if there is a movie file since you clicked on
                // a movie poster to get here. load the movie xml file.
                MoviesDocument.Load(MoviesPath);

                XmlNodeList TitleElemList = MoviesDocument.GetElementsByTagName("Title");
                XmlNodeList LengthElemList = MoviesDocument.GetElementsByTagName("Length");
                XmlNodeList RatingElemList = MoviesDocument.GetElementsByTagName("Rating");
                XmlNodeList GenreElemList = MoviesDocument.GetElementsByTagName("Genre");
                XmlNodeList ReleaseElemList = MoviesDocument.GetElementsByTagName("ReleaseDate");
                XmlNodeList ActorsElemList = MoviesDocument.GetElementsByTagName("Actor");
                XmlNodeList DirectorElemList = MoviesDocument.GetElementsByTagName("Director");
                XmlNodeList SynopsisElemList = MoviesDocument.GetElementsByTagName("Synopsis");
                XmlNodeList PosterPath = MoviesDocument.GetElementsByTagName("PosterPath");
                XmlNodeList Showtime1 = MoviesDocument.GetElementsByTagName("Showtime1");
                XmlNodeList Showtime2 = MoviesDocument.GetElementsByTagName("Showtime2");
                XmlNodeList Showtime3 = MoviesDocument.GetElementsByTagName("Showtime3");
                XmlNodeList Showtime4 = MoviesDocument.GetElementsByTagName("Showtime4");
                XmlNodeList Showtime5 = MoviesDocument.GetElementsByTagName("Showtime5");
                XmlNodeList Showtime6 = MoviesDocument.GetElementsByTagName("Showtime6");
                XmlNodeList Showtime7 = MoviesDocument.GetElementsByTagName("Showtime7");
                XmlNodeList Showtime8 = MoviesDocument.GetElementsByTagName("Showtime8");
                XmlNodeList Showtime9 = MoviesDocument.GetElementsByTagName("Showtime9");

                MessageBox.Show(index.ToString());

                TitleElemList[currentInd].InnerText = TitleBox.Text;
                LengthElemList[currentInd].InnerText = LengthBox.Text;
                SynopsisElemList[currentInd].InnerText = SynopsisBox.Text;
                GenreElemList[currentInd].InnerText = GenreBox.Text;
                RatingElemList[currentInd].InnerText = RatingBox.Text;
                ActorsElemList[currentInd].InnerText = ActorBox.Text;
                DirectorElemList[currentInd].InnerText = DirectorBox.Text;
                ReleaseElemList[currentInd].InnerText = ReleaseDTPicker.Text;
                PosterPath[currentInd].InnerText = posterPathtxt.Text;
                Showtime1[currentInd].InnerText = st1txt.Text;
                Showtime2[currentInd].InnerText = st2txt.Text;
                Showtime3[currentInd].InnerText = st3txt.Text;
                Showtime4[currentInd].InnerText = st4txt.Text;
                Showtime5[currentInd].InnerText = st5txt.Text;
                Showtime6[currentInd].InnerText = st6txt.Text;
                Showtime7[currentInd].InnerText = st7txt.Text;
                Showtime8[currentInd].InnerText = st8txt.Text;
                Showtime9[currentInd].InnerText = st9txt.Text;
                MoviesDocument.Save(MoviesPath);

            }

        }

        //Admin Clicks edit a movie
        private void adminEdit_Click(object sender, EventArgs e)
        {
            editInfo = true;
            editMovielbl.Visible = true;
            AddBtn.Text = "Update Info";
            resetShowtimes();
            BodyTabControl.SelectedTab = HomeTab;
        }
        //Admin clicks upload poster on add/edit movie page
        private void uploadPosterbtn_Click_1(object sender, EventArgs e)
        {
            string loc;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "openFileDialog1")
                loc = null;
            else
                loc = openFileDialog1.SafeFileName;

            string path = "../../Posters/";
            path = path + loc;
            posterPathtxt.Text = path;
        }
//-------------------------------------------------------------------------------------------------------------------------
//Seating Page////////////////////////////////////////////////////////////////////////////////////////////////////////////
//-------------------------------------------------------------------------------------------------------------------------
        private void selected_Seats(Button select, int i)
        {
            if (track[i] % 2 == 0)
            {
                select.BackColor = Color.Yellow;
                selectedSeatstxt.AppendText(select.Name + "\n");
            }
            else
            {
                select.BackColor = SystemColors.Control;
                selectedSeatstxt.Lines = selectedSeatstxt.Lines.Where(line => !line.Contains(select.Name)).ToArray();
            }

            track[i]++;
        }

        private void a1_Click(object sender, EventArgs e)
        {
            selected_Seats(a1, 0);
        }

        private void a2_Click(object sender, EventArgs e)
        {
            selected_Seats(a2, 1);
        }

        private void a3_Click(object sender, EventArgs e)
        {
            selected_Seats(a3, 2);
        }

        private void a4_Click(object sender, EventArgs e)
        {
            selected_Seats(a4, 3);
        }

        private void a5_Click(object sender, EventArgs e)
        {
            selected_Seats(a5, 4);
        }

        private void a6_Click(object sender, EventArgs e)
        {
            selected_Seats(a6, 5);
        }

        private void a7_Click(object sender, EventArgs e)
        {
            selected_Seats(a7, 6);
        }

        private void a8_Click(object sender, EventArgs e)
        {
            selected_Seats(a8, 7);
        }

        private void b1_Click(object sender, EventArgs e)
        {
            selected_Seats(b1, 8);
        }

        private void b2_Click(object sender, EventArgs e)
        {
            selected_Seats(b2, 9);
        }

        private void b3_Click(object sender, EventArgs e)
        {
            selected_Seats(b3, 10);
        }

        private void b4_Click(object sender, EventArgs e)
        {
            selected_Seats(b4, 11);
        }

        private void b5_Click(object sender, EventArgs e)
        {
            selected_Seats(b5, 12);
        }

        private void b6_Click(object sender, EventArgs e)
        {
            selected_Seats(b6, 13);
        }

        private void b7_Click(object sender, EventArgs e)
        {
            selected_Seats(b7, 14);
        }

        private void b8_Click(object sender, EventArgs e)
        {
            selected_Seats(b8, 15);
        }
        
        private void c1_Click(object sender, EventArgs e)
        {
            selected_Seats(c1, 16);
        }
        private void c2_Click(object sender, EventArgs e)
        {
            selected_Seats(c2, 17);
        }

        private void c3_Click(object sender, EventArgs e)
        {
            selected_Seats(c3, 18);
        }

        private void c4_Click(object sender, EventArgs e)
        {
            selected_Seats(c4, 19);
        }

        private void c5_Click(object sender, EventArgs e)
        {
            selected_Seats(c5, 20);
        }

        private void c6_Click(object sender, EventArgs e)
        {
            selected_Seats(c6, 21);
        }

        private void c7_Click(object sender, EventArgs e)
        {
            selected_Seats(c7, 22);
        }

        private void c8_Click(object sender, EventArgs e)
        {
            selected_Seats(c8, 23);
        }

        private void c9_Click(object sender, EventArgs e)
        {
            selected_Seats(c9, 24);
        }

        private void c10_Click(object sender, EventArgs e)
        {
            selected_Seats(c10, 25);
        }

        private void c11_Click(object sender, EventArgs e)
        {
            selected_Seats(c11, 26);
        }

        private void c12_Click(object sender, EventArgs e)
        {
            selected_Seats(c12, 27);
        }

        private void c13_Click(object sender, EventArgs e)
        {
            selected_Seats(c13, 28);
        }

        private void c14_Click(object sender, EventArgs e)
        {
            selected_Seats(c14, 29);
        }

        private void d1_Click(object sender, EventArgs e)
        {
            selected_Seats(d1, 30);
        }

        private void d2_Click(object sender, EventArgs e)
        {
            selected_Seats(d2, 31);
        }

        private void d3_Click(object sender, EventArgs e)
        {
            selected_Seats(d3, 32);
        }

        private void d4_Click(object sender, EventArgs e)
        {
            selected_Seats(d4, 33);
        }

        private void d5_Click(object sender, EventArgs e)
        {
            selected_Seats(d5, 34);
        }

        private void d6_Click(object sender, EventArgs e)
        {
            selected_Seats(d6, 35);
        }

        private void d7_Click(object sender, EventArgs e)
        {
            selected_Seats(d7, 36);
        }

        private void d8_Click(object sender, EventArgs e)
        {
            selected_Seats(d8, 37);
        }

        private void d9_Click(object sender, EventArgs e)
        {
            selected_Seats(d9, 38);
        }

        private void d10_Click(object sender, EventArgs e)
        {
            selected_Seats(d10, 39);
        }

        private void d11_Click(object sender, EventArgs e)
        {
            selected_Seats(d11, 40);
        }

        private void d12_Click(object sender, EventArgs e)
        {
            selected_Seats(d12, 41);
        }

        private void d13_Click(object sender, EventArgs e)
        {
            selected_Seats(d13, 42);
        }

        private void d14_Click(object sender, EventArgs e)
        {
            selected_Seats(d14, 43);
        }

        private void e1_Click(object sender, EventArgs e)
        {
            selected_Seats(e1, 44);
        }

        private void e2_Click(object sender, EventArgs e)
        {
            selected_Seats(e2, 45);
        }

        private void e3_Click(object sender, EventArgs e)
        {
            selected_Seats(e3, 46);
        }

        private void e4_Click(object sender, EventArgs e)
        {
            selected_Seats(e4, 47);
        }

        private void e5_Click(object sender, EventArgs e)
        {
            selected_Seats(e5, 48);
        }

        private void e6_Click(object sender, EventArgs e)
        {
            selected_Seats(e6, 49);
        }

        private void e7_Click(object sender, EventArgs e)
        {
            selected_Seats(e7, 50);
        }

        private void e8_Click(object sender, EventArgs e)
        {
            selected_Seats(e8, 51);
        }

        private void e9_Click(object sender, EventArgs e)
        {
            selected_Seats(e9, 52);
        }

        private void e10_Click(object sender, EventArgs e)
        {
            selected_Seats(e10, 53);
        }

        private void e11_Click(object sender, EventArgs e)
        {
            selected_Seats(e11, 54);
        }

        private void e12_Click(object sender, EventArgs e)
        {
            selected_Seats(e12, 55);
        }

        private void e13_Click(object sender, EventArgs e)
        {
            selected_Seats(e13, 56);
        }

        private void e14_Click(object sender, EventArgs e)
        {
            selected_Seats(e14, 57);
        }

        private void f1_Click(object sender, EventArgs e)
        {
            selected_Seats(f1, 58);
        }

        private void f2_Click(object sender, EventArgs e)
        {
            selected_Seats(f2, 59);
        }

        private void f3_Click(object sender, EventArgs e)
        {
            selected_Seats(f3, 60);
        }

        private void f4_Click(object sender, EventArgs e)
        {
            selected_Seats(f4, 61);
        }

        private void f5_Click(object sender, EventArgs e)
        {
            selected_Seats(f5, 62);
        }

        private void f6_Click(object sender, EventArgs e)
        {
            selected_Seats(f6, 63);
        }

        private void f7_Click(object sender, EventArgs e)
        {
            selected_Seats(f7, 64);
        }

        private void f8_Click(object sender, EventArgs e)
        {
            selected_Seats(f8, 65);
        }

        private void f9_Click(object sender, EventArgs e)
        {
            selected_Seats(f9, 66);
        }

        private void f10_Click(object sender, EventArgs e)
        {
            selected_Seats(f10, 67);
        }

        private void f11_Click(object sender, EventArgs e)
        {
            selected_Seats(f11, 68);
        }

        private void f12_Click(object sender, EventArgs e)
        {
            selected_Seats(f12, 69);
        }

        private void f13_Click(object sender, EventArgs e)
        {
            selected_Seats(f13, 70);
        }

        private void f14_Click(object sender, EventArgs e)
        {
            selected_Seats(f14, 71);
        }

//-----------------------------------------------------------------------------------------------------------------------
//BUY SEATING PAGE//////////////////////////////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------------------------------------------------
        private void button1_Click_1(object sender, EventArgs e)
        {
            //BUTTON TAKES USERS TO THE BUY TICKETS PAGE
            seatsSelected = getSeatsFromString(selectedSeatstxt.Text);
            Console.Out.Write(seatsSelected);
            BodyTabControl.SelectedTab = Purchase;
        }

        //GETS SEATS FROM STRING AND SAVES THEM
        private List<string> getSeatsFromString(string input)
        {
            List<string> result = new List<string>();
            string[] seats = input.Split();
            foreach (string str in seats)
            {
                bool flag = str.Equals("");
                if (!flag)
                {
                    result.Add(str);
                }
            }
            return result;
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
            BodyTabControl.SelectedTab = Seating;

            //SETS UP EVERYTHING ON THE NEXT PAGE
            double subtotal, child, senior, adult;
            int childTicket, seniorTicket, adultTicket;
            child = Convert.ToDouble(label40.Text);
            senior = Convert.ToDouble(label39.Text);
            adult = Convert.ToDouble(label38.Text);
            subtotal = child + senior + adult;
            totalCost.Text = subtotal.ToString("$0.00");

            //GETS NUMBER TICKETS TO CHECK
            if(comboBox1.Text == "")
            {
                adultTicket = 0;
            }
            else
                adultTicket = Convert.ToInt32(comboBox1.Text);

            if(comboBox2.Text == "")
            {
                seniorTicket = 0;
            }
            else 
                seniorTicket = Convert.ToInt32(comboBox2.Text);

            if(comboBox3.Text == "")
            {
                childTicket = 0;
            }
            else
                childTicket = Convert.ToInt32(comboBox3.Text);

            tixCounter.Add("Child", childTicket);
            tixCounter.Add("Adult", adultTicket);
            tixCounter.Add("Senior", seniorTicket);
            ticketTotal = childTicket + seniorTicket+ adultTicket;

            //Saves data for purchase
            time = displayShowtimelbl.Text;
            date = showtimeDate.Text;
            movieTitle = displayMovieTitle.Text;
        }

        //GLOBAL VARIABLES SO I CAN SAVE MY INFO FOR TIXS
        string time, date, movieTitle;
        int ticketTotal;
        List<string> seatsSelected;
        Dictionary<string, int> tixCounter = new Dictionary<string,int>();
        string name;
        List<Ticket> transaction = new List<Ticket>();
        //------------------------------------------------------------------------------------------------------
        //  PURCHASE PAGE
        private void button5_Click(object sender, EventArgs e)
        {

            name = textBox1.Text;
            int indexCounter = 0;
            foreach (string type in tixCounter.Keys)
            {
                int num = tixCounter[type];
                while (num > 0)
                {
                    //Console.Out.Write(type + " " + num.ToString());
                    TicketAdmissionLabel.Text = type;
                    TicketDateLabel.Text = date;
                    namelabeltix.Text = name;
                    timeticketlabel.Text = time;
                    
                    Ticket tix = new Ticket(movieTitle, type, seatsSelected.ElementAt(indexCounter), time, name, date);
                    transaction.Add(tix);
                    num--;
                    indexCounter++;
                }
                //indexCounter++;
            }

            PurchaseTransaction(transaction);

            //Officially buys tickets
            MessageBox.Show("You have purchased tickets!");
            BodyTabControl.SelectedTab = PrintTix;
            comboBox1.ResetText();
            comboBox2.ResetText();
            comboBox3.ResetText();

            //Prep info for ticket
            ticketTitle.Text = MDTitleLabel.Text;
            //namelabeltix.Text = currentfN + " " + currentlN;
            List<string> list = new List<string>(
                           selectedSeatstxt.Text.Split(new string[] { "\n" },
                           StringSplitOptions.RemoveEmptyEntries));
            SeatticketLabel.Text = string.Join(",", list);


        }


        private void PurchaseTransaction(List<Ticket> trans)
        {
            
             XmlElement Root;
            //If there is no current file, then create a new one
             if (!System.IO.File.Exists(TransactionsPath))
             {
                 XmlDeclaration Declaration = TransactionsDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                 XmlComment Comment = TransactionsDocument.CreateComment("PurchaseInfo");
                 Root = TransactionsDocument.CreateElement("Purchases");

                 TransactionsDocument.AppendChild(Declaration);
                 TransactionsDocument.AppendChild(Comment);
                 TransactionsDocument.AppendChild(Root);
             }
             else
             {
                 TransactionsDocument.Load(TransactionsPath);
                 Root = TransactionsDocument.DocumentElement;

             }

            XmlElement phis = TransactionsDocument.CreateElement("Purchase_History");
            XmlElement user = TransactionsDocument.CreateElement("Username");
            XmlElement date = TransactionsDocument.CreateElement("Date");
            XmlElement time = TransactionsDocument.CreateElement("Showtime");
            XmlElement total = TransactionsDocument.CreateElement("Total");
            XmlElement proot = TransactionsDocument.CreateElement("Tickets");

            Root.AppendChild(phis);
            phis.AppendChild(user);
            phis.AppendChild(date);
            phis.AppendChild(time);
            phis.AppendChild(total);
            phis.AppendChild(proot);

            user.InnerText = UserTxt.Text;
            date.InnerText = displayDatelbl.Text;
            time.InnerText = displayShowtimelbl.Text;
            total.InnerText = totalCost.Text; 

        }

        //------------------------------------------------------------------------------------------------------
        // PRINT TICKETS PAGE
        //
        private void printTixButton_Click(object sender, EventArgs e)
        {
            //WILL PRINT TICKET ONCE IT IS CLICKED


        }

        private void gobackhomebutton_Click(object sender, EventArgs e)
        {
            BodyTabControl.SelectedTab = HomeTab;
            //CLEARS OUT INFO SO THEY CAN BUY OTHER STUFF OR BROWSE
            //CLEARS OUT INFO
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            displayShowtimelbl.Text = "0:00";
            selectedSeatstxt.Text = "";

        }

        private void showtimeDate_ValueChanged(object sender, EventArgs e)
        {
            displayDatelbl.Text = showtimeDate.Value.Date.ToLongDateString();
        }

        private void showtime1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime1.Text;
        }

        private void showtime2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime2.Text;
        }

        private void showtime3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime3.Text;
        }

        private void showtime4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime4.Text;
        }

        private void showtime5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime5.Text;
        }

        private void showtime6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime6.Text;
        }

        private void showtime7_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime7.Text;
        }

        private void showtime8_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime8.Text;
        }

        private void showtime9_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            displayShowtimelbl.Text = showtime9.Text;
        }

        private void orderTicketsbtn_Click(object sender, EventArgs e)
        {
            BodyTabControl.SelectedTab = Ticket;
            displayMovieTitle.Text = MDTitleLabel.Text.ToString();
            displayRating.Text = MDRatingLabel.Text.ToString();
            displayLength.Text = MDLengthLabel.Text.ToString();
        }

        //-----------------------------------------------------------------------------------------------------
    }
}
