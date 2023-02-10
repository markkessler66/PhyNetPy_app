using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace PhyNetPy;

public partial class NewPage2 : ContentPage
{
    

    public NewPage2()
	{
		InitializeComponent();
	}


    private async void ExitSessionHandler(System.Object sender, System.EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void ImportHandler(System.Object sender, System.EventArgs e)
    {


        PickOptions options = new()
        {
            PickerTitle = "Please select a png file",
            FileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { ".png" } }, 
                    { DevicePlatform.macOS, new[] { ".png" } },
                    { DevicePlatform.MacCatalyst, new[] { "public.png", "public.jpeg" } }
                })

        };

        Console.WriteLine("Hello World!");
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                Console.WriteLine("RESULT NOT NULL");

                if (result.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    myImage.Source = ImageSource.FromStream(() => stream);

                }
            }
            else
            {
                Console.WriteLine("RESULT IS NULL");
            }
        }
        catch(Exception ex)
        {
            Console.Write("AN EXCEPTION HAS OCCURED");
            Console.Write(ex.Message);
        }
        
    }

   

    void pythonButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Console.WriteLine("1");
        var engine = Python.CreateEngine();
        Console.WriteLine("2");
        try
        {
            
            var source = engine.CreateScriptSourceFromFile("/Users/mak17/Projects/PhyNetPy/PhyNetPy/PythonCode/test.py");
            Console.WriteLine("3");
            var scope = engine.CreateScope();
            Console.WriteLine("4");
            //executing script in scope
            source.Execute(scope);
            Console.WriteLine("5");
            var classCalculator = scope.GetVariable("calculator");
            Console.WriteLine("6");
            //initializing class
            var calculatorInstance = engine.Operations.CreateInstance(classCalculator);
            Console.WriteLine("From Iron Python");
            Console.WriteLine("5 + 10 = {0}", calculatorInstance.add(5, 10));
            Console.WriteLine("5++ = {0}", calculatorInstance.increment(5));
        }
        catch(Exception ex)
        {
            Console.Write(ex.Message);
            Console.Write("ERROR CREATING PYTHON FROM FILE");
        }
        //var source = engine.CreateScriptSourceFromString("class calculator:\n    def add(self, x, y):\n        return x + y\n\n    def increment(self, x):\n        x += 1;\n        return x;");
        

    }


}
