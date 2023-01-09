namespace MasterMind;

public class GameState
{
    public int[] playerRows;

    public GameState()
    {

    }
}


// saving loading
// delete board on a reset button 
// testing docuemnt 
public partial class MainPage : ContentPage
{
    private string filename = "myFile.txt"; //for use in game load or save
    private GameState _gameState;// for use in game load and save
    private Random _random;

    private Button currentGuessPeg;
    Color[] myColours = { Colors.Red, Colors.Green, Colors.Yellow, Colors.Blue };// color array
    int IsCorrect = 0;
    int rowGuess = 0;


    public MainPage()
    {
        InitializeComponent();
        _random = new Random();
        BtnGuess1.BackgroundColor = myColours[_random.Next(0, 4)];
        BtnGuess2.BackgroundColor = myColours[_random.Next(0, 4)];
        BtnGuess3.BackgroundColor = myColours[_random.Next(0, 4)];
        BtnGuess4.BackgroundColor = myColours[_random.Next(0, 4)];
        Code1.Color = myColours[_random.Next(0, 4)];
        Code2.Color = myColours[_random.Next(0, 4)];
        Code3.Color = myColours[_random.Next(0, 4)];
        Code4.Color = myColours[_random.Next(0, 4)];

    }

    // couldnt get saving to work but left the skelliton here.

    ////private GameState ReadJsonFile()
    ////{
    ////    GameState gs = null;
    ////    string jsonText = "";

    ////    try     // reading the local directory (environment.specialfolders)
    ////    {
    ////        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    ////        string fname = Path.Combine(path, filename);
    ////        using (var reader = new StreamReader(fname))
    ////        {
    ////            jsonText = reader.ReadToEnd();
    ////        }
    ////    }
    ////    catch (Exception ex)    // if that fails, then read the embedded resource
    ////    {
    ////        string errorMsg = ex.Message;
    ////    }   // end try catch
    ////    if (jsonText != "")
    ////    {
    ////        gs = new GameState();
    ////        gs = JsonConvert.DeserializeObject<GameState>(jsonText);
    ////        return gs;
    ////    }
    ////    else
    ////        return null;
    ////}
    ////public void SaveListOfData(GameState gs)
    ////{
    ////    string path = Environment.GetFolderPath(
    ////                                   Environment.SpecialFolder.LocalApplicationData);
    ////    string fname = Path.Combine(path, filename);
    ////    using (var writer = new StreamWriter(fname, false))
    ////    {
    ////        string jsonText = JsonConvert.SerializeObject(gs);
    ////        writer.WriteLine(jsonText);
    ////    }
    ////}

    ////private void BtnReadFile_Clicked(object sender, EventArgs e)
    ////{
    ////    string fileContent = "";
    ////    _gameState = ReadJsonFile();

    ////    if (_gameState != null)
    ////    {
    ////        fileContent = "Row values: " + _gameState.playerRows[0];
    ////    }
    ////    else
    ////    {
    ////        fileContent = "no file found";
    ////    }
    ////    //        LblFileStuff.Text = fileContent;

    ////}

    ////private void BtnWriteFile_Clicked(object sender, EventArgs e)
    ////{
    ////    GameState gs = new GameState();

    ////    gs.playerRows[0] = 134;
    ////    gs.playerRows[1] = 234;
    ////    gs.playerRows[2] = 334;

    ////    SaveListOfData(gs);

    ////}


    // to highlight the button 
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (currentGuessPeg != null) currentGuessPeg.BorderColor = Colors.Transparent;

        currentGuessPeg = (Button)sender;
        currentGuessPeg.BorderColor = Colors.Magenta;
        GridColourPicker.IsVisible = true;
    }

    // for the colour picker taps
    private void ColorGuessBoxView_Tapped(object sender, EventArgs e)
    {
        BoxView b = (BoxView)sender;

        currentGuessPeg.BackgroundColor = b.Color;
        currentGuessPeg.BorderColor = Colors.Transparent;
        GridColourPicker.IsVisible = false;
    }

    // game over method for a game over
    private async void game_Over()
    {
        Code1.SetValue(Grid.IsVisibleProperty, 18);
        Code2.SetValue(Grid.IsVisibleProperty, 18);
        Code3.SetValue(Grid.IsVisibleProperty, 18);
        Code4.SetValue(Grid.IsVisibleProperty, 18);
        bool answer = await DisplayAlert("Game Over", "play again ?", "Reset", "cancel");
        if (answer == true)
        {
            ResetMeth();
        }
    }

    // checks the guesses when check is clicked. 
    private void Check_Clicked(object sender, EventArgs e)
    {
        IsCorrect = 0;

        // to check against  the mastermind
        if (BtnGuess1.BackgroundColor == Code1.Color)
        {
            IsCorrect++;

        }


        if (BtnGuess2.BackgroundColor == Code2.Color)
        {
            IsCorrect++;
        }


        if (BtnGuess3.BackgroundColor == Code3.Color)
        {
            IsCorrect++;
        }

        if (BtnGuess4.BackgroundColor == Code4.Color)
        {
            IsCorrect++;
        }



        // to drop down guesses
        rowGuess++;

        BtnGuess1.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess2.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess3.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess4.SetValue(Grid.RowProperty, rowGuess);
        CheckerGrid.SetValue(Grid.RowProperty, rowGuess);

        // to show peg checks
        if (IsCorrect >= 1)
        {
            Peg1.Color = Colors.Black;
        }
        else
        {
            Peg1.Color = Colors.White;
        }
        if (IsCorrect >= 2)
        {
            Peg2.Color = Colors.Black;
        }
        else
        {
            Peg2.Color = Colors.White;
        }
        if (IsCorrect >= 3)
        {
            Peg3.Color = Colors.Black;
        }
        else
        {
            Peg3.Color = Colors.White;
        }
        if (IsCorrect == 4)
        {
            Peg4.Color = Colors.Black;
            Winner();
        }
        previous_Guess();


        // to produce game over if guesses exhausted
        if (rowGuess >= 18)
        {
            game_Over();
        }
    }
    private void previous_Guess()
    {
        BoxView pGuess1 = new BoxView();
        BoxView pGuess2 = new BoxView();
        BoxView pGuess3 = new BoxView();
        BoxView pGuess4 = new BoxView();

        BoxView[] pGuesses = { pGuess1, pGuess2, pGuess3, pGuess4 };

        for (int i = 0; i < pGuesses.Length; i++)
        {
            GridTest.Children.Add(pGuesses[i]);
            pGuesses[i].SetValue(Grid.ColumnProperty, i);
            pGuesses[i].SetValue(Grid.RowProperty, rowGuess - 1);
            pGuesses[i].SetValue(Grid.HeightRequestProperty, 30);
            pGuesses[i].SetValue(Grid.WidthRequestProperty, 40);


        }
        pGuesses[0].Color = BtnGuess1.BackgroundColor;
        pGuesses[1].Color = BtnGuess2.BackgroundColor;
        pGuesses[2].Color = BtnGuess3.BackgroundColor;
        pGuesses[3].Color = BtnGuess4.BackgroundColor;


    }

    //Displays the how many guesses it took to guess the code
    private  async void Winner()
    {
        bool message =await DisplayAlert("You win", "well done!!!!!!!!!! in " + (rowGuess + 1), "Playagain","cancel");
        if(message == true)
        {
            ResetMeth();
        }

        Code1.SetValue(Grid.IsVisibleProperty, 18);
        Code2.SetValue(Grid.IsVisibleProperty, 18);
        Code3.SetValue(Grid.IsVisibleProperty, 18);
        Code4.SetValue(Grid.IsVisibleProperty, 18);
    }

    // reset game back to zero on reset button 

    private void Reset_Clicked(object sender, EventArgs e)
    {
        GridTest.Children.Clear();
        rowGuess = 0;

        // adds buttons
        GridTest.Children.Add(Check);
        GridTest.Children.Add(Reset);

        // adds back buttons
        GridTest.Children.Add(BtnGuess1);
        GridTest.Children.Add(BtnGuess2);
        GridTest.Children.Add(BtnGuess3);
        GridTest.Children.Add(BtnGuess4);

        GridTest.Children.Add(Code1);
        GridTest.Children.Add(Code2);
        GridTest.Children.Add(Code3);
        GridTest.Children.Add(Code4);

        // new randoms for reset
        Code1.Color = myColours[_random.Next(0, 4)];
        Code2.Color = myColours[_random.Next(0, 4)];
        Code3.Color = myColours[_random.Next(0, 4)];
        Code4.Color = myColours[_random.Next(0, 4)];
        // adds back checkgrid
        GridTest.Children.Add(CheckerGrid);
        Peg1.Color = Colors.White;
        Peg2.Color = Colors.White;
        Peg3.Color = Colors.White;
        Peg4.Color = Colors.White;

        BtnGuess1.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess2.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess3.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess4.SetValue(Grid.RowProperty, rowGuess);
        CheckerGrid.SetValue(Grid.RowProperty, rowGuess);

        Code1.SetValue(Grid.RowProperty, 18);
        Code2.SetValue(Grid.RowProperty, 18);
        Code3.SetValue(Grid.RowProperty, 18);
        Code4.SetValue(Grid.RowProperty, 18);

    }

    //method to reset without a button
    private void ResetMeth()
    {
        GridTest.Children.Clear();
        rowGuess = 0;

        // adds buttons
        GridTest.Children.Add(Check);
        GridTest.Children.Add(Reset);

        // adds back buttons
        GridTest.Children.Add(BtnGuess1);
        GridTest.Children.Add(BtnGuess2);
        GridTest.Children.Add(BtnGuess3);
        GridTest.Children.Add(BtnGuess4);

        GridTest.Children.Add(Code1);
        GridTest.Children.Add(Code2);
        GridTest.Children.Add(Code3);
        GridTest.Children.Add(Code4);

        // new randoms for reset
        Code1.Color = myColours[_random.Next(0, 4)];
        Code2.Color = myColours[_random.Next(0, 4)];
        Code3.Color = myColours[_random.Next(0, 4)];
        Code4.Color = myColours[_random.Next(0, 4)];
        // adds back checkgrid
        GridTest.Children.Add(CheckerGrid);
        Peg1.Color = Colors.White;
        Peg2.Color = Colors.White;
        Peg3.Color = Colors.White;
        Peg4.Color = Colors.White;

        BtnGuess1.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess2.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess3.SetValue(Grid.RowProperty, rowGuess);
        BtnGuess4.SetValue(Grid.RowProperty, rowGuess);
        CheckerGrid.SetValue(Grid.RowProperty, rowGuess);

        Code1.SetValue(Grid.RowProperty, 18);
        Code2.SetValue(Grid.RowProperty, 18);
        Code3.SetValue(Grid.RowProperty, 18);
        Code4.SetValue(Grid.RowProperty, 18);
    }


}
