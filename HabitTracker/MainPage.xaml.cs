namespace HabitTracker
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editDayId;
       
        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listView.ItemsSource = await _dbService.GetUsers());
            
        }
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editDayId == 0)
            {
                await _dbService.Create(new User
                {
                    Day = dayEntryField.Text,
                    Time = timeEntryField.Text,
                    Habit = habitEntryField.Text,
                    Goal = goalEntryField.Text
                });
            }
            else
            {
                await _dbService.Update(new User
                {
                    Id = _editDayId,
                    Day = dayEntryField.Text,
                    Time = timeEntryField.Text,
                    Habit = habitEntryField.Text,
                    Goal = goalEntryField.Text
                });
                _editDayId = 0;
            }
            dayEntryField.Text = string.Empty;
            timeEntryField.Text = string.Empty;
            habitEntryField.Text = string.Empty;
            goalEntryField.Text = string.Empty;
            
            listView.ItemsSource = await _dbService.GetUsers();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var user = (User)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":

                    _editDayId = user.Id;
                    dayEntryField.Text = user.Day;
                    timeEntryField.Text = user.Time;
                    habitEntryField.Text = user.Habit;
                    goalEntryField.Text = user.Goal;                    
                    break;

                case "Delete":

                    await _dbService.Delete(user);
                    listView.ItemsSource = await _dbService.GetUsers();
                    break;
            }
        }
       
    }

}
