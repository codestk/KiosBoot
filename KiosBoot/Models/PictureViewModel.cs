﻿using KiosBoot.Models;
using KiosBoot.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml.Media;

namespace KiosBoot.Models
{
    public class PictureViewModel : ObservableObject
    {
        //Model for this view
        private PictureModel _model;

        //ID of this slide
        public int Id { get; private set; }

        public string Name { get;   set; }


        //Slide status
        private bool _isViewed;
        private bool _isMatched;
        private bool _isFailed;

        //Is being viewed by user
        public bool isViewed
        {
            get
            {
                return _isViewed;
            }
            private set
            {
                _isViewed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //Has been matched
        public bool isMatched
        {
            get
            {
                return _isMatched;
            }
            private set
            {
                _isMatched = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //Has failed to be matched
        public bool isFailed
        {
            get
            {
                return _isFailed;
            }
            private set
            {
                _isFailed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //User can select this slide
        public bool isSelectable
        {
            get
            {
                if (isMatched)
                    return false;
                if (isViewed)
                    return false;

                return true;
            }
        }

        //Image to be displayed
        public string SlideImage
        {
            get
            {
                if (isMatched)
                    return _model.ImageSource;
                if (isViewed)
                    return _model.ImageSource;
                

                //return "/KiosBoot;component/Assets/mystery_image.jpg";
                //return "ms-appx:///Assets/mystery_image.jpg";

                
                return "ms-appx:///Assets/Game/question.png";
            }
        }

        //Brush color of border based on status
        public Brush BorderBrush
        {


            get
            {
                     
                if (isFailed)
                    return new SolidColorBrush(Windows.UI.Colors.OrangeRed);
                if (isMatched)
                    return new SolidColorBrush(Windows.UI.Colors.LightGreen);
                if (isViewed)
                    return new SolidColorBrush(Windows.UI.Colors.Gold);

                //if (isFailed)
                //    return new SolidColorBrush(Windows.UI.Colors.Red);
                //if (isMatched)
                //    return new SolidColorBrush(Windows.UI.Colors.Green);
                //if (isViewed)
                //    return new SolidColorBrush(Windows.UI.Colors.Yellow);



                return new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

       
        public PictureViewModel(PictureModel model,string ViewMaster)
        {
            _model = model;
            Id = model.Id;

            Name = "btn"+ model.Id + ViewMaster;
        }

        //Has been matched
        public void MarkMatched()
        {
            isMatched = true;
        }

        //Has failed to match
        public void MarkFailed()
        {
            isFailed = true;
        }

        //No longer being viewed
        public void ClosePeek()
        {
            isViewed = false;
            isFailed = false;
            OnPropertyChanged("isSelectable");
            OnPropertyChanged("SlideImage");
        }

        //Let user view
        public void PeekAtImage()
        {
            isViewed = true;
            OnPropertyChanged("SlideImage");
        }
    }
}
