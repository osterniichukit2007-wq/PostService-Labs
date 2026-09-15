using PostService.Models;

namespace PostService.Services;

public class PostingService : IPostingService
{
    private static List<Posting> _postings = new List<Posting>
    {
        new Posting
        {
            Id = 1,
            From = "Alice",
            To = "Bob",
            Content = "Books",
            DeliveryType = DeliveryType.Courier,
            Weight = 2.5f,
            Width = 30,
            Height = 20,
            Depth = 10,
            Value = 50.0f,
            Price = 10.0f,
            CreatedAt = DateTime.UtcNow
        },
        new Posting
        {
            Id = 2,
            From = "Ivan",
            To = "Olena",
            Content = "Documents",
            DeliveryType = DeliveryType.ExpressCourier,
            Weight = 0.5f,
            Width = 25,
            Height = 15,
            Depth = 2,
            Value = 100.0f,
            Price = 25.0f,
            CreatedAt = DateTime.UtcNow
        }
    };

    public Posting Create(Posting newPosting)
    {
        var maxId = 1;
        if (_postings.Count > 0)
        {
            maxId = _postings.Max(p => p.Id) + 1;
        }

        newPosting.Id = maxId;
        newPosting.CreatedAt = DateTime.UtcNow;
        newPosting.Price = CalculatePrice(newPosting.Weight, newPosting.DeliveryType);

        _postings.Add(newPosting);
        return newPosting;
    }

    public Posting? Find(int postingId)
    {
        return _postings.FirstOrDefault(p => p.Id == postingId);
    }

    public List<Posting> GetAll()
    {
        return _postings;
    }

    public Posting? Update(Posting posting)
    {
        var existing = _postings.FirstOrDefault(p => p.Id == posting.Id);
        if (existing is null)
            return null;

        existing.From = posting.From;
        existing.To = posting.To;
        existing.Content = posting.Content;
        existing.DeliveryType = posting.DeliveryType;
        existing.Weight = posting.Weight;
        existing.Width = posting.Width;
        existing.Height = posting.Height;
        existing.Depth = posting.Depth;
        existing.Value = posting.Value;
        existing.Price = posting.Price;

        return existing;
    }

    public int Delete(int postingId)
    {
        var posting = _postings.FirstOrDefault(p => p.Id == postingId);
        if (posting is null)
            return 0;

        _postings.Remove(posting);
        return 1;
    }

    private static float CalculatePrice(float weight, DeliveryType deliveryType)
    {
        const float baseRatePerKg = 5.0f;
        float basePrice = weight * baseRatePerKg;

        float multiplier = deliveryType switch
        {
            DeliveryType.Department => 1.0f,
            DeliveryType.Courier => 1.5f,
            DeliveryType.ExpressCourier => 2.5f,
            _ => 1.0f
        };

        return basePrice * multiplier;
    }
}
