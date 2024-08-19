using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AnimalGame
{
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        DispatcherTimer timer= new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }



        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text += " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐶", "🐶", // 강아지
                "🐱", "🐱", // 고양이
                "🐭", "🐭", // 쥐
                "🐹", "🐹", // 햄스터
                "🐰", "🐰", // 토끼
                "🦊", "🦊", // 여우
                "🐻", "🐻", // 곰
                "🐼", "🐼"  // 판다
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "TimeTextBlock")//타이머 블록이 아니면
                {
                    textBlock.Visibility = Visibility.Visible;//visibility는 가시성. textBlock의 가시성을 Visibility.Visible 보이게 함
                    int index = random.Next(animalEmoji.Count);//리스트 갯수범위(16)안에 랜덤수 생성
                    string nextEmoji = animalEmoji[index]; 리스트의 index 번째의 이모지를 nextEmoji에 넣고
                    textBlock.Text = nextEmoji; //textBlock ui 의 텍스트를 nextEmoji가 나오게 출력
                    animalEmoji.RemoveAt(index); //넣었으니 index 번째의 이모지를 리스트에서 삭제
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;


        }
        private TextBlock lastTextBlockClicked;
        private bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (textBlock == null || textBlock.Visibility != Visibility.Visible)
                return;

            if (!findingMatch)
            {
                // 한짝 클릭
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else
            {
                // 두번째 클릭
                if (textBlock.Text == lastTextBlockClicked.Text)
                {
                    matchesFound++;
                    textBlock.Visibility = Visibility.Hidden;
                    lastTextBlockClicked.Visibility = Visibility.Hidden;
                }
                else
                {
                    // 안맞을때
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                }
                findingMatch = false;
            }
        }
    }
}
