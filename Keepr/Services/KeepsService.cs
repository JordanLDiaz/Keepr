namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  internal Keep Create(Keep keepData)
  {
    Keep keep = _repo.Create(keepData);
    return keep;
  }

  internal List<Keep> Get()
  {
    List<Keep> keeps = _repo.Get();
    return keeps;
  }

  internal Keep GetOne(int id, string userId)
  {
    Keep keep = _repo.GetOne(id);
    if (keep == null)
    {
      throw new Exception("No keep at this id.");
    }
    return keep;
  }

  internal Keep EditKeep(Keep updateData, int id, string userId)
  {
    Keep original = GetOne(id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("This is not your keep to edit.");
    }
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;

    _repo.EditKeep(original);
    return original;
  }

  internal string Remove(int id, string userId)
  {
    Keep keep = GetOne(id, userId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this keep.");
    }
    string message = _repo.Remove(id);
    return message;
  }
}
