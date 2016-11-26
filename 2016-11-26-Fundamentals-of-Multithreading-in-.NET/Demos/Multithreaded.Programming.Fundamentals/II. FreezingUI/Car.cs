namespace II.FreezingUI
{
    public class Car
    {
        private const int NameIndex = 0;
        private const int MilesPerGalonIndex = 1;
        private const int CylindersIndex = 2;
        private const int DisplacementIndex = 3;
        private const int HorsepowerIndex = 4;
        private const int WeightIndex = 5;
        private const int AccelerationIndex = 6;
        private const int YearIndex = 7;
        private const int OriginIndex = 8;
        
        public string Name { get; set; }
        public string Origin { get; set; }
        public double MilesPerGalon { get; set; }
        public double Displacement { get; set; }
        public double Acceleration { get; set; }
        public double Horsepower { get; set; }
        public double Weight { get; set; }
        public int Cylinders { get; set; }
        public int Year { get; set; }

        public static Car Parse(string carData)
        {
            var carParams = carData.Split(';');

            return new Car
            {
                Name = carParams[NameIndex],
                MilesPerGalon = carParams[MilesPerGalonIndex].ToDouble(),
                Cylinders = carParams[CylindersIndex].ToInteger(),
                Displacement = carParams[DisplacementIndex].ToDouble(),
                Horsepower = carParams[HorsepowerIndex].ToDouble(),
                Weight = carParams[WeightIndex].ToDouble(),
                Acceleration = carParams[AccelerationIndex].ToDouble(),
                Year = carParams[YearIndex].ToInteger(),
                Origin = carParams[OriginIndex]
            };
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Year} - {this.Horsepower} - {this.Weight}";
        }
    }
}
