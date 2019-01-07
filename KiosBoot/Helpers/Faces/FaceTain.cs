using Microsoft.ProjectOxford.Face.Contract;
using ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace KiosBoot.Helpers.Faces
{
   public  class FaceTain
    {
        public ObservableCollection<PersonGroup> PersonGroups { get; set; } = new ObservableCollection<PersonGroup>();
        public PersonGroup CurrentPersonGroup { get; set; }
        public ObservableCollection<Person> PersonsInCurrentGroup { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersistedFace> SelectedPersonFaces { get; set; } = new ObservableCollection<PersistedFace>();
        public Person SelectedPerson { get; set; }


        public  FaceTain()
        {
            LoadPersonGroupsFromService();
           
        }




        public async Task LoadPersonGroupsFromService()
        {
          //  this.progressControl.IsActive = true;

            try
            {
                this.PersonGroups.Clear();
                IEnumerable<PersonGroup> personGroups = await FaceServiceHelper.ListPersonGroupsAsync(SettingsHelper.Instance.WorkspaceKey) ;
                this.PersonGroups.AddRange(personGroups.OrderBy(pg => pg.Name));
                this.CurrentPersonGroup = (PersonGroup)this.PersonGroups[0];
                //if (this.personGroupsListView.Items.Any())
                //{
                //    this.personGroupsListView.SelectedIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                await Util.GenericApiCallExceptionHandler(ex, "Failure loading Person Groups");
            }

            //this.progressControl.IsActive = false;
        }

        #region People magagement

        private async void OnPersonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (this.personGroupPeopleListView.SelectedValue != null)
            //{
            //    this.SelectedPerson = this.personGroupPeopleListView.SelectedValue as Person;
            //    await this.LoadPersonFacesFromService();
            //}
            //else
            //{
            //    this.SelectedPersonFaces.Clear();
            //}
        }

        private async Task LoadPersonsInCurrentGroup()
        {
            this.PersonsInCurrentGroup.Clear();

            try
            {
                Person[] personsInGroup = await FaceServiceHelper.GetPersonsAsync(this.CurrentPersonGroup.PersonGroupId);
                foreach (Person person in personsInGroup.OrderBy(p => p.Name))
                {
                    this.PersonsInCurrentGroup.Add(person);
                }
            }
            catch (Exception e)
            {
                await Util.GenericApiCallExceptionHandler(e, "Failure loading people in the group");
            }
        }

        private async void OnAddPersonButtonClicked(object sender, RoutedEventArgs e)
        {
            //await this.AddPerson(this.personNameTextBox.Text);
        }

        private async void OnPersonNameQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            await this.AddPerson(args.ChosenSuggestion != null ? args.ChosenSuggestion.ToString() : args.QueryText);
        }

        public async Task AddPerson(string name)
        {
            name = Util.CapitalizeString(name);

            await this.CreatePersonAsync(name);
            //this.personGroupPeopleListView.SelectedValue = this.PersonsInCurrentGroup.FirstOrDefault(p => p.Name == name);
            //trainingImageCollectorFlyout.ShowAt(this.addFacesButton);
        }

        private void DismissFlyout()
        {
            //this.addPersonFlyout.Hide();
            //this.personNameTextBox.Text = "";
        }

        private void OnCancelAddPersonButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DismissFlyout();
        }

        private async void OnPersonNameTextBoxChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //try
                //{
                //    this.personNameTextBox.ItemsSource = await BingSearchHelper.GetAutoSuggestResults(this.personNameTextBox.Text);
                //}
                //catch (HttpRequestException)
                //{
                //    // default to no suggestions
                //    this.personNameTextBox.ItemsSource = null;
                //}
            }
        }

        private async Task CreatePersonAsync(string name)
        {
            try
            {
                CreatePersonResult result = await FaceServiceHelper.CreatePersonAsync(this.CurrentPersonGroup.PersonGroupId, name);
                this.PersonsInCurrentGroup.Add(new Person { Name = name, PersonId = result.PersonId });
                //this.needsTraining = true;
                //this.DismissFlyout();
            }
            catch (Exception e)
            {
                await Util.GenericApiCallExceptionHandler(e, "Failure creating person");
            }
        }

        private async void OnDeletePersonClicked(object sender, RoutedEventArgs e)
        {
            await Util.ConfirmActionAndExecute("Delete person?", async () => { await DeletePersonAsync(); });
        }

        private async Task DeletePersonAsync()
        {
            try
            {
                await FaceServiceHelper.DeletePersonAsync(this.CurrentPersonGroup.PersonGroupId, this.SelectedPerson.PersonId);
                this.PersonsInCurrentGroup.Remove(this.SelectedPerson);
            }
            catch (Exception ex)
            {
                await Util.GenericApiCallExceptionHandler(ex, "Failure deleting person");
            }
        }


        #endregion


        #region Face management

        private async Task LoadPersonFacesFromService()
        {
            //this.progressControl.IsActive = true;

            this.SelectedPersonFaces.Clear();

            try
            {
                Person latestVersionOfCurrentPerson = await FaceServiceHelper.GetPersonAsync(this.CurrentPersonGroup.PersonGroupId, this.SelectedPerson.PersonId);
                this.SelectedPerson.PersistedFaceIds = latestVersionOfCurrentPerson.PersistedFaceIds;

                if (this.SelectedPerson.PersistedFaceIds != null)
                {
                    foreach (Guid face in this.SelectedPerson.PersistedFaceIds)
                    {
                        PersistedFace personFace = await FaceServiceHelper.GetPersonFaceAsync(this.CurrentPersonGroup.PersonGroupId, this.SelectedPerson.PersonId, face);
                        this.SelectedPersonFaces.Add(personFace);
                    }
                }
            }
            catch (Exception e)
            {
                await Util.GenericApiCallExceptionHandler(e, "Failure downloading person faces");
            }

           // this.progressControl.IsActive = false;
        }

        private void OnImageSearchCanceled(object sender, EventArgs e)
        {
          //  this.trainingImageCollectorFlyout.Hide();
        }

        private async void OnImageSearchCompleted(object sender, IEnumerable<ImageAnalyzer> args)
        {
           // this.progressControl.IsActive = true;

           // this.trainingImageCollectorFlyout.Hide();

            bool foundError = false;
            Exception lastError = null;
            foreach (var item in args)
            {
                try
                {
                    AddPersistedFaceResult addResult;
                    if (item.GetImageStreamCallback != null)
                    {
                        addResult = await FaceServiceHelper.AddPersonFaceAsync(
                            this.CurrentPersonGroup.PersonGroupId,
                            this.SelectedPerson.PersonId,
                            imageStreamCallback: item.GetImageStreamCallback,
                            userData: item.LocalImagePath,
                            targetFace: null);
                    }
                    else
                    {
                        addResult = await FaceServiceHelper.AddPersonFaceAsync(
                            this.CurrentPersonGroup.PersonGroupId,
                            this.SelectedPerson.PersonId,
                            imageUrl: item.ImageUrl,
                            userData: item.ImageUrl,
                            targetFace: null);
                    }

                    if (addResult != null)
                    {
                        this.SelectedPersonFaces.Add(new PersistedFace { PersistedFaceId = addResult.PersistedFaceId, UserData = item.GetImageStreamCallback != null ? item.LocalImagePath : item.ImageUrl });
                       // this.needsTraining = true;
                    }
                }
                catch (Exception e)
                {
                    foundError = true;
                    lastError = e;
                }
            }

            if (foundError)
            {
                await Util.GenericApiCallExceptionHandler(lastError, "Failure adding one or more of the faces");
            }

            //this.progressControl.IsActive = false;
        }

        private void OnImageSearchFlyoutOpened(object sender, object e)
        {
           // this.bingSearchControl.TriggerSearch(this.SelectedPerson.Name);
        }

        private async void OnDeleteFaceClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                //foreach (var item in this.selectedPersonFacesGridView.SelectedItems.ToArray())
                //{
                //    PersistedFace personFace = (PersistedFace)item;
                //    await FaceServiceHelper.DeletePersonFaceAsync(this.CurrentPersonGroup.PersonGroupId, this.SelectedPerson.PersonId, personFace.PersistedFaceId);
                //    this.SelectedPersonFaces.Remove(personFace);

                //   // this.needsTraining = true;
                //}
            }
            catch (Exception ex)
            {
                await Util.GenericApiCallExceptionHandler(ex, "Failure deleting images");
            }
        }

        private async void OnImageDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            PersistedFace dataContext = sender.DataContext as PersistedFace;

            if (dataContext != null)
            {
                Image image = sender as Image;
                if (image != null)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    image.Source = bitmapImage;

                    try
                    {
                        if (Path.IsPathRooted(dataContext.UserData))
                        {
                            // local file
                            bitmapImage.SetSource(await (await StorageFile.GetFileFromPathAsync(dataContext.UserData)).OpenReadAsync());
                        }
                        else
                        {
                            // url
                            bitmapImage.UriSource = new Uri(dataContext.UserData);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        #endregion

    }
}
