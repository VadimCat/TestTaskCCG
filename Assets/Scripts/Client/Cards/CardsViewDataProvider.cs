using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client
{
    public class CardsViewDataProvider : ICardsViewDataProvider
    {
        private const string CardNameFormat = "CardName{0}";
        private const string DescFormat = "{0} by {1}";
        private readonly CardsArtDownloader artDownloader;
        private Dictionary<int, CardViewData> cardsViewData = new();

        public CardsViewDataProvider(CardsArtDownloader artDownloader)
        {
            this.artDownloader = artDownloader;
        }

        public CardViewData GetViewData(int id)
        {
            return cardsViewData[id];
        }
    
        public async Task GenerateData(int count)
        {
            var data = await this.artDownloader.GetSprites(count);

            for (int i = 0; i < data.Length; i++)
            {
                var desc = string.Format(DescFormat, data[i].Item2.url, data[i].Item2.author);
                var title = string.Format(CardNameFormat, i);
                cardsViewData[i] = new CardViewData(title, desc, data[i].Item1);
            }
        }
    }
}