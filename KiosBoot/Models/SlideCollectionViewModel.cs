using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.ConvertModel;
using KiosBoot.Helpers.Server;
using KiosBoot.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace KiosBoot.Models
{
    public class SlideCollectionViewModel : ObservableObject
    {
        //Collection of picture slides
        public ObservableCollection<PictureViewModel> MemorySlides { get; private set; }

        //Selected slides for matching
        private PictureViewModel SelectedSlide1;

        private PictureViewModel SelectedSlide2;

        //Timers for peeking at slides and initial display for memorizing
        private DispatcherTimer _peekTimer;

        private DispatcherTimer _openingTimer;

        //Interval for how long a user peeks at selections
        private const int _peekSeconds = 1;

        //Interval for how long a user has to memorize slides
        private const int _openSeconds = 5;

        //Are selected slides still being displayed
        public bool areSlidesActive
        {
            get
            {
                if (SelectedSlide1 == null || SelectedSlide2 == null)
                    return true;

                return false;
            }
        }

        //Have all slides been matched
        public bool AllSlidesMatched
        {
            get
            {
                foreach (var slide in MemorySlides)
                {
                    if (!slide.isMatched)
                        return false;
                }

                return true;
            }
        }

        //Can user select a slide
        public bool canSelect { get; private set; }

        public SlideCollectionViewModel()
        {
            _peekTimer = new DispatcherTimer();
            _peekTimer.Interval = new TimeSpan(0, 0, _peekSeconds);
            _peekTimer.Tick += PeekTimer_Tick;
            //dispatcherTimer.Tick += dispatcherTimer_Tick;
            //void dispatcherTimer_Tick(object sender, object e)

            _openingTimer = new DispatcherTimer();
            _openingTimer.Interval = new TimeSpan(0, 0, _openSeconds);
            _openingTimer.Tick += OpeningTimer_Tick;
        }

        public GamePictureModel CallImageFromServer(string url)
        {
            var api = new ApiData();

            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeA";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeB";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeC";

            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            GamePictureModel picture = JsonConvert.DeserializeObject<GamePictureModel>(result);

            return picture;
        }

        //Create slides from images in file directory
        public void CreateSlides(SlideCategories category)
        {
            string url = "";
            int MaxImage = 0;
            if (category == SlideCategories.Lv10)
            {
                MaxImage = 5;
                url = DataConfig.ApiDomain() + "/api/collections/get/GameTypeA";
            }
            else if (category == SlideCategories.Lv16)
            {
                MaxImage = 8;
                url = DataConfig.ApiDomain() + "/api/collections/get/GameTypeB";
            }
            else if (category == SlideCategories.Lv24)
            {
                MaxImage = 26;
                url = DataConfig.ApiDomain() + "/api/collections/get/GameTypeC";
            }

            //New list of slides
            MemorySlides = new ObservableCollection<PictureViewModel>();
            var models = GetModelsFrom(@url);

            //Create slides with matching pairs from models
            for (int i = 0; i < MaxImage; i++)
            {
                //Create 2 matching slides
                var newSlide = new PictureViewModel(models[i], "Master");
                var newSlideMatch = new PictureViewModel(models[i], "Mirrer");
                //Add new slides to collection
                MemorySlides.Add(newSlide);
                MemorySlides.Add(newSlideMatch);
                //Initially display images for user
                newSlide.PeekAtImage();
                newSlideMatch.PeekAtImage();
            }

            ShuffleSlides();
            OnPropertyChanged("MemorySlides");
        }

        //Select a slide to be matched
        public void SelectSlide(PictureViewModel slide)
        {
            slide.PeekAtImage();

            if (SelectedSlide1 == null)
            {
                SelectedSlide1 = slide;
            }
            else if (SelectedSlide2 == null)
            {
                //ตรวจว่าเลือกใบเดืมหรือไม่
                if (SelectedSlide1.Name == slide.Name)
                {
                    return;
                }

                SelectedSlide2 = slide;
                HideUnmatched();
            }

            SoundManager.PlayCardFlip();
            OnPropertyChanged("areSlidesActive");
        }

        //Are the selected slides a match
        public bool CheckIfMatched()
        {
            if (SelectedSlide1.Id == SelectedSlide2.Id)
            {
                MatchCorrect();
                return true;
            }
            else
            {
                MatchFailed();
                return false;
            }
        }

        //Selected slides did not match
        private void MatchFailed()
        {
            SelectedSlide1.MarkFailed();
            SelectedSlide2.MarkFailed();
            ClearSelected();
        }

        //Selected slides matched
        private void MatchCorrect()
        {
            SelectedSlide1.MarkMatched();
            SelectedSlide2.MarkMatched();
            ClearSelected();
        }

        //Clear selected slides
        private void ClearSelected()
        {
            SelectedSlide1 = null;
            SelectedSlide2 = null;
            canSelect = false;
        }

        //Reveal all unmatched slides
        public void RevealUnmatched()
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.isMatched)
                {
                    _peekTimer.Stop();
                    slide.MarkFailed();
                    slide.PeekAtImage();
                }
            }
        }

        //Hid all slides that are unmatched
        public void HideUnmatched()
        {
            _peekTimer.Start();
        }

        //Display slides for memorizing
        public void Memorize()
        {
            _openingTimer.Start();
        }

        //Get slide picture models for creating picture views
        private List<PictureModel> GetModelsFrom(string relativePath)
        {
            GamePictureModel picture = CallImageFromServer(relativePath);

            //List of models for picture slides
            var models = new List<PictureModel>();
            //Get all image URIs in folder
            //var images = Directory.GetFiles(@relativePath, "*.jpg", SearchOption.AllDirectories);
            //Slide id begin at 0
            var id = 0;
            //ms - appx:///Assets/Logo/Circle.png
            foreach (var i in picture.Entries)
            {
                string imageUrl = DataConfig.StorageUploadsUrl() + i.Image.Path;
                models.Add(new PictureModel() { Id = id, ImageSource = imageUrl });
                //models.Add(new PictureModel() { Id = id, ImageSource = " ms-appx:///" + i });
                id++;
            }

            ////List of models for picture slides
            //var models = new List<PictureModel>();
            ////Get all image URIs in folder
            //var images = Directory.GetFiles(@relativePath, "*.jpg", SearchOption.AllDirectories);
            ////Slide id begin at 0
            //var id = 0;
            ////ms - appx:///Assets/Logo/Circle.png
            //foreach (string i in images)
            //{
            //    models.Add(new PictureModel() { Id = id, ImageSource = " ms-appx:///" + i });
            //    id++;
            //}

            return models;
        }

        //Randomize the location of the slides in collection
        private void ShuffleSlides()
        {
            //Randomizing slide indexes
            var rnd = new Random();
            //Shuffle memory slides
            for (int i = 0; i < 64; i++)
            {
                MemorySlides.Reverse();
                MemorySlides.Move(rnd.Next(0, MemorySlides.Count), rnd.Next(0, MemorySlides.Count));
            }
        }

        //Close slides being memorized
        private void OpeningTimer_Tick(object sender, object e)
        {
            foreach (var slide in MemorySlides)
            {
                slide.ClosePeek();
                canSelect = true;
            }
            OnPropertyChanged("areSlidesActive");
            _openingTimer.Stop();
        }

        //Display selected card
        private void PeekTimer_Tick(object sender, object e)
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.isMatched)
                {
                    slide.ClosePeek();
                    canSelect = true;
                }
            }
            OnPropertyChanged("areSlidesActive");
            _peekTimer.Stop();
        }
    }
}
