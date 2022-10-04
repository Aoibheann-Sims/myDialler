namespace myDialler;

public partial class MainPage : ContentPage
{
	string translatedNumber;
	public MainPage()
	{
		InitializeComponent();
	}


	private void TranslateBtn_Clicked(object sender, EventArgs e)
	{
		string enteredNumber = PhoneNumberText.Text;
		translatedNumber = myDialler.MyPhoneNumberTranslator.ToNumber(enteredNumber);
       
		if(!string.IsNullOrEmpty(translatedNumber))
		{
            CallNumberBtn.IsEnabled = true;
            CallNumberBtn.Text = $"Call {translatedNumber}";
        }
		else
		{
			CallNumberBtn.IsEnabled = false;
			CallNumberBtn.Text = "Call";
		}

    }

	async void CallNumberBtn_Clicked(object sender, EventArgs e)
	{
		//do something
		if(await this.DisplayAlert(
			"Dial a Number",
			"Would you like to call " + translatedNumber + "?", 
			"Yes",
			"No"))
		{
			try
			{
				if (PhoneDialer.Default.IsSupported)
					PhoneDialer.Default.Open(translatedNumber);
			}
			catch(ArgumentNullException)
			{
				await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
			}
			catch(Exception)
			{
				//other error has occurred
				await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
			}
		}
	}
}

