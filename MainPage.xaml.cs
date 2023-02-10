namespace PhyNetPy;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn2.Text = $"Clicked {count} time";
		else if (count == 10)
			CounterBtn2.Text = $"Clicked {count} times: ITS 10 omgomgomgomg";
		else
			CounterBtn2.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn2.Text);
	}
}


