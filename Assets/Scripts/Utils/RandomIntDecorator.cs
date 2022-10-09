using System;

namespace Utils
{
    public class RandomIntDecorator : RandomDecorator<int>
    {
        public RandomIntDecorator(int min, int max) : base(min, max)
        {
        }

        protected override void SaveRandomValue(int min, int max)
        {
            randomValue = new Random().Next(min, max);
        }
    }
}