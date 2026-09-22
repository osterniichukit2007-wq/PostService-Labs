using PostService.Models;
using PostService.CommonTypes;
using PostService.DataAccess;

namespace PostService.BusinessLogic;

public class PostingService : IPostingService
{
    private readonly IPostingRepository _repository;

    public PostingService(IPostingRepository repository)
    {
        _repository = repository;
    }

    public Posting Create(Posting newPosting)
    {
        newPosting.CreatedAt = DateTime.UtcNow;
        newPosting.Price = CalculatePrice(newPosting.Weight, newPosting.DeliveryType);

        var postingId = _repository.Create(newPosting);
        newPosting.Id = postingId;

        return newPosting;
    }

    public Posting? Find(int postingId)
    {
        return _repository.GetById(postingId);
    }

    public List<Posting> GetAll()
    {
        return _repository.GetList();
    }

    public Posting? Update(Posting posting)
    {
        var existing = _repository.GetById(posting.Id);
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

        _repository.Update(existing);

        return existing;
    }

    public int Delete(int postingId)
    {
        return _repository.Delete(postingId);
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
