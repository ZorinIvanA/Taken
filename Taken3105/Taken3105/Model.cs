using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Taken3105
{
    public class Model : INotifyPropertyChanged
    {
        public ICommand RestartCommand { get; set; }
        public ICommand MoveCommand { get; set; }
        public int Turns { get; set; }
        public bool IsWin { get; set; } = false;
        public bool IsNotWin
        {
            get { return !IsWin; }
        }

        public ObservableCollection<Cube> Cubes { get; set; } = new ObservableCollection<Cube>();

        public Model()
        {
            GetCubes();
            RestartCommand = new MyCommand(Restart);
            MoveCommand = new MyCommand(Move);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Move(Cube cube)
        {
            Cube empty = Cubes.Single(x => x.Value == null);

            if ((Math.Abs(cube.X - empty.X) == 1 && cube.Y == empty.Y) ||
                (Math.Abs(cube.Y - empty.Y) == 1 && cube.X == empty.X))
            {
                var temp = cube.Position;
                cube.Position = empty.Position;
                empty.Position = temp;
                Turns++;
                IsWin = Cubes.All(x => x.Value == null ||
                    x.Value == x.Position + 1);
            };

            var list = Cubes.ToList().OrderBy(x => x.Position);
            Cubes.Clear();
            foreach (var item in list)
                Cubes.Add(item);

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Cubes)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Turns)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsNotWin)));

        }

        private void Restart(Cube obj)
        {
            GetCubes();
            Turns = 0;
        }

        private void GetCubes()
        {
            do
            {
                Cubes.Clear();
                var values = new int[15];
                Random rnd = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i < 15; i++)
                {
                    int temp = 0;
                    while (values.Any(x => x == temp))
                    {
                        temp = rnd.Next(1, 16);
                    }
                    values[i] = temp;
                }
                for (int i = 0; i < 15; i += 1)
                {
                    Cubes.Add(new Cube { Position = i, Value = values[i] });
                }

                Cubes.Add(new Cube { Position = 15, Value = null });
            }
            while (CheckForCorrect());
        }

        private bool CheckForCorrect()
        {
            int sum = 0;
            foreach (Cube cube in Cubes)
            {
                sum += Cubes.Where(x => x.Position > cube.Position && x.Value < cube.Value)
                    .Count();
            }
            sum += 3;

            //if (sum % 2 == 0)
            //    return true;
            //else
            //    return false;

            return sum % 2 == 0;
        }
    }
}
