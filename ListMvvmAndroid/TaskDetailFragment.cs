using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Fragment.App;
using Core;
using Core.ViewModels;
using Android.App;
using Android.Graphics;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using ListMvvmAndroid.Services;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace ListMvvmAndroid
{
    public class TaskDetailFragment : AndroidX.Fragment.App.Fragment, IOnMapReadyCallback
    {
        private TaskItem _taskItem;
        private MainViewModel _mainViewModel;
        private DetailViewModel _viewModel;
        private Button doneButton;
        private EventHandler updateDoneButtonHandler;
        private INavigationService _navigationService;
        private MapView mapView;
        private GoogleMap _googleMap;
        private double latitude;
        private double longitude;

        public TaskDetailFragment(TaskItem taskItem, MainViewModel mainViewModel, INavigationService navigationService)
        {
            _taskItem = taskItem;
            _mainViewModel = mainViewModel;
            _viewModel = new DetailViewModel(taskItem, mainViewModel);
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            latitude = _mainViewModel.Latitude;
            longitude = _mainViewModel.Longitude;
            _mainViewModel.SetLocation(taskItem.Latitude, taskItem.Longitude);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.IsComplete))
            {
                UpdateDoneButton();
            }
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            if (mapView != null)
            {
                mapView.OnDestroy();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_task_detail, container, false);
            view.SetBackgroundColor(Android.Graphics.Color.White);

            TextView titleTextView = view.FindViewById<TextView>(Resource.Id.titleTextView);
            TextView descriptionTextView = view.FindViewById<TextView>(Resource.Id.descriptionTextView);
            TextView categoryTextView = view.FindViewById<TextView>(Resource.Id.categoryTextView);
            Button deleteTaskButton = view.FindViewById<Button>(Resource.Id.deleteTaskButton);
            doneButton = view.FindViewById<Button>(Resource.Id.doneButton);

            titleTextView.Text = _taskItem.Title;
            descriptionTextView.Text = _taskItem.Description;
            categoryTextView.Text = _taskItem.Category;

            doneButton.Click += (s, e) =>
            {
                _viewModel.ToggleCompleteCommand.Execute(null);
                ParentFragmentManager.PopBackStack();
            };

            updateDoneButtonHandler = (s, e) =>
            {
                UpdateDoneButton();
            };

            _viewModel.IsCompleteChanged += updateDoneButtonHandler;

            deleteTaskButton.Click += (s, e) =>
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
                builder.SetTitle(Constants.Constants.DeleteTask);
                builder.SetMessage(Constants.Constants.MessageDeleteTask);
                builder.SetPositiveButton(Constants.Constants.Yes, (sender, args) =>
                {
                    _mainViewModel.Items.Remove(_taskItem);
                    Toast.MakeText(Activity, Constants.Constants.MessageDeleteTaskSuccess, ToastLength.Short).Show();
                    ParentFragmentManager.PopBackStack();
                });
                builder.SetNegativeButton(Constants.Constants.No, (sender, args) => { });
                AlertDialog dialog = builder.Create();
                dialog.Show();
            };

            _viewModel.IsCompleteChanged += (s, e) =>
            {
                UpdateDoneButton();
            };

            mapView = view.FindViewById<MapView>(Resource.Id.mapView);
            mapView.GetMapAsync(this);
            mapView.OnCreate(savedInstanceState);
            mapView.GetMapAsync(this);
            return view;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            LatLng myLocation = new LatLng(latitude, longitude);
            _googleMap.AddMarker(new MarkerOptions().SetPosition(myLocation).SetTitle("Mi ubicación"));
            _googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(myLocation, 15));
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }


        public void UpdateData(TaskItem taskItem)
        {
            _taskItem = taskItem;
            _viewModel = new DetailViewModel(taskItem, _mainViewModel);

            TextView titleTextView = View.FindViewById<TextView>(Resource.Id.titleTextView);
            TextView descriptionTextView = View.FindViewById<TextView>(Resource.Id.descriptionTextView);
            TextView categoryTextView = View.FindViewById<TextView>(Resource.Id.categoryTextView);

            titleTextView.Text = _taskItem.Title;
            descriptionTextView.Text = _taskItem.Description;
            categoryTextView.Text = _taskItem.Category;
        }

        private void UpdateDoneButton()
        {
            if (_viewModel.IsComplete)
            {
                doneButton.Text = Constants.Constants.Pending;
            }
            else
            {
                doneButton.Text = Constants.Constants.Done;
            }
        }
    }
}
