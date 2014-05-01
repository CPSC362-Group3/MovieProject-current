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
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace MovieTheater
{
    public partial class homepage : Form
    {
        //Boolean Flags
        bool adminLogged = false;
        bool logged = false;
        bool editInfo = false;
        bool seatingChart = false;
        bool needslogin = false;
        bool editMovie = false;

        //Int counters
        int browsePageNum = 1;
        int seats = 0;
        int totalseats = 0;
        int currentInd = 0;
        int seatInd = 0;
        int showtimes = 2;
        int[] track = new int[72];

        string currentUser, SC, currentemail;

        //String file paths, xml related
        string MoviesPath = "../../xml/Movies.xml";
        string SeatingPath = "../../xml/Seats.xml";
        string AccountsPath = "../../xml/accountInfo.xml";
        string TransactionsPath = "../../xml/transactions.xml";
        XmlDocument MoviesDocument = new XmlDocument();
        XmlDocument TransactionsDocument = new XmlDocument();
        XmlDocument SeatingDocument = new XmlDocument();
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

            setBackgrounds();
            displayDatelbl.Text = DateTime.Today.ToLongDateString();
            showtimeDate.MinDate = DateTime.Today;
            showtimeDate.MaxDate = DateTime.Today.AddDays(14.0);

            updateHomePage();

        }
        //------------------------------------------------------------------------------------------------------------------------
        // GLOBAL FUNCTIONS, Main button controls ///////////////////////////////////////////////////////////////////////////////
        //------------------------------------------------------------------------------------------------------------------------

        // setBackground parents
        public void setBackgrounds()
        {
            emailLabelAccount.Parent = backgroundCA;

            // Home Page
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

            // Search page
            SSearchString.Parent = backgroundS;
            STitleLabel1.Parent = backgroundS;
            STitleLabel2.Parent = backgroundS;
            STitleLabel3.Parent = backgroundS;
            STitleLabel4.Parent = backgroundS;
            STitleLabel5.Parent = backgroundS;
            STitleLabel6.Parent = backgroundS;
            SReleaseLabel1.Parent = backgroundS;
            SReleaseLabel2.Parent = backgroundS;
            SReleaseLabel3.Parent = backgroundS;
            SReleaseLabel4.Parent = backgroundS;
            SReleaseLabel5.Parent = backgroundS;
            SReleaseLabel6.Parent = backgroundS;
            SLengthLabel1.Parent = backgroundS;
            SLengthLabel2.Parent = backgroundS;
            SLengthLabel3.Parent = backgroundS;
            SLengthLabel4.Parent = backgroundS;
            SLengthLabel5.Parent = backgroundS;
            SLengthLabel6.Parent = backgroundS;
            SRatingLabel1.Parent = backgroundS;
            SRatingLabel2.Parent = backgroundS;
            SRatingLabel3.Parent = backgroundS;
            SRatingLabel4.Parent = backgroundS;
            SRatingLabel5.Parent = backgroundS;
            SRatingLabel6.Parent = backgroundS;
            SGenreLabel1.Parent = backgroundS;
            SGenreLabel2.Parent = backgroundS;
            SGenreLabel3.Parent = backgroundS;
            SGenreLabel4.Parent = backgroundS;
            SGenreLabel5.Parent = backgroundS;
            SGenreLabel6.Parent = backgroundS;
            SRelease1.Parent = backgroundS;
            SRelease2.Parent = backgroundS;
            SRelease3.Parent = backgroundS;
            SRelease4.Parent = backgroundS;
            SRelease5.Parent = backgroundS;
            SRelease6.Parent = backgroundS;
            SLength1.Parent = backgroundS;
            SLength2.Parent = backgroundS;
            SLength3.Parent = backgroundS;
            SLength4.Parent = backgroundS;
            SLength5.Parent = backgroundS;
            SLength6.Parent = backgroundS;
            SRating1.Parent = backgroundS;
            SRating2.Parent = backgroundS;
            SRating3.Parent = backgroundS;
            SRating4.Parent = backgroundS;
            SRating5.Parent = backgroundS;
            SRating6.Parent = backgroundS;
            SGenre1.Parent = backgroundS;
            SGenre2.Parent = backgroundS;
            SGenre3.Parent = backgroundS;
            SGenre4.Parent = backgroundS;
            SGenre5.Parent = backgroundS;
            SGenre6.Parent = backgroundS;

            // Contact us page
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

            // Account creation page
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

            //Browse page
            BMovieTitle1.Parent = backgroundB;
            BMovieTitle2.Parent = backgroundB;
            BMovieTitle3.Parent = backgroundB;
            BMovieTitle4.Parent = backgroundB;
            BMovieTitle5.Parent = backgroundB;
            BMovieTitle6.Parent = backgroundB;
            BMovieTitle7.Parent = backgroundB;
            BMovieTitle8.Parent = backgroundB;
            BMovieTitle9.Parent = backgroundB;
            BMovieTitle10.Parent = backgroundB;
            BMovieTitle11.Parent = backgroundB;
            BMovieTitle12.Parent = backgroundB;
            BMovieTitle13.Parent = backgroundB;
            BMovieTitle14.Parent = backgroundB;
            BMovieTitle15.Parent = backgroundB;
            BShowing1.Parent = backgroundB;
            BShowing2.Parent = backgroundB;
            BShowing3.Parent = backgroundB;
            BShowing4.Parent = backgroundB;
            BShowing5.Parent = backgroundB;
            BShowing6.Parent = backgroundB;
            BShowing7.Parent = backgroundB;
            BShowing8.Parent = backgroundB;
            BShowing9.Parent = backgroundB;
            BShowing10.Parent = backgroundB;
            BShowing11.Parent = backgroundB;
            BShowing12.Parent = backgroundB;
            BShowing13.Parent = backgroundB;
            BShowing14.Parent = backgroundB;
            BShowing15.Parent = backgroundB;

            // Account Info
            AIPersonalInfo.Parent = backgroundAI;
            AICreditCardInfo.Parent = backgroundAI;
            AIAccountInfoLabel.Parent = backgroundAI;
            AIFirstName.Parent = backgroundAI;
            AILastName.Parent = backgroundAI;
            AIAddress.Parent = backgroundAI;
            AICity.Parent = backgroundAI;
            AIState.Parent = backgroundAI;
            AIUsername.Parent = backgroundAI;
            AIPassword.Parent = backgroundAI;
            AICFirstName.Parent = backgroundAI;
            AICLastName.Parent = backgroundAI;
            AICCNumber.Parent = backgroundAI;
            AISecurityCode.Parent = backgroundAI;
            AIExpDate.Parent = backgroundAI;

            homeLogged.Parent = Header;
            checkLog.Parent = Header;
        }

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
            editInfo = false;
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

            resetSeats();
            usernameTxt.Clear();
            passwordTxt.Clear();
        }

        /* Home button click */
        private void homeBtn_Click(object sender, EventArgs e)
        {
            updateHomePage();
            resetSeats();
            editInfo = false;
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
            editInfo = false;
            if (adminLogged)
            {
                BodyTabControl.SelectedTab = AdminCtrl;
            }
             else if (logged)
            {
                refreshAccountInfo();
                BodyTabControl.SelectedTab = AccountInfoTab;
            }
            else
            {
                BodyTabControl.SelectedTab = LoginTab;
                usernameTxt.Clear();
                passwordTxt.Clear();
                resetSeats();
            }
        }

        /* Login button enter */
        private void LoginBtn_MouseEnter(object sender, EventArgs e)
        {
            if (logged || adminLogged)
            {
                LoginBtn.Image = new Bitmap("../../Resources/account_button_hover.jpg");
            }
            else
            {
                LoginBtn.Image = new Bitmap("../../Resources/login_button_hover.jpg");
            }
        }

        /* Login button exit */
        private void LoginBtn_MouseLeave(object sender, EventArgs e)
        {
            if (logged || adminLogged)
            {
                LoginBtn.Image = new Bitmap("../../Resources/account_button.jpg");
            }
            else
            {
                LoginBtn.Image = new Bitmap("../../Resources/login_button.jpg");
            }
        }

        /* browse button click */
        private void browseBtn_Click(object sender, EventArgs e)
        {
            editInfo = false;
            browsePageNum = 1;
            RefreshBrowse(browsePageNum);
            BodyTabControl.SelectedTab = BrowseTab;
            resetSeats();
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
            editInfo = false;
            BodyTabControl.SelectedTab = ContactUsTab;
            resetSeats();
        }

        /* Contact Us button enter */
        private void contactUsBtn_MouseEnter(object sender, EventArgs e)
        {
            contactUsBtn.Image = new Bitmap("../../Resources/contact_us_button_hover.jpg");
        }

        /* Contact Us button exit */
        private void contactUsBtn_MouseLeave(object sender, EventArgs e)
        {
            contactUsBtn.Image = new Bitmap("../../Resources/contact_us_button.jpg");
        }

        //---------------------------------------------------------------------------------------------------------------------
        //Update and refresh movie info////////////////////////////////////////////////////////////////////////////////////////
        //----------------------------------------------------------------------------------------------------------------------

        public void updateHomePage()
        {
            RefreshNewReleases();
            RefreshNowShowing();
        }

        public void RefreshNewReleases()
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

        public void RefreshNowShowing()
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
                NSReleaseDateLabel6.Visible = true;
                NSTitleLabel6.Visible = true;
                NSPoster6.Visible = true;
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
                NSReleaseDateLabel7.Visible = true;
                NSTitleLabel7.Visible = true;
                NSPoster7.Visible = true;
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
                NSReleaseDateLabel8.Visible = true;
                NSTitleLabel8.Visible = true;
                NSPoster8.Visible = true;
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
                NSReleaseDateLabel9.Visible = true;
                NSTitleLabel9.Visible = true;
                NSPoster9.Visible = true;
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
                NSReleaseDateLabel10.Visible = true;
                NSTitleLabel10.Visible = true;
                NSPoster10.Visible = true;
                NSPoster10.ImageLocation = PosterElemList[index].InnerText;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //Browse Movie page ///////////////////////////////////////////////////////////////////////////////////////////////////
        //---------------------------------------------------------------------------------------------------------------------

        public void RefreshBrowse(int page)
        {
            BMovieTitle1.Visible = false;
            BShowing1.Visible = false;
            BPoster1.Visible = false;
            BMovieTitle2.Visible = false;
            BShowing2.Visible = false;
            BPoster2.Visible = false;
            BMovieTitle3.Visible = false;
            BShowing3.Visible = false;
            BPoster3.Visible = false;
            BMovieTitle4.Visible = false;
            BShowing4.Visible = false;
            BPoster4.Visible = false;
            BMovieTitle5.Visible = false;
            BShowing5.Visible = false;
            BPoster5.Visible = false;
            BMovieTitle6.Visible = false;
            BShowing6.Visible = false;
            BPoster6.Visible = false;
            BMovieTitle7.Visible = false;
            BShowing7.Visible = false;
            BPoster7.Visible = false;
            BMovieTitle8.Visible = false;
            BShowing8.Visible = false;
            BPoster8.Visible = false;
            BMovieTitle9.Visible = false;
            BShowing9.Visible = false;
            BPoster9.Visible = false;
            BMovieTitle10.Visible = false;
            BShowing10.Visible = false;
            BPoster10.Visible = false;
            BMovieTitle11.Visible = false;
            BShowing11.Visible = false;
            BPoster11.Visible = false;
            BMovieTitle12.Visible = false;
            BShowing12.Visible = false;
            BPoster12.Visible = false;
            BMovieTitle13.Visible = false;
            BShowing13.Visible = false;
            BPoster13.Visible = false;
            BMovieTitle14.Visible = false;
            BShowing14.Visible = false;
            BPoster14.Visible = false;
            BMovieTitle15.Visible = false;
            BShowing15.Visible = false;
            BPoster15.Visible = false;

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

            int index = 0;
            for (int i = 0; i < page; i++)
            {
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing1.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing1.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle1.Visible = true;
                    BShowing1.Visible = true;
                    BPoster1.Visible = true;
                    BMovieTitle1.Text = titleElemList[index].InnerText;
                    BPoster1.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing2.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing2.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle2.Visible = true;
                    BShowing2.Visible = true;
                    BPoster2.Visible = true;
                    BMovieTitle2.Text = titleElemList[index].InnerText;
                    BPoster2.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing3.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing3.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle3.Visible = true;
                    BShowing3.Visible = true;
                    BPoster3.Visible = true;
                    BMovieTitle3.Text = titleElemList[index].InnerText;
                    BPoster3.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing4.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing4.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle4.Visible = true;
                    BShowing4.Visible = true;
                    BPoster4.Visible = true;
                    BMovieTitle4.Text = titleElemList[index].InnerText;
                    BPoster4.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing5.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing5.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle5.Visible = true;
                    BShowing5.Visible = true;
                    BPoster5.Visible = true;
                    BMovieTitle5.Text = titleElemList[index].InnerText;
                    BPoster5.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing6.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing6.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle1.Visible = true;
                    BShowing6.Visible = true;
                    BPoster6.Visible = true;
                    BMovieTitle6.Text = titleElemList[index].InnerText;
                    BPoster6.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing7.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing7.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle7.Visible = true;
                    BShowing7.Visible = true;
                    BPoster7.Visible = true;
                    BMovieTitle7.Text = titleElemList[index].InnerText;
                    BPoster7.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing8.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing8.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle8.Visible = true;
                    BShowing8.Visible = true;
                    BPoster8.Visible = true;
                    BMovieTitle8.Text = titleElemList[index].InnerText;
                    BPoster8.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing9.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing9.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle9.Visible = true;
                    BShowing9.Visible = true;
                    BPoster9.Visible = true;
                    BMovieTitle9.Text = titleElemList[index].InnerText;
                    BPoster9.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing10.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing10.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle10.Visible = true;
                    BShowing10.Visible = true;
                    BPoster10.Visible = true;
                    BMovieTitle10.Text = titleElemList[index].InnerText;
                    BPoster10.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing11.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing11.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle11.Visible = true;
                    BShowing11.Visible = true;
                    BPoster11.Visible = true;
                    BMovieTitle11.Text = titleElemList[index].InnerText;
                    BPoster11.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing12.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing12.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle12.Visible = true;
                    BShowing12.Visible = true;
                    BPoster12.Visible = true;
                    BMovieTitle12.Text = titleElemList[index].InnerText;
                    BPoster12.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing13.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing13.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle13.Visible = true;
                    BShowing13.Visible = true;
                    BPoster13.Visible = true;
                    BMovieTitle13.Text = titleElemList[index].InnerText;
                    BPoster13.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing14.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing14.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle14.Visible = true;
                    BShowing14.Visible = true;
                    BPoster14.Visible = true;
                    BMovieTitle14.Text = titleElemList[index].InnerText;
                    BPoster14.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
                if (index < numMovies)
                {
                    ReleaseTime = Convert.ToDateTime(ReleaseDateElemList[index].InnerText);
                    if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
                    {
                        BShowing15.Text = "NOW SHOWING";
                    }
                    else
                    {
                        BShowing15.Text = ReleaseDateElemList[index].InnerText;
                    }
                    BMovieTitle15.Visible = true;
                    BShowing15.Visible = true;
                    BPoster15.Visible = true;
                    BMovieTitle15.Text = titleElemList[index].InnerText;
                    BPoster15.ImageLocation = PosterElemList[index].InnerText;
                    index++;
                }
            }

        }

        public void loadMovie(string title)
        {
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

            int index = 0;
            for (int i = 0; i < TitleElemList.Count; i++)
            {
                if (TitleElemList[index].InnerText == title)
                {
                    index = i;
                }
            }
            AMITitleBox.Text = TitleElemList[index].InnerText;
            AMILengthBox.Text = LengthElemList[index].InnerText;
            AMIRatingBox.Text = RatingElemList[index].InnerText;
            AMIGenreBox.Text = GenreElemList[index].InnerText;
            //AMIReleaseDTPicker
        }

        private void BPoster1_Click(object sender, EventArgs e)
        {
            if (BShowing1.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle1.Text);
            updateMI();
        }

        private void BPoster2_Click(object sender, EventArgs e)
        {
            if (BShowing2.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle2.Text);
            updateMI();
        }

        private void BPoster3_Click(object sender, EventArgs e)
        {
            if (BShowing3.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle3.Text);
            updateMI();
        }

        private void BPoster4_Click(object sender, EventArgs e)
        {
            if (BShowing4.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle4.Text);
            updateMI();
        }

        private void BPoster5_Click(object sender, EventArgs e)
        {
            if (BShowing5.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle5.Text);
            updateMI();
        }

        private void BPoster6_Click(object sender, EventArgs e)
        {
            if (BShowing6.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle6.Text);
            updateMI();
        }

        private void BPoster7_Click(object sender, EventArgs e)
        {
            if (BShowing7.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle7.Text);
            updateMI();
        }

        private void BPoster8_Click(object sender, EventArgs e)
        {
            if (BShowing8.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle8.Text);
            updateMI();
        }

        private void BPoster9_Click(object sender, EventArgs e)
        {
            if (BShowing9.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle9.Text);
            updateMI();
        }

        private void BPoster10_Click(object sender, EventArgs e)
        {
            if (BShowing10.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle10.Text);
            updateMI();
        }

        private void BPoster11_Click(object sender, EventArgs e)
        {
            if (BShowing11.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle11.Text);
            updateMI();
        }

        private void BPoster12_Click(object sender, EventArgs e)
        {
            if (BShowing12.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle12.Text);
            updateMI();
        }

        private void BPoster13_Click(object sender, EventArgs e)
        {
            if (BShowing13.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle13.Text);
            updateMI();
        }

        private void BPoster14_Click(object sender, EventArgs e)
        {
            if (BShowing14.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle14.Text);
            updateMI();
        }

        private void BPoster15_Click(object sender, EventArgs e)
        {
            if (BShowing15.Text == "NOW SHOWING")
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(BMovieTitle15.Text);
            updateMI();
        }

        //---------------------------------------------------------------------------------------------------------------------
        //Account Info page ///////////////////////////////////////////////////////////////////////////////////////////////////
        //---------------------------------------------------------------------------------------------------------------------

        public void refreshAccountInfo()
        {
            AISaveBtn.Visible = false;
            AIEditBtn.Visible = true;

            int index = 0;
            XmlDocument accounts = new XmlDocument();
            if (System.IO.File.Exists(AccountsPath))
            {
                accounts.Load(AccountsPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                Console.WriteLine("Error: AccountInfo file does not exist.");
                return;
            }

            XmlNodeList Account = accounts.GetElementsByTagName("Account");
            XmlNodeList Address = accounts.GetElementsByTagName("Address");
            XmlNodeList City = accounts.GetElementsByTagName("City");
            XmlNodeList State = accounts.GetElementsByTagName("State");
            XmlNodeList Username = accounts.GetElementsByTagName("Username");
            XmlNodeList Password = accounts.GetElementsByTagName("Password");
            XmlNodeList CH_FNAME = accounts.GetElementsByTagName("CH_FNAME");
            XmlNodeList CH_LNAME = accounts.GetElementsByTagName("CH_LNAME");
            XmlNodeList Card_Number = accounts.GetElementsByTagName("Card_Number");
            XmlNodeList Security_Code = accounts.GetElementsByTagName("Security_Code");

            for (int i = 0; i < Username.Count; i++)
            {
                if (Username[i].InnerText == currentUser)
                {
                    index = i;
                    break;
                }
            }

            AIFirstNameBox.ReadOnly = true;
            AILastNameBox.ReadOnly = true;
            AIAddressBox.ReadOnly = true;
            AICityBox.ReadOnly = true;
            AIStateBox.ReadOnly = true;
            AIUsernameBox.ReadOnly = true;
            AIPasswordBox.ReadOnly = true;
            AICHFirstNameBox.ReadOnly = true;
            AICHLastNameBox.ReadOnly = true;
            AICredit.ReadOnly = true;
            AISecurity.ReadOnly = true;

            AIFirstNameBox.Text = Account[index].Attributes["First_Name"].InnerText;
            AILastNameBox.Text = Account[index].Attributes["Last_Name"].InnerText;
            AIAddressBox.Text = Address[index].InnerText;
            AICityBox.Text = City[index].InnerText;
            AIStateBox.Text = State[index].InnerText;
            AIUsernameBox.Text = Username[index].InnerText;
            // password
            AIPasswordBox.Text = "";
            for (int i = 0; i < Password[index].InnerText.Length; i++)
            {
                AIPasswordBox.Text = AIPasswordBox.Text + "*";
            }
            AICHFirstNameBox.Text = CH_FNAME[index].InnerText;
            AICHLastNameBox.Text = CH_LNAME[index].InnerText;
            // credit card
            AICredit.Text = "****-****-****-";
            if (Card_Number[index].InnerText.Length > 12)
            {
                for (int i = 12; i < Card_Number[index].InnerText.Length; i++)
                {
                    AICredit.Text = AICredit.Text + Card_Number[index].InnerText[i];
                }
            }
            // security code
            //AISecurity.Text = Security_Code[index].InnerText;
            AISecurity.Text = "***";
        }

        // edit account info button
        private void AIEditBtn_Click(object sender, EventArgs e)
        {
            AISaveBtn.Visible = true;
            AIEditBtn.Visible = false;
            AIFirstNameBox.ReadOnly = false;
            AILastNameBox.ReadOnly = false;
            AIAddressBox.ReadOnly = false;
            AICityBox.ReadOnly = false;
            AIStateBox.ReadOnly = false;
            AIPasswordBox.ReadOnly = false;
            AICHFirstNameBox.ReadOnly = false;
            AICHLastNameBox.ReadOnly = false;
            AICredit.ReadOnly = false;
            AISecurity.ReadOnly = false;
        }

        private void AISaveBtn_Click(object sender, EventArgs e)
        {
            AIFirstNameBox.ReadOnly = true;
            AILastNameBox.ReadOnly = true;
            AIAddressBox.ReadOnly = true;
            AICityBox.ReadOnly = true;
            AIStateBox.ReadOnly = true;
            AIPasswordBox.ReadOnly = true;
            AICHFirstNameBox.ReadOnly = true;
            AICHLastNameBox.ReadOnly = true;
            AICredit.ReadOnly = true;
            AISecurity.ReadOnly = true;

            int index = 0;
            XmlDocument accounts = new XmlDocument();

            if (System.IO.File.Exists(AccountsPath))
            {
                accounts.Load(AccountsPath);
            }
            else
            {
                // if there is no file, then all elements are invisible.
                // no reason to continue from here.
                Console.WriteLine("Error: AccountInfo file does not exist.");
                return;
            }

            XmlNodeList Account = accounts.GetElementsByTagName("Account");
            XmlNodeList Address = accounts.GetElementsByTagName("Address");
            XmlNodeList City = accounts.GetElementsByTagName("City");
            XmlNodeList State = accounts.GetElementsByTagName("State");
            XmlNodeList Username = accounts.GetElementsByTagName("Username");
            XmlNodeList Password = accounts.GetElementsByTagName("Password");
            XmlNodeList CH_FNAME = accounts.GetElementsByTagName("CH_FNAME");
            XmlNodeList CH_LNAME = accounts.GetElementsByTagName("CH_LNAME");
            XmlNodeList Card_Number = accounts.GetElementsByTagName("Card_Number");
            XmlNodeList Security_Code = accounts.GetElementsByTagName("Security_Code");

            for (int i = 0; i < Username.Count; i++)
            {
                if (Username[i].InnerText == currentUser)
                {
                    index = i;
                    break;
                }
            }
            // ADD ERROR CHECKS HERE!
            if (AIFirstNameBox.Text.Length > 1)
            {
                Account[index].Attributes["First_Name"].InnerText = AIFirstNameBox.Text;
            }
            if (AILastNameBox.Text.Length > 1)
            {
                Account[index].Attributes["Last_Name"].InnerText = AILastNameBox.Text;
            }
            if (AIAddressBox.Text.Length > 1)
            {
                Address[index].InnerText = AIAddressBox.Text;
            }
            if (AICityBox.Text.Length > 1)
            {
                City[index].InnerText = AICityBox.Text;
            }
            if (AIStateBox.Text.Length > 1)
            {
                State[index].InnerText = AIStateBox.Text;
            }
            if (!AIPasswordBox.Text.Contains("*"))
            {
                Password[index].InnerText = AIPasswordBox.Text;
            }
            if (AICHFirstNameBox.Text.Length > 1)
            {
                CH_FNAME[index].InnerText = AICHFirstNameBox.Text;
            }
            if (AICHLastNameBox.Text.Length > 1)
            {
                CH_LNAME[index].InnerText = AICHLastNameBox.Text;
            }
            if (!AICredit.Text.Contains("*"))
            {
                Card_Number[index].InnerText = AICredit.Text;
            }
            if (!AISecurity.Text.Contains("*"))
            {
                if (AISecurity.Text.Length > 1 && AISecurity.Text.Length < 4)
                {
                    Security_Code[index].InnerText = AISecurity.Text;
                }
            }

            accounts.Save(AccountsPath);
            refreshAccountInfo();
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
            SReleaseLabel1.Visible = false;
            SReleaseLabel2.Visible = false;
            SReleaseLabel3.Visible = false;
            SReleaseLabel4.Visible = false;
            SReleaseLabel5.Visible = false;
            SReleaseLabel6.Visible = false;
            SLengthLabel1.Visible = false;
            SLengthLabel2.Visible = false;
            SLengthLabel3.Visible = false;
            SLengthLabel4.Visible = false;
            SLengthLabel5.Visible = false;
            SLengthLabel6.Visible = false;
            SRatingLabel1.Visible = false;
            SRatingLabel2.Visible = false;
            SRatingLabel3.Visible = false;
            SRatingLabel4.Visible = false;
            SRatingLabel5.Visible = false;
            SRatingLabel6.Visible = false;
            SGenreLabel1.Visible = false;
            SGenreLabel2.Visible = false;
            SGenreLabel3.Visible = false;
            SGenreLabel4.Visible = false;
            SGenreLabel5.Visible = false;
            SGenreLabel6.Visible = false;
            SRelease1.Visible = false;
            SRelease2.Visible = false;
            SRelease3.Visible = false;
            SRelease4.Visible = false;
            SRelease5.Visible = false;
            SRelease6.Visible = false;
            SLength1.Visible = false;
            SLength2.Visible = false;
            SLength3.Visible = false;
            SLength4.Visible = false;
            SLength5.Visible = false;
            SLength6.Visible = false;
            SRating1.Visible = false;
            SRating2.Visible = false;
            SRating3.Visible = false;
            SRating4.Visible = false;
            SRating5.Visible = false;
            SRating6.Visible = false;
            SGenre1.Visible = false;
            SGenre2.Visible = false;
            SGenre3.Visible = false;
            SGenre4.Visible = false;
            SGenre5.Visible = false;
            SGenre6.Visible = false;

            SSearchString.Text = "\"" + search + "\"";

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
                SReleaseLabel1.Visible = true;
                SRelease1.Visible = true;
                SLengthLabel1.Visible = true;
                SLength1.Visible = true;
                SRatingLabel1.Visible = true;
                SRating1.Visible = true;
                SGenreLabel1.Visible = true;
                SGenre1.Visible = true;
                STitleLabel1.Text = TitleElemList[index].InnerText;
                searchPoster1.ImageLocation = PosterPath[index].InnerText;
                SRelease1.Text = ReleaseElemList[index].InnerText;
                SLength1.Text = LengthElemList[index].InnerText;
                SRating1.Text = RatingElemList[index].InnerText;
                SGenre1.Text = GenreElemList[index].InnerText;
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
                SReleaseLabel2.Visible = true;
                SRelease2.Visible = true;
                SLengthLabel2.Visible = true;
                SLength2.Visible = true;
                SRatingLabel2.Visible = true;
                SRating2.Visible = true;
                SGenreLabel2.Visible = true;
                SGenre2.Visible = true;
                STitleLabel2.Text = TitleElemList[index].InnerText;
                searchPoster2.ImageLocation = PosterPath[index].InnerText;
                SRelease2.Text = ReleaseElemList[index].InnerText;
                SLength2.Text = LengthElemList[index].InnerText;
                SRating2.Text = RatingElemList[index].InnerText;
                SGenre2.Text = GenreElemList[index].InnerText;
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
                searchPoster3.Visible = true;
                STitleLabel3.Visible = true;
                SReleaseLabel3.Visible = true;
                SRelease3.Visible = true;
                SLengthLabel1.Visible = true;
                SLength3.Visible = true;
                SRatingLabel1.Visible = true;
                SRating3.Visible = true;
                SGenreLabel1.Visible = true;
                SGenre3.Visible = true;
                STitleLabel3.Text = TitleElemList[index].InnerText;
                searchPoster3.ImageLocation = PosterPath[index].InnerText;
                SRelease3.Text = ReleaseElemList[index].InnerText;
                SLength3.Text = LengthElemList[index].InnerText;
                SRating3.Text = RatingElemList[index].InnerText;
                SGenre3.Text = GenreElemList[index].InnerText;
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
                searchPoster4.Visible = true;
                STitleLabel4.Visible = true;
                SReleaseLabel4.Visible = true;
                SRelease4.Visible = true;
                SLengthLabel4.Visible = true;
                SLength4.Visible = true;
                SRatingLabel4.Visible = true;
                SRating4.Visible = true;
                SGenreLabel4.Visible = true;
                SGenre4.Visible = true;
                STitleLabel4.Text = TitleElemList[index].InnerText;
                searchPoster4.ImageLocation = PosterPath[index].InnerText;
                SRelease4.Text = ReleaseElemList[index].InnerText;
                SLength4.Text = LengthElemList[index].InnerText;
                SRating4.Text = RatingElemList[index].InnerText;
                SGenre4.Text = GenreElemList[index].InnerText;
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
                searchPoster5.Visible = true;
                STitleLabel5.Visible = true;
                SReleaseLabel5.Visible = true;
                SRelease5.Visible = true;
                SLengthLabel5.Visible = true;
                SLength5.Visible = true;
                SRatingLabel5.Visible = true;
                SRating5.Visible = true;
                SGenreLabel5.Visible = true;
                SGenre5.Visible = true;
                STitleLabel5.Text = TitleElemList[index].InnerText;
                searchPoster5.ImageLocation = PosterPath[index].InnerText;
                SRelease5.Text = ReleaseElemList[index].InnerText;
                SLength5.Text = LengthElemList[index].InnerText;
                SRating5.Text = RatingElemList[index].InnerText;
                SGenre5.Text = GenreElemList[index].InnerText;
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
                searchPoster6.Visible = true;
                STitleLabel6.Visible = true;
                SReleaseLabel6.Visible = true;
                SRelease6.Visible = true;
                SLengthLabel6.Visible = true;
                SLength6.Visible = true;
                SRatingLabel6.Visible = true;
                SRating6.Visible = true;
                SGenreLabel6.Visible = true;
                SGenre6.Visible = true;
                STitleLabel6.Text = TitleElemList[index].InnerText;
                searchPoster6.ImageLocation = PosterPath[index].InnerText;
                SRelease6.Text = ReleaseElemList[index].InnerText;
                SLength6.Text = LengthElemList[index].InnerText;
                SRating6.Text = RatingElemList[index].InnerText;
                SGenre6.Text = GenreElemList[index].InnerText;
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
        }

        private void searchPoster1_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease1.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel1.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster2_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease2.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel2.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster3_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease3.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel3.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster4_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease4.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel4.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster5_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease5.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel5.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        private void searchPoster6_Click(object sender, EventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ReleaseTime = Convert.ToDateTime(SRelease6.Text);
            if (DateTime.Compare(ReleaseTime, CurrentTime) <= 0)
            {
                orderTicketsbtn.Visible = true;
            }
            else
            {
                orderTicketsbtn.Visible = false;
            }
            updateMovieDetailsPage(STitleLabel6.Text);
            BodyTabControl.SelectedTab = MovieDetailsTab;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        //Movie Details/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //--------------------------------------------------------------------------------------------------------------------------

        private void checkValidtime(XmlNodeList show, int index, Label display)
        {
           if (show[index].InnerText != "  :")
               display.Text = show[index].InnerText;
           else
               display.Text = "";
        }

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
                checkValidtime(Showtime1, index, showtime1);
                checkValidtime(Showtime2, index, showtime2);
                checkValidtime(Showtime3, index, showtime3);
                checkValidtime(Showtime4, index, showtime4);
                checkValidtime(Showtime5, index, showtime5);
                checkValidtime(Showtime6, index, showtime6);
                checkValidtime(Showtime7, index, showtime7);
                checkValidtime(Showtime8, index, showtime8);
                checkValidtime(Showtime9, index, showtime9);


            }
            else if (editInfo == true)
            {
                AMITitleBox.Text = TitleElemList[index].InnerText;
                AMILengthBox.Text = LengthElemList[index].InnerText;
                AMISynopsisBox.Text = SynopsisElemList[index].InnerText;
                AMIGenreBox.Text = GenreElemList[index].InnerText;
                AMIRatingBox.Text = RatingElemList[index].InnerText;
                AMIActorBox.Text = ActorsElemList[index].InnerText;
                AMIDirectorBox.Text = DirectorElemList[index].InnerText;
                AMIReleaseDTPicker.Text = ReleaseElemList[index].InnerText;
                AMIposterPathtxt.Text = PosterPath[index].InnerText;
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
                if (AMITitleBox.Text == TitleElemList[i].InnerText)
                {
                    currentInd = i;
                    break;
                }
            }
        }

        /* Poster Clicks */
        private void NRPoster1_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = false;
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel1.Text));
            updateMI();
        }

        private void NRPoster2_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = false;
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel2.Text));
            updateMI();
            
        }

        private void NRPoster3_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = false;
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel3.Text));
            updateMI();   
        }

        private void NRPoster4_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = false;
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel4.Text));
            updateMI();        
        }

        private void NRPoster5_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = false;
            updateMovieDetailsPage(GetMovieTitle(NRTitleLabel5.Text));
            updateMI();        
        }

        private void NSPoster1_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel1.Text));
            updateMI();           
        }

        private void NSPoster2_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel2.Text));
            updateMI();           
        }

        private void NSPoster3_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel3.Text));
            updateMI();           
        }

        private void NSPoster4_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel4.Text));
            updateMI();         
        }

        private void NSPoster5_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel5.Text));
            updateMI();           
        }

        private void NSPoster6_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel6.Text));
            updateMI();           
        }

        private void NSPoster7_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel7.Text));
            updateMI();          
        }

        private void NSPoster8_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel8.Text));
            updateMI();           
        }

        private void NSPoster9_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
            updateMovieDetailsPage(GetMovieTitle(NSTitleLabel9.Text));
            updateMI();          
        }

        private void NSPoster10_Click(object sender, EventArgs e)
        {
            orderTicketsbtn.Visible = true;
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
                || (string.IsNullOrEmpty(fNameTxt.Text)) || (string.IsNullOrEmpty(chfname.Text))
                || (string.IsNullOrEmpty(chlname.Text)) || (string.IsNullOrEmpty(ccn.Text))
                || (string.IsNullOrEmpty(CCV.Text)))
                MessageBox.Show("Required Fields Missing, please enter data.");
            else if (ccn.TextLength < 16)
                MessageBox.Show("Invalid credit card number. Must be 16 digits long.");
            else
            {
                XmlDocument doc = new XmlDocument();

                //If there is no current file, then create a new one
                if (!System.IO.File.Exists(AccountsPath))
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

                    XmlElement cardfname = doc.CreateElement("CH_FNAME");
                    XmlElement cardlname = doc.CreateElement("CH_LNAME");
                    XmlElement cardnum = doc.CreateElement("Card_Number");
                    XmlElement securitycode = doc.CreateElement("Security_Code");
                    XmlElement email = doc.CreateElement("email");

                    //Add the values for each nodes
                    fName.Value = fNameTxt.Text;
                    lName.Value = lNameTxt.Text;
                    address.InnerText = AddrTxt.Text;
                    city.InnerText = CityTxt.Text;
                    state.InnerText = StateTxt.Text;
                    username.InnerText = UserTxt.Text;
                    password.InnerText = passTxt.Text;
                    cardfname.InnerText = chfname.Text;
                    cardlname.InnerText = chlname.Text;
                    cardnum.InnerText = ccn.Text;
                    securitycode.InnerText = secCode.Text;
                    email.InnerText = emailBox.Text;


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
                    acc.AppendChild(cardfname);
                    acc.AppendChild(cardlname);
                    acc.AppendChild(cardnum);
                    acc.AppendChild(securitycode);
                    acc.AppendChild(email);

                    doc.Save(AccountsPath);
                }
                else //If there is already a file
                {
                    //Load the XML File
                    doc.Load(AccountsPath);

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

                    XmlElement cardfname = doc.CreateElement("CH_FNAME");
                    XmlElement cardlname = doc.CreateElement("CH_LNAME");
                    XmlElement cardnum = doc.CreateElement("Card_Number");
                    XmlElement securitycode = doc.CreateElement("Security_Code");
                    XmlElement email = doc.CreateElement("email");

                    //Add the values for each nodes
                    fName.Value = fNameTxt.Text;
                    lName.Value = lNameTxt.Text;
                    address.InnerText = AddrTxt.Text;
                    city.InnerText = CityTxt.Text;
                    state.InnerText = StateTxt.Text;
                    username.InnerText = UserTxt.Text;
                    password.InnerText = passTxt.Text;
                    cardfname.InnerText = chfname.Text;
                    cardlname.InnerText = chlname.Text;
                    cardnum.InnerText = ccn.Text;
                    securitycode.InnerText = CCV.Text;
                    email.InnerText = emailBox.Text;

                    //Construct the Person element
                    acc.Attributes.Append(fName);
                    acc.Attributes.Append(lName);
                    acc.AppendChild(address);
                    acc.AppendChild(city);
                    acc.AppendChild(state);
                    acc.AppendChild(username);
                    acc.AppendChild(password);
                    acc.AppendChild(cardfname);
                    acc.AppendChild(cardlname);
                    acc.AppendChild(cardnum);
                    acc.AppendChild(securitycode);
                    acc.AppendChild(email);
                    Console.Out.Write(securitycode.InnerText + "\n");

                    //Add the New person element to the end of the root element
                    root.AppendChild(acc);

                    //Save the document
                    doc.Save(AccountsPath);
                }

                fNameTxt.Clear();
                lNameTxt.Clear();
                AddrTxt.Clear();
                CityTxt.Clear();
                StateTxt.Clear();
                emailBox.Clear();
                UserTxt.Clear();
                passTxt.Clear();
                chfname.Clear();
                chlname.Clear();
                ccn.Clear();
                CCV.Clear();

                MessageBox.Show("Successfully created account!");

                BodyTabControl.SelectedTab = LoginTab; //Return to login screen
            }

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
                                  CHfName = acc.Element("CH_FNAME").Value,
                                  CHlName = acc.Element("CH_LNAME").Value,
                                  CHnum = acc.Element("Card_Number").Value,
                                  Sec = acc.Element("Security_Code").Value,
                                  emailto = acc.Element("email").Value,

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
                            if (needslogin == false)
                                BodyTabControl.SelectedTab = HomeTab;
                            else
                                BodyTabControl.SelectedTab = Purchase;

                            adminLogged = false;
                        }

                        currentUser = acc.Username;
                        currentemail = acc.emailto;
                        PurchaseFname.Text = acc.CHfName;
                        PurchaseLname.Text = acc.CHlName;
                        //MAKE SURE TO CHECK THAT EVERYTHING THEY ENTERED IS CORRECT
                        //VALIDATION
                        PurchaseCnum.Text = "****-****-****-" + acc.CHnum.Substring(12, 4);
                        //PurchaseCnum.Text = "****-****-****-" + acc.CHnum.Substring(4, 12);
                        SC = acc.Sec;


                        MessageBox.Show("Successfully signed in!");
                        logged = true;

                        LoginBtn.Image = new Bitmap("../../Resources/account_button.jpg");

                        homeLogged.Visible = true;
                        checkLog.Visible = true;
                        checkLog.Text = usernameTxt.Text;

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
                        st2cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 3:
                    {
                        st3lbl.Visible = true;
                        st3txt.Visible = true;
                        st3cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 4:
                    {
                        st4lbl.Visible = true;
                        st4txt.Visible = true;
                        st4cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 5:
                    {
                        st5lbl.Visible = true;
                        st5txt.Visible = true;
                        st5cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 6:
                    {
                        st6lbl.Visible = true;
                        st6txt.Visible = true;
                        st6cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 7:
                    {
                        st7lbl.Visible = true;
                        st7txt.Visible = true;
                        st7cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 8:
                    {
                        st8lbl.Visible = true;
                        st8txt.Visible = true;
                        st8cb.Visible = true;
                        showtimes++;
                        break;
                    }
                case 9:
                    {
                        st9lbl.Visible = true;
                        st9txt.Visible = true;
                        st9cb.Visible = true;
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
                st2cb.Visible = false;

                st3lbl.Visible = false;
                st3txt.Visible = false;
                st3cb.Visible = false;

                st4lbl.Visible = false;
                st4txt.Visible = false;
                st4cb.Visible = false;

                st5lbl.Visible = false;
                st5txt.Visible = false;
                st5cb.Visible = false;

                st6lbl.Visible = false;
                st6txt.Visible = false;
                st6cb.Visible = false;

                st7lbl.Visible = false;
                st7txt.Visible = false;
                st7cb.Visible = false;

                st8lbl.Visible = false;
                st8txt.Visible = false;
                st8cb.Visible = false;

                st9lbl.Visible = false;
                st9txt.Visible = false;
                st9cb.Visible = false;
            }

            else
            {
                st2lbl.Visible = true;
                st2txt.Visible = true;
                st2cb.Visible = true;

                st3lbl.Visible = true;
                st3txt.Visible = true;
                st3cb.Visible = true;

                st4lbl.Visible = true;
                st4txt.Visible = true;
                st4cb.Visible = true;

                st5lbl.Visible = true;
                st5txt.Visible = true;
                st5cb.Visible = true;

                st6lbl.Visible = true;
                st6txt.Visible = true;
                st6cb.Visible = true;

                st7lbl.Visible = true;
                st7txt.Visible = true;
                st7cb.Visible = true;

                st8lbl.Visible = true;
                st8txt.Visible = true;
                st8cb.Visible = true;

                st9lbl.Visible = true;
                st9txt.Visible = true;
                st9cb.Visible = true;

                moreShowingsbtn.Visible = false;
            }
        }

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

                Title.InnerText = AMITitleBox.Text;
                Length.InnerText = AMILengthBox.Text;
                Synopsis.InnerText = AMISynopsisBox.Text;
                Description.InnerText = AMIDescriptionBox.Text;
                Genre.InnerText = AMIGenreBox.Text;
                Rating.InnerText = AMIRatingBox.Text;
                Actor.InnerText = AMIActorBox.Text;
                Director.InnerText = AMIDirectorBox.Text;
                ReleaseDate.InnerText = AMIReleaseDTPicker.Value.ToString("MM/dd/yyyy");
                PosterPath.InnerText = openFileDialog1.FileName;
                Showtime1.InnerText = st1txt.Text + st1cb.ToString();
                Showtime2.InnerText = st2txt.Text + st2cb.ToString();
                Showtime3.InnerText = st3txt.Text + st3cb.ToString();
                Showtime4.InnerText = st4txt.Text + st4cb.ToString();
                Showtime5.InnerText = st5txt.Text + st5cb.ToString();
                Showtime6.InnerText = st6txt.Text + st6cb.ToString();
                Showtime7.InnerText = st7txt.Text + st7cb.ToString();
                Showtime8.InnerText = st8txt.Text + st8cb.ToString();
                Showtime9.InnerText = st9txt.Text + st9cb.ToString();

                MoviesDocument.Save(MoviesPath);

                AMITitleBox.Clear();
                AMILengthBox.Clear();
                AMISynopsisBox.Clear();
                AMIDescriptionBox.Clear();
                AMIGenreBox.ResetText();
                AMIRatingBox.ResetText();
                AMIActorBox.Clear();
                AMIDirectorBox.Clear();
                AMIReleaseDTPicker.ResetText();
                AMIposterPathtxt.Clear();
                resetShowtimes();
            }
            else
            {

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

                MessageBox.Show("Updated movie: " + AMITitleBox.Text);

                TitleElemList[currentInd].InnerText = AMITitleBox.Text;
                LengthElemList[currentInd].InnerText = AMILengthBox.Text;
                SynopsisElemList[currentInd].InnerText = AMISynopsisBox.Text;
                GenreElemList[currentInd].InnerText = AMIGenreBox.Text;
                RatingElemList[currentInd].InnerText = AMIRatingBox.Text;
                ActorsElemList[currentInd].InnerText = AMIActorBox.Text;
                DirectorElemList[currentInd].InnerText = AMIDirectorBox.Text;
                ReleaseElemList[currentInd].InnerText = AMIReleaseDTPicker.Text;
                PosterPath[currentInd].InnerText = AMIposterPathtxt.Text;
                Showtime1[currentInd].InnerText = st1txt.Text + st1cb.Text;
                Showtime2[currentInd].InnerText = st2txt.Text + st2cb.Text;
                Showtime3[currentInd].InnerText = st3txt.Text + st3cb.Text;
                Showtime4[currentInd].InnerText = st4txt.Text + st4cb.Text;
                Showtime5[currentInd].InnerText = st5txt.Text + st5cb.Text;
                Showtime6[currentInd].InnerText = st6txt.Text + st6cb.Text;
                Showtime7[currentInd].InnerText = st7txt.Text + st7cb.Text;
                Showtime8[currentInd].InnerText = st8txt.Text + st8cb.Text;
                Showtime9[currentInd].InnerText = st9txt.Text + st9cb.Text;
                MoviesDocument.Save(MoviesPath);

                editInfo = false;
                BodyTabControl.SelectedTab = AdminTab;
            }

        }

        //Admin Clicks edit a movie
        private void adminEdit_Click(object sender, EventArgs e)
        {
            editMovie = true;
            editInfo = true;
            AddBtn.Text = "Update Info";
            resetShowtimes();
            RefreshBrowse(1);
            BodyTabControl.SelectedTab = BrowseTab;
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
            AMIposterPathtxt.Text = path;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Seating Page////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //-------------------------------------------------------------------------------------------------------------------------
        private void selected_Seats(Button select, int i)
        {
            if (seats < totalseats)
            {
                if (track[i] % 2 == 0)
                {
                    select.BackColor = Color.Yellow;
                    selectedSeatstxt.AppendText(select.Name + "\n");
                    seats++;
                }
                else
                {
                    select.BackColor = Color.Maroon;
                    selectedSeatstxt.Lines = selectedSeatstxt.Lines.Where(line => !line.Contains(select.Name)).ToArray();
                    seats--;
                }

                track[i]++;
            }
            else if (seats == totalseats && select.BackColor == Color.Yellow)
            {
                select.BackColor = Color.Maroon;
                selectedSeatstxt.Lines = selectedSeatstxt.Lines.Where(line => !line.Contains(select.Name)).ToArray();
                seats--;
                track[i]++;
            }
            else
                MessageBox.Show("You have already picked your seats.");

            Console.WriteLine("seats: {0}, totalseats: {1}", seats, totalseats);

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
            selected_Seats(c_10, 25);
        }

        private void c11_Click(object sender, EventArgs e)
        {
            selected_Seats(c_11, 26);
        }

        private void c12_Click(object sender, EventArgs e)
        {
            selected_Seats(c_12, 27);
        }

        private void c13_Click(object sender, EventArgs e)
        {
            selected_Seats(c_13, 28);
        }

        private void c14_Click(object sender, EventArgs e)
        {
            selected_Seats(c_14, 29);
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
            selected_Seats(d_10, 39);
        }

        private void d11_Click(object sender, EventArgs e)
        {
            selected_Seats(d_11, 40);
        }

        private void d12_Click(object sender, EventArgs e)
        {
            selected_Seats(d_12, 41);
        }

        private void d13_Click(object sender, EventArgs e)
        {
            selected_Seats(d_13, 42);
        }

        private void d14_Click(object sender, EventArgs e)
        {
            selected_Seats(d_14, 43);
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
            selected_Seats(e_10, 53);
        }

        private void e11_Click(object sender, EventArgs e)
        {
            selected_Seats(e_11, 54);
        }

        private void e12_Click(object sender, EventArgs e)
        {
            selected_Seats(e_12, 55);
        }

        private void e13_Click(object sender, EventArgs e)
        {
            selected_Seats(e_13, 56);
        }

        private void e14_Click(object sender, EventArgs e)
        {
            selected_Seats(e_14, 57);
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
            selected_Seats(f_10, 67);
        }

        private void f11_Click(object sender, EventArgs e)
        {
            selected_Seats(f_11, 68);
        }

        private void f12_Click(object sender, EventArgs e)
        {
            selected_Seats(f_12, 69);
        }

        private void f13_Click(object sender, EventArgs e)
        {
            selected_Seats(f_13, 70);
        }

        private void f14_Click(object sender, EventArgs e)
        {
            selected_Seats(f_14, 71);
        }

        //-----------------------------------------------------------------------------------------------------------------------
        //BUY SEATING PAGE//////////////////////////////////////////////////////////////////////////////////////////////////////
        //-----------------------------------------------------------------------------------------------------------------------
        private void resetSeats()
        {
            Array.Clear(track, 0, track.Length);
            selectedSeatstxt.Clear();
            totalseats = 0;
            seats = 0;
            a1.BackColor = Color.Maroon;
            a2.BackColor = Color.Maroon;
            a3.BackColor = Color.Maroon;
            a4.BackColor = Color.Maroon;
            a5.BackColor = Color.Maroon;
            a6.BackColor = Color.Maroon;
            a7.BackColor = Color.Maroon;
            a8.BackColor = Color.Maroon; 
            b1.BackColor = Color.Maroon;
            b2.BackColor = Color.Maroon;
            b3.BackColor = Color.Maroon;
            b4.BackColor = Color.Maroon;
            b5.BackColor = Color.Maroon;
            b6.BackColor = Color.Maroon;
            b7.BackColor = Color.Maroon;
            b8.BackColor = Color.Maroon;
            c1.BackColor = Color.Maroon;
            c2.BackColor = Color.Maroon;
            c3.BackColor = Color.Maroon;
            c4.BackColor = Color.Maroon;
            c5.BackColor = Color.Maroon;
            c6.BackColor = Color.Maroon;
            c7.BackColor = Color.Maroon;
            c8.BackColor = Color.Maroon;
            c9.BackColor = Color.Maroon;
            c_10.BackColor = Color.Maroon;
            c_11.BackColor = Color.Maroon;
            c_12.BackColor = Color.Maroon;
            c_13.BackColor = Color.Maroon;
            c_14.BackColor = Color.Maroon;
            d1.BackColor = Color.Maroon;
            d2.BackColor = Color.Maroon;
            d3.BackColor = Color.Maroon;
            d4.BackColor = Color.Maroon;
            d5.BackColor = Color.Maroon;
            d6.BackColor = Color.Maroon;
            d7.BackColor = Color.Maroon;
            d8.BackColor = Color.Maroon;
            d9.BackColor = Color.Maroon;
            d_10.BackColor = Color.Maroon;
            d_11.BackColor = Color.Maroon;
            d_12.BackColor = Color.Maroon;
            d_13.BackColor = Color.Maroon;
            d_14.BackColor = Color.Maroon;
            e1.BackColor = Color.Maroon;
            e2.BackColor = Color.Maroon;
            e3.BackColor = Color.Maroon;
            e4.BackColor = Color.Maroon;
            e5.BackColor = Color.Maroon;
            e6.BackColor = Color.Maroon;
            e7.BackColor = Color.Maroon;
            e8.BackColor = Color.Maroon;
            e9.BackColor = Color.Maroon;
            e_10.BackColor = Color.Maroon;
            e_11.BackColor = Color.Maroon;
            e_12.BackColor = Color.Maroon;
            e_13.BackColor = Color.Maroon;
            e_14.BackColor = Color.Maroon;
            f1.BackColor = Color.Maroon;
            f2.BackColor = Color.Maroon;
            f3.BackColor = Color.Maroon;
            f4.BackColor = Color.Maroon;
            f5.BackColor = Color.Maroon;
            f6.BackColor = Color.Maroon;
            f7.BackColor = Color.Maroon;
            f8.BackColor = Color.Maroon;
            f9.BackColor = Color.Maroon;
            f_10.BackColor = Color.Maroon;
            f_11.BackColor = Color.Maroon;
            f_12.BackColor = Color.Maroon;
            f_13.BackColor = Color.Maroon;
            f_14.BackColor = Color.Maroon;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (seats != totalseats)
                MessageBox.Show("Please choose the rest of your seats.");
            else
            {
                //BUTTON TAKES USERS TO THE BUY TICKETS PAGE
                if (logged == false)
                {
                    needslogin = true;
                    DialogResult dialogResult = MessageBox.Show("You need to be logged into your account to purchase tickets. \n Do you want to login now? (No returns to homepage.)", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        BodyTabControl.SelectedTab = LoginTab;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        BodyTabControl.SelectedTab = HomeTab;
                    }
                }
                else
                    BodyTabControl.SelectedTab = Purchase;

                seatsSelected = getSeatsFromString(selectedSeatstxt.Text);
                seatsTaken();

                List<string> list = new List<string>(
                               selectedSeatstxt.Text.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
                SeatticketLabel.Text = string.Join(", ", list);

                resetSeats();
            }
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
            // BodyTabControl.SelectedTab = Seating;

            updateSeats();

            //SETS UP EVERYTHING ON THE NEXT PAGE
            double subtotal, child, senior, adult;
            int childTicket, seniorTicket, adultTicket;
            child = Convert.ToDouble(label40.Text);
            senior = Convert.ToDouble(label39.Text);
            adult = Convert.ToDouble(label38.Text);
            subtotal = child + senior + adult;
            totalCost.Text = subtotal.ToString("$0.00");

            //CHECKS FOR TICKET AMOUNT PURCHASE

            //MISSING BOTH SHOWTIME AND TICKET AMOUNT
            if (displayShowtimelbl.Text == "0:00" && comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "")
            {
                MessageBox.Show("Please enter ticket amount and show time.");
                tixCounter.Clear();


            }
            //MISSING TICKET AMOUNT
            else if (comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "")
            {
                MessageBox.Show("Please enter a ticket amount!");
                tixCounter.Clear();

            }

            //MISSING JUST SHOW TIME
            else if (displayShowtimelbl.Text == "0:00")
            {
                MessageBox.Show("Please enter show time.");
                tixCounter.Clear();
            }
            else if (HasMovieTimePassed(displayShowtimelbl.Text))
            {
                MessageBox.Show("Time has passed. Please pick another time.");
            }
            else
                BodyTabControl.SelectedTab = Seating;

            //GETS NUMBER TICKETS TO CHECK
            if (comboBox1.Text == "")
            {
                adultTicket = 0;
            }
            else
                adultTicket = Convert.ToInt32(comboBox1.Text);

            if (comboBox2.Text == "")
            {
                seniorTicket = 0;
            }
            else
                seniorTicket = Convert.ToInt32(comboBox2.Text);

            if (comboBox3.Text == "")
            {
                childTicket = 0;
            }
            else
                childTicket = Convert.ToInt32(comboBox3.Text);

            totalseats = childTicket + adultTicket + seniorTicket;

            //CHECKS AND CLEARS
            if (tixCounter.Count != 0)
                tixCounter.Clear();

            //ADDS UP LATEST DATA
            tixCounter.Add("Child", childTicket);
            tixCounter.Add("Adult", adultTicket);
            tixCounter.Add("Senior", seniorTicket);
            ticketTotal = childTicket + seniorTicket + adultTicket;
            ticketcount.Text = ticketTotal.ToString();

            //Saves data for purchase
            time = displayShowtimelbl.Text;
            date = showtimeDate.Text;
            movieTitle = displayMovieTitle.Text;

            movieSeattxt.Text = MDTitleLabel.Text.ToString();
            lengthSeattxt.Text = MDLengthLabel.Text.ToString();
            ShowtimeSeattxt.Text = displayShowtimelbl.Text.ToString();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

        }


        DateTime selectedDate = new DateTime();

        private bool HasMovieTimePassed(string movieTime)
        {
            bool flag = false;
            var currDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second);
            int sentinel;
            var currTime = DateTime.Now.TimeOfDay;

            if (selectedDate != null)
            {
                sentinel = currDate.CompareTo(selectedDate);
                if (sentinel > 0)
                {
                    flag = true;
                }
                else if (sentinel == 0)
                {
                    int result = TimeSpan.Compare(currTime, ConvertToMilitaryTime(movieTime));
                    if (result >= 0)
                    {
                        //CURRTIME AFTER CURRENT TIME
                        flag = true;
                    }
                    else
                        flag = false;
                    
                }
                else return flag;

            }
            return flag;
        }

        //Note: This assumes a format of hh:mmtt which all showtimes follow
        private TimeSpan ConvertToMilitaryTime(string time)
        {
            int colonIndex = time.IndexOf(':');
            int hours = Convert.ToInt32(time.Substring(0, colonIndex));
            int minutes = Convert.ToInt32(time.Substring(colonIndex + 1, 2));
            string meridian = time.Substring(time.Length - 2).ToLower();
            if (meridian == "pm" && hours != 12)
                hours += 12;
            if (meridian == "am" && hours == 12)
                hours = 0;
            var result = new TimeSpan(hours, minutes, 0);
            return result;  
        }







        //GLOBAL VARIABLES SO I CAN SAVE MY INFO FOR TIXS
        string time, date, movieTitle;
        int ticketTotal;
        List<string> seatsSelected;
        Dictionary<string, int> tixCounter = new Dictionary<string, int>();
        string name;
        List<Ticket> transaction = new List<Ticket>();
        //------------------------------------------------------------------------------------------------------
        //  PURCHASE PAGE
        private void button5_Click(object sender, EventArgs e)
        {
            if (SC != PurchaseSec.Text)
                MessageBox.Show("Security Code does not match our records. Please re-enter the code.");
            else
            {
                name = PurchaseFname.Text + "  " + PurchaseLname.Text;
                int indexCounter = 0;
                foreach (string type in tixCounter.Keys)
                {
                    int num = tixCounter[type];
                    while (num > 0)
                    {
                        //Console.Out.Write(type + " " + num.ToString());
                        TicketAdmissionLabel.Text = type;
                        TicketDateLabel.Text = date;
                        nameticketlabel.Text = name;
                        timeticketlabel.Text = time;
                        ticketPoster.ImageLocation = MDBigPoster.ImageLocation;
                        address1tick.Text = addressInfo1Label.Text;
                        address2tik.Text = addressInfo2Label.Text;

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
            }

        }

        private void containsSeat(string seats)
        {
            if (seats.Contains("a1"))
                a1.Visible = false;
            if (seats.Contains("a2"))
                a2.Visible = false;
            if (seats.Contains("a3"))
                a3.Visible = false;
            if (seats.Contains("a4"))
                a4.Visible = false;
            if (seats.Contains("a5"))
                a5.Visible = false;
            if (seats.Contains("a6"))
                a6.Visible = false;
            if (seats.Contains("a7"))
                a7.Visible = false;
            if (seats.Contains("a8"))
                a8.Visible = false;
            if (seats.Contains("b1"))
                b1.Visible = false;
            if (seats.Contains("b2"))
                b2.Visible = false;
            if (seats.Contains("b3"))
                b3.Visible = false;
            if (seats.Contains("b4"))
                b4.Visible = false;
            if (seats.Contains("b5"))
                b5.Visible = false;
            if (seats.Contains("b6"))
                b6.Visible = false;
            if (seats.Contains("b7"))
                b7.Visible = false;
            if (seats.Contains("b8"))
                b8.Visible = false;
            if (seats.Contains("c1"))
                c1.Visible = false;
            if (seats.Contains("c2"))
                c2.Visible = false;
            if (seats.Contains("c3"))
                c3.Visible = false;
            if (seats.Contains("c4"))
                c4.Visible = false;
            if (seats.Contains("c5"))
                c5.Visible = false;
            if (seats.Contains("c6"))
                c6.Visible = false;
            if (seats.Contains("c7"))
                c7.Visible = false;
            if (seats.Contains("c8"))
                c8.Visible = false;
            if (seats.Contains("c9"))
                c9.Visible = false;
            if (seats.Contains("c_10"))
                c_10.Visible = false;
            if (seats.Contains("c_11"))
                c_11.Visible = false;
            if (seats.Contains("c_12"))
                c_12.Visible = false;
            if (seats.Contains("c_13"))
                c_13.Visible = false;
            if (seats.Contains("c_14"))
                c_14.Visible = false;
            if (seats.Contains("d1"))
                d1.Visible = false;
            if (seats.Contains("d2"))
                d2.Visible = false;
            if (seats.Contains("d3"))
                d3.Visible = false;
            if (seats.Contains("d4"))
                d4.Visible = false;
            if (seats.Contains("d5"))
                d5.Visible = false;
            if (seats.Contains("d6"))
                d6.Visible = false;
            if (seats.Contains("d7"))
                d7.Visible = false;
            if (seats.Contains("d8"))
                d8.Visible = false;
            if (seats.Contains("d9"))
                d9.Visible = false;
            if (seats.Contains("d_10"))
                d_10.Visible = false;
            if (seats.Contains("d_11"))
                d_11.Visible = false;
            if (seats.Contains("d_12"))
                d_12.Visible = false;
            if (seats.Contains("d_13"))
                d_13.Visible = false;
            if (seats.Contains("d_14"))
                d_14.Visible = false;
            if (seats.Contains("e1"))
                e1.Visible = false;
            if (seats.Contains("e2"))
                e2.Visible = false;
            if (seats.Contains("e3"))
                e3.Visible = false;
            if (seats.Contains("e4"))
                e4.Visible = false;
            if (seats.Contains("e5"))
                e5.Visible = false;
            if (seats.Contains("e6"))
                e6.Visible = false;
            if (seats.Contains("e7"))
                e7.Visible = false;
            if (seats.Contains("e8"))
                e8.Visible = false;
            if (seats.Contains("e9"))
                e9.Visible = false;
            if (seats.Contains("e_10"))
                e_10.Visible = false;
            if (seats.Contains("e_11"))
                e_11.Visible = false;
            if (seats.Contains("e_12"))
                e_12.Visible = false;
            if (seats.Contains("e_13"))
                e_13.Visible = false;
            if (seats.Contains("e_14"))
                e_14.Visible = false;
            if (seats.Contains("f1"))
                f1.Visible = false;
            if (seats.Contains("f2"))
                f2.Visible = false;
            if (seats.Contains("f3"))
                f3.Visible = false;
            if (seats.Contains("f4"))
                f4.Visible = false;
            if (seats.Contains("f5"))
                f5.Visible = false;
            if (seats.Contains("f6"))
                f6.Visible = false;
            if (seats.Contains("f7"))
                f7.Visible = false;
            if (seats.Contains("f8"))
                f8.Visible = false;
            if (seats.Contains("f9"))
                f9.Visible = false;
            if (seats.Contains("f_10"))
                f_10.Visible = false;
            if (seats.Contains("f_11"))
                f_11.Visible = false;
            if (seats.Contains("f_12"))
                f_12.Visible = false;
            if (seats.Contains("f_13"))
                f_13.Visible = false;
            if (seats.Contains("f_14"))
                f_14.Visible = false;
        }



        private void updateSeats()//Updates the movie info according to admin edits
        {
            if (System.IO.File.Exists(SeatingPath))
            {
                SeatingDocument.Load(SeatingPath);

                XmlNodeList TitleElemList = SeatingDocument.GetElementsByTagName("Title");
                XmlNodeList DateElemList = SeatingDocument.GetElementsByTagName("Date");
                XmlNodeList ShowtimeElemList = SeatingDocument.GetElementsByTagName("Showtime");
                XmlNodeList seatsElemList = SeatingDocument.GetElementsByTagName("Reserved");

                for (int i = 0; i < TitleElemList.Count; i++)
                {
                    if (displayMovieTitle.Text == TitleElemList[i].InnerText)
                    {
                        if (showtimeDate.Value.ToString("MM/dd/yyyy") == DateElemList[i].InnerText)
                        {
                            if (displayShowtimelbl.Text == ShowtimeElemList[i].InnerText)
                            {
                                seatingChart = true;
                                seatInd = i;
                                containsSeat(seatsElemList[seatInd].InnerText);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void seatsTaken()
        {
                XmlElement Root;

                if (!System.IO.File.Exists(SeatingPath))
                {
                    XmlDeclaration Declaration = SeatingDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    XmlComment Comment = SeatingDocument.CreateComment("SeatingInfo");
                    Root = SeatingDocument.CreateElement("Seats");

                    SeatingDocument.AppendChild(Declaration);
                    SeatingDocument.AppendChild(Comment);
                    SeatingDocument.AppendChild(Root);

                    XmlElement seating = SeatingDocument.CreateElement("Seats_taken");
                    XmlElement Title = SeatingDocument.CreateElement("Title");
                    XmlElement Date = SeatingDocument.CreateElement("Date");
                    XmlElement Showtime = SeatingDocument.CreateElement("Showtime");
                    XmlElement seats_taken = SeatingDocument.CreateElement("Reserved");

                    Root.AppendChild(seating);
                    seating.AppendChild(Title);
                    seating.AppendChild(Showtime);
                    seating.AppendChild(Date);
                    seating.AppendChild(seats_taken);

                    Title.InnerText = displayMovieTitle.Text;
                    Showtime.InnerText = displayShowtimelbl.Text;
                    Date.InnerText = showtimeDate.Value.ToString("MM/dd/yyyy");
                    List<string> list = new List<string>(
                               selectedSeatstxt.Text.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
                    seats_taken.InnerText = string.Join(",", list);

                    SeatingDocument.Save(SeatingPath);
                }

                else
                {
                    if (seatingChart == false)
                    {
                        SeatingDocument.Load(SeatingPath);
                        Root = SeatingDocument.DocumentElement;
                        // dont have to check if there is a movie file since you clicked on
                        // a movie poster to get here. load the movie xml file.

                        XmlElement seating = SeatingDocument.CreateElement("Seats_taken");
                        XmlElement Title = SeatingDocument.CreateElement("Title");
                        XmlElement Date = SeatingDocument.CreateElement("Date");
                        XmlElement Showtime = SeatingDocument.CreateElement("Showtime");
                        XmlElement seats_taken = SeatingDocument.CreateElement("Reserved");

                        Root.AppendChild(seating);
                        seating.AppendChild(Title);
                        seating.AppendChild(Showtime);
                        seating.AppendChild(Date);
                        seating.AppendChild(seats_taken);

                        Title.InnerText = displayMovieTitle.Text;
                        Showtime.InnerText = displayShowtimelbl.Text;
                        Date.InnerText = showtimeDate.Value.ToString("MM/dd/yyyy");
                        List<string> list = new List<string>(
                                   selectedSeatstxt.Text.Split(new string[] { "\n" },
                                   StringSplitOptions.RemoveEmptyEntries));
                        seats_taken.InnerText = string.Join(",", list);

                        SeatingDocument.Save(SeatingPath);
                    }
                    else
                    {
                        SeatingDocument.Load(SeatingPath);

                        XmlNodeList seatsElemList = SeatingDocument.GetElementsByTagName("Reserved");

                        List<string> list = new List<string>(
                                   selectedSeatstxt.Text.Split(new string[] { "\n" },
                                   StringSplitOptions.RemoveEmptyEntries));
                        seatsElemList[seatInd].InnerText = seatsElemList[seatInd].InnerText + string.Join(",", list);

                        SeatingDocument.Save(SeatingPath);
                    }

                }
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

            TransactionsDocument.Save(TransactionsPath);

        }

        //------------------------------------------------------------------------------------------------------
        // PRINT TICKETS PAGE
        //
        private void printTixButton_Click(object sender, EventArgs e)
        {
            //WILL PRINT TICKET ONCE IT IS CLICKED

            //766, 315
            Rectangle bounds = this.Bounds;
            //using (Bitmap bitmap = new Bitmap(groupTicketBox.Width, groupTicketBox.Height))
            using(Bitmap bitmap = new Bitmap (1000, 350))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(groupTicketBox.Left, groupTicketBox.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save("../../Posters/test.jpg", ImageFormat.Jpeg);
            }

            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPage;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }

        }


        private void emailtickets_Click(object sender, EventArgs e)
        {
            //EMAILS 
            //=======================================================================================
            //766, 315

            //SAVES IMAGE CODE
            Rectangle bounds = this.Bounds;
            //using (Bitmap bitmap = new Bitmap(groupTicketBox.Width, groupTicketBox.Height))
            using (Bitmap bitmap = new Bitmap(1000, 350))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(groupTicketBox.Left, groupTicketBox.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save("../../Posters/test.jpg", ImageFormat.Jpeg);
            }

            //ACTUAL EMAIL CODE
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("movies362@gmail.com");
                mail.To.Add(currentemail);
                mail.Subject = "Movie Ticket Purchase Confirmation";
                mail.Body = " Hello, The movie ticket(s) you ordered have been sent and attached to this email! \n You can print them when you please. Just make sure to have them before entering the establishment.\n Regards, Pseudo Cinema";

                string attachmentPath = @"../../Posters/test.jpg";
                Attachment inline = new Attachment(attachmentPath);
                inline.ContentDisposition.Inline = true;
                inline.ContentType.MediaType = "image/jpg";
                inline.ContentType.Name = Path.GetFileName(attachmentPath);

                mail.Attachments.Add(inline);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("semovies362@gmail.com", "movies362");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Email sent to: " + currentemail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }



        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            //FIX IMAGE CAPTURE
            Bitmap bmp = new Bitmap(this.groupTicketBox.Width, this.groupTicketBox.Height);
            this.groupTicketBox.DrawToBitmap(bmp, new Rectangle(0, 0, this.groupTicketBox.Width, this.groupTicketBox.Height));
            e.Graphics.DrawImage((Image)bmp, x, y);
            //ADDS TEXT TO THE IMAGE
            e.Graphics.DrawString(nameticketlabel.Text, SystemFonts.DefaultFont, Brushes.Black, 642, 201);          //NAME
            e.Graphics.DrawString(ticketTitle.Text, SystemFonts.DefaultFont, Brushes.Black, 650, 219);               //MOVIE TITLE
            e.Graphics.DrawString(SeatticketLabel.Text, SystemFonts.DefaultFont, Brushes.Black, 574, 245);          //SEAT
            e.Graphics.DrawString(cinemanametik.Text, SystemFonts.DefaultFont, Brushes.Black, 600, 280);             //PLACE
            e.Graphics.DrawString(address1tick.Text, SystemFonts.DefaultFont, Brushes.Black, 600, 300);             //ADDRESS
            e.Graphics.DrawString(address2tik.Text, SystemFonts.DefaultFont, Brushes.Black, 600, 320);              // ADDRESS
            e.Graphics.DrawString(TicketDateLabel.Text, SystemFonts.DefaultFont, Brushes.Black, 574, 400);          //DATE
            e.Graphics.DrawString(timeticketlabel.Text, SystemFonts.DefaultFont, Brushes.Black, 874, 400);          //TIME
            e.Graphics.DrawString(TicketAdmissionLabel.Text, SystemFonts.DefaultFont, Brushes.Black, 874, 450);    //TYPE
            e.Graphics.DrawString(randtik.Text, SystemFonts.DefaultFont, Brushes.Black, 500, 245);                  //SEAT TITLE

            //ADDS GRAPHIC TO IMAGE
            //RESIZE IT
            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(165, 190, 350, 350);

            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, 350, 450);
            GraphicsUnit units = GraphicsUnit.Pixel;
            e.Graphics.DrawImage(ticketPoster.Image, destRect, srcRect, units);
        }


        private void gobackhomebutton_Click(object sender, EventArgs e)
        {
            BodyTabControl.SelectedTab = HomeTab;
            //CLEARS OUT INFO SO THEY CAN BUY OTHER STUFF OR BROWSE
            //CLEARS OUT INFO
            //PurchaseFname.Text = null;
            //PurchaseLname.Text = null;
            //PurchaseCnum.Text = null;
            PurchaseSec.Text = null;
            displayShowtimelbl.Text = "0:00";
            selectedSeatstxt.Text = "";
            seats = 0;
            resetSeats();

        }

        private void showtimeDate_ValueChanged(object sender, EventArgs e)
        {
            displayDatelbl.Text = showtimeDate.Value.Date.ToLongDateString();
            selectedDate = showtimeDate.Value.Date;
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
            displayMovieTitle.Text = MDTitleLabel.Text.ToString();
            displayRating.Text = MDRatingLabel.Text.ToString();
            displayLength.Text = MDLengthLabel.Text.ToString();
            BodyTabControl.SelectedTab = Ticket;
        }

        // log out stuff
        private void AILogoutBtn_Click(object sender, EventArgs e)
        {
            logout();
        }

        public void logout()
        {
            MessageBox.Show("You have been logged out.");
            logged = false;
            adminLogged = false;
            LoginBtn.Image = new Bitmap("../../Resources/login_button.jpg");
            updateHomePage();
            checkLog.Visible = false;
            homeLogged.Visible = false;
            checkLog.Text = "";
            BodyTabControl.SelectedTab = HomeTab;
        }

        private void ALogoutBtn_Click(object sender, EventArgs e)
        {
            logout();
        }

        //-----------------------------------------------------------------------------------------------------
    }
}
